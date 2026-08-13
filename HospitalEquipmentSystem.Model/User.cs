using System;

namespace HospitalEquipment.Model
{
    /// <summary>
    /// 用户实体：系统登录用户信息
    /// </summary>
    public class User
    {
        /// <summary>用户主键 ID</summary>
        public int UserId { get; set; }

        /// <summary>登录用户名</summary>
        public string Username { get; set; }

        /// <summary>密码哈希值，不保存明文密码</summary>
        public string PasswordHash { get; set; }

        /// <summary>用户真实姓名</summary>
        public string RealName { get; set; }

        /// <summary>角色：如管理员/操作员/普通用户等</summary>
        public string Role { get; set; }

        /// <summary>所属科室 ID（关联 Department）；无科室时为 null</summary>
        public int? DeptId { get; set; }

        /// <summary>联系电话</summary>
        public string Phone { get; set; }

        /// <summary>职称或职务</summary>
        public string Title { get; set; }

        /// <summary>是否启用；停用用户不能登录</summary>
        public bool IsActive { get; set; }

        /// <summary>最后一次登录时间；从未登录时为 null</summary>
        public DateTime? LastLoginAt { get; set; }

        /// <summary>用户记录创建时间</summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string AvatarUrl { get; set; }
    }
}
