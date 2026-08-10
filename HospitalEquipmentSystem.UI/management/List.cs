using HospitalEquipment.BLL;
using HospitalEquipment.BLL.management;
using HospitalEquipment.Model.management;
using MiniExcelLibs;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
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
        private add _addForm;

        public List()
        {
            InitializeComponent();
            this.btnPrevPage.Click += (s, e) => { if (currentPage > 1) { currentPage--; _ = LoadData(); } };
            this.btnNextPage.Click += (s, e) => { int totalPages = (int)Math.Ceiling((double)totalCount / pageSize); if (currentPage < totalPages) { currentPage++; _ = LoadData(); } };

            this.uiButton1.Click += BtnAdd_Click; //新增
            this.uiButton2.Click += BtnImport_Click; //导入
            this.uiButton3.Click += BtnExport_Click; //导出
            this.uiButton4.Click += BtnSearch_Click;//查询
            this.uiButton5.Click += BtnReset_Click; //重置

            this.Resize += (s, e) =>
            {
                if (_addForm != null && !_addForm.IsDisposed)
                {
                    _addForm.Location = GetAddFormLocation(_addForm);
                }
            };

            this.VisibleChanged += (s, e) =>
            {
                if (!Visible && _addForm != null && !_addForm.IsDisposed)
                {
                    CloseAddForm(_addForm);
                }
            };
        }

        /// <summary>
        /// 新增按钮：在列表窗体上弹出新增窗体，不隐藏当前列表
        /// </summary>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (_addForm != null && !_addForm.IsDisposed)
            {
                _addForm.BringToFront();
                return;
            }

            var form = new add();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.StartPosition = FormStartPosition.Manual;
            form.ShowInTaskbar = false;

            Controls.Add(form);
            form.Location = GetAddFormLocation(form);
            form.FormClosed += (s, e) => CloseAddForm(form);
            form.DataSaved += async () => { await LoadData(); };
            form.BringToFront();
            form.Show();
            _addForm = form;
        }

        /// <summary>
        /// 关闭叠加的新增窗体并恢复列表操作
        /// </summary>
        private void CloseAddForm(add form)
        {
            if (form == null) return;

            if (!form.IsDisposed)
            {
                if (form.Parent == this)
                {
                    Controls.Remove(form);
                }
                form.Dispose();
            }

            if (ReferenceEquals(_addForm, form))
            {
                _addForm = null;
            }

            if (Visible)
            {
                Activate();
            }
        }

        /// <summary>
        /// 计算新增窗体在列表窗体中的居中位置，避免遮住标题栏和分页栏
        /// </summary>
        private Point GetAddFormLocation(add form)
        {
            int top = uiTitlePanel1.Height;
            int bottom = pnlPager.Height;
            int availableHeight = Math.Max(0, ClientSize.Height - top - bottom);
            int x = Math.Max(0, (ClientSize.Width - form.Width) / 2);
            int y = top + Math.Max(0, (availableHeight - form.Height) / 2);
            return new Point(x, y);
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

        /// <summary>
        /// 导入按钮：从 Excel 导入设备数据
        /// </summary>
        private async void BtnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "选择设备导入文件";
                ofd.Filter = "Excel 工作簿 (*.xlsx)|*.xlsx";
                if (ofd.ShowDialog() != DialogResult.OK) return;

                if (!UIMessageBox.ShowAsk($"确定要从文件导入设备数据吗？\n文件：{ofd.FileName}"))
                    return;

                await ImportEquipmentAsync(ofd.FileName);
            }
        }

        /// <summary>
        /// 执行设备 Excel 导入：逐行校验后写入数据库
        /// </summary>
        private async Task ImportEquipmentAsync(string filePath)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                var rows = MiniExcel.Query(filePath, useHeaderRow: true).ToList();
                if (rows.Count == 0)
                {
                    UIMessageBox.ShowWarning("文件中没有可导入的数据行。");
                    return;
                }

                var categories = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                foreach (var c in await new CategoryManager().GetAll())
                {
                    if (!string.IsNullOrWhiteSpace(c.Name) && !categories.ContainsKey(c.Name.Trim()))
                        categories[c.Name.Trim()] = c.CategoryId;
                }

                var suppliers = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                foreach (var s in await new SupplierManager().GetActive())
                {
                    if (!string.IsNullOrWhiteSpace(s.SupplierName) && !suppliers.ContainsKey(s.SupplierName.Trim()))
                        suppliers[s.SupplierName.Trim()] = s.SupplierId;
                }

                var departments = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                foreach (var d in await manager.GetDepartments())
                {
                    if (!string.IsNullOrWhiteSpace(d.Value) && !departments.ContainsKey(d.Value.Trim()))
                        departments[d.Value.Trim()] = d.Key;
                }

                var users = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                foreach (var u in await manager.GetUsers())
                {
                    if (!string.IsNullOrWhiteSpace(u.Value) && !users.ContainsKey(u.Value.Trim()))
                        users[u.Value.Trim()] = u.Key;
                }

                var seenNos = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                int successCount = 0;
                var errors = new List<string>();

                for (int i = 0; i < rows.Count; i++)
                {
                    var row = rows[i] as IDictionary<string, object>;
                    int rowNo = i + 2;
                    if (row == null)
                    {
                        errors.Add($"第 {rowNo} 行：无法读取该行数据");
                        continue;
                    }

                    string equipmentNo = CellText(row, "设备编号", "EquipmentNo");
                    string equipmentName = CellText(row, "设备名称", "EquipmentName");
                    string model = CellText(row, "型号", "Model");
                    string manufacturer = CellText(row, "制造商", "Manufacturer");
                    string categoryName = CellText(row, "分类", "Category", "CategoryName");
                    string supplierName = CellText(row, "供应商", "Supplier", "SupplierName");
                    string deptName = CellText(row, "科室", "Department", "DeptName");
                    string responsibleName = CellText(row, "责任人", "ResponsibleUser", "ResponsibleUserRealName");
                    string location = CellText(row, "存放位置", "Location");
                    string priceText = CellText(row, "采购价格", "Price");
                    string purchaseDateText = CellText(row, "采购日期", "PurchaseDate");
                    string warrantyText = CellText(row, "保修期限", "WarrantyMonths");
                    string lifeText = CellText(row, "使用年限", "ServiceLife");
                    string statusText = CellText(row, "状态", "Status");
                    string remarks = CellText(row, "备注", "Remarks");

                    var rowErrors = new List<string>();
                    if (string.IsNullOrWhiteSpace(equipmentNo))
                        rowErrors.Add("设备编号不能为空");
                    else if (seenNos.Contains(equipmentNo))
                        rowErrors.Add("设备编号在文件中重复");
                    else if (await manager.IsEquipmentNoExists(equipmentNo))
                        rowErrors.Add("设备编号已存在于数据库");
                    else
                        seenNos.Add(equipmentNo);

                    if (string.IsNullOrWhiteSpace(equipmentName))
                        rowErrors.Add("设备名称不能为空");

                    int? categoryId = null;
                    if (!string.IsNullOrWhiteSpace(categoryName))
                    {
                        if (categories.TryGetValue(categoryName.Trim(), out int catId))
                            categoryId = catId;
                        else
                            rowErrors.Add($"分类 '{categoryName}' 不存在");
                    }

                    int? supplierId = null;
                    if (!string.IsNullOrWhiteSpace(supplierName))
                    {
                        if (suppliers.TryGetValue(supplierName.Trim(), out int supId))
                            supplierId = supId;
                        else
                            rowErrors.Add($"供应商 '{supplierName}' 不存在");
                    }

                    int? deptId = null;
                    if (!string.IsNullOrWhiteSpace(deptName))
                    {
                        if (departments.TryGetValue(deptName.Trim(), out int depId))
                            deptId = depId;
                        else
                            rowErrors.Add($"科室 '{deptName}' 不存在");
                    }

                    int? responsibleUserId = null;
                    if (!string.IsNullOrWhiteSpace(responsibleName))
                    {
                        if (users.TryGetValue(responsibleName.Trim(), out int userId))
                            responsibleUserId = userId;
                        else
                            rowErrors.Add($"责任人 '{responsibleName}' 不存在");
                    }

                    decimal? price = null;
                    if (!string.IsNullOrWhiteSpace(priceText))
                    {
                        if (decimal.TryParse(priceText.Trim(), out decimal p) && p >= 0)
                            price = p;
                        else
                            rowErrors.Add("采购价格格式不正确");
                    }

                    DateTime? purchaseDate = null;
                    if (!string.IsNullOrWhiteSpace(purchaseDateText))
                    {
                        if (DateTime.TryParse(purchaseDateText.Trim(), out DateTime d))
                            purchaseDate = d.Date;
                        else
                            rowErrors.Add("采购日期格式不正确");
                    }

                    int? warrantyMonths = ParseIntOrAddError(warrantyText, "保修期限", rowErrors);
                    int? serviceLife = ParseIntOrAddError(lifeText, "使用年限", rowErrors);

                    string status = StatusToEn(statusText);
                    if (!string.IsNullOrWhiteSpace(statusText) && status == null)
                        rowErrors.Add("状态值无效（可用：闲置/使用中/维修中/已借用/已报废）");
                    status = status ?? "Idle";

                    if (rowErrors.Count > 0)
                    {
                        errors.Add($"第 {rowNo} 行：{string.Join("；", rowErrors)}");
                        continue;
                    }

                    var eq = new Equipment
                    {
                        EquipmentNo = equipmentNo,
                        EquipmentName = equipmentName,
                        Model = model,
                        Manufacturer = manufacturer,
                        CategoryId = categoryId,
                        SupplierId = supplierId,
                        DeptId = deptId,
                        ResponsibleUserId = responsibleUserId,
                        Location = location,
                        Price = price,
                        PurchaseDate = purchaseDate,
                        WarrantyMonths = warrantyMonths,
                        ServiceLife = serviceLife,
                        Status = status,
                        Remarks = remarks
                    };

                    try
                    {
                        if (manager.Insert(eq))
                            successCount++;
                        else
                            errors.Add($"第 {rowNo} 行：保存失败");
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"第 {rowNo} 行：{ex.Message}");
                    }
                }

                if (successCount > 0)
                {
                    UIMessageBox.ShowSuccess($"导入完成：成功 {successCount} 条，失败 {errors.Count} 条。");
                    currentPage = 1;
                    await LoadData();
                }
                else
                {
                    UIMessageBox.ShowError($"导入失败：{errors.Count} 条记录均未导入。");
                }

                if (errors.Count > 0)
                {
                    string detail = string.Join(Environment.NewLine, errors.Take(20));
                    if (errors.Count > 20)
                        detail += Environment.NewLine + $"... 共 {errors.Count} 条错误";
                    UIMessageBox.Show(detail, "导入错误明细", UIStyle.Red);
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"导入失败：{ex.Message}");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 导出按钮：按当前筛选条件导出全部设备到 Excel
        /// </summary>
        private async void BtnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "导出设备列表";
                sfd.Filter = "Excel 工作簿 (*.xlsx)|*.xlsx";
                sfd.FileName = $"设备列表_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                if (!UIMessageBox.ShowAsk($"确定要导出当前筛选条件下的全部设备数据吗？\n文件：{sfd.FileName}"))
                    return;

                this.Cursor = Cursors.WaitCursor;
                try
                {
                    string keyword = uiTextBox1.Text.Trim();
                    string status = uiComboBox1.SelectedValue as string ?? "";
                    int deptId = 0;
                    if (uiComboBox2.SelectedValue is int val) deptId = val;
                    int? deptIdParam = deptId == 0 ? (int?)null : deptId;

                    var list = await manager.GetExportData(keyword, status, deptIdParam);
                    var table = new DataTable("设备列表");
                    table.Columns.Add("设备编号", typeof(string));
                    table.Columns.Add("设备名称", typeof(string));
                    table.Columns.Add("型号", typeof(string));
                    table.Columns.Add("制造商", typeof(string));
                    table.Columns.Add("分类", typeof(string));
                    table.Columns.Add("供应商", typeof(string));
                    table.Columns.Add("科室", typeof(string));
                    table.Columns.Add("存放位置", typeof(string));
                    table.Columns.Add("责任人", typeof(string));
                    table.Columns.Add("采购价格", typeof(string));
                    table.Columns.Add("采购日期", typeof(string));
                    table.Columns.Add("保修期限(月)", typeof(string));
                    table.Columns.Add("使用年限(年)", typeof(string));
                    table.Columns.Add("最近维护日期", typeof(string));
                    table.Columns.Add("下次维护日期", typeof(string));
                    table.Columns.Add("状态", typeof(string));
                    table.Columns.Add("备注", typeof(string));

                    foreach (var eq in list)
                    {
                        table.Rows.Add(
                            eq.EquipmentNo ?? "",
                            eq.EquipmentName ?? "",
                            eq.Model ?? "",
                            eq.Manufacturer ?? "",
                            eq.CategoryName ?? "",
                            eq.SupplierName ?? "",
                            eq.DeptName ?? "",
                            eq.Location ?? "",
                            eq.ResponsibleUserRealName ?? "",
                            eq.Price.HasValue ? eq.Price.Value.ToString("F2") : "",
                            eq.PurchaseDate.HasValue ? eq.PurchaseDate.Value.ToString("yyyy-MM-dd") : "",
                            eq.WarrantyMonths.HasValue ? eq.WarrantyMonths.Value.ToString() : "",
                            eq.ServiceLife.HasValue ? eq.ServiceLife.Value.ToString() : "",
                            eq.LastMaintainDate.HasValue ? eq.LastMaintainDate.Value.ToString("yyyy-MM-dd") : "",
                            eq.NextMaintainDate.HasValue ? eq.NextMaintainDate.Value.ToString("yyyy-MM-dd") : "",
                            manager.GetStatusChinese(eq.Status),
                            eq.Remarks ?? ""
                        );
                    }

                    MiniExcel.SaveAs(sfd.FileName, table);
                    UIMessageBox.ShowSuccess($"导出成功，共 {list.Count} 台设备。");
                }
                catch (Exception ex)
                {
                    UIMessageBox.ShowError($"导出失败：{ex.Message}");
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private static object GetCell(IDictionary<string, object> row, params string[] keys)
        {
            foreach (var key in keys)
            {
                foreach (var pair in row)
                {
                    if (string.Equals((pair.Key ?? "").Trim(), key, StringComparison.OrdinalIgnoreCase))
                        return pair.Value;
                }
            }
            return null;
        }

        private static string CellText(IDictionary<string, object> row, params string[] keys)
        {
            object value = GetCell(row, keys);
            if (value == null || value == DBNull.Value) return "";
            return Convert.ToString(value)?.Trim() ?? "";
        }

        private static int? ParseIntOrAddError(string text, string fieldName, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            if (int.TryParse(text.Trim(), out int value) && value >= 0) return value;
            errors.Add($"{fieldName}格式不正确");
            return null;
        }

        private static string StatusToEn(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            switch (text.Trim())
            {
                case "闲置":
                case "Idle":
                case "idle":
                    return "Idle";
                case "使用中":
                case "InUse":
                case "inuse":
                    return "InUse";
                case "维修中":
                case "Maintenance":
                case "maintenance":
                    return "Maintenance";
                case "已借用":
                case "已借出":
                case "Borrowed":
                case "borrowed":
                    return "Borrowed";
                case "已报废":
                case "Scrapped":
                case "scrapped":
                    return "Scrapped";
                default:
                    return null;
            }
        }
    }
}
