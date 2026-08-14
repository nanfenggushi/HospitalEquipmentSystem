using HospitalEquipment.BLL;
using HospitalEquipment.Model;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI
{
    /// <summary>
    /// Agent 页面：AI 识别故障 + 物料推荐 + 手动补充 + 金额计算
    /// 通过构造函数传入 recordId 加载工单
    /// </summary>
    public partial class AgentForm : UIForm
    {
        private readonly int _recordId;
        private DataRow _orderRow;

        private MaintenanceBLL _mntBll;
        private MaintenanceMaterialManager _matManager;
        private MaterialManager _materialManager;

        private MaintenanceBLL MntBLL => _mntBll ?? (_mntBll = new MaintenanceBLL());
        private MaintenanceMaterialManager MatManager
        {
            get
            {
                if (_matManager == null)
                {
                    // 优先使用真实 AI，未配置密钥时降级为 Mock
                    IFaultRecognizer recognizer;
                    string apiKey = System.Configuration.ConfigurationManager.AppSettings["AiApiKey"] ?? "";
                    if (!string.IsNullOrEmpty(apiKey))
                        recognizer = new OpenAiFaultRecognizer();
                    else
                        recognizer = new MockFaultRecognizer();

                    _matManager = new MaintenanceMaterialManager(recognizer);
                }
                return _matManager;
            }
        }
        private MaterialManager MaterialMgr => _materialManager ?? (_materialManager = new MaterialManager());

        // 标记是否正在绑定数据（防止 CellValueChanged 在绑定期间触发计算）
        private bool _isBindingData = false;

        public AgentForm(int recordId)
        {
            InitializeComponent();
            _recordId = recordId;

            if (DesignMode) return;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint |
                          ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;

            // 事件绑定
            btnAnalyze.Click += BtnAnalyze_Click;
            btnConfirmFault.Click += BtnConfirmFault_Click;
            btnAddToGrid.Click += BtnAddToGrid_Click;
            btnConfirm.Click += BtnConfirm_Click;
            dgvMaterials.CellValueChanged += DgvMaterials_CellValueChanged;
            dgvMaterials.CurrentCellDirtyStateChanged += DgvMaterials_CurrentCellDirtyStateChanged;
            dgvMaterials.CellContentClick += DgvMaterials_CellContentClick;
            dgvMaterials.DataError += (s, ev) => { ev.ThrowException = false; };
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // 强制设置下拉框字体（SUNNY UI 可能不继承 Design 时设置的字体）
            cmbMaterialSelect.Font = new System.Drawing.Font("微软雅黑", 9F);
            // 手动分析区始终可见，加载时即填充故障类型下拉框
            LoadFaultTypeOptions();
            await LoadMaterialDropdown();
            await LoadOrderInfo();
        }

        /// <summary>
        /// 填充手动分析下拉框（从数据库获取全部故障类型，不限设备分类）
        /// </summary>
        private void LoadFaultTypeOptions()
        {
            cmbFaultType.Items.Clear();
            List<string> faultTypes = MntBLL.GetFaultTypeOptionsList();
            if (faultTypes != null && faultTypes.Count > 0)
            {
                foreach (var ft in faultTypes) cmbFaultType.Items.Add(ft);
                cmbFaultType.SelectedIndex = 0;
            }
            else
            {
                cmbFaultType.Items.Add("请先在物料管理中配置故障类型");
                cmbFaultType.SelectedIndex = 0;
            }
        }

        // ==================== 数据加载 ====================

        private async Task LoadOrderInfo()
        {
            try
            {
                DataTable dt = MntBLL.GetOrderById(_recordId);
                if (dt == null || dt.Rows.Count == 0)
                {
                    UIMessageBox.ShowError("工单不存在！");
                    this.Close();
                    return;
                }

                _orderRow = dt.Rows[0];

                // 显示设备信息
                string equipName = _orderRow["EquipmentName"]?.ToString() ?? "未知设备";
                string categoryId = _orderRow["CategoryId"]?.ToString() ?? "";
                lblEquipment.Text = $"{equipName}（分类ID: {categoryId}）";

                // 显示故障描述
                lblFaultDesc.Text = _orderRow["FaultDesc"]?.ToString() ?? "";

                // 自动计算预估停机时长（报修时间 → 当前时间）
                if (_orderRow["ReportTime"] != DBNull.Value)
                {
                    var reportTime = Convert.ToDateTime(_orderRow["ReportTime"]);
                    int estimatedHours = (int)Math.Ceiling((DateTime.Now - reportTime).TotalHours);
                    if (estimatedHours < 0) estimatedHours = 0;
                    txtDowntimeHours.Text = estimatedHours.ToString();
                }

                // 显示照片
                string photoPath = _orderRow["PhotoPath"]?.ToString();
                LoadPhoto(photoPath);

                // 如果已有 AI 结果则回显
                string aiFaultType = _orderRow["AiFaultType"]?.ToString();
                decimal aiConfidence = _orderRow["AiConfidence"] != DBNull.Value
                    ? Convert.ToDecimal(_orderRow["AiConfidence"]) : 0;
                if (!string.IsNullOrEmpty(aiFaultType))
                {
                    if (aiConfidence < 0.5m)
                    {
                        lblAiResult.ForeColor = Color.Red;
                        lblAiResult.Text = $"AI识别：{aiFaultType}（置信度 {aiConfidence:P0}）⚠️ 置信度较低，建议手动选择故障类型";
                    }
                    else
                    {
                        lblAiResult.ForeColor = Color.Black;
                        lblAiResult.Text = $"AI识别：{aiFaultType}（置信度 {aiConfidence:P0}）";
                    }
                    await LoadRecommendedMaterials();
                }
                else
                {
                    // AI 还没跑过：回显旧的维修物料记录（如果有的话，用于参考）
                    List<MaintenanceMaterial> existingMats = await MatManager.GetExistingMaterials(_recordId);
                    if (existingMats.Count > 0)
                    {
                        BindMaterialsToGrid(existingMats);
                    }
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"加载工单失败：{ex.Message}");
            }
        }

        private void LoadPhoto(string photoPath)
        {
            if (string.IsNullOrEmpty(photoPath) || !File.Exists(photoPath)) return;
            try
            {
                using (var fs = new FileStream(photoPath, FileMode.Open, FileAccess.Read))
                {
                    picFault.Image = Image.FromStream(fs);
                }
            }
            catch { /* 照片加载失败不影响主流程 */ }
        }

        // ==================== AI 分析 ====================

        private async void BtnAnalyze_Click(object sender, EventArgs e)
        {
            btnAnalyze.Enabled = false;
            lblAiResult.Text = "AI 分析中，请稍候...";

            try
            {
                string photoPath = _orderRow["PhotoPath"]?.ToString();
                if (string.IsNullOrEmpty(photoPath) || !File.Exists(photoPath))
                {
                    FallbackToManualSelection();
                    return;
                }

                byte[] photoBytes = File.ReadAllBytes(photoPath);

                FaultRecognitionResult result = await Task.Run(() =>
                    MatManager.AnalyzeFaultPhoto(_recordId, photoBytes));

                if (result == null || string.IsNullOrEmpty(result.FaultType) || result.FaultType == "未识别"
                    || result.FaultType == "无可用故障类型")
                {
                    FallbackToManualSelection();
                    return;
                }

                if (result.Confidence < 0.5m)
                {
                    lblAiResult.ForeColor = Color.Red;
                    lblAiResult.Text = $"AI识别：{result.FaultType}（置信度 {result.Confidence:P0}）⚠️ 置信度较低，建议手动选择故障类型";

                    // 低置信度不加载物料，清空网格并提示手动选择
                    ShowManualSelectHint("置信度较低，请手动选择故障类型后确认");
                }
                else
                {
                    lblAiResult.ForeColor = Color.Black;
                    lblAiResult.Text = $"AI识别：{result.FaultType}（置信度 {result.Confidence:P0}）";

                    DataTable dt = MntBLL.GetOrderById(_recordId);
                    if (dt != null && dt.Rows.Count > 0) _orderRow = dt.Rows[0];

                    await LoadRecommendedMaterials();
                }
            }
            catch (Exception ex)
            {
                lblAiResult.Text = $"AI 分析失败：{ex.Message}";
                FallbackToManualSelection();
            }
            finally
            {
                btnAnalyze.Enabled = true;
            }
        }

        /// <summary>
        /// AI 失败时的降级方案：提示手动选择（下拉框始终可见，只需刷新选项）
        /// </summary>
        private void FallbackToManualSelection()
        {
            lblAiResult.Text = "AI 无法识别，请手动选择故障类型";
            LoadFaultTypeOptions();
            ShowManualSelectHint("AI 无法识别，请手动选择故障类型");
        }

        /// <summary>
        /// 清空物料网格并显示手动选择提示，同时重置故障类型选中项
        /// </summary>
        private void ShowManualSelectHint(string message)
        {
            dgvMaterials.Rows.Clear();
            dgvMaterials.Rows.Add(false, message, 0, "", 0, 0, DBNull.Value, "0");
            cmbFaultType.Visible = true;
            btnConfirmFault.Visible = true;
            cmbFaultType.SelectedIndex = -1;
            RecalcAmounts();
        }

        /// <summary>
        /// 用户点击"确认故障类型"→ 写入 AiFaultType → 加载推荐物料
        /// </summary>
        private async void BtnConfirmFault_Click(object sender, EventArgs e)
        {
            if (cmbFaultType.SelectedIndex < 0) return;
            string selected = cmbFaultType.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selected) || selected.Contains("请先"))
            {
                UIMessageBox.ShowInfo("请选择有效的故障类型");
                return;
            }

            btnConfirmFault.Enabled = false;
            try
            {
                var mntDal = new HospitalEquipment.DAL.MaintenanceDAL();
                mntDal.UpdateAiResult(_recordId, selected, 0);
                DataTable dt = MntBLL.GetOrderById(_recordId);
                if (dt != null && dt.Rows.Count > 0) _orderRow = dt.Rows[0];

                lblAiResult.Text = $"手动选择：{selected}";

                await LoadRecommendedMaterials();
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"加载推荐物料失败：{ex.Message}");
            }
            finally
            {
                btnConfirmFault.Enabled = true;
            }
        }

        // ==================== 物料推荐 ====================

        private async Task LoadRecommendedMaterials()
        {
            try
            {
                List<Material> recommended = await MatManager.GetRecommendedMaterials(_recordId);
                if (recommended == null || recommended.Count == 0)
                {
                    dgvMaterials.Rows.Clear();
                    dgvMaterials.Rows.Add(false, "该设备+故障类型组合下暂无预配置物料，请手动添加", 0, "", 0, 0, DBNull.Value, "0");
                    return;
                }

                _isBindingData = true;
                dgvMaterials.Rows.Clear();

                foreach (var mat in recommended)
                {
                    decimal subtotal = mat.DefaultQuantity * mat.UnitPrice;
                    int rowIdx = dgvMaterials.Rows.Add(
                        true,                           // 默认勾选
                        mat.MaterialName,
                        mat.DefaultQuantity,
                        mat.Unit ?? "个",
                        mat.UnitPrice,
                        subtotal,
                        mat.MaterialId,
                        "0"                             // 0 = AI 推荐
                    );
                }

                _isBindingData = false;
                RecalcAmounts();
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"加载物料推荐失败：{ex.Message}");
            }
        }

        // ==================== 手动添加物料（下拉框选择） ====================

        /// <summary>
        /// 加载物料下拉框 + 该设备可能用到的全部物料到网格
        /// </summary>
        private async Task LoadMaterialDropdown()
        {
            try
            {
                var materials = await MaterialMgr.GetAllMaterials();
                cmbMaterialSelect.Items.Clear();
                if (materials != null && materials.Count > 0)
                {
                    foreach (var m in materials)
                    {
                        if (string.IsNullOrWhiteSpace(m.MaterialName)) continue;
                        string unit = string.IsNullOrWhiteSpace(m.Unit) ? "个" : m.Unit;
                        string text = $"{m.MaterialName}（¥{m.UnitPrice:F2}/{unit}）";
                        cmbMaterialSelect.Items.Add(text);
                    }
                }
                if (cmbMaterialSelect.Items.Count == 0)
                {
                    cmbMaterialSelect.Items.Add("暂无可用物料");
                }
                cmbMaterialSelect.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                cmbMaterialSelect.Items.Clear();
                cmbMaterialSelect.Items.Add($"物料加载失败：{ex.Message}");
            }
        }

        private async void BtnAddToGrid_Click(object sender, EventArgs e)
        {
            if (cmbMaterialSelect.SelectedIndex < 0 || cmbMaterialSelect.Items.Count == 0)
            {
                UIMessageBox.ShowInfo("请先选择物料");
                return;
            }

            string selected = cmbMaterialSelect.SelectedItem?.ToString() ?? "";
            if (selected == "暂无可用物料" || selected.StartsWith("物料加载失败"))
            {
                UIMessageBox.ShowInfo("物料列表不可用");
                return;
            }

            if (!int.TryParse(txtManualQty.Text.Trim(), out int qty) || qty <= 0)
            {
                UIMessageBox.ShowInfo("请输入有效的数量");
                return;
            }

            // 从下拉框中解析物料信息
            // 格式: "物料名称（¥单价/单位）"
            int bracketIdx = selected.LastIndexOf('（');
            if (bracketIdx < 0)
            {
                UIMessageBox.ShowInfo("无效的物料格式");
                return;
            }
            string name = selected.Substring(0, bracketIdx);
            string info = selected.Substring(bracketIdx + 1).TrimEnd('）');
            var parts = info.Split('/');
            decimal price = 0;
            string unit = "个";
            if (parts.Length >= 1) decimal.TryParse(parts[0].TrimStart('¥'), out price);
            if (parts.Length >= 2) unit = parts[1];

            // 根据名称从物料库中查找 MaterialId
            int? matId = null;
            try
            {
                var materials = await MaterialMgr.GetAllMaterials();
                if (materials != null)
                {
                    int idx = cmbMaterialSelect.SelectedIndex;
                    if (idx >= 0 && idx < materials.Count)
                    {
                        matId = materials[idx].MaterialId;
                    }
                }
            }
            catch { /* MaterialId 查找失败不影响添加 */ }

            decimal subtotal = qty * price;

            _isBindingData = true;
            int rowIdx = dgvMaterials.Rows.Add(
                true,
                name,
                qty,
                unit,
                price,
                subtotal,
                matId ?? (object)DBNull.Value,
                "1"
            );
            _isBindingData = false;

            RecalcAmounts();
        }

        // ==================== 金额计算 ====================

        private void DgvMaterials_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // CheckBox 列点击时立即提交
            if (dgvMaterials.IsCurrentCellDirty &&
                (dgvMaterials.CurrentCell.OwningColumn.Name == "colCheck" ||
                 dgvMaterials.CurrentCell.OwningColumn.Name == "colQuantity"))
            {
                dgvMaterials.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        /// <summary>
        /// 点击删除按钮时移除该行并刷新金额
        /// </summary>
        private void DgvMaterials_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvMaterials.Columns[e.ColumnIndex].Name == "colDelete")
            {
                dgvMaterials.Rows.RemoveAt(e.RowIndex);
                RecalcAmounts();
            }
        }

        private void DgvMaterials_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_isBindingData || e.RowIndex < 0) return;

            var row = dgvMaterials.Rows[e.RowIndex];
            string colName = dgvMaterials.Columns[e.ColumnIndex].Name;

            if (colName == "colQuantity")
            {
                // 数量变更 → 重新计算小计
                if (row.Cells["colQuantity"].Value != null &&
                    int.TryParse(row.Cells["colQuantity"].Value.ToString(), out int qty) &&
                    row.Cells["colUnitPrice"].Value != null &&
                    decimal.TryParse(row.Cells["colUnitPrice"].Value.ToString(), out decimal price))
                {
                    row.Cells["colSubtotal"].Value = qty * price;
                }
            }

            // 无论哪个列变更，都重算汇总
            RecalcAmounts();
        }

        /// <summary>
        /// 实时计算并刷新三个金额标签
        /// </summary>
        private void RecalcAmounts()
        {
            decimal aiTotal = 0;
            decimal manualTotal = 0;

            foreach (DataGridViewRow row in dgvMaterials.Rows)
            {
                if (row.IsNewRow) continue;

                bool isChecked = row.Cells["colCheck"].Value != null &&
                                 Convert.ToBoolean(row.Cells["colCheck"].Value);
                if (!isChecked) continue;

                decimal subtotal = row.Cells["colSubtotal"].Value != null
                    ? Convert.ToDecimal(row.Cells["colSubtotal"].Value) : 0;

                bool isManual = row.Cells["colIsManual"].Value != null &&
                                row.Cells["colIsManual"].Value.ToString() == "1";

                if (isManual) manualTotal += subtotal;
                else aiTotal += subtotal;
            }

            lblAiAmount.Text = $"AI推荐：¥{aiTotal:F2}";
            lblManualAmount.Text = $"手动添加：¥{manualTotal:F2}";
            lblTotalAmount.Text = $"总金额：¥{(aiTotal + manualTotal):F2}";
        }

        // ==================== 数据绑定辅助 ====================

        private void BindMaterialsToGrid(List<MaintenanceMaterial> materials)
        {
            if (materials == null || materials.Count == 0) return;

            _isBindingData = true;
            dgvMaterials.Rows.Clear();

            foreach (var mm in materials)
            {
                bool isManual = !mm.MaterialId.HasValue || mm.MaterialId == 0;
                dgvMaterials.Rows.Add(
                    true,
                    mm.MaterialName,
                    mm.Quantity,
                    "",
                    mm.UnitPrice,
                    mm.Subtotal,
                    mm.MaterialId ?? (object)DBNull.Value,
                    isManual ? "1" : "0"
                );
            }

            _isBindingData = false;
            RecalcAmounts();
        }

        // ==================== 确认保存 ====================

        private async void BtnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                // 从 DataGridView 收集勾选的物料
                var materials = new List<MaintenanceMaterial>();

                foreach (DataGridViewRow row in dgvMaterials.Rows)
                {
                    if (row.IsNewRow) continue;

                    bool isChecked = row.Cells["colCheck"].Value != null &&
                                     Convert.ToBoolean(row.Cells["colCheck"].Value);
                    if (!isChecked) continue;

                    string name = row.Cells["colMatName"].Value?.ToString() ?? "";
                    if (string.IsNullOrWhiteSpace(name)) continue;

                    int qty = row.Cells["colQuantity"].Value != null &&
                              int.TryParse(row.Cells["colQuantity"].Value.ToString(), out int q) ? q : 1;
                    decimal price = row.Cells["colUnitPrice"].Value != null &&
                                    decimal.TryParse(row.Cells["colUnitPrice"].Value.ToString(), out decimal p) ? p : 0;

                    bool isManual = row.Cells["colIsManual"].Value != null &&
                                    row.Cells["colIsManual"].Value.ToString() == "1";

                    materials.Add(new MaintenanceMaterial
                    {
                        RecordId = _recordId,
                        MaterialId = !isManual && row.Cells["colMatId"].Value != DBNull.Value
                            ? Convert.ToInt32(row.Cells["colMatId"].Value) : (int?)null,
                        MaterialName = name,
                        Quantity = qty,
                        UnitPrice = price,
                    });
                }

                if (materials.Count == 0)
                {
                    if (!UIMessageBox.ShowAsk("未选择任何物料，确认直接完成维修（无物料消耗）？"))
                        return;
                }

                // ① 保存物料清单
                await MatManager.SaveMaintenanceMaterials(_recordId, materials);

                // ② 汇总物料总金额
                decimal totalAmount = materials.Sum(m => m.Subtotal);

                // ③ 生成维修结果文本
                string repairResult;
                if (materials.Count > 0)
                {
                    var matSummary = string.Join("、", materials.Select(m => $"{m.MaterialName} x{m.Quantity}"));
                    repairResult = $"{matSummary}，物料总金额 ¥{totalAmount:N2}";
                }
                else
                {
                    repairResult = "无物料消耗";
                }

                // ④ 完成维修：写入 RepairResult、RepairCost，状态更新为 Done
                // 自动计算停机时长：优先取用户输入，无效时按"当前时间 - 报修时间"估算
                int downtimeHours = 0;
                if (!int.TryParse(txtDowntimeHours.Text.Trim(), out downtimeHours) || downtimeHours <= 0)
                {
                    var reportTime = Convert.ToDateTime(_orderRow["ReportTime"]);
                    downtimeHours = (int)Math.Ceiling((DateTime.Now - reportTime).TotalHours);
                    txtDowntimeHours.Text = downtimeHours.ToString();  // 回填到文本框
                }
                bool success = MntBLL.SubmitRepairResult(_recordId, repairResult, totalAmount, downtimeHours);
                if (!success)
                {
                    UIMessageBox.ShowError("提交维修结果失败，请重试");
                    return;
                }

                UIMessageBox.Show("维修完成，物料清单已保存");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"保存失败：{ex.Message}");
            }
        }
    }
}
