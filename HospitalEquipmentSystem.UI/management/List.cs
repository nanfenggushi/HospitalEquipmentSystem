using HospitalEquipment.BLL.management;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI.management
{
    public partial class List : UIForm
    {
        private EquipmentManager manager = new EquipmentManager();
        private int currentPage = 1;
        private int pageSize = 10;
        private int totalCount = 0;
        private int loadVersion = 0;

        public List()
        {
            InitializeComponent();
            this.btnPrevPage.Click += (s, e) => { if (currentPage > 1) { currentPage--; _ = LoadData(); } };
            this.btnNextPage.Click += (s, e) => { int totalPages = (int)Math.Ceiling((double)totalCount / pageSize); if (currentPage < totalPages) { currentPage++; _ = LoadData(); } };

            this.uiButton4.Click += BtnSearch_Click;//查询
            this.uiButton5.Click += BtnReset_Click; //重置
        }

        /// <summary>
        /// 重置按钮：清空所有筛选条件
        /// </summary>
        private async void BtnReset_Click(object sender, EventArgs e)
        {

            uiTextBox1.Clear();//清空搜索框
            uiComboBox1.SelectedIndex = 0; //状态下拉框重置为“全部”
            uiComboBox2.SelectedIndex = 0; //科室下拉框重置为“全部科室”
            currentPage = 1;
            await LoadData();
        }
        /// <summary>
        /// 查询按钮：根据当前筛选条件搜索
        /// </summary>
        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            await LoadData();
        }

        private async void List_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. 配置表格列（绑定数据字段）
                ConfigureDataGridView();
                // 2. 初始化下拉框
                await LoadComboBoxes();
                // 3. 加载第一页数据
                currentPage = 1;
                await LoadData();
                uiTextBox1.Watermark = "输入设备名称/编号/型号搜索";
            }
            catch (Exception ex)
            {
                UIMessageBox.Show($"初始化失败：{ex.Message}", "错误", UIStyle.Red);
            }
        }

        /// <summary>
        /// 配置表格列（直接使用设计器中已有的列）
        /// </summary>
        private void ConfigureDataGridView()
        {
            // 关闭自动生成列（必须）
            uiDataGridView1.AutoGenerateColumns = false;

            // 注意：列名需要与设计器中的 Name 一致
            if (uiDataGridView1.Columns.Contains("Column1"))
            {
                uiDataGridView1.Columns["Column1"].DataPropertyName = "EquipmentNo";
                uiDataGridView1.Columns["Column1"].HeaderText = "编号";
            }
            if (uiDataGridView1.Columns.Contains("Column2"))
            {
                uiDataGridView1.Columns["Column2"].DataPropertyName = "EquipmentName";
                uiDataGridView1.Columns["Column2"].HeaderText = "名称";
            }
            if (uiDataGridView1.Columns.Contains("Column3"))
            {
                uiDataGridView1.Columns["Column3"].DataPropertyName = "Model";
                uiDataGridView1.Columns["Column3"].HeaderText = "型号";
            }
            if (uiDataGridView1.Columns.Contains("Column4"))
            {
                uiDataGridView1.Columns["Column4"].DataPropertyName = "DeptName";
                uiDataGridView1.Columns["Column4"].HeaderText = "科室";
            }
            if (uiDataGridView1.Columns.Contains("Column5"))
            {
                uiDataGridView1.Columns["Column5"].DataPropertyName = "Status";
                uiDataGridView1.Columns["Column5"].HeaderText = "状态";
            }
            if (uiDataGridView1.Columns.Contains("Column6"))
            {
                uiDataGridView1.Columns["Column6"].HeaderText = "操作";
                // Column6 是按钮列，不能设置 DataPropertyName
            }

            // 状态转中文（使用 CellFormatting 事件）
            uiDataGridView1.CellFormatting += (s, ev) =>
            {
                if (ev.ColumnIndex >= 0 && uiDataGridView1.Columns[ev.ColumnIndex].Name == "Column5" && ev.Value != null)
                {
                    ev.Value = manager.GetStatusChinese(ev.Value.ToString());
                    ev.FormattingApplied = true;
                }
            };
        }

        /// <summary>
        /// 加载筛选下拉框数据
        /// </summary>
        private async Task LoadComboBoxes()
        {
            // 状态下拉（显示中文，存储英文值）
            var statusItems = new List<KeyValuePair<string, string>>();
            statusItems.Add(new KeyValuePair<string, string>("", "全部"));
            statusItems.Add(new KeyValuePair<string, string>("Idle", "闲置"));
            statusItems.Add(new KeyValuePair<string, string>("InUse", "使用中"));
            statusItems.Add(new KeyValuePair<string, string>("Maintenance", "维修中"));
            statusItems.Add(new KeyValuePair<string, string>("Borrowed", "已借用"));
            statusItems.Add(new KeyValuePair<string, string>("Scrapped", "已报废"));

            uiComboBox1.DataSource = null;
            uiComboBox1.DisplayMember = "Value";
            uiComboBox1.ValueMember = "Key";
            uiComboBox1.DataSource = statusItems;
            uiComboBox1.SelectedIndex = 0;

            // 科室下拉（保持原样）
            var depts = await manager.GetDepartments();
            var items = new List<KeyValuePair<int, string>>();
            items.Add(new KeyValuePair<int, string>(0, "全部科室"));
            items.AddRange(depts);
            uiComboBox2.DataSource = items;
            uiComboBox2.DisplayMember = "Value";
            uiComboBox2.ValueMember = "Key";
            uiComboBox2.SelectedIndex = 0;
        }

        /// <summary>
        /// 加载数据列表
        /// </summary>
        private async Task LoadData()
        {
            try
            {
                int version = ++loadVersion;
                string keyword = uiTextBox1.Text.Trim();
                string status = uiComboBox1.SelectedValue as string ?? "";

                int deptId = 0;
                if (uiComboBox2.SelectedValue is int val) deptId = val;
                int? deptIdParam = deptId == 0 ? (int?)null : deptId;

                var result = await manager.GetPaged(currentPage, pageSize, keyword, status, deptIdParam);
                if (version != loadVersion) return;
                totalCount = result.total;

                int displayedRow = uiDataGridView1.FirstDisplayedScrollingRowIndex;
                int displayedColumn = uiDataGridView1.FirstDisplayedScrollingColumnIndex;
                int currentRow = uiDataGridView1.CurrentCell != null ? uiDataGridView1.CurrentCell.RowIndex : -1;
                int currentColumn = uiDataGridView1.CurrentCell != null ? uiDataGridView1.CurrentCell.ColumnIndex : -1;

                //// 调试：显示查询结果数量
                //UIMessageBox.Show($"查询条件：关键字='{keyword}', 状态='{status}', 科室ID={deptIdParam?.ToString() ?? "null"}\n总记录数={result.total}", "调试信息");
                // 绑定数据
                uiDataGridView1.DataSource = null;
                uiDataGridView1.DataSource = result.list;

                // 显示总记录数
                uiLabel2.Text = $"共 {totalCount} 台设备";

                // 更新分页控件
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                if (totalPages < 1) totalPages = 1;
                lblPageInfo.Text = $"第 {currentPage}/{totalPages} 页 · 每页 {pageSize} 条 · 共 {totalCount} 条";
                btnPrevPage.Enabled = currentPage > 1;
                btnNextPage.Enabled = currentPage < totalPages;

                try
                {
                    if (result.list.Count > 0)
                    {
                        if (currentRow >= 0 && currentRow < uiDataGridView1.Rows.Count)
                        {
                            int restoreColumn = currentColumn >= 0 && currentColumn < uiDataGridView1.Columns.Count
                                ? currentColumn
                                : 0;
                            uiDataGridView1.CurrentCell = uiDataGridView1.Rows[currentRow].Cells[restoreColumn];
                            uiDataGridView1.Rows[currentRow].Selected = true;
                        }
                        if (displayedRow >= 0 && displayedRow < uiDataGridView1.Rows.Count)
                            uiDataGridView1.FirstDisplayedScrollingRowIndex = displayedRow;
                        if (displayedColumn >= 0 && displayedColumn < uiDataGridView1.Columns.Count)
                            uiDataGridView1.FirstDisplayedScrollingColumnIndex = displayedColumn;
                    }
                }
                catch
                {
                    // 恢复失败时不阻断数据加载，仅保留默认位置
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.Show($"加载数据失败：{ex.Message}", "错误", UIStyle.Red);
            }
        }
    }
}
