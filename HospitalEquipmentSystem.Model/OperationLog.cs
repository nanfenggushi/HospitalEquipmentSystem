using System;

namespace HospitalEquipment.Model
{
    /// <summary>
    /// 操作日志实体：记录用户对系统数据的操作记录
    /// </summary>
    public class OperationLog
    {
        /// <summary>日志主键 ID</summary>
        public long LogId { get; set; }

        /// <summary>操作用户 ID（关联 User）；系统操作为 null</summary>
        public int? UserId { get; set; }

        /// <summary>操作类型：如新增/修改/删除/登录等</summary>
        public string ActionType { get; set; }

        /// <summary>被操作的数据表名</summary>
        public string TargetTable { get; set; }

        /// <summary>被操作记录的主键 ID；无关联记录时为 null</summary>
        public int? TargetId { get; set; }

        /// <summary>操作详情描述</summary>
        public string Detail { get; set; }

        /// <summary>日志记录时间</summary>
        public DateTime CreatedAt { get; set; }
    }
}
