using HospitalEquipment.BLL.management;
using HospitalEquipment.Model.management;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI.management
{
    public partial class Classification : UIForm
    {
        private CategoryManager categoryManager = new CategoryManager();
        private int? currentCategoryId = null;   // 当前选中的分类ID

        public Classification()
        {
            InitializeComponent();
        }

        private async void Classification_Load(object sender, EventArgs e)
        {
            // 绑定按钮事件（如果设计器未绑定，在此统一绑定）
            this.uiButton1.Click += uiButton1_Click;        // 新增根分类
            this.uiButton4.Click += uiButton4_Click;        // 保存
            this.uiButton3.Click += uiButton3_Click;        // 删除
            this.uiButton5.Click += uiButton5_Click;        // 取消

            try
            {
                await LoadCategoryTree();
                await LoadParentComboBox();
            }
            catch (Exception ex)
            {
                UIMessageBox.Show($"加载分类数据失败：{ex.Message}", "错误", UIStyle.Red);
            }
            EnableEdit(false);      // 初始禁用编辑区
            ClearEditFields();      // 清空所有输入
            await UpdateStatistics(null); // 清空统计信息
        }

        #region 数据加载

        /// <summary>
        /// 加载分类树
        /// </summary>
        private async Task LoadCategoryTree()
        {
            uiTreeView1.Nodes.Clear();

            List<Category> allCategories = await categoryManager.GetAll();
            var roots = allCategories.Where(c => c.ParentId == null)
                                     .OrderBy(c => c.SortOrder)
                                     .ToList();

            foreach (var root in roots)
            {
                TreeNode node = new TreeNode(root.Name);
                node.Tag = root.CategoryId;
                AddChildNodes(node, allCategories);
                uiTreeView1.Nodes.Add(node);
            }

            uiTreeView1.ExpandAll();
            currentCategoryId = null;
            ClearEditFields();
            EnableEdit(false);
            await UpdateStatistics(null);
        }

        /// <summary>
        /// 递归添加子节点
        /// </summary>
        private void AddChildNodes(TreeNode parentNode, List<Category> allCategories)
        {
            int parentId = (int)parentNode.Tag;
            var children = allCategories.Where(c => c.ParentId == parentId)
                                        .OrderBy(c => c.SortOrder)
                                        .ToList();

            foreach (var child in children)
            {
                TreeNode node = new TreeNode(child.Name);
                node.Tag = child.CategoryId;
                AddChildNodes(node, allCategories);
                parentNode.Nodes.Add(node);
            }
        }

        /// <summary>
        /// 加载上级分类下拉框
        /// </summary>
        private async Task LoadParentComboBox()
        {
            List<Category> allCats = await categoryManager.GetAll();

            var items = new List<KeyValuePair<int, string>>();
            items.Add(new KeyValuePair<int, string>(-1, "（顶级分类）"));

            foreach (var cat in allCats.OrderBy(c => c.SortOrder))
            {
                string prefix = cat.ParentId == null ? "" : "  ├─ ";
                items.Add(new KeyValuePair<int, string>(cat.CategoryId, prefix + cat.Name));
            }

            uiComboBox1.DataSource = null;
            uiComboBox1.DisplayMember = "Value";
            uiComboBox1.ValueMember = "Key";
            uiComboBox1.DataSource = items;
        }

        #endregion

        #region 编辑区控制

        /// <summary>
        /// 启用/禁用编辑区控件
        /// </summary>
        private void EnableEdit(bool enable)
        {
            uiTextBox1.Enabled = enable;
            uiComboBox1.Enabled = enable;
            uiTextBox2.Enabled = enable;
            uiTextBox3.Enabled = enable;
            uiTextBox4.Enabled = enable;
            uiButton4.Enabled = enable;   // 保存
            uiButton5.Enabled = enable;   // 取消
            uiButton3.Enabled = enable;   // 删除
        }

        /// <summary>
        /// 清空编辑区
        /// </summary>
        private void ClearEditFields()
        {
            uiTextBox1.Clear();
            uiComboBox1.SelectedIndex = -1;
            uiTextBox2.Clear();
            uiTextBox3.Clear();
            uiTextBox4.Clear();
        }

        /// <summary>
        /// 更新统计信息（设备数量、描述）
        /// </summary>
        private async Task UpdateStatistics(Category category)
        {
            if (category == null)
            {
                uiLabel7.Text = "设备数量: 0 台";
                uiLabel8.Text = "含子分类设备: 0 台";
                uiLabel9.Text = "描述: ";
                return;
            }

            int directCount = await categoryManager.GetEquipmentCount(category.CategoryId);
            uiLabel7.Text = $"设备数量: {directCount} 台";

            int totalWithChildren = await GetEquipmentCountWithChildren(category.CategoryId);
            uiLabel8.Text = $"含子分类设备: {totalWithChildren} 台";

            uiLabel9.Text = $"描述: {category.Description ?? ""}";
        }

        /// <summary>
        /// 递归计算分类及其所有子分类下的设备总数
        /// </summary>
        private async Task<int> GetEquipmentCountWithChildren(int categoryId)
        {
            int count = await categoryManager.GetEquipmentCount(categoryId);
            var allCats = await categoryManager.GetAll();
            var children = allCats.Where(c => c.ParentId == categoryId).ToList();
            foreach (var child in children)
                count += await GetEquipmentCountWithChildren(child.CategoryId);
            return count;
        }

        #endregion

        #region 树节点事件

        private async void uiTreeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node == null || e.Node.Tag == null)
                {
                    currentCategoryId = null;
                    ClearEditFields();
                    EnableEdit(false);
                    await UpdateStatistics(null);
                    return;
                }

                int categoryId = (int)e.Node.Tag;
                Category category = await categoryManager.GetById(categoryId);
                if (category == null)
                {
                    currentCategoryId = null;
                    ClearEditFields();
                    EnableEdit(false);
                    await UpdateStatistics(null);
                    return;
                }

                currentCategoryId = categoryId;

                // 回显数据
                uiTextBox1.Text = category.Name;
                uiTextBox2.Text = category.Code ?? "";
                uiTextBox3.Text = category.SortOrder.ToString();
                uiTextBox4.Text = category.Description ?? "";
                int selectedValue = category.ParentId ?? -1;
                uiComboBox1.SelectedValue = selectedValue;

                EnableEdit(true);
                await UpdateStatistics(category);
            }
            catch (Exception ex)
            {
                UIMessageBox.Show($"加载分类信息失败：{ex.Message}", "错误", UIStyle.Red);
            }
        }

        #endregion

        #region 按钮事件

        /// <summary>
        /// 新增根分类
        /// </summary>
        private async void uiButton1_Click(object sender, EventArgs e)
        {
            Category newCat = new Category
            {
                Name = "新分类",
                ParentId = null,
                SortOrder = 0,
                Code = "",
                Description = ""
            };

            try
            {
                if (await categoryManager.Insert(newCat))
                {
                    UIMessageBox.Show("新增根分类成功！", "提示", UIStyle.Green);
                    await LoadCategoryTree();
                    await LoadParentComboBox();
                    // 自动选中新添加的分类（根据ID查找）
                    SelectNodeById(newCat.CategoryId);
                }
                else
                {
                    UIMessageBox.Show("新增失败！", "错误", UIStyle.Red);
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.Show($"新增出错：{ex.Message}", "错误", UIStyle.Red);
            }
        }

        /// <summary>
        /// 保存分类
        /// </summary>
        private async void uiButton4_Click(object sender, EventArgs e)
        {
            if (!currentCategoryId.HasValue)
            {
                UIMessageBox.Show("请先在左侧树中选中一个分类！", "提示", UIStyle.Green);
                return;
            }

            try
            {
                Category category = await categoryManager.GetById(currentCategoryId.Value);
                if (category == null)
                {
                    UIMessageBox.Show("分类不存在，请刷新后重试！", "错误", UIStyle.Red);
                    return;
                }

                string name = uiTextBox1.Text.Trim();
                if (string.IsNullOrEmpty(name))
                {
                    UIMessageBox.Show("分类名称不能为空！", "提示", UIStyle.Green);
                    uiTextBox1.Focus();
                    return;
                }

                string code = uiTextBox2.Text.Trim();
                string description = uiTextBox4.Text.Trim();

                if (!int.TryParse(uiTextBox3.Text.Trim(), out int sortOrder))
                {
                    UIMessageBox.Show("排序号请输入有效的数字！", "提示", UIStyle.Green);
                    uiTextBox3.Focus();
                    return;
                }

                int? parentId = null;
                if (uiComboBox1.SelectedValue != null && uiComboBox1.SelectedValue is int)
                {
                    int val = (int)uiComboBox1.SelectedValue;
                    if (val != -1) parentId = val;
                }

                // 防止将分类设为自己的子分类（循环引用）
                if (parentId.HasValue && parentId.Value == category.CategoryId)
                {
                    UIMessageBox.Show("不能将分类设置为其自身的子分类！", "提示", UIStyle.Green);
                    return;
                }

                category.Name = name;
                category.Code = code;
                category.SortOrder = sortOrder;
                category.Description = description;
                category.ParentId = parentId;

                if (await categoryManager.Update(category))
                {
                    UIMessageBox.Show("保存成功！", "提示", UIStyle.Green);
                    await LoadCategoryTree();
                    await LoadParentComboBox();
                    SelectNodeById(category.CategoryId); // 重新选中当前分类
                }
                else
                {
                    UIMessageBox.Show("保存失败，请检查控制台错误信息。", "错误", UIStyle.Red);
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.Show($"保存出错：{ex.Message}", "错误", UIStyle.Red);
            }
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        private async void uiButton3_Click(object sender, EventArgs e)
        {
            if (!currentCategoryId.HasValue)
            {
                UIMessageBox.Show("请先在左侧树中选中一个分类！", "提示", UIStyle.Green);
                return;
            }

            string name = uiTreeView1.SelectedNode?.Text ?? "当前分类";

            if (UIMessageBox.Show($"确定要删除分类“{name}”及其所有子分类吗？", "删除确认", UIStyle.Red))
            {
                try
                {
                    if (await categoryManager.Delete(currentCategoryId.Value))
                    {
                        UIMessageBox.Show("删除成功！", "提示", UIStyle.Green);
                        currentCategoryId = null;
                        ClearEditFields();
                        EnableEdit(false);
                        await UpdateStatistics(null);
                        await LoadCategoryTree();
                        await LoadParentComboBox();
                    }
                }
                catch (Exception ex)
                {
                    UIMessageBox.Show($"删除失败：{ex.Message}", "错误", UIStyle.Red);
                }
            }
        }

        /// <summary>
        /// 取消编辑（清空并禁用编辑区）
        /// </summary>
        private async void uiButton5_Click(object sender, EventArgs e)
        {
            ClearEditFields();
            EnableEdit(false);
            uiTreeView1.SelectedNode = null;
            currentCategoryId = null;
            await UpdateStatistics(null);
        }
        #endregion

        #region 辅助方法

        /// <summary>
        /// 根据分类ID在树中查找并选中节点
        /// </summary>
        private void SelectNodeById(int categoryId)
        {
            foreach (TreeNode node in uiTreeView1.Nodes)
            {
                TreeNode found = FindNodeByTag(node, categoryId);
                if (found != null)
                {
                    uiTreeView1.SelectedNode = found;
                    found.EnsureVisible();
                    break;
                }
            }
        }

        private TreeNode FindNodeByTag(TreeNode parent, int tagValue)
        {
            if (parent.Tag != null && (int)parent.Tag == tagValue)
                return parent;

            foreach (TreeNode child in parent.Nodes)
            {
                TreeNode result = FindNodeByTag(child, tagValue);
                if (result != null)
                    return result;
            }
            return null;
        }

        #endregion

      
    }
}
