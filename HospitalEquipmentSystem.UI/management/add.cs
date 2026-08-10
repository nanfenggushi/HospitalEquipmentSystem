using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalEquipment.BLL;
using HospitalEquipment.BLL.management;
using HospitalEquipment.Model.management;

namespace HospitalEquipmentSystem.UI.management
{
    public partial class add : UIForm
    {
        private readonly EquipmentManager _manager = new EquipmentManager();
        private readonly CategoryManager _categoryManager = new CategoryManager();
        private readonly SupplierManager _supplierManager = new SupplierManager();

        /// <summary>保存成功后通知父窗体刷新列表</summary>
        public event Action DataSaved;

        public add()
        {
            InitializeComponent();
            this.Load += Add_Load;
            this.uiButton3.Click += (s, e) => this.Close();
            this.uiButton5.Click += BtnSave_Click;
        }

        private async void Add_Load(object sender, EventArgs e)
        {
            try
            {
                uiLabel1.Text = await _manager.GenerateEquipmentNo();
                uiDatePicker1.Value = DateTime.Today;

                await LoadCategoryCombo();
                await LoadSupplierCombo();
                await LoadDeptCombo();
                await LoadUserCombo();
                LoadStatusCombo();
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"初始化新增设备窗体失败：{ex.Message}");
            }
        }

        private async Task LoadCategoryCombo()
        {
            var categories = await _categoryManager.GetAll();
            var items = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(-1, "未分类")
            };
            foreach (var c in categories)
                items.Add(new KeyValuePair<int, string>(c.CategoryId, c.Name));

            BindCombo(uiComboBox1, items, 0);
        }

        private async Task LoadSupplierCombo()
        {
            var suppliers = await _supplierManager.GetActive();
            var items = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(-1, "未选择")
            };
            foreach (var s in suppliers)
                items.Add(new KeyValuePair<int, string>(s.SupplierId, s.SupplierName));

            BindCombo(uiComboBox2, items, 0);
        }

        private async Task LoadDeptCombo()
        {
            var departments = await _manager.GetDepartments();
            var items = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(0, "未指定")
            };
            items.AddRange(departments);

            BindCombo(uiComboBox3, items, 0);
        }

        private async Task LoadUserCombo()
        {
            var users = await _manager.GetUsers();
            var items = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(0, "未指定")
            };
            items.AddRange(users);

            BindCombo(uiComboBox4, items, 0);
        }

        private void LoadStatusCombo()
        {
            var items = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Idle", "闲置"),
                new KeyValuePair<string, string>("InUse", "使用中"),
                new KeyValuePair<string, string>("Maintenance", "维修中"),
                new KeyValuePair<string, string>("Borrowed", "已借用"),
                new KeyValuePair<string, string>("Scrapped", "已报废")
            };

            uiComboBox5.DataSource = null;
            uiComboBox5.DisplayMember = "Value";
            uiComboBox5.ValueMember = "Key";
            uiComboBox5.DataSource = items;
            uiComboBox5.SelectedIndex = 0;
        }

        private static void BindCombo(UIComboBox combo, object dataSource, int selectedIndex)
        {
            combo.DataSource = null;
            combo.DisplayMember = "Value";
            combo.ValueMember = "Key";
            combo.DataSource = dataSource;
            if (selectedIndex >= 0 && selectedIndex < combo.Items.Count)
                combo.SelectedIndex = selectedIndex;
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uiTextBox2.Text))
                {
                    UIMessageBox.ShowWarning("设备名称不能为空！");
                    uiTextBox2.Focus();
                    return;
                }

                var eq = new Equipment
                {
                    EquipmentNo = uiLabel1.Text.Trim(),
                    EquipmentName = uiTextBox2.Text.Trim(),
                    Model = uiTextBox3.Text.Trim(),
                    Manufacturer = uiTextBox1.Text.Trim(),
                    CategoryId = GetNullableComboId(uiComboBox1),
                    SupplierId = GetNullableComboId(uiComboBox2),
                    DeptId = GetNullableComboId(uiComboBox3),
                    ResponsibleUserId = GetNullableComboId(uiComboBox4),
                    Location = uiTextBox4.Text.Trim(),
                    Price = ParseNullableDecimal(uiTextBox5.Text, "采购价格"),
                    PurchaseDate = uiDatePicker1.Value.Date,
                    WarrantyMonths = ParseNullableInt(uiTextBox6.Text, "保修期限"),
                    ServiceLife = ParseNullableInt(uiTextBox7.Text, "使用年限"),
                    Status = uiComboBox5.SelectedValue as string ?? "Idle",
                    Remarks = uiTextBox8.Text.Trim()
                };

                if (await _manager.IsEquipmentNoExists(eq.EquipmentNo))
                    throw new Exception($"设备编号 '{eq.EquipmentNo}' 已存在，请关闭后重新打开新增窗体");

                if (_manager.Insert(eq))
                {
                    UIMessageBox.ShowSuccess("设备保存成功！");
                    DataSaved?.Invoke();
                    Close();
                }
                else
                {
                    UIMessageBox.ShowError("设备保存失败，请检查数据库连接后重试。");
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError($"保存失败：{ex.Message}");
            }
        }

        private static int? GetNullableComboId(UIComboBox combo)
        {
            if (combo.SelectedValue == null) return null;
            if (combo.SelectedValue is int id && id > 0) return id;
            return null;
        }

        private static decimal? ParseNullableDecimal(string text, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            if (!decimal.TryParse(text.Trim(), out decimal value))
                throw new Exception($"{fieldName}格式不正确");
            if (value < 0)
                throw new Exception($"{fieldName}不能为负数");
            return value;
        }

        private static int? ParseNullableInt(string text, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            if (!int.TryParse(text.Trim(), out int value))
                throw new Exception($"{fieldName}格式不正确");
            if (value < 0)
                throw new Exception($"{fieldName}不能为负数");
            return value;
        }
    }
}
