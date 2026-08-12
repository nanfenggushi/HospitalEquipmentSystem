using HospitalEquipment.Model;
using System;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI.management
{
    public partial class MaterialEditForm : Form
    {
        public Material Material { get; private set; }

        public MaterialEditForm()
        {
            InitializeComponent();
            Material = new Material { IsActive = true, DefaultQuantity = 1 };
            LoadCategories();
        }

        public MaterialEditForm(Material material)
        {
            InitializeComponent();
            Material = material;
            LoadCategories();
            PopulateFields();
        }

        private void LoadCategories()
        {
            try
            {
                var dt = HospitalEquipmentSystem.Common.DbHelper.GetDataTable(
                    "SELECT CategoryId, CategoryName FROM EquipmentCategories ORDER BY CategoryName");
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "CategoryId";
                cmbCategory.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载设备分类失败：{ex.Message}", "错误");
            }
        }

        private void PopulateFields()
        {
            txtName.Text = Material.MaterialName ?? "";
            cmbCategory.SelectedValue = Material.CategoryId;
            cmbFaultType.Text = Material.FaultType ?? "";
            txtUnitPrice.Text = Material.UnitPrice > 0 ? Material.UnitPrice.ToString("F2") : "";
            txtDefaultQuantity.Text = Material.DefaultQuantity > 0 ? Material.DefaultQuantity.ToString() : "1";
            txtUnit.Text = Material.Unit ?? "";
            txtDescription.Text = Material.Description ?? "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("物料名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }

                string faultType = cmbFaultType.Text.Trim();
                if (string.IsNullOrWhiteSpace(faultType))
                {
                    MessageBox.Show("故障类型不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbFaultType.Focus();
                    return;
                }

                if (!decimal.TryParse(txtUnitPrice.Text.Trim(), out decimal unitPrice) || unitPrice <= 0)
                {
                    MessageBox.Show("请输入有效的单价（大于 0）！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUnitPrice.Focus();
                    return;
                }

                if (!int.TryParse(txtDefaultQuantity.Text.Trim(), out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("请输入有效的默认数量（大于 0）！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDefaultQuantity.Focus();
                    return;
                }

                string unit = txtUnit.Text.Trim();
                if (string.IsNullOrWhiteSpace(unit))
                {
                    MessageBox.Show("计量单位不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUnit.Focus();
                    return;
                }

                Material.MaterialName = name;
                Material.FaultType = faultType;
                Material.UnitPrice = unitPrice;
                Material.DefaultQuantity = quantity;
                Material.Unit = unit;
                Material.Description = txtDescription.Text.Trim();
                Material.CategoryId = cmbCategory.SelectedValue != null ? Convert.ToInt32(cmbCategory.SelectedValue) : 0;

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
