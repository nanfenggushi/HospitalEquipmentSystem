using HospitalEquipment.BLL;
using HospitalEquipment.BLL.management;
using HospitalEquipment.Model;
using HospitalEquipment.Model.management;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI.management
{
    public partial class Classification : UIForm
    {
        // 在字段区域声明 Manager
        private CategoryManager categoryManager = new CategoryManager();

        public Classification()
        {
            InitializeComponent();
            // 绑定按钮事件（若 Designer 未设置，可在此手动绑定）
            
        }

        // 窗体加载时加载树
        private void Classification_Load(object sender, EventArgs e)
        {
            LoadCategoryTree();
            // 2. 加载上级分类下拉框（ComboBox）
            LoadParentComboBox();


        }
        /// <summary>
        /// 加载上级分类下拉框
        /// </summary>
        private void LoadParentComboBox()
        {
            // 获取所有分类
            List<Category> allCats = categoryManager.GetAll();

            // 创建一个字典或列表作为下拉框数据源
            // 注意：DropDownList 显示名称，存分类ID
            var items = new List<KeyValuePair<int, string>>();

            // 1. 添加一个“顶级分类”选项，用 -1 代表 null
            items.Add(new KeyValuePair<int, string>(-1, "（顶级分类）"));

            // 2. 按顺序添加所有分类（显示缩进效果便于识别层级）
            foreach (var cat in allCats.OrderBy(c => c.SortOrder))
            {
                // 显示名称，加点前缀表示层级（简单处理）
                string prefix = cat.ParentId == null ? "" : "  ├─ ";
                items.Add(new KeyValuePair<int, string>(cat.CategoryId, prefix + cat.Name));
            }

            // 绑定到 ComboBox
            uiComboBox1.DataSource = null;
            uiComboBox1.DisplayMember = "Value";   // 显示文字
            uiComboBox1.ValueMember = "Key";      // 存储的ID值
            uiComboBox1.DataSource = items;
        }

        /// <summary>
        /// 从数据库加载分类树
        /// </summary>
        private void LoadCategoryTree()
        {
            uiTreeView1.Nodes.Clear();

            // 从BLL获取全部分类(扁平数据)
            List<Category> allCategories = categoryManager.GetAll();

            // 过滤出根节点(ParentId == 0),按SortOrder排序
            var roots = allCategories.Where(c => c.ParentId == null)
                                     .OrderBy(c => c.SortOrder)
                                     .ToList();

            foreach (var root in roots)
            {
                TreeNode node = new TreeNode(root.Name);
                node.Tag = root.CategoryId;          // 存分类ID,方便后续获取
                AddChildNodes(node, allCategories);  // 递归添加子节点
                uiTreeView1.Nodes.Add(node);
            }

            // 展开所有节点
            uiTreeView1.ExpandAll();
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
                AddChildNodes(node, allCategories);  // 继续递归
                parentNode.Nodes.Add(node);
            }
        }

        // 选中节点时触发(可选,用于加载右侧编辑面板)
        private void uiTreeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null) return;

            int categoryId = (int)e.Node.Tag;
            Category category = categoryManager.GetById(categoryId);
            if (category == null) return;

            // 回显基础信息
            uiTextBox1.Text = category.Name;
            uiTextBox2.Text = category.Code ?? "";
            uiTextBox3.Text = category.SortOrder.ToString();
            uiTextBox4.Text = category.Description ?? "";

            // 回显上级分类（下拉框）
            // 如果 ParentId 为 null，选中值为 -1 的项；否则选中对应的 CategoryId
            int selectedValue = category.ParentId ?? -1;
            uiComboBox1.SelectedValue = selectedValue;
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            // 创建一个默认的新分类
            Category newCat = new Category
            {
                Name = "新分类",
                ParentId = null,       // 默认是根分类
                SortOrder = 0,
                Code = "",
                Description = ""
            };

            try
            {
                if (categoryManager.Insert(newCat))
                {
                    UIMessageBox.Show("新增根分类成功！");
                    LoadCategoryTree();  // 刷新树
                }
                else
                {
                    UIMessageBox.Show("新增失败！");
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.Show($"新增出错：{ex.Message}");
            }
        }

        private void uiButton4_Click(object sender, EventArgs e)
        {
            // 1. 确保选中的是树节点
            if (uiTreeView1.SelectedNode == null || uiTreeView1.SelectedNode.Tag == null)
            {
                UIMessageBox.Show("请先在左侧树中选中一个分类！");
                return;
            }

            int id = (int)uiTreeView1.SelectedNode.Tag;
            Category category = categoryManager.GetById(id);
            if (category == null) return;

            // 2. 获取界面输入
            string name = uiTextBox1.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                UIMessageBox.Show("分类名称不能为空！");
                return;
            }

            string code = uiTextBox2.Text.Trim();
            string description = uiTextBox4.Text.Trim();

            // 3. 处理排序号（整数）
            if (!int.TryParse(uiTextBox3.Text.Trim(), out int sortOrder))
            {
                UIMessageBox.Show("排序号请输入有效的数字！");
                return;
            }

            // 4. 处理上级分类（重点）
            int? parentId = null;
            if (uiComboBox1.SelectedValue != null && uiComboBox1.SelectedValue is int)
            {
                int val = (int)uiComboBox1.SelectedValue;
                // 如果选中的是 -1，表示“顶级分类”，赋值为 null
                if (val != -1)
                {
                    parentId = val;
                }
            }

            // 5. 赋值给实体
            category.Name = name;
            category.Code = code;
            category.SortOrder = sortOrder;
            category.Description = description;
            category.ParentId = parentId;

            // 6. 调用 BLL 更新
            try
            {
                if (categoryManager.Update(category))
                {
                    UIMessageBox.Show("保存成功！");
                    LoadCategoryTree();          // 刷新左侧树
                                                 // 刷新后重新选中当前节点（略，可自行扩展）
                }
                else
                {
                    UIMessageBox.Show("保存失败，请检查控制台错误信息。");
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.Show($"保存出错：{ex.Message}");
            }
        }

        private void UiButton3_Click(object sender, EventArgs e)
        {
            if (uiTreeView1.SelectedNode == null || uiTreeView1.SelectedNode.Tag == null) return;

            int id = (int)uiTreeView1.SelectedNode.Tag;
            string name = uiTreeView1.SelectedNode.Text;

            // 注意：这里使用修正后的条件判断（直接判断 bool）
            if (UIMessageBox.Show("确定要删除分类“" + name + "”及其所有子分类吗？", "删除确认", UIStyle.Red))
            {
                try
                {
                    if (categoryManager.Delete(id))
                    {
                        UIMessageBox.Show("删除成功！");
                        LoadCategoryTree();
                        ClearEditFields();
                    }
                }
                catch (Exception ex)
                {
                    UIMessageBox.Show($"删除失败：{ex.Message}");
                }
            }
        }

        private void ClearEditFields()
        {
            uiTextBox1.Clear();
            uiTextBox2.Clear();
            uiTextBox3.Clear();
            uiTextBox4.Clear();
            uiComboBox1.SelectedIndex = -1; // 取消选中
        }

        private void uiButton5_Click(object sender, EventArgs e)
        {

        }
    }
}