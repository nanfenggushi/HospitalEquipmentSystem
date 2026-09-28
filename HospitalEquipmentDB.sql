/*
 Navicat Premium Dump SQL

 Source Server         : 阿里云
 Source Server Type    : SQL Server
 Source Server Version : 16004265 (16.00.4265)
 Source Host           : 8.163.70.157:1433
 Source Catalog        : HospitalEquipmentDB
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 16004265 (16.00.4265)
 File Encoding         : 65001

 Date: 16/08/2026 13:58:24
*/
/*
============================================================================
 本文件用途说明（注释由 Claude 补充，非原始导出内容）
============================================================================
 这是"医院医疗设备管理系统"（HospitalEquipmentSystem）数据库的完整导出脚本，
 由 Navicat Premium 从阿里云 SQL Server 实例的 HospitalEquipmentDB 库导出。

 脚本内容包含（按出现顺序）：
   1. 13 张业务表的 DROP + CREATE TABLE 语句（表结构定义）
   2. 每张表对应的历史数据 INSERT 语句
   3. 1 个统计视图 v_CategoryUsageRate
   4. 各表的主键(PK)、唯一约束(UQ)、普通索引(IX)、检查约束(CK)
   5. 自增列当前值重置语句 DBCC CHECKIDENT
   6. 表之间的外键约束(FK)，定义在文件末尾（因为要等所有表都建好才能建外键）

 业务领域概览：
   - Departments          科室字典表
   - Users                系统用户/员工表（医生、护士、维修工程师、管理员）
   - Suppliers            设备供应商字典表
   - EquipmentCategories  设备分类字典表（支持父子级联分类）
   - Equipment            设备主表（医院所有医疗设备台账）
   - InboundRecords       设备入库记录（采购入库流水）
   - BorrowRecords        设备科室间借用记录（借用申请-审批-归还全流程）
   - BorrowNoCounter      借用单号按月自增计数器（用于生成 B-YYYY-MM-NNN 编号）
   - MaintenanceRecords   设备报修/维修工单主表
   - Materials            维修备件/耗材字典表
   - MaintenanceMaterials 维修工单实际使用的备件明细表（工单与备件的关联表）
   - DeptRevenue          科室营收月度统计表
   - OperationLogs        系统操作审计日志表
   - SmsCodes             短信验证码表（登录/找回密码用）

 表间关系简述：
   Equipment --(N:1)--> Suppliers / EquipmentCategories / Departments / Users(责任人)
   BorrowRecords --(N:1)--> Equipment / Users(申请人、审批人) / Departments
   MaintenanceRecords --(N:1)--> Equipment / Users(报修人、维修人) / Departments
   MaintenanceMaterials --(N:1)--> MaintenanceRecords / Materials
   InboundRecords --(N:1)--> Equipment / Users(经办人)
   DeptRevenue --(N:1)--> Departments
   OperationLogs --(N:1)--> Users
============================================================================
*/



-- ----------------------------
-- Table structure for BorrowNoCounter
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[BorrowNoCounter]') AND type IN ('U'))
	DROP TABLE [dbo].[BorrowNoCounter]
GO

-- 借用单号生成计数器：按“年-月”维度记录当月已分配到的最大流水号，
-- 用于拼接生成形如 B-2026-08-005 的借用单号（BorrowNo），避免并发下单号重复。
CREATE TABLE [dbo].[BorrowNoCounter] (
  [YearMonth] char(7) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,  -- 年月，格式 YYYY-MM，作为主键
  [LastSeq] int  NOT NULL  -- 该月已使用到的最大流水序号，生成新单号时取值 +1
)
GO

ALTER TABLE [dbo].[BorrowNoCounter] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of BorrowNoCounter
-- ----------------------------
INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2023-01', N'15')
GO

INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2023-04', N'12')
GO

INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2023-09', N'9')
GO

INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2023-11', N'22')
GO

INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2024-02', N'18')
GO

INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2024-05', N'8')
GO

INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2024-09', N'25')
GO

INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2024-10', N'22')
GO

INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2024-11', N'35')
GO

INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2025-04', N'12')
GO

INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2025-07', N'8')
GO

INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2025-08', N'31')
GO

INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2025-09', N'12')
GO

INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2025-12', N'42')
GO

INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2026-02', N'4')
GO

INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2026-03', N'18')
GO

INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2026-04', N'8')
GO

INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2026-05', N'1')
GO

INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2026-06', N'2')
GO

INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2026-07', N'7')
GO

INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2026-08', N'8')
GO

INSERT INTO [dbo].[BorrowNoCounter] ([YearMonth], [LastSeq]) VALUES (N'2027-02', N'3')
GO


-- ----------------------------
-- Table structure for BorrowRecords
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[BorrowRecords]') AND type IN ('U'))
	DROP TABLE [dbo].[BorrowRecords]
GO

-- 设备借用记录主表：记录科室之间“借用申请 -> 审批 -> 使用 -> 归还/逾期”的完整生命周期。
-- Status 状态机通常为：Pending(待审批) -> Approved(已批准)/Rejected(已拒绝)
--                     -> Returned(已归还)/Overdue(已逾期未还)。
CREATE TABLE [dbo].[BorrowRecords] (
  [BorrowId] int  IDENTITY(1,1) NOT NULL,  -- 借用记录主键，自增
  [BorrowNo] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,  -- 借用单号（业务编号），唯一，如 B-2026-08-005
  [EquipmentId] int  NOT NULL,  -- 被借用的设备ID，外键 -> Equipment.EquipmentId
  [ApplicantId] int  NOT NULL,  -- 申请人用户ID，外键 -> Users.UserId
  [ApplicantDeptId] int  NULL,  -- 申请科室ID，外键 -> Departments.DeptId
  [Purpose] nvarchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 借用用途/事由说明
  [ExpectedReturnDate] date  NOT NULL,  -- 预计归还日期
  [ApproverId] int  NULL,  -- 审批人用户ID，外键 -> Users.UserId，未审批时为空
  [ApproveDate] datetime  NULL,  -- 审批时间
  [Status] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'Pending' NOT NULL,  -- 借用状态：Pending/Approved/Rejected/Returned/Overdue，默认 Pending
  [ActualReturnDate] date  NULL,  -- 实际归还日期
  [ReturnNote] nvarchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 归还时的备注说明（如设备状况）
  [Remarks] nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 其他备注信息
  [CreatedAt] datetime DEFAULT getdate() NOT NULL,  -- 记录创建时间，默认当前时间
  [UpdatedAt] datetime  NULL  -- 记录最后更新时间
)
GO

ALTER TABLE [dbo].[BorrowRecords] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of BorrowRecords
-- ----------------------------
SET IDENTITY_INSERT [dbo].[BorrowRecords] ON
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'1', N'B-2026-07-001', N'5', N'3', N'2', N'急诊科夜间值班急需生化检测设备', N'2026-08-03', N'2', N'2026-07-28 09:00:00.000', N'Overdue', NULL, NULL, NULL, N'2026-07-30 07:23:47.883', N'2026-08-04 01:14:36.320')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'2', N'B-2026-07-002', N'7', N'4', N'3', N'ICU临时增加床位需要备用呼吸机', N'2026-07-30', N'2', N'2026-07-25 10:30:00.000', N'Returned', N'2026-07-28', N'设备已归还，运行正常', NULL, N'2026-07-30 07:23:47.883', NULL)
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'3', N'B-2026-07-003', N'16', N'4', N'3', N'ICU监护仪不够用，临时借用', N'2026-08-10', N'2', N'2026-07-26 14:00:00.000', N'Overdue', NULL, NULL, NULL, N'2026-07-30 07:23:47.883', N'2026-08-11 01:05:55.400')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'4', N'B-2026-06-001', N'3', N'3', N'2', N'急诊科抢救室需要心电监护', N'2026-06-25', N'2', N'2026-06-18 09:00:00.000', N'Returned', N'2026-06-24', N'使用完毕，已归还', NULL, N'2026-07-30 07:23:47.883', NULL)
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'5', N'B-2026-06-002', N'19', N'4', N'3', N'ICU患者增加需注射泵', N'2026-06-30', N'2', N'2026-06-20 11:00:00.000', N'Returned', N'2026-06-29', N'已归还', NULL, N'2026-07-30 07:23:47.883', NULL)
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'6', N'B-2026-07-004', N'13', N'2', N'7', N'手术室临时加台需要备用手术床', N'2026-07-31', N'1', N'2026-07-27 08:00:00.000', N'Overdue', NULL, NULL, NULL, N'2026-07-30 07:23:47.883', N'2026-08-03 01:07:36.990')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'7', N'B-2026-05-001', N'17', N'3', N'2', N'急诊科除颤仪送修期间临时使用', N'2026-05-20', N'2', N'2026-05-10 09:30:00.000', N'Returned', N'2026-05-19', N'已归还，设备完好', NULL, N'2026-07-30 07:23:47.883', NULL)
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'8', N'B-2026-07-005', N'20', N'3', N'2', N'急诊科洗胃机故障，临时借用备用', N'2026-07-28', N'2', N'2026-08-03 03:45:10.290', N'Overdue', NULL, NULL, NULL, N'2026-07-30 07:23:47.883', N'2026-08-03 05:58:43.947')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'9', N'B-2026-07-006', N'8', N'2', N'1', N'111', N'2026-08-07', N'2', N'2026-07-31 08:07:04.820', N'Returned', N'2026-07-31', N'', N'222', N'2026-07-31 07:50:29.287', N'2026-07-31 08:13:30.160')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'10', N'B-2026-07-007', N'7', N'2', N'1', N'我要用', N'2026-08-07', N'2', N'2026-07-31 08:06:54.063', N'Rejected', NULL, NULL, N'好用', N'2026-07-31 07:55:59.320', N'2026-07-31 08:06:54.063')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'11', N'B-2026-08-001', N'13', N'2', N'1', N'病人手术或缺手术台', N'2026-08-18', N'2', N'2026-08-03 01:43:17.763', N'Approved', NULL, NULL, N'需长期使用2周', N'2026-08-03 01:42:30.253', N'2026-08-03 01:43:17.763')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'12', N'B-2026-08-002', N'8', N'3', N'2', N'有用你被问', N'2026-08-12', NULL, NULL, N'Pending', NULL, NULL, N'', N'2026-08-05 05:56:48.047', N'2026-08-05 05:56:48.047')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'13', N'B-2026-08-003', N'21', N'11', N'6', N'电死你啊', N'2026-08-20', N'11', N'2026-08-13 08:29:54.387', N'Returned', N'2026-08-14', N'', N'国家电网合作对象', N'2026-08-13 08:29:32.647', N'2026-08-14 01:40:08.070')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'14', N'B-2026-08-004', N'23', N'11', N'6', N'123', N'2026-08-13', N'11', N'2026-08-13 10:28:53.523', N'Returned', N'2026-08-13', N'', N'', N'2026-08-13 10:28:37.770', N'2026-08-13 10:29:05.777')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'15', N'B-2023-01-001', N'34', N'5', N'11', N'病房心电图普查', N'2023-01-15', N'2', N'2023-01-10 09:00:00.000', N'Returned', N'2023-01-14', N'按时归还', NULL, N'2023-01-09 15:30:00.000', N'2023-01-14 16:00:00.000')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'16', N'B-2024-05-001', N'33', N'4', N'9', N'骨科手术室急需牵引床', N'2024-05-20', N'2', N'2024-05-18 10:00:00.000', N'Returned', N'2024-05-21', N'延迟一天归还，已说明情况', N'手术延期', N'2024-05-17 08:20:00.000', N'2024-05-21 09:10:00.000')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'17', N'B-2025-09-001', N'6', N'6', N'2', N'急救演练借用除颤仪', N'2025-09-10', N'3', N'2025-09-08 14:00:00.000', N'Returned', N'2025-09-10', N'演练结束，设备完好', NULL, N'2025-09-07 11:00:00.000', N'2025-09-10 17:30:00.000')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'18', N'B-2027-02-001', N'38', N'3', N'2', N'救护车拉练储备', N'2027-02-28', N'2', N'2027-02-20 09:30:00.000', N'Pending', NULL, NULL, N'提前申请审批', N'2027-02-19 16:45:00.000', NULL)
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'19', N'B-2027-04-002', N'35', N'4', N'10', N'门诊疑难杂症会诊使用', N'2027-04-15', N'1', N'2027-04-10 08:15:00.000', N'Approved', NULL, NULL, NULL, N'2027-04-09 10:20:00.000', N'2027-04-10 08:15:00.000')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'20001', N'B-2023-04-012', N'20004', N'2', N'10002', N'儿科病房临时转运疫苗保冷', N'2023-04-20', N'1', N'2023-04-18 09:00:00.000', N'Returned', N'2023-04-19', N'转运完毕', NULL, N'2023-04-18 08:30:00.000', N'2023-04-19 16:00:00.000')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'20002', N'B-2024-09-025', N'20004', N'4', N'10001', N'ICU急需特种药品冷链暂存', N'2024-09-30', N'3', N'2024-09-15 10:00:00.000', N'Returned', N'2024-09-28', N'清洗消毒后归还药库', N'设备运转正常', N'2024-09-15 08:20:00.000', N'2024-09-28 09:10:00.000')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'30001', N'B-2024-02-018', N'30001', N'5', N'10001', N'ICU突发紧急气管插管需麻醉支持', N'2024-02-20', N'1', N'2024-02-18 09:00:00.000', N'Returned', N'2024-02-19', N'抢救成功，设备清洁后归还', NULL, N'2024-02-18 08:30:00.000', N'2024-02-19 16:00:00.000')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'30002', N'B-2025-08-031', N'30002', N'2', N'10002', N'儿科急诊腹腔镜探查术借用镜头', N'2025-08-16', N'2', N'2025-08-15 10:00:00.000', N'Returned', N'2025-08-16', N'已交接CSSD清洗', N'跨科室调配', N'2025-08-15 08:20:00.000', N'2025-08-16 09:10:00.000')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'40001', N'B-2023-11-015', N'40001', N'1', N'10001', N'ICU除颤仪保养，临时调用急诊备用机', N'2023-11-20', N'2', N'2023-11-18 09:00:00.000', N'Returned', N'2023-11-20', N'设备正常归还', N'生命支持类调配', N'2023-11-18 08:30:00.000', N'2023-11-20 16:00:00.000')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'40002', N'B-2024-10-022', N'40002', N'3', N'30001', N'大型复合手术备用心肺复苏机', N'2024-10-16', N'4', N'2024-10-15 10:00:00.000', N'Returned', N'2024-10-16', N'未使用，原样归还', NULL, N'2024-10-15 08:20:00.000', N'2024-10-16 09:10:00.000')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'50001', N'B-2023-09-009', N'50002', N'5', N'10003', N'骨科病房VIP患者术后床旁理疗借用', N'2023-09-15', N'2', N'2023-09-10 09:00:00.000', N'Returned', N'2023-09-14', N'疗程结束，设备完好归还', N'跨科室理疗支持', N'2023-09-10 08:30:00.000', N'2023-09-14 16:00:00.000')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'50002', N'B-2025-04-012', N'50004', N'1', N'50002', N'影像科网络升级临时调用备用交换机做级联', N'2025-04-30', N'4', N'2025-04-20 10:00:00.000', N'Returned', N'2025-04-28', N'网络改造完成，设备已撤回', NULL, N'2025-04-20 08:20:00.000', N'2025-04-28 09:10:00.000')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'70001', N'B-2026-03-015', N'40002', N'1', N'70001', N'影像科磁共振患者突发心梗，紧急调配心肺复苏机', N'2026-03-05', N'2', N'2026-03-04 14:00:00.000', N'Returned', N'2026-03-05', N'抢救结束，设备清洁消毒后归还', N'跨科室生命通道支持', N'2026-03-04 13:55:00.000', N'2026-03-05 10:00:00.000')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'70002', N'B-2026-08-005', N'50002', N'3', N'60001', N'检验科操作员手腕劳损，借用气压弹道冲击波治疗仪理疗', N'2026-08-20', N'4', N'2026-08-14 09:00:00.000', N'Borrowed', NULL, NULL, N'职工关怀用途', N'2026-08-14 08:45:00.000', NULL)
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'80001', N'B-2026-04-008', N'80002', N'2', N'40001', N'急诊科抢救大批一氧化碳中毒患者，急调ICU高档呼吸机', N'2026-04-10', N'1', N'2026-04-02 20:30:00.000', N'Returned', N'2026-04-08', N'患者病情稳定脱机，设备消毒后归还', N'跨科室急救调配', N'2026-04-02 20:15:00.000', N'2026-04-08 14:00:00.000')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'80002', N'B-2026-08-012', N'80003', N'3', N'40001', N'急诊抢救室爆发性心肌炎患者急需ECMO上机支持（V-A模式）', N'2026-08-25', N'4', N'2026-08-12 11:00:00.000', N'Borrowed', NULL, NULL, N'顶级生命支持设备跨科室调配', N'2026-08-12 10:45:00.000', NULL)
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'100001', N'B-2026-02-004', N'100003', N'2', N'80002', N'ICU床旁重症超声引导下深静脉穿刺', N'2026-02-10', N'1', N'2026-02-09 08:30:00.000', N'Returned', N'2026-02-09', N'穿刺顺利结束，探头消毒完毕后归还', N'生命通道建立', N'2026-02-09 08:15:00.000', N'2026-02-09 11:00:00.000')
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'100002', N'B-2026-08-018', N'100003', N'4', N'40001', N'急诊科送来大批外伤患者，需紧急床旁FAST(创伤重点超声)评估', N'2026-08-15', N'3', N'2026-08-14 09:30:00.000', N'Borrowed', NULL, NULL, N'急救黄金期调用', N'2026-08-14 09:20:00.000', NULL)
GO

INSERT INTO [dbo].[BorrowRecords] ([BorrowId], [BorrowNo], [EquipmentId], [ApplicantId], [ApplicantDeptId], [Purpose], [ExpectedReturnDate], [ApproverId], [ApproveDate], [Status], [ActualReturnDate], [ReturnNote], [Remarks], [CreatedAt], [UpdatedAt]) VALUES (N'100003', N'B-2026-08-008', N'23', N'11', N'6', N'123', N'2026-08-14', N'11', N'2026-08-14 06:51:10.517', N'Overdue', NULL, NULL, N'', N'2026-08-14 06:50:51.320', N'2026-08-15 15:24:55.053')
GO

SET IDENTITY_INSERT [dbo].[BorrowRecords] OFF
GO


-- ----------------------------
-- Table structure for Departments
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Departments]') AND type IN ('U'))
	DROP TABLE [dbo].[Departments]
GO

-- 科室字典表：医院内部各临床/行政科室的基础信息，供 Users、Equipment 等表通过 DeptId 关联引用。
CREATE TABLE [dbo].[Departments] (
  [DeptId] int  IDENTITY(1,1) NOT NULL,  -- 科室主键，自增
  [DeptName] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,  -- 科室名称，如“急诊科”“ICU”
  [DeptCode] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 科室编码
  [Location] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 科室所在位置/楼层
  [Phone] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 科室联系电话
  [IsActive] bit DEFAULT 1 NOT NULL,  -- 是否启用，1=启用 0=停用，默认1
  [CreatedAt] datetime DEFAULT getdate() NOT NULL  -- 创建时间，默认当前时间
)
GO

ALTER TABLE [dbo].[Departments] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Departments
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Departments] ON
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'1', N'设备科', N'Dept-001', N'行政楼3层', N'1001', N'1', N'2026-07-30 07:23:45.440')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'2', N'急诊科', N'Dept-002', N'急诊楼1层', N'2001', N'1', N'2026-07-30 07:23:45.440')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'3', N'ICU', N'Dept-003', N'住院楼5层', N'3001', N'1', N'2026-07-30 07:23:45.440')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'4', N'放射科', N'Dept-004', N'医技楼2层', N'4001', N'1', N'2026-07-30 07:23:45.440')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'5', N'检验科', N'Dept-005', N'医技楼3层', N'5001', N'1', N'2026-07-30 07:23:45.440')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'6', N'妇产科', N'Dept-006', N'住院楼8层', N'6001', N'1', N'2026-07-30 07:23:45.440')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'7', N'手术室', N'Dept-007', N'住院楼4层', N'7001', N'1', N'2026-07-30 07:23:45.440')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'8', N'康复科', N'Dept-008', N'康复楼1层', N'8001', N'1', N'2022-03-15 09:30:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'9', N'骨科', N'Dept-009', N'住院楼6层', N'9001', N'1', N'2022-05-20 10:15:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'10', N'神经内科', N'Dept-010', N'住院楼7层', N'10001', N'1', N'2023-01-10 08:00:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'11', N'心血管内科', N'Dept-011', N'住院楼9层', N'11001', N'1', N'2023-08-12 14:20:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'12', N'门诊部', N'Dept-012', N'门诊楼1-3层', N'12001', N'1', N'2024-02-28 09:00:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'10001', N'重症医学科(ICU)', N'Dept-101', N'外科楼3层', N'10101', N'1', N'2022-01-15 08:30:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'10002', N'儿科', N'Dept-102', N'门诊楼4层', N'10201', N'1', N'2022-02-20 09:15:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'10003', N'放射影像科', N'Dept-103', N'医技楼1层', N'10301', N'1', N'2023-05-10 10:00:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'10004', N'超声科', N'Dept-104', N'医技楼2层', N'10401', N'1', N'2023-08-12 14:20:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'20001', N'检验科', N'Dept-201', N'医技楼3层', N'20101', N'1', N'2022-04-10 08:30:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'20002', N'药剂科', N'Dept-202', N'门诊楼1层', N'20201', N'1', N'2022-04-12 09:15:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'30001', N'手术室', N'Dept-301', N'外科楼8层', N'30101', N'1', N'2022-01-05 08:00:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'30002', N'消毒供应中心', N'Dept-302', N'后勤楼2层', N'30201', N'1', N'2022-01-10 09:15:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'40001', N'急诊科', N'Dept-401', N'门诊楼1层(急诊通道)', N'40101', N'1', N'2022-02-01 08:00:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'40002', N'血液净化中心', N'Dept-402', N'内科楼5层', N'40201', N'1', N'2022-03-10 09:00:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'50001', N'康复医学科', N'Dept-501', N'康复楼2-3层', N'50101', N'1', N'2022-06-15 09:00:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'50002', N'信息中心', N'Dept-502', N'行政楼4层', N'50201', N'1', N'2022-01-10 08:30:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'60001', N'医学检验科', N'Dept-601', N'医技楼2层', N'60101', N'1', N'2022-01-01 08:00:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'70001', N'医学影像科', N'Dept-701', N'医技楼1层', N'70101', N'1', N'2022-01-01 08:00:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'80001', N'手术室', N'Dept-801', N'外科楼6-8层', N'80101', N'1', N'2022-01-05 08:00:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'80002', N'重症医学科(ICU)', N'Dept-802', N'外科楼5层', N'80201', N'1', N'2022-01-05 08:00:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'90001', N'检验科', N'Dept-901', N'医技楼3层', N'90101', N'1', N'2022-01-10 08:00:00.000')
GO

INSERT INTO [dbo].[Departments] ([DeptId], [DeptName], [DeptCode], [Location], [Phone], [IsActive], [CreatedAt]) VALUES (N'100001', N'超声医学科', N'Dept-1001', N'门诊楼3层', N'100101', N'1', N'2022-02-01 08:00:00.000')
GO

SET IDENTITY_INSERT [dbo].[Departments] OFF
GO


-- ----------------------------
-- Table structure for DeptRevenue
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DeptRevenue]') AND type IN ('U'))
	DROP TABLE [dbo].[DeptRevenue]
GO

-- 科室营收统计表：按“科室 + 月份(Period)”维度汇总的营收金额，用于财务/绩效报表分析。
CREATE TABLE [dbo].[DeptRevenue] (
  [RevenueId] int  IDENTITY(1,1) NOT NULL,  -- 营收记录主键，自增
  [DeptId] int  NOT NULL,  -- 所属科室ID，外键 -> Departments.DeptId
  [Period] char(7) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,  -- 统计周期（年月），格式 YYYY-MM
  [Amount] decimal(18,2) DEFAULT 0 NOT NULL,  -- 该科室当月营收金额，默认0
  [Remark] nvarchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 备注说明
  [CreatedAt] datetime2(0) DEFAULT sysdatetime() NOT NULL  -- 记录创建时间，使用 datetime2 精度，默认当前时间
)
GO

ALTER TABLE [dbo].[DeptRevenue] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of DeptRevenue
-- ----------------------------
SET IDENTITY_INSERT [dbo].[DeptRevenue] ON
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'2', N'7', N'2026-08', N'650000.00', N'', N'2026-08-10 03:36:50')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'3', N'5', N'2026-08', N'89000.00', N'', N'2026-08-10 03:55:34')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'4', N'3', N'2026-08', N'450000.00', N'', N'2026-08-10 03:55:55')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'5', N'4', N'2026-08', N'89000.00', N'x光穿刺胃部', N'2026-08-10 03:56:20')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'7', N'7', N'2026-07', N'520000.00', N'', N'2026-08-14 01:43:45')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'8', N'3', N'2026-07', N'234456.00', N'', N'2026-08-14 01:44:23')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'9', N'4', N'2026-07', N'75400.00', N'', N'2026-08-14 01:46:05')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'10', N'8', N'2023-12', N'320000.00', N'年底康复科结算', N'2024-01-05 09:00:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'11', N'9', N'2024-06', N'850000.00', N'骨科手术收入峰值', N'2024-07-02 10:30:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'12', N'10', N'2025-03', N'420000.00', N'神经内科一季度核算', N'2025-04-05 14:15:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'13', N'11', N'2025-11', N'960000.00', N'心血管介入治疗收入', N'2025-12-03 08:45:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'20001', N'20001', N'2024-08', N'1850000.00', N'体检高峰期检验科收入', N'2024-09-05 09:00:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'20002', N'20001', N'2025-02', N'1320000.00', N'春节后常规化验结算', N'2025-03-02 10:30:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'20003', N'20002', N'2025-11', N'4500000.00', N'药剂科处方药综合流水', N'2025-12-05 14:15:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'30001', N'30001', N'2024-07', N'3500000.00', N'暑期手术高峰期收入', N'2024-08-05 09:00:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'30002', N'30001', N'2025-12', N'2800000.00', N'年底手术结算', N'2026-01-05 10:30:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'30003', N'30002', N'2025-06', N'450000.00', N'CSSD器械消毒服务内部核算', N'2025-07-02 14:15:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'40001', N'40001', N'2024-12', N'2150000.00', N'冬季心脑血管急症高发期急诊收入', N'2025-01-05 09:00:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'40002', N'40002', N'2025-06', N'3450000.00', N'透析中心半年度常规结算', N'2025-07-02 10:30:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'50001', N'50001', N'2024-10', N'1150000.00', N'秋季康复疗程结算高峰', N'2024-11-05 09:00:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'50002', N'50001', N'2025-05', N'980000.00', N'常规康复门诊及住院收入', N'2025-06-02 10:30:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'60001', N'60001', N'2026-01', N'5200000.00', N'2026年首月常规体检及门诊检验收入', N'2026-02-05 09:00:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'60002', N'60001', N'2026-02', N'4800000.00', N'春节期间急诊检验及住院排查收入', N'2026-03-03 10:00:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'60003', N'60001', N'2026-03', N'5500000.00', N'春季流感高发期发热门诊检验增量', N'2026-04-05 09:30:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'60004', N'60001', N'2026-04', N'5100000.00', N'2026年4月常规医保结算', N'2026-05-06 08:45:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'60005', N'40001', N'2026-05', N'2350000.00', N'五一长假急诊创伤急救及留观收入', N'2026-06-04 11:00:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'60006', N'40002', N'2026-06', N'3600000.00', N'血透中心2026年中期集中结算', N'2026-07-02 09:15:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'60007', N'50001', N'2026-07', N'1450000.00', N'暑假期间学生体态矫正及康复理疗增量', N'2026-08-05 10:30:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'70001', N'70001', N'2026-01', N'8500000.00', N'2026年1月全院门诊及住院影像检查收入', N'2026-02-02 09:00:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'70002', N'70001', N'2026-02', N'7800000.00', N'春节长假期间急诊CT及磁共振创收', N'2026-03-02 09:30:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'70003', N'70001', N'2026-03', N'9200000.00', N'节后就诊高峰影像科营收', N'2026-04-03 10:00:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'70004', N'70001', N'2026-04', N'8800000.00', N'4月份职工体检季影像增量', N'2026-05-04 11:00:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'70005', N'70001', N'2026-05', N'9100000.00', N'5月份常规影像结算', N'2026-06-02 10:15:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'70006', N'70001', N'2026-06', N'8900000.00', N'6月份年中财务核算收入', N'2026-07-03 09:45:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'70007', N'70001', N'2026-07', N'9500000.00', N'7月暑期学生及教师体检大检', N'2026-08-04 08:30:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'70008', N'70001', N'2026-08', N'4200000.00', N'8月上半月（截至8月14日）阶段性营收', N'2026-08-14 12:00:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'80001', N'80001', N'2026-01', N'6500000.00', N'1月份各类外科择期手术收入', N'2026-02-05 09:00:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'80002', N'80001', N'2026-02', N'4200000.00', N'2月份春节期间急诊手术收入为主', N'2026-03-05 09:30:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'80003', N'80001', N'2026-03', N'7100000.00', N'3月份择期手术全面恢复，营收冲高', N'2026-04-05 10:00:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'80004', N'80001', N'2026-04', N'6800000.00', N'4月份手术室常规核算', N'2026-05-06 11:00:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'80005', N'80001', N'2026-05', N'6950000.00', N'5月份手术耗材及麻醉费用结算', N'2026-06-05 10:15:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'80006', N'80001', N'2026-06', N'7300000.00', N'6月年中大型骨科及神外手术集中结算', N'2026-07-06 09:45:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'80007', N'80001', N'2026-07', N'7500000.00', N'7月暑期学生群体扁桃体/疝气等择期手术高峰', N'2026-08-05 08:30:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'80008', N'80001', N'2026-08', N'3600000.00', N'8月上半月（截至8月14日）阶段性营收', N'2026-08-14 11:30:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'80009', N'80002', N'2026-01', N'5800000.00', N'冬季心脑血管急危重症患者ICU治疗费', N'2026-02-05 10:00:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'80010', N'80002', N'2026-05', N'5200000.00', N'5月份重大车祸创伤及术后监护收入', N'2026-06-05 09:30:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'80011', N'80002', N'2026-08', N'2500000.00', N'8月上半月（截至8月14日）重症床位及设备费', N'2026-08-14 14:00:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'100001', N'100001', N'2026-01', N'4500000.00', N'1月常规腹部及妇产超声流水', N'2026-02-04 09:00:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'100002', N'100001', N'2026-02', N'3900000.00', N'2月春节长假效应导致门诊量略降', N'2026-03-04 09:30:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'100003', N'100001', N'2026-03', N'5200000.00', N'3月孕检建档高峰，四维彩超满负荷', N'2026-04-04 10:00:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'100004', N'100001', N'2026-04', N'5050000.00', N'4月企事业单位体检超声项目增收', N'2026-05-04 11:00:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'100005', N'100001', N'2026-05', N'4980000.00', N'5月常规核算', N'2026-06-04 10:15:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'100006', N'100001', N'2026-06', N'5100000.00', N'6月心脏及血管超声检查量激增', N'2026-07-04 09:45:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'100007', N'100001', N'2026-07', N'5300000.00', N'7月暑期就诊量到达峰值', N'2026-08-04 08:30:00')
GO

INSERT INTO [dbo].[DeptRevenue] ([RevenueId], [DeptId], [Period], [Amount], [Remark], [CreatedAt]) VALUES (N'100008', N'100001', N'2026-08', N'2400000.00', N'8月上半月（截至8月14日）阶段性营收', N'2026-08-14 11:30:00')
GO

SET IDENTITY_INSERT [dbo].[DeptRevenue] OFF
GO


-- ----------------------------
-- Table structure for Equipment
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Equipment]') AND type IN ('U'))
	DROP TABLE [dbo].[Equipment]
GO

-- 设备主表（核心表）：医院全部医疗设备的台账信息，覆盖采购、归属、责任人、
-- 保修/使用年限、维保计划及当前状态等全生命周期属性。
-- Status 常见取值：Idle(闲置)/InUse(使用中)/Borrowed(借出)/Scrapped(已报废) 等。
CREATE TABLE [dbo].[Equipment] (
  [EquipmentId] int  IDENTITY(1,1) NOT NULL,  -- 设备主键，自增
  [EquipmentNo] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,  -- 设备编号（业务编号），唯一
  [EquipmentName] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,  -- 设备名称，如“呼吸机”“除颤仪”
  [Model] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 设备型号
  [Manufacturer] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 生产厂商
  [SupplierId] int  NULL,  -- 供应商ID，外键 -> Suppliers.SupplierId
  [CategoryId] int  NULL,  -- 设备分类ID，外键 -> EquipmentCategories.CategoryId
  [DeptId] int  NULL,  -- 所属科室ID，外键 -> Departments.DeptId
  [Location] nvarchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 设备存放/使用地点
  [ResponsibleUserId] int  NULL,  -- 责任人用户ID，外键 -> Users.UserId
  [Price] decimal(18,2)  NULL,  -- 设备采购价格
  [PurchaseDate] date  NULL,  -- 采购日期
  [WarrantyMonths] int  NULL,  -- 保修期（月）
  [ServiceLife] int  NULL,  -- 预计使用年限（年）
  [LastMaintainDate] date  NULL,  -- 上次维护/保养日期
  [NextMaintainDate] date  NULL,  -- 下次计划维护/保养日期
  [Status] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'Idle' NOT NULL,  -- 设备状态：Idle(闲置)/InUse(使用中)/Borrowed(借出)/Scrapped(报废)等，默认 Idle
  [Remarks] nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 备注信息
  [IsActive] bit DEFAULT 1 NOT NULL,  -- 是否有效（软删除标记），1=有效 0=已删除，默认1
  [CreatedAt] datetime DEFAULT getdate() NOT NULL,  -- 创建时间，默认当前时间
  [UpdatedAt] datetime  NULL  -- 最后更新时间
)
GO

ALTER TABLE [dbo].[Equipment] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Equipment
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Equipment] ON
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'1', N'CT-2026-001', N'多层螺旋CT', N'Revolution CT', N'GE医疗', N'2', N'6', N'4', N'放射科CT室1', N'2', N'5800000.00', N'2026-03-15', N'36', N'10', N'2026-06-15', N'2026-09-15', N'InUse', N'2026年新购高端CT', N'1', N'2026-07-30 07:23:46.693', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'2', N'HX-2025-088', N'呼吸机', N'V60 Plus', N'迈瑞', N'3', N'8', N'2', N'急诊科抢救室', N'3', N'180000.00', N'2025-09-20', N'24', N'8', N'2026-06-20', N'2026-09-20', N'Maintenance', N'屏幕故障已送维修部', N'1', N'2026-07-30 07:23:46.693', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'3', N'XD-2026-022', N'心电监护仪', N'BeneView T8', N'迈瑞', N'3', N'3', N'3', N'ICU 3床', N'4', N'45000.00', N'2026-01-10', N'24', N'6', N'2026-07-10', N'2026-10-10', N'InUse', NULL, N'1', N'2026-07-30 07:23:46.693', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'4', N'CS-2019-034', N'超声诊断仪', N'Voluson E10', N'GE医疗', N'2', N'7', N'6', N'妇产科超声室', N'2', N'1200000.00', N'2019-11-05', N'36', N'8', N'2026-01-10', NULL, N'Scrapped', N'设备老化，已批准报废', N'1', N'2026-07-30 07:23:46.693', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'5', N'JY-2026-045', N'生化分析仪', N'AU5800', N'贝克曼', N'1', N'4', N'5', N'检验科1室', N'2', N'850000.00', N'2026-05-08', N'24', N'10', NULL, NULL, N'Borrowed', N'借出给急诊科，预计8月3日归还', N'1', N'2026-07-30 07:23:46.693', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'6', N'CC-2026-005', N'除颤仪', N'Lifepak 20', N'飞利浦', N'4', N'9', N'2', N'急诊科抢救室', N'3', N'95000.00', N'2026-02-01', N'24', N'8', N'2026-07-05', N'2026-10-05', N'InUse', NULL, N'1', N'2026-07-30 07:23:46.693', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'7', N'HX-2024-112', N'呼吸机(备用)', N'V60', N'迈瑞', N'3', N'8', N'3', N'ICU库房', N'4', N'165000.00', N'2024-12-01', N'24', N'8', N'2026-06-28', N'2026-09-28', N'Idle', N'ICU备用设备', N'1', N'2026-07-30 07:23:46.693', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'8', N'MY-2026-088', N'免疫分析仪', N'Cobas 8000', N'罗氏', N'5', N'4', N'5', N'检验科2室', N'2', N'580000.00', N'2026-07-28', N'24', N'10', NULL, NULL, N'Idle', N'新入库待分配科室', N'1', N'2026-07-30 07:23:46.693', N'2026-07-31 08:13:30.280')
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'9', N'MR-2025-033', N'核磁共振成像仪', N'Signa Pioneer', N'GE医疗', N'2', N'1', N'4', N'放射科MR室', N'2', N'8500000.00', N'2025-06-01', N'48', N'12', N'2026-06-01', N'2026-09-01', N'InUse', N'3.0T高场强MRI', N'1', N'2026-07-30 07:23:46.693', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'10', N'DR-2026-011', N'DR拍片机', N'DigitalDiagnost', N'飞利浦', N'4', N'1', N'4', N'放射科DR室', N'2', N'680000.00', N'2026-04-01', N'36', N'10', NULL, NULL, N'InUse', NULL, N'1', N'2026-07-30 07:23:46.693', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'11', N'XG-2018-022', N'X光机', N'R-200', N'万东医疗', N'1', N'1', N'4', N'放射科X光室', N'2', N'350000.00', N'2018-03-12', N'24', N'8', N'2025-12-01', NULL, N'Scrapped', N'技术落后已停用待报废', N'1', N'2026-07-30 07:23:46.693', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'12', N'SS-2025-015', N'麻醉机', N'Fabius GS', N'德尔格', N'1', N'5', N'7', N'手术室3号间', N'2', N'420000.00', N'2025-08-15', N'24', N'10', N'2026-07-15', N'2026-10-15', N'InUse', NULL, N'1', N'2026-07-30 07:23:46.693', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'13', N'SS-2024-008', N'手术床', N'OT-8800', N'迈瑞', N'3', N'5', N'7', N'手术室1号间', N'2', N'185000.00', N'2024-03-20', N'36', N'12', N'2026-06-20', N'2026-12-20', N'Borrowed', N'备用手术床', N'1', N'2026-07-30 07:23:46.693', N'2026-08-03 01:43:17.990')
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'14', N'JY-2025-055', N'血液分析仪', N'BC-6800', N'迈瑞', N'3', N'4', N'5', N'检验科3室', N'2', N'320000.00', N'2025-11-01', N'24', N'8', N'2026-07-01', N'2026-10-01', N'InUse', NULL, N'1', N'2026-07-30 07:23:46.693', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'15', N'JY-2024-042', N'凝血分析仪', N'CS-5100', N'希森美康', N'1', N'4', N'5', N'检验科1室', N'2', N'280000.00', N'2024-07-15', N'24', N'8', N'2026-05-15', N'2026-08-15', N'Maintenance', N'加样臂故障维修中', N'1', N'2026-07-30 07:23:46.693', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'16', N'XD-2025-018', N'多参数监护仪', N'PM-9000', N'迈瑞', N'3', N'3', N'2', N'急诊科观察室', N'3', N'35000.00', N'2025-03-10', N'24', N'6', N'2026-06-10', N'2026-09-10', N'Borrowed', N'借给ICU临时使用', N'1', N'2026-07-30 07:23:46.693', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'17', N'CC-2024-003', N'除颤仪', N'Lifepak 15', N'飞利浦', N'4', N'9', N'3', N'ICU抢救室', N'4', N'120000.00', N'2024-08-01', N'24', N'8', N'2026-06-01', N'2026-09-01', N'InUse', NULL, N'1', N'2026-07-30 07:23:46.693', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'18', N'HX-2023-066', N'呼吸机', N'V60 Classic', N'迈瑞', N'3', N'8', N'2', N'急诊科库房', N'3', N'150000.00', N'2023-04-10', N'24', N'8', N'2025-10-10', NULL, N'Scrapped', N'设备老旧性能下降已报废', N'1', N'2026-07-30 07:23:46.693', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'19', N'ZS-2026-012', N'注射泵', N'BYZ-810', N'迈瑞', N'3', N'2', N'2', N'急诊科5床', N'3', N'8500.00', N'2026-06-15', N'24', N'6', NULL, NULL, N'InUse', NULL, N'1', N'2026-07-30 07:23:46.693', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'20', N'XW-2025-033', N'洗胃机', N'XW-III', N'凯达科技', N'1', N'2', N'2', N'急诊科处置室', N'3', N'12000.00', N'2025-05-20', N'24', N'6', N'2026-05-20', N'2026-08-20', N'Borrowed', NULL, N'1', N'2026-07-30 07:23:46.693', N'2026-08-03 03:45:10.420')
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'21', N'EQ-20260810-001', N'电击枪', N'DJQ-653', N'德克士', N'2', N'9', N'2', N'暂存库房', N'2', N'56200.00', N'2026-08-10', N'12', N'3', NULL, NULL, N'Idle', N'测试', N'1', N'2026-08-10 06:04:28.307', N'2026-08-14 01:40:08.093')
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'22', N'EQ-20260810-002', N'心电监护仪', N'PHILIPS MX450', N'飞利浦', N'4', N'3', N'3', N'ICU-01', N'2', N'85000.00', N'2026-08-10', NULL, NULL, NULL, NULL, N'Idle', N'导入功能测试', N'1', N'2026-08-10 06:09:24.273', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'23', N'EQ-20260810-003', N'便携式超声诊断仪', N'GE LOGIQ V2', N'通用电气', N'1', N'7', N'6', N'妇产科B超室', N'4', N'120000.00', N'2026-08-10', NULL, NULL, NULL, NULL, N'Borrowed', N'导入功能测试', N'1', N'2026-08-10 06:09:24.357', N'2026-08-14 06:51:10.550')
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'24', N'EQ-20260810-004', N'除颤监护仪', N'迈瑞 BeneHeart D6', N'迈瑞医疗', N'3', N'9', N'2', N'急诊抢救室', N'3', N'46000.00', N'2026-08-10', NULL, NULL, NULL, NULL, N'Idle', N'导入功能测试', N'1', N'2026-08-10 06:09:24.430', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'25', N'EQ-20260810-005', N'呼吸机', N'迈瑞 SV300', N'迈瑞医疗', N'1', N'8', N'3', N'ICU-02', N'6', N'98000.00', N'2026-08-10', NULL, NULL, NULL, NULL, N'Idle', N'导入功能测试', N'1', N'2026-08-10 06:09:24.510', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'26', N'EQ-20260810-006', N'全自动血液分析仪', N'迈瑞 BC-5180', N'迈瑞医疗', N'5', N'4', N'5', N'检验科一楼', N'1', N'135000.00', N'2026-08-10', NULL, NULL, NULL, NULL, N'Idle', N'导入功能测试', N'1', N'2026-08-10 06:09:24.667', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'27', N'EQ-20260814-101', N'心电监护仪', N'PHILIPS MX550', N'飞利浦', N'4', N'3', N'3', N'ICU-01', N'2', N'92000.00', N'2026-08-14', NULL, NULL, NULL, NULL, N'InUse', N'导入测试2', N'1', N'2026-08-14 03:07:04.190', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'28', N'EQ-20260814-102', N'便携式超声诊断仪', N'GE LOGIQ V3', N'通用电气', N'1', N'7', N'6', N'妇产科B超室', N'4', N'128000.00', N'2026-08-14', NULL, NULL, NULL, NULL, N'Idle', N'导入测试2', N'1', N'2026-08-14 03:07:04.247', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'29', N'EQ-20260814-103', N'除颤监护仪', N'迈瑞 BeneHeart D7', N'迈瑞医疗', N'3', N'9', N'2', N'急诊抢救室', N'3', N'52000.00', N'2026-08-14', NULL, NULL, NULL, NULL, N'InUse', N'导入测试2', N'1', N'2026-08-14 03:07:04.323', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'30', N'EQ-20260814-104', N'呼吸机', N'迈瑞 SV350', N'迈瑞医疗', N'1', N'8', N'3', N'ICU-02', N'6', N'105000.00', N'2026-08-14', NULL, NULL, NULL, NULL, N'Idle', N'导入测试2', N'1', N'2026-08-14 03:07:04.410', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'31', N'EQ-20260814-105', N'全自动血液分析仪', N'迈瑞 BC-5380', N'迈瑞医疗', N'5', N'4', N'5', N'检验科一楼', N'1', N'145000.00', N'2026-08-14', NULL, NULL, NULL, NULL, N'InUse', N'导入测试2', N'1', N'2026-08-14 03:07:04.473', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'32', N'KFX-2022-001', N'康复训练机', N'Rehab-Pro 200', N'飞利浦', N'4', N'1', N'8', N'康复科训练大厅', N'5', N'150000.00', N'2022-04-10', N'36', N'10', N'2025-04-10', N'2026-04-10', N'InUse', N'早期购入设备', N'1', N'2022-04-12 10:00:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'33', N'GZ-2023-015', N'骨科牵引床', N'Tract-X', N'迈瑞医疗', N'3', N'5', N'9', N'骨科治疗室', N'6', N'85000.00', N'2023-06-15', N'24', N'8', N'2025-06-15', N'2026-06-15', N'InUse', N'骨科专用', N'1', N'2023-06-20 11:30:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'34', N'XD-2024-055', N'十二导联心电图机', N'ECG-1200', N'GE医疗', N'2', N'3', N'11', N'心内科检查室', N'2', N'45000.00', N'2024-08-22', N'24', N'5', N'2025-08-22', N'2026-02-22', N'Idle', N'备用设备', N'1', N'2024-08-25 09:15:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'35', N'NHD-2025-012', N'脑电图仪', N'EEG-V5', N'罗氏', N'5', N'3', N'10', N'神经内科检查室', N'4', N'120000.00', N'2025-10-05', N'36', N'8', NULL, N'2026-10-05', N'InUse', N'', N'1', N'2025-10-10 14:00:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'36', N'XG-2022-099', N'便携式X光机', N'Mobile-X', N'通用电气', N'1', N'1', N'4', N'放射科库房', N'2', N'320000.00', N'2022-11-11', N'24', N'6', N'2025-11-11', NULL, N'Scrapped', N'管球老化严重，已报废', N'0', N'2022-11-15 08:30:00.000', N'2026-01-10 10:00:00.000')
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'37', N'FUTURE-2027-01', N'次世代全自动生化仪', N'AutoLab 9000', N'贝克曼', N'1', N'4', N'5', N'检验科未来实验室', N'1', N'1500000.00', N'2027-01-15', N'48', N'10', NULL, N'2027-07-15', N'InUse', N'2027年预购设备', N'1', N'2027-01-20 09:00:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'38', N'FUTURE-2027-02', N'便携式呼吸机', N'AirPro Mini', N'迈瑞医疗', N'3', N'8', N'2', N'急诊科救护车', N'3', N'65000.00', N'2027-03-01', N'24', N'5', NULL, N'2027-09-01', N'Idle', N'车载备用', N'1', N'2027-03-05 10:30:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'20001', N'LAB-2022-001', N'全自动化学发光免疫分析仪', N'Cobas e411', N'罗氏诊断', N'5', N'4', N'20001', N'检验科免疫室', N'2', N'850000.00', N'2022-05-20', N'36', N'10', N'2025-05-20', N'2026-05-20', N'InUse', N'高频使用设备', N'1', N'2022-05-25 10:00:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'20002', N'LAB-2023-015', N'全自动血细胞分析仪', N'BC-6800Plus', N'迈瑞医疗', N'3', N'4', N'20001', N'检验科临检室', N'3', N'420000.00', N'2023-08-15', N'24', N'8', N'2025-08-15', N'2026-02-15', N'InUse', N'常规体检主力机型', N'1', N'2023-08-20 11:30:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'20003', N'PHA-2024-001', N'门诊全自动发药机', N'Consis-A', N'艾隆科技', N'2', N'1', N'20002', N'门诊西药房', N'4', N'2200000.00', N'2024-06-10', N'36', N'10', N'2025-12-10', N'2026-06-10', N'InUse', N'大幅提升发药效率', N'1', N'2024-06-15 09:15:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'20004', N'PHA-2025-012', N'医用冷藏箱', N'HYC-390', N'海尔生物', N'1', N'7', N'20002', N'中心药库', N'5', N'28000.00', N'2025-03-05', N'24', N'8', N'2026-03-05', N'2027-03-05', N'InUse', N'用于存放生物制剂及疫苗', N'1', N'2025-03-10 14:00:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'30001', N'OR-2022-001', N'高端麻醉工作站', N'Fabius Plus', N'德尔格', N'4', N'8', N'30001', N'第一手术间', N'1', N'450000.00', N'2022-03-15', N'36', N'8', N'2025-03-15', N'2026-03-15', N'InUse', N'核心维保设备', N'1', N'2022-03-20 10:00:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'30002', N'OR-2023-005', N'超高清腹腔镜系统', N'EVIS EXERA III', N'奥林巴斯', N'2', N'2', N'30001', N'第三微创手术间', N'2', N'1200000.00', N'2023-06-10', N'24', N'6', N'2025-06-10', N'2026-06-10', N'InUse', N'微创手术主力', N'1', N'2023-06-15 11:30:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'30003', N'CSSD-2024-001', N'脉动真空蒸汽灭菌器', N'MAST-V', N'新华医疗', N'5', N'7', N'30002', N'灭菌区', N'3', N'550000.00', N'2024-04-22', N'36', N'10', N'2025-10-22', N'2026-04-22', N'InUse', N'特种设备需年检', N'1', N'2024-04-25 09:15:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'30004', N'CSSD-2025-002', N'全自动超声波清洗机', N'XQ-800', N'老肯医疗', N'3', N'7', N'30002', N'去污区', N'4', N'180000.00', N'2025-01-10', N'24', N'8', N'2026-01-10', N'2026-07-10', N'InUse', N'大批量器械初洗', N'1', N'2025-01-15 14:00:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'40001', N'ER-2022-001', N'除颤监护仪', N'BeneHeart D6', N'迈瑞医疗', N'3', N'8', N'40001', N'急诊抢救室1', N'2', N'85000.00', N'2022-05-15', N'36', N'6', N'2025-05-15', N'2026-05-15', N'InUse', N'生命急救必备，需每日巡检', N'1', N'2022-05-20 10:00:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'40002', N'ER-2023-002', N'自动心肺复苏机', N'LUCAS 3', N'史赛克', N'4', N'8', N'40001', N'急救车车载', N'3', N'150000.00', N'2023-08-10', N'24', N'5', N'2025-08-10', N'2026-02-10', N'InUse', N'院前急救/转运按压', N'1', N'2023-08-15 11:30:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'40003', N'HD-2024-001', N'血液透析机', N'4008S V10', N'费森尤斯', N'5', N'3', N'40002', N'透析A区01床', N'4', N'220000.00', N'2024-03-22', N'36', N'8', N'2025-09-22', N'2026-03-22', N'InUse', N'高频运转设备，需定期消毒水路', N'1', N'2024-03-25 09:15:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'40004', N'HD-2024-010', N'医用双级反渗透水处理系统', N'CWP-600', N'劳饵', N'1', N'7', N'40002', N'透析水处理间', N'5', N'680000.00', N'2024-02-10', N'24', N'10', N'2026-02-10', N'2026-08-10', N'InUse', N'透析室核心基建设备', N'1', N'2024-02-15 14:00:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'50001', N'REH-2023-001', N'下肢外骨骼康复机器人', N'LokomatPro', N'Hocoma', N'4', N'6', N'50001', N'康复训练大厅', N'2', N'3500000.00', N'2023-04-10', N'36', N'10', N'2025-10-10', N'2026-04-10', N'InUse', N'神经康复核心高价值设备', N'1', N'2023-04-15 10:00:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'50002', N'REH-2024-005', N'气压弹道冲击波治疗仪', N'Swiss DolorClast', N'EMS', N'2', N'6', N'50001', N'理疗室3', N'3', N'450000.00', N'2024-07-22', N'24', N'8', N'2025-07-22', N'2026-07-22', N'InUse', N'运动损伤理疗常用', N'1', N'2024-07-25 11:30:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'50003', N'IT-2022-001', N'HIS核心数据库服务器', N'PowerEdge R940', N'戴尔', N'5', N'9', N'50002', N'中心机房机柜A1', N'1', N'120000.00', N'2022-02-15', N'60', N'5', N'2026-02-15', N'2026-08-15', N'InUse', N'全院核心业务支撑，7x24小时运行', N'1', N'2022-02-20 09:15:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'50004', N'IT-2025-002', N'万兆核心交换机', N'S12700', N'华为', N'3', N'9', N'50002', N'中心机房机柜B2', N'4', N'280000.00', N'2025-01-10', N'36', N'8', N'2026-01-10', N'2027-01-10', N'InUse', N'内网骨干数据交换节点', N'1', N'2025-01-15 14:00:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'60001', N'LAB-2024-001', N'全自动生化分析仪', N'Cobas c702', N'罗氏诊断', N'2', N'4', N'60001', N'生化免疫流水线A区', N'3', N'2800000.00', N'2024-01-10', N'36', N'10', N'2026-01-10', N'2026-07-10', N'InUse', N'高通量设备，日常发病率检测主力', N'1', N'2024-01-15 09:00:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'60002', N'LAB-2025-002', N'化学发光免疫分析仪', N'Architect i2000SR', N'雅培', N'5', N'4', N'60001', N'生化免疫流水线B区', N'3', N'1500000.00', N'2025-06-15', N'24', N'8', N'2026-06-15', N'2026-12-15', N'InUse', N'肿瘤标志物及甲功等免疫检测', N'1', N'2025-06-20 10:30:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'70001', N'IMG-2024-001', N'3.0T 超导核磁共振(MRI)', N'MAGNETOM Lumina', N'西门子', N'1', N'2', N'70001', N'影像科磁共振一室', N'1', N'18500000.00', N'2024-11-15', N'36', N'12', N'2026-05-15', N'2026-11-15', N'InUse', N'全院最昂贵设备，需保持液氦制冷', N'1', N'2024-11-20 09:00:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'70002', N'IMG-2025-002', N'256排超高端CT', N'Revolution CT', N'GE医疗', N'3', N'2', N'70001', N'影像科CT二室', N'2', N'12000000.00', N'2025-08-10', N'24', N'10', N'2026-02-10', N'2026-08-10', N'InUse', N'高频运转，球管损耗大', N'1', N'2025-08-15 10:30:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'80001', N'OR-2024-001', N'高端麻醉工作站', N'Primus', N'德尔格', N'4', N'8', N'80001', N'第1中心手术间', N'2', N'650000.00', N'2024-06-15', N'36', N'10', N'2026-06-15', N'2026-12-15', N'InUse', N'手术室核心设备，必须确保气路绝对安全', N'1', N'2024-06-20 09:00:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'80002', N'ICU-2025-001', N'重症治疗型呼吸机', N'PB980', N'美敦力', N'5', N'8', N'80002', N'ICU 03床', N'3', N'380000.00', N'2025-03-10', N'24', N'8', N'2026-03-10', N'2026-09-10', N'InUse', N'高频生命支持设备', N'1', N'2025-03-15 10:30:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'80003', N'ICU-2025-002', N'体外膜肺氧合系统(ECMO)', N'Cardiohelp', N'迈柯唯', N'2', N'8', N'80002', N'ICU ECMO库房', N'1', N'1650000.00', N'2025-11-20', N'24', N'10', N'2026-05-20', N'2026-11-20', N'Idle', N'顶级生命支持设备(人工心肺)', N'1', N'2025-11-25 14:00:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'90001', N'LAB-90001', N'全自动生化分析仪', N'Cobas c702', N'罗氏诊断', N'2', N'3', N'90001', N'检验科生化流水线', N'2', N'2200000.00', N'2024-03-12', N'36', N'8', N'2026-03-12', N'2026-09-12', N'InUse', N'核心生化检测设备，每日标本处理量极大', N'1', N'2024-03-15 09:00:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'90002', N'LAB-90002', N'全自动化学发光免疫分析仪', N'Alinity i', N'雅培', N'5', N'3', N'90001', N'检验科免疫室', N'3', N'1850000.00', N'2025-07-20', N'24', N'8', N'2026-01-20', N'2026-07-20', N'InUse', N'用于肿瘤标志物、甲功等精密免疫检测', N'1', N'2025-07-25 10:30:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'90003', N'LAB-90003', N'医用高速冷冻离心机', N'Allegra V-15R', N'贝克曼', N'1', N'3', N'90001', N'检验科标本前处理区', N'1', N'120000.00', N'2025-09-10', N'24', N'10', N'2026-03-10', N'2026-09-10', N'InUse', N'标本分离必备设备，需定期维护压缩机', N'1', N'2025-09-12 14:00:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'100001', N'US-100001', N'高端四维彩色多普勒超声', N'Voluson E10', N'GE医疗', N'3', N'2', N'100001', N'超声科一诊室(产筛)', N'1', N'1850000.00', N'2024-05-10', N'36', N'8', N'2026-05-10', N'2026-11-10', N'InUse', N'主力产检排畸设备，高频使用', N'1', N'2024-05-12 09:00:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'100002', N'US-100002', N'心脏彩色多普勒超声', N'EPIQ 7C', N'飞利浦', N'4', N'2', N'100001', N'超声科三诊室(心血管)', N'2', N'1600000.00', N'2025-02-15', N'24', N'8', N'2026-02-15', N'2026-08-15', N'InUse', N'专用于心血管系统超声检查', N'1', N'2025-02-20 10:30:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'100003', N'US-100003', N'便携式彩色多普勒超声', N'M9', N'迈瑞', N'1', N'2', N'100001', N'超声科库房(床旁机)', N'3', N'450000.00', N'2025-08-10', N'36', N'10', N'2026-02-10', N'2026-08-10', N'InUse', N'常用于ICU、急诊等科室的床旁超声会诊', N'1', N'2025-08-12 14:00:00.000', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'100004', N'EQ-20260814-201', N'心电监护仪', N'迈瑞 BeneVision N12', N'迈瑞医疗', N'3', N'3', N'3', N'ICU-03', N'12', N'88000.00', N'2026-08-14', NULL, NULL, NULL, NULL, N'InUse', N'导入测试3', N'1', N'2026-08-14 06:58:36.690', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'100005', N'EQ-20260814-202', N'便携式超声诊断仪', N'GE LOGIQ Fortis', N'通用电气', N'1', N'7', N'6', N'妇产科B超室', N'4', N'138000.00', N'2026-08-14', NULL, NULL, NULL, NULL, N'Idle', N'导入测试3', N'1', N'2026-08-14 06:58:37.217', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'100006', N'EQ-20260814-203', N'除颤监护仪', N'迈瑞 BeneHeart D7', N'迈瑞医疗', N'3', N'9', N'2', N'急诊抢救室', N'3', N'52000.00', N'2026-08-14', NULL, NULL, NULL, NULL, N'Idle', N'导入测试3', N'1', N'2026-08-14 06:58:37.573', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'100007', N'EQ-20260814-204', N'呼吸机', N'迈瑞 SV800', N'迈瑞医疗', N'1', N'8', N'3', N'ICU-04', N'6', N'165000.00', N'2026-08-14', NULL, NULL, NULL, NULL, N'InUse', N'导入测试3', N'1', N'2026-08-14 06:58:38.003', NULL)
GO

INSERT INTO [dbo].[Equipment] ([EquipmentId], [EquipmentNo], [EquipmentName], [Model], [Manufacturer], [SupplierId], [CategoryId], [DeptId], [Location], [ResponsibleUserId], [Price], [PurchaseDate], [WarrantyMonths], [ServiceLife], [LastMaintainDate], [NextMaintainDate], [Status], [Remarks], [IsActive], [CreatedAt], [UpdatedAt]) VALUES (N'100008', N'EQ-20260814-205', N'全自动血液分析仪', N'迈瑞 BC-5380', N'迈瑞医疗', N'5', N'4', N'5', N'检验科二楼', N'1', N'210000.00', N'2026-08-14', NULL, NULL, NULL, NULL, N'Idle', N'导入测试3', N'1', N'2026-08-14 06:58:38.107', NULL)
GO

SET IDENTITY_INSERT [dbo].[Equipment] OFF
GO


-- ----------------------------
-- Table structure for EquipmentCategories
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[EquipmentCategories]') AND type IN ('U'))
	DROP TABLE [dbo].[EquipmentCategories]
GO

-- 设备分类字典表：支持通过 ParentId 自关联实现树形（父子级）分类结构，
-- 例如“临床设备”下再细分“CT设备”“超声设备”等子类。
CREATE TABLE [dbo].[EquipmentCategories] (
  [CategoryId] int  IDENTITY(1,1) NOT NULL,  -- 分类主键，自增
  [CategoryName] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,  -- 分类名称，如“监护设备”“CT设备”
  [CategoryCode] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 分类编码
  [ParentId] int  NULL,  -- 父分类ID，自关联 -> EquipmentCategories.CategoryId，顶级分类为空
  [SortOrder] int DEFAULT 0 NOT NULL,  -- 排序序号，默认0
  [IsActive] bit DEFAULT 1 NOT NULL,  -- 是否启用，默认1
  [CreatedAt] datetime DEFAULT getdate() NOT NULL,  -- 创建时间，默认当前时间
  [Description] nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 分类描述说明
  [MinAvailableCount] int DEFAULT 0 NOT NULL  -- 该分类设备最低可用（不外借）库存数量预警阈值，默认0
)
GO

ALTER TABLE [dbo].[EquipmentCategories] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of EquipmentCategories
-- ----------------------------
SET IDENTITY_INSERT [dbo].[EquipmentCategories] ON
GO

INSERT INTO [dbo].[EquipmentCategories] ([CategoryId], [CategoryName], [CategoryCode], [ParentId], [SortOrder], [IsActive], [CreatedAt], [Description], [MinAvailableCount]) VALUES (N'1', N'临床设备', N'CAT-IMG 4584626', N'5', N'6', N'1', N'2026-07-30 07:23:45.950', N'测试', N'1')
GO

INSERT INTO [dbo].[EquipmentCategories] ([CategoryId], [CategoryName], [CategoryCode], [ParentId], [SortOrder], [IsActive], [CreatedAt], [Description], [MinAvailableCount]) VALUES (N'2', N'急救设备', N'CAT-EMR', NULL, N'2', N'1', N'2026-07-30 07:23:45.950', NULL, N'1')
GO

INSERT INTO [dbo].[EquipmentCategories] ([CategoryId], [CategoryName], [CategoryCode], [ParentId], [SortOrder], [IsActive], [CreatedAt], [Description], [MinAvailableCount]) VALUES (N'3', N'监护设备', N'CAT-MON', NULL, N'3', N'1', N'2026-07-30 07:23:45.950', NULL, N'1')
GO

INSERT INTO [dbo].[EquipmentCategories] ([CategoryId], [CategoryName], [CategoryCode], [ParentId], [SortOrder], [IsActive], [CreatedAt], [Description], [MinAvailableCount]) VALUES (N'4', N'检验设备', N'CAT-LAB', NULL, N'4', N'1', N'2026-07-30 07:23:45.950', NULL, N'2')
GO

INSERT INTO [dbo].[EquipmentCategories] ([CategoryId], [CategoryName], [CategoryCode], [ParentId], [SortOrder], [IsActive], [CreatedAt], [Description], [MinAvailableCount]) VALUES (N'5', N'手术设备', N'CAT-OPT', NULL, N'5', N'1', N'2026-07-30 07:23:45.950', NULL, N'1')
GO

INSERT INTO [dbo].[EquipmentCategories] ([CategoryId], [CategoryName], [CategoryCode], [ParentId], [SortOrder], [IsActive], [CreatedAt], [Description], [MinAvailableCount]) VALUES (N'6', N'CT设备', N'CAT-CT', N'1', N'1', N'1', N'2026-07-30 07:23:45.950', NULL, N'1')
GO

INSERT INTO [dbo].[EquipmentCategories] ([CategoryId], [CategoryName], [CategoryCode], [ParentId], [SortOrder], [IsActive], [CreatedAt], [Description], [MinAvailableCount]) VALUES (N'7', N'超声设备2', N'CAT-US', N'6', N'2', N'1', N'2026-07-30 07:23:45.950', N'', N'1')
GO

INSERT INTO [dbo].[EquipmentCategories] ([CategoryId], [CategoryName], [CategoryCode], [ParentId], [SortOrder], [IsActive], [CreatedAt], [Description], [MinAvailableCount]) VALUES (N'8', N'呼吸设备', N'CAT-VEN', N'2', N'1', N'1', N'2026-07-30 07:23:45.950', N'', N'1')
GO

INSERT INTO [dbo].[EquipmentCategories] ([CategoryId], [CategoryName], [CategoryCode], [ParentId], [SortOrder], [IsActive], [CreatedAt], [Description], [MinAvailableCount]) VALUES (N'9', N'除颤设备', N'CAT-DEF', N'2', N'2', N'1', N'2026-07-30 07:23:45.950', NULL, N'1')
GO

INSERT INTO [dbo].[EquipmentCategories] ([CategoryId], [CategoryName], [CategoryCode], [ParentId], [SortOrder], [IsActive], [CreatedAt], [Description], [MinAvailableCount]) VALUES (N'46', N'新分类', N'', NULL, N'0', N'1', N'2026-08-14 06:56:56.367', N'', N'0')
GO

SET IDENTITY_INSERT [dbo].[EquipmentCategories] OFF
GO


-- ----------------------------
-- Table structure for InboundRecords
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[InboundRecords]') AND type IN ('U'))
	DROP TABLE [dbo].[InboundRecords]
GO

-- 设备入库记录表：记录设备采购到货、入库登记及入库审核状态，
-- 与 Equipment 表通过 EquipmentId 关联，是设备资产的“来源凭证”。
CREATE TABLE [dbo].[InboundRecords] (
  [InboundId] int  IDENTITY(1,1) NOT NULL,  -- 入库记录主键，自增
  [EquipmentId] int  NOT NULL,  -- 关联设备ID，外键 -> Equipment.EquipmentId
  [InboundNo] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,  -- 入库单号（业务编号）
  [Supplier] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 供应商名称（冗余存储，非外键）
  [PurchasePrice] decimal(18,2)  NULL,  -- 本次入库的采购单价
  [Quantity] int DEFAULT 1 NOT NULL,  -- 入库数量，默认1
  [InboundDate] datetime DEFAULT getdate() NOT NULL,  -- 入库日期，默认当前时间
  [OperatorId] int  NULL,  -- 经办人用户ID，外键 -> Users.UserId
  [AuditStatus] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'Pending' NOT NULL,  -- 入库审核状态：Pending(待审核)/Approved(已通过)等，默认 Pending
  [Remarks] nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 备注信息
  [CreatedAt] datetime DEFAULT getdate() NOT NULL  -- 记录创建时间，默认当前时间
)
GO

ALTER TABLE [dbo].[InboundRecords] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of InboundRecords
-- ----------------------------
SET IDENTITY_INSERT [dbo].[InboundRecords] ON
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'1', N'1', N'RK-2026-03-001', N'GE医疗', N'5800000.00', N'1', N'2026-03-15 00:00:00.000', N'1', N'Approved', NULL, N'2026-07-30 07:23:46.880')
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'2', N'9', N'RK-2025-06-002', N'GE医疗', N'8500000.00', N'1', N'2025-06-01 00:00:00.000', N'1', N'Approved', NULL, N'2026-07-30 07:23:46.880')
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'3', N'5', N'RK-2026-05-003', N'贝克曼', N'850000.00', N'1', N'2026-05-08 00:00:00.000', N'1', N'Approved', NULL, N'2026-07-30 07:23:46.880')
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'4', N'10', N'RK-2026-04-004', N'飞利浦', N'680000.00', N'1', N'2026-04-01 00:00:00.000', N'1', N'Approved', NULL, N'2026-07-30 07:23:46.880')
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'5', N'8', N'RK-2026-07-005', N'罗氏', N'580000.00', N'1', N'2026-07-28 00:00:00.000', N'2', N'Pending', NULL, N'2026-07-30 07:23:46.880')
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'6', N'12', N'RK-2025-08-006', N'德尔格', N'420000.00', N'1', N'2025-08-15 00:00:00.000', N'1', N'Approved', NULL, N'2026-07-30 07:23:46.880')
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'7', N'19', N'RK-2026-06-007', N'迈瑞', N'8500.00', N'5', N'2026-06-15 00:00:00.000', N'2', N'Approved', NULL, N'2026-07-30 07:23:46.880')
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'8', N'14', N'RK-2025-11-008', N'迈瑞', N'320000.00', N'1', N'2025-11-01 00:00:00.000', N'1', N'Approved', NULL, N'2026-07-30 07:23:46.880')
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'9', N'16', N'RK-2025-03-009', N'迈瑞', N'35000.00', N'3', N'2025-03-10 00:00:00.000', N'1', N'Approved', NULL, N'2026-07-30 07:23:46.880')
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'10', N'20', N'RK-2025-05-010', N'凯达科技', N'12000.00', N'2', N'2025-05-20 00:00:00.000', N'2', N'Approved', NULL, N'2026-07-30 07:23:46.880')
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'11', N'32', N'RK-2022-04-001', N'飞利浦', N'150000.00', N'1', N'2022-04-10 00:00:00.000', N'1', N'Approved', N'康复设备采购', N'2022-04-12 10:00:00.000')
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'12', N'33', N'RK-2023-06-001', N'迈瑞医疗', N'85000.00', N'2', N'2023-06-15 00:00:00.000', N'2', N'Approved', N'骨科批量采购', N'2023-06-20 11:30:00.000')
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'13', N'34', N'RK-2024-08-001', N'GE医疗', N'45000.00', N'5', N'2024-08-22 00:00:00.000', N'1', N'Approved', N'全院心电图机更新', N'2024-08-25 09:15:00.000')
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'14', N'37', N'RK-2027-01-001', N'贝克曼', N'1500000.00', N'1', N'2027-01-15 00:00:00.000', N'2', N'Pending', N'未来实验室建设专项采购', N'2027-01-20 09:00:00.000')
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'20001', N'20001', N'RK-2022-05-008', N'罗氏诊断', N'850000.00', N'1', N'2022-05-20 00:00:00.000', N'1', N'Approved', N'发光免疫系统建设', N'2022-05-25 10:00:00.000')
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'20002', N'20003', N'RK-2024-06-015', N'艾隆科技', N'2200000.00', N'1', N'2024-06-10 00:00:00.000', N'2', N'Approved', N'智慧药房自动化改造项目', N'2024-06-15 11:30:00.000')
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'30001', N'30002', N'RK-2023-06-004', N'奥林巴斯', N'1200000.00', N'1', N'2023-06-10 00:00:00.000', N'1', N'Approved', N'外科楼新建手术间配套配置', N'2023-06-15 10:00:00.000')
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'30002', N'30003', N'RK-2024-04-012', N'新华医疗', N'550000.00', N'1', N'2024-04-22 00:00:00.000', N'2', N'Approved', N'供应室灭菌锅换代升级', N'2024-04-25 11:30:00.000')
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'40001', N'40001', N'RK-2022-05-012', N'迈瑞医疗', N'85000.00', N'5', N'2022-05-15 00:00:00.000', N'2', N'Approved', N'急救通道标准化配置', N'2022-05-20 10:00:00.000')
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'40002', N'40003', N'RK-2024-03-005', N'费森尤斯', N'220000.00', N'15', N'2024-03-22 00:00:00.000', N'4', N'Approved', N'血透中心一期扩建项目', N'2024-03-25 11:30:00.000')
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'50001', N'50001', N'RK-2023-04-002', N'Hocoma', N'3500000.00', N'1', N'2023-04-10 00:00:00.000', N'2', N'Approved', N'康复中心重点学科建设引进', N'2023-04-15 10:00:00.000')
GO

INSERT INTO [dbo].[InboundRecords] ([InboundId], [EquipmentId], [InboundNo], [Supplier], [PurchasePrice], [Quantity], [InboundDate], [OperatorId], [AuditStatus], [Remarks], [CreatedAt]) VALUES (N'50002', N'50003', N'RK-2022-02-005', N'戴尔', N'120000.00', N'3', N'2022-02-15 00:00:00.000', N'1', N'Approved', N'数据中心一期服务器采购', N'2022-02-20 11:30:00.000')
GO

SET IDENTITY_INSERT [dbo].[InboundRecords] OFF
GO


-- ----------------------------
-- Table structure for MaintenanceMaterials
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[MaintenanceMaterials]') AND type IN ('U'))
	DROP TABLE [dbo].[MaintenanceMaterials]
GO

-- 维修用料明细表：记录某一维修工单(RecordId)实际领用/更换的备件明细，
-- 与 MaintenanceRecords 是“一对多”关系（一个工单可用多种备件），
-- Subtotal = Quantity * UnitPrice，用于汇总该工单的维修物料成本。
CREATE TABLE [dbo].[MaintenanceMaterials] (
  [Id] int  IDENTITY(1,1) NOT NULL,  -- 主键，自增
  [RecordId] int  NOT NULL,  -- 所属维修工单ID，外键 -> MaintenanceRecords.RecordId
  [MaterialId] int  NULL,  -- 所用备件ID，外键 -> Materials.MaterialId（可为空，表示自定义临时物料）
  [MaterialName] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,  -- 备件名称（冗余存储，便于历史追溯不受字典变更影响）
  [Quantity] int  NOT NULL,  -- 使用数量，须 > 0（见 CK_MM_Quantity 约束）
  [UnitPrice] decimal(10,2)  NOT NULL,  -- 使用时的单价，须 >= 0（见 CK_MM_UnitPrice 约束）
  [Subtotal] decimal(10,2)  NOT NULL  -- 小计金额 = Quantity * UnitPrice，须 >= 0（见 CK_MM_Subtotal 约束）
)
GO

ALTER TABLE [dbo].[MaintenanceMaterials] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of MaintenanceMaterials
-- ----------------------------
SET IDENTITY_INSERT [dbo].[MaintenanceMaterials] ON
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'9001', N'36', N'9256', N'医用高级润滑脂', N'1', N'150.00', N'150.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'9002', N'37', N'9255', N'12导联心电线缆', N'1', N'850.00', N'850.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'9003', N'4', N'9257', N'生化仪加样针', N'1', N'3500.00', N'3500.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'20001', N'20001', N'20001', N'发光仪进样皮带', N'1', N'1200.00', N'1200.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'20002', N'20001', N'9257', N'生化仪加样针', N'1', N'3500.00', N'3500.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'20003', N'20002', N'20002', N'高精度定位光栅', N'2', N'850.00', N'1700.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'30001', N'30001', N'30001', N'麻醉机流量传感器', N'1', N'1800.00', N'1800.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'30002', N'30002', N'30002', N'灭菌器耐高温密封圈', N'1', N'850.00', N'850.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'40001', N'40001', N'40001', N'医疗设备专用大容量锂电池组', N'1', N'1600.00', N'1600.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'40002', N'40002', N'40002', N'高精度电导率传感器', N'1', N'2500.00', N'2500.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'50001', N'50001', N'50001', N'医用精密伺服电机', N'1', N'8500.00', N'8500.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'50002', N'50002', N'50002', N'2.4TB 10K RPM SAS 企业级硬盘', N'1', N'2200.00', N'2200.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'60001', N'60001', N'60001', N'特氟龙液流加样管', N'1', N'450.00', N'450.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'60002', N'60002', N'60002', N'医疗设备半导体制冷片 12V 10A', N'1', N'800.00', N'800.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'70001', N'70001', N'70001', N'MRI专用二级冷头 (Cold Head)', N'1', N'125000.00', N'125000.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'70002', N'70002', N'70002', N'Revolution CT 液态金属轴承球管', N'1', N'850000.00', N'850000.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'80001', N'80001', N'80001', N'医用氧气传感器(氧电池)', N'1', N'1800.00', N'1800.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'80002', N'80002', N'80002', N'呼吸机一体化可重复使用呼气阀', N'1', N'3500.00', N'3500.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'80003', N'80003', N'80003', N'ECMO主机专用医疗级UPS蓄电池', N'2', N'2750.00', N'5500.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'100001', N'100001', N'100001', N'超声操作面板轨迹球组件', N'1', N'650.00', N'650.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'100002', N'100002', N'100002', N'飞利浦 S5-1 相控阵探头', N'1', N'68000.00', N'68000.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'100003', N'29', N'269', N'参数校准服务', N'1', N'2000.00', N'2000.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'100004', N'29', N'268', N'固件升级', N'1', N'1000.00', N'1000.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'100005', N'38', N'9255', N'12导联心电线缆', N'1', N'850.00', N'850.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'100006', N'38', N'288', N'主板', N'1', N'5000.00', N'5000.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'100007', N'38', N'286', N'心电导联线', N'1', N'200.00', N'200.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'100008', N'38', N'287', N'无创血压袖带', N'1', N'150.00', N'150.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'100009', N'38', N'284', N'电源模块', N'1', N'350.00', N'350.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'100010', N'38', N'9258', N'脑电电极帽', N'1', N'1200.00', N'1200.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'100011', N'38', NULL, N'电源适配器', N'1', N'1500.00', N'1500.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'100012', N'100006', N'80001', N'医用氧气传感器(氧电池)', N'1', N'1800.00', N'1800.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'100013', N'100006', N'40002', N'高精度电导率传感器', N'1', N'2500.00', N'2500.00')
GO

INSERT INTO [dbo].[MaintenanceMaterials] ([Id], [RecordId], [MaterialId], [MaterialName], [Quantity], [UnitPrice], [Subtotal]) VALUES (N'100014', N'100006', N'30001', N'麻醉机流量传感器', N'1', N'1800.00', N'1800.00')
GO

SET IDENTITY_INSERT [dbo].[MaintenanceMaterials] OFF
GO


-- ----------------------------
-- Table structure for MaintenanceRecords
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[MaintenanceRecords]') AND type IN ('U'))
	DROP TABLE [dbo].[MaintenanceRecords]
GO

-- 设备报修/维修工单主表（核心业务表）：记录从“故障上报 -> 派工 -> 维修 -> 完成/驳回”的完整流程，
-- 并集成了 AI 故障识别相关字段（AiFaultType/AiConfidence，来自照片智能分析）。
-- ProgressStage 常见取值：Pending(待分配)/Assigned(已派工)/InProgress(维修中)/Done(已完成)。
CREATE TABLE [dbo].[MaintenanceRecords] (
  [RecordId] int  IDENTITY(1,1) NOT NULL,  -- 维修工单主键，自增
  [EquipmentId] int  NOT NULL,  -- 故障设备ID，外键 -> Equipment.EquipmentId
  [RepairNo] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,  -- 报修单号（业务编号），如 BX-2026-0715-01
  [ReporterId] int  NULL,  -- 报修人用户ID，外键 -> Users.UserId
  [ReportDeptId] int  NULL,  -- 报修科室ID，外键 -> Departments.DeptId
  [FaultDesc] nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,  -- 故障现象描述
  [FaultType] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 故障类型，如“电气故障”“机械故障”“软件故障”
  [Urgency] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'Normal' NOT NULL,  -- 紧急程度：Low/Normal/Urgent，默认 Normal
  [ProgressStage] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'Pending' NOT NULL,  -- 处理阶段：Pending(待分配)/Assigned(已派工)/InProgress(维修中)/Done(已完成)，默认 Pending
  [AssignedTo] int  NULL,  -- 被指派的维修工程师用户ID，外键 -> Users.UserId
  [RepairResult] nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 维修结果说明（含更换的物料清单及金额汇总文本）
  [RepairCost] decimal(18,2)  NULL,  -- 维修总费用
  [DowntimeHours] decimal(6,2)  NULL,  -- 设备停机时长（小时）
  [ReportTime] datetime DEFAULT getdate() NOT NULL,  -- 报修时间，默认当前时间
  [CompleteTime] datetime  NULL,  -- 维修完成时间
  [Status] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'Pending' NOT NULL,  -- 工单状态：Pending/InProgress/Completed 等，默认 Pending（与 ProgressStage 配合使用）
  [Remarks] nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 备注信息
  [CreatedAt] datetime DEFAULT getdate() NOT NULL,  -- 记录创建时间，默认当前时间
  [PhotoPath] nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 故障照片存储路径/URL
  [AiFaultType] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- AI 根据照片识别出的疑似故障类型
  [AiConfidence] decimal(5,2)  NULL,  -- AI 识别置信度，取值范围 0~1（见 CK_MaintenanceRecords_AiConfidence 约束）
  [RejectReason] nvarchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL  -- 工单被驳回时的原因说明
)
GO

ALTER TABLE [dbo].[MaintenanceRecords] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of MaintenanceRecords
-- ----------------------------
SET IDENTITY_INSERT [dbo].[MaintenanceRecords] ON
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'1', N'2', N'BX-2026-0715-01', N'3', N'2', N'开机后屏幕无显示，导致无法正常使用', N'电气故障', N'Urgent', N'Done', N'5', NULL, NULL, NULL, N'2026-07-15 14:30:00.000', NULL, N'Completed', NULL, N'2026-07-30 07:23:47.047', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'2', N'3', N'BX-2026-0714-02', N'4', N'3', N'心率监测数据异常，数值波动较大', N'软件故障', N'Normal', N'Done', N'5', N'重新校准传感器，重启后恢复正常', N'0.00', N'1.50', N'2026-07-14 09:15:00.000', N'2026-07-15 10:30:00.000', N'Completed', NULL, N'2026-07-30 07:23:47.047', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'3', N'1', N'BX-2026-0713-03', N'2', N'4', N'扫描过程中断，报错代码E-045', N'机械故障', N'Urgent', N'Done', N'5', N'修好了1', N'110.00', N'1.00', N'2026-07-13 11:20:00.000', N'2026-08-04 03:27:08.087', N'Completed', NULL, N'2026-07-30 07:23:47.047', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'4', N'5', N'BX-2026-0712-04', N'3', N'2', N'试剂针堵塞，加样不准确', N'机械故障', N'Normal', N'Done', N'5', N'清洗试剂针，更换密封圈', N'350.00', N'3.00', N'2026-07-12 16:40:00.000', N'2026-07-13 14:00:00.000', N'Completed', NULL, N'2026-07-30 07:23:47.047', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'5', N'9', N'BX-2026-0710-05', N'2', N'4', N'磁体冷却系统报警，温度偏高', N'机械故障', N'Urgent', N'Done', N'5', N'1111', N'1.00', N'1.00', N'2026-07-10 08:20:00.000', N'2026-08-04 03:30:16.120', N'Completed', NULL, N'2026-07-30 07:23:47.047', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'6', N'15', N'BX-2026-0708-06', N'3', N'5', N'加样臂定位偏移，测试结果不准确', N'耗材堵塞', N'Urgent', N'InProgress', N'5', NULL, NULL, NULL, N'2026-07-08 10:00:00.000', NULL, N'InProgress', NULL, N'2026-07-30 07:23:47.047', NULL, N'耗材堵塞', N'0.00', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'7', N'6', N'BX-2026-0705-07', N'3', N'2', N'除颤仪电池无法充电，电量显示异常', N'电气故障', N'Normal', N'Done', N'5', N'更换电池组，测试正常', N'1200.00', N'24.00', N'2026-07-05 15:10:00.000', N'2026-07-06 15:10:00.000', N'Completed', NULL, N'2026-07-30 07:23:47.047', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'8', N'16', N'BX-2026-0703-08', N'4', N'3', N'血氧饱和度监测数值不准', N'软件故障', N'Normal', N'Done', N'5', N'更换血氧探头，校准成功', N'450.00', N'2.00', N'2026-07-03 09:30:00.000', N'2026-07-03 15:00:00.000', N'Completed', NULL, N'2026-07-30 07:23:47.047', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'9', N'12', N'BX-2026-0628-09', N'2', N'7', N'麻醉机回路泄漏，气压不稳定', N'机械故障', N'Urgent', N'Done', N'5', N'更换回路密封圈，检修单向阀', N'2800.00', N'8.00', N'2026-06-28 14:00:00.000', N'2026-06-29 10:00:00.000', N'Completed', NULL, N'2026-07-30 07:23:47.047', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'10', N'7', N'BX-2026-0620-10', N'4', N'3', N'呼吸机管路连接处漏气', N'机械故障', N'Normal', N'Assigned', N'6', NULL, NULL, NULL, N'2026-06-20 11:00:00.000', NULL, N'Pending', NULL, N'2026-07-30 07:23:47.047', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'11', N'19', N'BX-2026-0615-11', N'3', N'2', N'注射泵推进速度不准', N'机械故障', N'Low', N'Assigned', N'6', NULL, NULL, NULL, N'2026-06-15 08:30:00.000', NULL, N'Pending', NULL, N'2026-07-30 07:23:47.047', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'12', N'11', N'BX-2026-0510-12', N'2', N'4', N'X光机曝光时图像模糊', N'电气故障', N'Normal', N'Done', N'5', N'球管老化无法修复，建议报废', N'0.00', N'48.00', N'2026-05-10 09:00:00.000', N'2026-05-12 09:00:00.000', N'Completed', NULL, N'2026-07-30 07:23:47.047', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'13', N'20', N'BX-2026-0731-01', N'1', N'3', N'洗胃机坏了', N'机械故障', N'Normal', N'InProgress', N'6', NULL, NULL, NULL, N'2026-07-31 08:20:05.610', NULL, N'InProgress', NULL, N'2026-07-31 08:20:05.610', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'14', N'11', N'BX-2026-0731-02', N'1', N'3', N'1111', N'其他', N'Urgent', N'Assigned', N'6', NULL, NULL, NULL, N'2026-07-31 08:59:35.770', NULL, N'Pending', NULL, N'2026-07-31 08:59:35.770', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'17', N'10', N'BX-2026-0804-01', N'1', N'3', N'111111111', N'电气故障', N'Normal', N'InProgress', N'6', NULL, NULL, NULL, N'2026-08-04 03:24:29.960', NULL, N'InProgress', NULL, N'2026-08-04 03:24:29.960', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'18', N'15', N'BX-2026-0804-02', N'1', N'6', N'222222', N'机械故障', N'Urgent', N'InProgress', N'5', NULL, NULL, NULL, N'2026-08-04 03:25:21.877', NULL, N'InProgress', NULL, N'2026-08-04 03:25:21.877', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'20', N'5', N'BX-2026-0804-03', N'3', N'2', N'软件有故障请更新1111', N'软件故障', N'Urgent', N'Assigned', N'5', NULL, NULL, NULL, N'2026-08-04 08:19:20.570', NULL, N'Pending', NULL, N'2026-08-04 08:19:20.570', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'21', N'2', N'BX-2026-0804-04', N'3', N'2', N'本科室设备需要报修111', N'机械故障', N'Normal', N'Assigned', N'6', NULL, NULL, NULL, N'2026-08-04 08:19:52.310', NULL, N'Pending', NULL, N'2026-08-04 08:19:52.310', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'22', N'18', N'BX-2026-0810-01', N'3', N'2', N'111', N'机械故障', N'Normal', N'InProgress', N'6', NULL, NULL, NULL, N'2026-08-10 08:04:09.357', NULL, N'InProgress', NULL, N'2026-08-10 08:04:09.357', NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'23', N'16', N'BX-2026-0811-01', N'3', N'2', N'打不开了', N'', N'Normal', N'InProgress', N'5', NULL, NULL, NULL, N'2026-08-11 09:34:17.280', NULL, N'InProgress', NULL, N'2026-08-11 09:34:17.280', NULL, N'其他', N'0.00', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'24', N'18', N'BX-2026-0811-02', N'3', N'2', N'2121', N'', N'Normal', N'InProgress', N'5', NULL, NULL, NULL, N'2026-08-11 09:48:53.800', NULL, N'InProgress', NULL, N'2026-08-11 09:48:53.800', NULL, N'电气故障', N'0.00', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'25', N'18', N'BX-2026-0811-03', N'3', N'2', N'8/11', N'', N'Normal', N'Done', N'6', N'CO2传感器 x1、氧电池 x1、12导联心电导联线 x1，物料总金额 ¥2,200.00', N'2200.00', N'5.00', N'2026-08-11 09:56:47.190', N'2026-08-12 04:03:33.650', N'Completed', NULL, N'2026-08-11 09:56:47.190', N'D:\SMT\HospitalEquipmentSystem.UI\bin\Debug\Uploads\Photos\20260811175647_a307cec0.png', N'硬件故障', N'0.85', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'26', N'26', N'BX-2026-0813-01', N'1', N'3', N'拍片机故障', N'', N'Urgent', N'Assigned', N'6', NULL, NULL, NULL, N'2026-08-13 02:05:51.640', NULL, N'Pending', NULL, N'2026-08-13 02:05:51.640', N'D:\SMT\HospitalEquipmentSystem.UI\bin\Debug\Uploads\Photos\20260813100520_0ebd251f7077460e9d4b28571b0420fd.png', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'27', N'25', N'BX-2026-0813-02', N'1', N'7', N'手术室呼吸机坏了', N'', N'Low', N'Done', N'5', N'CO2传感器 x1、氧电池 x1、氧电池 x1，物料总金额 ¥1,800.00', N'1800.00', N'2.00', N'2026-08-13 02:38:33.780', N'2026-08-13 02:41:45.707', N'Completed', NULL, N'2026-08-13 02:38:33.780', N'D:\SMT\HospitalEquipmentSystem.UI\bin\Debug\Uploads\Photos\20260813103829_e83619e8635d411ab834d1c0665a00c2.png', N'硬件故障', N'0.85', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'28', N'6', N'BX-2026-0813-03', N'3', N'2', N'除颤仪打不开', N'', N'Low', N'Assigned', N'5', NULL, NULL, NULL, N'2026-08-13 02:40:29.913', NULL, N'Pending', NULL, N'2026-08-13 02:40:29.913', N'D:\SMT\HospitalEquipmentSystem.UI\bin\Debug\Uploads\Photos\20260813104023_38997122.png', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'29', N'18', N'BX-2026-0814-01', N'3', N'2', N'坏', N'软件故障', N'Normal', N'Done', N'6', N'参数校准服务 x1、固件升级 x1，物料总金额 ¥3,000.00', N'3000.00', N'13.00', N'2026-08-14 02:05:14.313', N'2026-08-14 06:20:32.367', N'Completed', NULL, N'2026-08-14 02:05:14.313', N'D:\SMT\HospitalEquipmentSystem.UI\bin\Debug\Uploads\Photos\20260814100510_bee9c966.png', N'软件故障', N'0.85', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'30', N'18', N'BX-2026-0814-02', N'3', N'2', N'呼吸机坏了', N'', N'Normal', N'Assigned', N'6', NULL, NULL, NULL, N'2026-08-14 02:18:52.120', NULL, N'Pending', NULL, N'2026-08-14 02:18:52.120', N'D:\SMT\HospitalEquipmentSystem.UI\bin\Debug\Uploads\Photos\20260814101848_14fc4a9a.png', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'31', N'21', N'BX-2026-0814-03', N'3', N'2', N'电击枪不供电了', N'', N'Urgent', N'Assigned', N'5', NULL, NULL, NULL, N'2026-08-14 02:19:16.640', NULL, N'Pending', NULL, N'2026-08-14 02:19:16.640', N'D:\SMT\HospitalEquipmentSystem.UI\bin\Debug\Uploads\Photos\20260814101911_5eec07fa.png', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'32', N'24', N'BX-2026-0814-04', N'3', N'2', N'除颤仪故障', N'', N'Normal', N'InProgress', N'5', NULL, NULL, NULL, N'2026-08-14 02:19:44.630', NULL, N'InProgress', NULL, N'2026-08-14 02:19:44.630', N'D:\SMT\HospitalEquipmentSystem.UI\bin\Debug\Uploads\Photos\20260814101941_ab2fe080.png', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'33', N'18', N'BX-2026-0814-05', N'1', N'2', N'打不开了', N'电气故障', N'Normal', N'Pending', NULL, NULL, NULL, NULL, N'2026-08-14 02:32:42.627', NULL, N'Pending', NULL, N'2026-08-14 02:32:42.627', N'D:\SMT\HospitalEquipmentSystem.UI\bin\Debug\Uploads\Photos\20260814103239.jpg', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'34', N'19', N'BX-2026-0814-06', N'1', N'6', N'开不了机了', N'电气故障', N'Normal', N'Pending', NULL, NULL, NULL, NULL, N'2026-08-14 02:34:36.103', NULL, N'Pending', NULL, N'2026-08-14 02:34:36.103', N'D:\SMT\HospitalEquipmentSystem.UI\bin\Debug\Uploads\Photos\20260814103411.jpg', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'35', N'22', N'BX-2026-0814-07', N'1', N'6', N'监护仪打不开', N'', N'Normal', N'Pending', NULL, NULL, NULL, NULL, N'2026-08-14 02:52:27.210', NULL, N'Pending', NULL, N'2026-08-14 02:52:27.210', N'D:\SMT\HospitalEquipmentSystem.UI\bin\Debug\Uploads\Photos\20260814105207.jpg', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'36', N'32', N'BX-2023-1005-01', N'5', N'8', N'履带卡顿异响', N'机械故障', N'Normal', N'Done', N'5', N'清理履带杂物并加注润滑油', N'150.00', N'2.00', N'2023-10-05 09:00:00.000', N'2023-10-05 14:00:00.000', N'Completed', NULL, N'2023-10-05 09:10:00.000', NULL, N'硬件故障', N'0.88', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'37', N'34', N'BX-2025-0218-01', N'2', N'11', N'心电波形干扰严重，导联线破损', N'电气故障', N'Urgent', N'Done', N'6', N'更换12导联线套装', N'850.00', N'1.50', N'2025-02-18 10:30:00.000', N'2025-02-18 11:45:00.000', N'Completed', NULL, N'2025-02-18 10:35:00.000', NULL, N'电气故障', N'0.95', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'38', N'35', N'BX-2027-0510-01', N'4', N'10', N'脑电图仪系统软件崩溃，无法启动', N'电气故障', N'Urgent', N'Done', N'5', N'12导联心电线缆 x1、主板 x1、心电导联线 x1、无创血压袖带 x1、电源模块 x1、脑电电极帽 x1、电源适配器 x1，物料总金额 ¥9,250.00', N'9250.00', N'-6449.00', N'2027-05-10 08:20:00.000', N'2026-08-14 07:12:17.227', N'Completed', N'厂家技术支持远程排查中', N'2027-05-10 08:25:00.000', NULL, N'电气故障', N'0.00', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'39', N'36', N'BX-2024-1105-02', N'2', N'4', N'曝光完全没有反应', N'核心部件故障', N'Normal', N'Done', N'6', N'球管完全损毁，维修成本过高，建议报废', N'0.00', N'720.00', N'2024-11-05 14:00:00.000', N'2024-11-10 09:00:00.000', N'Completed', N'转报废流程', N'2024-11-05 14:15:00.000', NULL, N'硬件故障', N'0.99', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'20001', N'20001', N'BX-2024-1110-01', N'2', N'20001', N'进样轨道卡阻，试剂针滴漏', N'机械故障', N'Urgent', N'Done', N'3', N'更换进样皮带及清洗试剂针组件', N'4500.00', N'5.50', N'2024-11-10 09:00:00.000', N'2024-11-10 14:30:00.000', N'Completed', NULL, N'2024-11-10 09:10:00.000', NULL, N'机械故障', N'0.94', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'20002', N'20003', N'BX-2025-0805-02', N'4', N'20002', N'发药机机械臂定位偏移，导致药盒掉落', N'机械故障', N'Urgent', N'Done', N'5', N'重新校准三轴机械臂，更换磨损光栅', N'2100.00', N'3.00', N'2025-08-05 10:30:00.000', N'2025-08-05 13:30:00.000', N'Completed', N'影响门诊发药效率，已加急处理', N'2025-08-05 10:35:00.000', NULL, N'硬件故障', N'0.88', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'20003', N'20004', N'BX-2026-0120-01', N'5', N'20002', N'冷藏箱温度报警，无法维持在2-8度区间', N'核心部件故障', N'Normal', N'InProgress', N'2', NULL, NULL, NULL, N'2026-01-20 08:20:00.000', NULL, N'InProgress', N'疑似压缩机问题，厂家上门中', N'2026-01-20 08:25:00.000', NULL, N'电气故障', N'0.90', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'30001', N'30001', N'BX-2024-0920-01', N'1', N'30001', N'麻醉机流量传感器校准失败报警', N'传感器故障', N'Urgent', N'Done', N'3', N'更换吸入端流量传感器并重新校准', N'1800.00', N'2.50', N'2024-09-20 09:00:00.000', N'2024-09-20 11:30:00.000', N'Completed', N'手术间设备，极高优先级', N'2024-09-20 09:10:00.000', NULL, N'硬件故障', N'0.95', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'30002', N'30003', N'BX-2025-1212-02', N'3', N'30002', N'灭菌器腔门密封漏气，无法建立真空', N'机械故障', N'Urgent', N'Done', N'4', N'更换耐高温硅胶密封圈', N'850.00', N'4.00', N'2025-12-12 10:30:00.000', N'2025-12-12 14:30:00.000', N'Completed', N'影响灭菌锅正常排班', N'2025-12-12 10:35:00.000', NULL, N'机械故障', N'0.98', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'30003', N'30004', N'BX-2026-0518-01', N'4', N'30002', N'超声波清洗机加热槽不加热', N'电气故障', N'Normal', N'InProgress', N'2', NULL, NULL, NULL, N'2026-05-18 08:20:00.000', NULL, N'InProgress', N'已订购加热管配件，等待到货', N'2026-05-18 08:25:00.000', NULL, N'电气故障', N'0.92', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'40001', N'40001', N'BX-2024-0820-01', N'2', N'40001', N'除颤仪自检提示电池寿命耗尽，无法充电', N'配件老化', N'Urgent', N'Done', N'1', N'更换原装锂电池', N'1600.00', N'2.00', N'2024-08-20 09:00:00.000', N'2024-08-20 11:00:00.000', N'Completed', N'急救设备，即刻维修', N'2024-08-20 09:10:00.000', NULL, N'电气故障', N'0.96', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'40002', N'40003', N'BX-2025-1115-02', N'4', N'40002', N'透析机自检报电导率越限报警', N'传感器故障', N'Normal', N'Done', N'3', N'拆洗电导池，更换电导率传感器探头并定标', N'2500.00', N'4.50', N'2025-11-15 10:30:00.000', N'2025-11-15 15:00:00.000', N'Completed', N'患者上机前排查出故障', N'2025-11-15 10:35:00.000', NULL, N'传感器故障', N'0.94', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'40003', N'40004', N'BX-2026-0710-01', N'5', N'40002', N'水处理系统产水压力低，初级滤芯堵塞', N'耗材堵塞', N'Urgent', N'InProgress', N'2', NULL, NULL, NULL, N'2026-07-10 08:20:00.000', NULL, N'InProgress', N'已切至备用管路，正在更换耗材', N'2026-07-10 08:25:00.000', NULL, N'机械故障', N'0.88', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'50001', N'50001', N'BX-2024-1110-01', N'2', N'50001', N'机器人左侧膝关节电机异响且卡顿', N'机械故障', N'Urgent', N'Done', N'3', N'更换左侧伺服电机及减速齿轮组', N'8500.00', N'6.00', N'2024-11-10 09:00:00.000', N'2024-11-10 15:00:00.000', N'Completed', N'特种设备，需原厂工程师指导', N'2024-11-10 09:10:00.000', NULL, N'机械故障', N'0.97', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'50002', N'50003', N'BX-2025-0605-02', N'1', N'50002', N'服务器前面板报黄灯，阵列显示一块硬盘离线', N'硬件故障', N'Urgent', N'Done', N'4', N'热插拔更换同型号SAS硬盘并重建RAID 5阵列', N'2200.00', N'1.00', N'2025-06-05 10:30:00.000', N'2025-06-05 11:30:00.000', N'Completed', N'核心数据库，快速响应未造成停机', N'2025-06-05 10:35:00.000', NULL, N'硬件故障', N'0.99', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'50003', N'50004', N'BX-2026-0312-01', N'4', N'50002', N'交换机光口模块频繁掉线，链路不稳定', N'电气故障', N'Normal', N'InProgress', N'1', NULL, NULL, NULL, N'2026-03-12 08:20:00.000', NULL, N'InProgress', N'已订购新万兆光模块备件', N'2026-03-12 08:25:00.000', NULL, N'电气故障', N'0.85', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'60001', N'60001', N'BX-2026-0218-01', N'3', N'60001', N'生化分析仪加样针堵塞，系统持续报液面探测错误', N'耗材堵塞', N'Urgent', N'Done', N'2', N'疏通加样针，更换特氟龙管路并重新执行液路排空定标', N'850.00', N'3.50', N'2026-02-18 08:30:00.000', N'2026-02-18 12:00:00.000', N'Completed', N'影响大批早高峰抽血标本出库，作紧急处理', N'2026-02-18 08:35:00.000', NULL, N'机械故障', N'0.92', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'60002', N'60002', N'BX-2026-0405-02', N'3', N'60001', N'免疫发光仪冷藏仓温度报警，当前监控显示12度（要求2-8度）', N'硬件故障', N'Urgent', N'Done', N'1', N'更换冷凝器半导体制冷片，重填导热硅脂并清洁散热风扇', N'1200.00', N'5.00', N'2026-04-05 14:00:00.000', N'2026-04-05 19:00:00.000', N'Completed', N'仓内大批昂贵试剂有失效风险，科室高度重视，已优先抢修', N'2026-04-05 14:10:00.000', NULL, N'硬件故障', N'0.88', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'60003', N'40002', N'BX-2026-0520-01', N'2', N'40001', N'心肺复苏机胸外按压吸盘漏气，机器提示按压深度不足', N'配件老化', N'Urgent', N'Done', N'4', N'更换一次性硅胶吸盘及内部负压泵密封圈', N'680.00', N'1.50', N'2026-05-20 10:00:00.000', N'2026-05-20 11:30:00.000', N'Completed', N'急救车早班日常巡检时提前发现隐患，未在施救时发生', N'2026-05-20 10:05:00.000', NULL, N'机械故障', N'0.95', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'60004', N'50004', N'BX-2026-0802-01', N'4', N'50002', N'机房动环监控报警，核心交换机风扇模块停转，机箱内部温度超限', N'硬件故障', N'Urgent', N'Done', N'1', N'不停机热插拔更换冗余风扇模块组', N'1500.00', N'0.50', N'2026-08-02 09:15:00.000', N'2026-08-02 09:45:00.000', N'Completed', N'响应极速，网络未中断，隐患解除', N'2026-08-02 09:20:00.000', NULL, N'硬件故障', N'0.98', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'60005', N'60001', N'BX-2026-0810-02', N'3', N'60001', N'生化仪光源灯泡亮度衰减，早间水空白及吸光度定标频繁失败', N'配件老化', N'Normal', N'InProgress', N'2', NULL, NULL, NULL, N'2026-08-10 16:30:00.000', NULL, N'InProgress', N'已订购原厂12V 50W特种卤素灯泡，等待库房到货后上机更换', N'2026-08-10 16:35:00.000', NULL, N'电气故障', N'0.90', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'70001', N'70001', N'BX-2026-0310-01', N'1', N'70001', N'MRI水冷机报警，液氦挥发率异常升高，冷头发出异响', N'硬件故障', N'Urgent', N'Done', N'2', N'原厂工程师上门，更换冷头(Cold Head)并补充高纯度液氦', N'125000.00', N'48.00', N'2026-03-10 08:15:00.000', N'2026-03-12 09:30:00.000', N'Completed', N'大型设备一级报警，已第一时间停机保压', N'2026-03-10 08:20:00.000', NULL, N'机械故障', N'0.96', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'70002', N'70002', N'BX-2026-0622-02', N'2', N'70001', N'CT扫描过程中报错，提示球管打火，曝光中断', N'配件老化', N'Urgent', N'Done', N'3', N'球管曝光次数已达上限，更换全新液态金属轴承球管', N'850000.00', N'16.00', N'2026-06-22 14:00:00.000', N'2026-06-23 10:00:00.000', N'Completed', N'核心高值耗材更换，走紧急采购审批流程', N'2026-06-22 14:10:00.000', NULL, N'电气故障', N'0.94', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'70003', N'40001', N'BX-2026-0805-01', N'2', N'40001', N'除颤仪电极导联线外皮破损，心电波形干扰大', N'配件老化', N'Normal', N'Done', N'1', N'更换全套原装心电导联线', N'1200.00', N'1.00', N'2026-08-05 09:30:00.000', N'2026-08-05 10:30:00.000', N'Completed', N'耗材日常磨损更换', N'2026-08-05 09:35:00.000', NULL, N'配件老化', N'0.91', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'70004', N'50003', N'BX-2026-0814-01', N'4', N'50002', N'HIS服务器B网卡指示灯不亮，内网传输丢包率达30%', N'硬件故障', N'Urgent', N'InProgress', N'3', NULL, NULL, NULL, N'2026-08-14 08:30:00.000', NULL, N'InProgress', N'网络中心已临时切至备用链路，正在排查光模块及网卡', N'2026-08-14 08:35:00.000', NULL, N'硬件故障', N'0.89', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'70005', N'60001', N'BX-2026-0814-02', N'3', N'60001', N'生化分析仪样本条码扫描器无法识读试管条码', N'传感器故障', N'Normal', N'Pending', NULL, NULL, NULL, NULL, N'2026-08-14 13:10:00.000', NULL, N'Pending', N'操作员已改为手工录入条码，请工程师尽快检修', N'2026-08-14 13:15:00.000', NULL, N'传感器故障', N'0.85', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'80001', N'80001', N'BX-2026-0225-01', N'2', N'80001', N'麻醉机开机自检无法通过，提示氧电池失效，氧浓度监测为0', N'传感器故障', N'Urgent', N'Done', N'3', N'更换原装氧气传感器（氧电池）并重新定标', N'1800.00', N'2.00', N'2026-02-25 07:30:00.000', N'2026-02-25 09:30:00.000', N'Completed', N'首台手术前排查出故障，已紧急更换备件，未延误手术', N'2026-02-25 07:35:00.000', NULL, N'传感器故障', N'0.98', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'80002', N'80002', N'BX-2026-0512-02', N'3', N'80002', N'呼吸机在使用过程中频繁报“潮气量偏低”和“管路漏气”', N'配件老化', N'Urgent', N'Done', N'4', N'排查为呼气端膜片老化破损，更换全新呼气阀及硅胶膜片', N'3500.00', N'1.50', N'2026-05-12 14:15:00.000', N'2026-05-12 15:45:00.000', N'Completed', N'患者已临时更换备用呼吸机。故障机维修后经模拟肺测试正常。', N'2026-05-12 14:20:00.000', NULL, N'机械故障', N'0.91', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'80003', N'80003', N'BX-2026-0720-01', N'1', N'80002', N'ECMO主机提示内置UPS电池寿命不足，需进行预防性更换', N'配件老化', N'Normal', N'Done', N'2', N'更换ECMO主机内置两块原装长效蓄电池', N'5500.00', N'3.00', N'2026-07-20 09:00:00.000', N'2026-07-20 12:00:00.000', N'Completed', N'生命支持设备预防性维护，确保转运过程中断电续航', N'2026-07-20 09:05:00.000', NULL, N'电气故障', N'0.95', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'80004', N'80001', N'BX-2026-0814-01', N'2', N'80001', N'麻醉机废气排放系统(AGSS)负压不足，吸引管路似乎有堵塞', N'耗材堵塞', N'Normal', N'Pending', NULL, NULL, NULL, NULL, N'2026-08-14 10:15:00.000', NULL, N'Pending', N'今天上午第三台手术结束后发现，目前不影响主体功能，请空闲时检修', N'2026-08-14 10:20:00.000', NULL, N'机械故障', N'0.88', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'100001', N'100001', N'BX-2026-0305-01', N'2', N'100001', N'GE彩超操作面板轨迹球滚动不灵敏，光标漂移严重', N'机械故障', N'Normal', N'Done', N'3', N'拆解操作面板，清理轨迹球内部耦合剂结晶，并更换光电感应轴承', N'650.00', N'1.00', N'2026-03-05 12:30:00.000', N'2026-03-05 13:30:00.000', N'Completed', N'医生手部耦合剂未擦净导致，已提醒科室注意', N'2026-03-05 12:35:00.000', NULL, N'机械故障', N'0.90', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'100002', N'100002', N'BX-2026-0618-02', N'1', N'100001', N'心脏彩超相控阵探头(S5-1)根部线缆外皮开裂，偶有伪影', N'配件老化', N'Urgent', N'Done', N'2', N'为安全起见，更换全新原装相控阵探头并进行系统校准', N'68000.00', N'24.00', N'2026-06-18 09:15:00.000', N'2026-06-19 10:00:00.000', N'Completed', N'昂贵配件，已走高值耗材审批流程。', N'2026-06-18 09:20:00.000', NULL, N'电气故障', N'0.88', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'100003', N'100003', N'BX-2026-0814-02', N'3', N'100001', N'迈瑞便携彩超电池充不进电，拔掉电源线直接关机', N'硬件故障', N'Normal', N'Assigned', N'6', NULL, NULL, NULL, N'2026-08-14 14:10:00.000', NULL, N'Pending', N'影响ICU床旁推车查房，请工程师带备用电池来测试', N'2026-08-14 14:15:00.000', NULL, N'电气故障', N'0.94', NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'100004', N'6', N'BX-2026-0814-08', N'1', N'8', N'11', N'', N'Normal', N'Pending', NULL, NULL, NULL, NULL, N'2026-08-14 07:05:26.557', NULL, N'Pending', NULL, N'2026-08-14 07:05:26.557', N'E:\C Project\hospital-equipment-system\HospitalEquipmentSystem.UI\bin\Debug\Uploads\Photos\20260814150458.jpg', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'100005', N'2', N'BX-2026-0814-09', N'3', N'2', N'qqq', N'', N'Urgent', N'Assigned', N'6', NULL, NULL, NULL, N'2026-08-14 07:09:47.540', NULL, N'Pending', NULL, N'2026-08-14 07:09:47.540', N'E:\C Project\hospital-equipment-system\HospitalEquipmentSystem.UI\bin\Debug\Uploads\Photos\20260814150936_4612172c.png', NULL, NULL, NULL)
GO

INSERT INTO [dbo].[MaintenanceRecords] ([RecordId], [EquipmentId], [RepairNo], [ReporterId], [ReportDeptId], [FaultDesc], [FaultType], [Urgency], [ProgressStage], [AssignedTo], [RepairResult], [RepairCost], [DowntimeHours], [ReportTime], [CompleteTime], [Status], [Remarks], [CreatedAt], [PhotoPath], [AiFaultType], [AiConfidence], [RejectReason]) VALUES (N'100006', N'38', N'BX-2026-0814-10', N'3', N'2', N'电击枪故障', N'传感器故障', N'Urgent', N'Done', N'6', N'医用氧气传感器(氧电池) x1、高精度电导率传感器 x1、麻醉机流量传感器 x1，物料总金额 ¥6,100.00', N'6100.00', N'9.00', N'2026-08-14 07:13:35.197', N'2026-08-14 07:17:10.137', N'Completed', NULL, N'2026-08-14 07:13:35.197', N'E:\C Project\hospital-equipment-system\HospitalEquipmentSystem.UI\bin\Debug\Uploads\Photos\20260814151318_c310277e.png', N'传感器故障', N'0.85', NULL)
GO

SET IDENTITY_INSERT [dbo].[MaintenanceRecords] OFF
GO


-- ----------------------------
-- Table structure for Materials
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Materials]') AND type IN ('U'))
	DROP TABLE [dbo].[Materials]
GO

-- 维修备件/耗材字典表：预置各类设备常用备件的名称、所属分类、常见故障类型、
-- 单价及默认用量，供维修工单填单时快速选用（对应 MaintenanceMaterials 明细）。
CREATE TABLE [dbo].[Materials] (
  [MaterialId] int  IDENTITY(1,1) NOT NULL,  -- 备件主键，自增
  [MaterialName] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,  -- 备件/耗材名称
  [CategoryId] int  NOT NULL,  -- 适用的设备分类ID，外键 -> EquipmentCategories.CategoryId
  [FaultType] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,  -- 常对应的故障类型，用于报修时按故障类型推荐备件
  [UnitPrice] decimal(10,2)  NOT NULL,  -- 参考单价，须 > 0（见 CK_Materials_UnitPrice 约束）
  [DefaultQuantity] int  NOT NULL,  -- 默认使用数量，须 > 0（见 CK_Materials_DefaultQuantity 约束）
  [Unit] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,  -- 计量单位，如“个”“套”“根”
  [Description] nvarchar(300) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 备件说明/用途描述
  [IsActive] bit DEFAULT 1 NOT NULL,  -- 是否启用，默认1
  [CreatedAt] datetime DEFAULT getdate() NOT NULL  -- 创建时间，默认当前时间
)
GO

ALTER TABLE [dbo].[Materials] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Materials
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Materials] ON
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'245', N'球管', N'6', N'机械故障', N'85000.00', N'1', N'个', N'CT设备核心部件，用于产生X射线', N'1', N'2026-08-13 03:39:05.473')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'246', N'准直器', N'6', N'机械故障', N'12000.00', N'1', N'个', N'控制X射线束宽度的装置', N'1', N'2026-08-13 03:39:05.473')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'247', N'高压发生器', N'6', N'机械故障', N'45000.00', N'1', N'台', N'为球管提供高压电源', N'1', N'2026-08-13 03:39:05.473')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'248', N'轴承组件', N'6', N'机械故障', N'8000.00', N'1', N'套', N'旋转机架轴承', N'1', N'2026-08-13 03:39:05.473')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'249', N'传动皮带', N'6', N'机械故障', N'1500.00', N'1', N'条', N'机架旋转传动皮带', N'1', N'2026-08-13 03:39:05.473')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'250', N'系统重装服务', N'6', N'软件故障', N'3000.00', N'1', N'次', N'操作系统及CT控制软件重装', N'1', N'2026-08-13 03:39:05.477')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'251', N'图像重建模块授权', N'6', N'软件故障', N'15000.00', N'1', N'套', N'图像重建算法许可', N'1', N'2026-08-13 03:39:05.477')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'252', N'电源模块', N'6', N'电气故障', N'12000.00', N'1', N'个', N'主电源供电模块', N'1', N'2026-08-13 03:39:05.477')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'253', N'控制板', N'6', N'电气故障', N'25000.00', N'1', N'块', N'主控制电路板', N'1', N'2026-08-13 03:39:05.477')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'254', N'高压电缆', N'6', N'电气故障', N'3500.00', N'1', N'根', N'高压发生器到球管的连接电缆', N'1', N'2026-08-13 03:39:05.477')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'255', N'超声探头', N'7', N'机械故障', N'35000.00', N'1', N'个', N'超声扫描探头', N'1', N'2026-08-13 03:39:05.480')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'256', N'探头线缆', N'7', N'机械故障', N'2000.00', N'1', N'根', N'探头连接线缆', N'1', N'2026-08-13 03:39:05.480')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'257', N'显示器支架', N'7', N'机械故障', N'800.00', N'1', N'个', N'设备显示器固定支架', N'1', N'2026-08-13 03:39:05.480')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'258', N'超声软件升级', N'7', N'软件故障', N'5000.00', N'1', N'次', N'超声成像软件版本升级', N'1', N'2026-08-13 03:39:05.480')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'259', N'图像存储扩容', N'7', N'软件故障', N'2000.00', N'1', N'次', N'存储空间扩容及配置', N'1', N'2026-08-13 03:39:05.480')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'260', N'电源适配器', N'7', N'电气故障', N'1500.00', N'1', N'个', N'设备电源适配器', N'1', N'2026-08-13 03:39:05.480')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'261', N'主板', N'7', N'电气故障', N'8000.00', N'1', N'块', N'超声设备主板', N'1', N'2026-08-13 03:39:05.480')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'262', N'散热风扇', N'7', N'电气故障', N'300.00', N'2', N'个', N'设备散热风扇', N'1', N'2026-08-13 03:39:05.480')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'263', N'呼吸管路', N'8', N'机械故障', N'300.00', N'2', N'套', N'一次性呼吸管路', N'1', N'2026-08-13 03:39:05.483')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'264', N'面罩', N'8', N'机械故障', N'500.00', N'1', N'个', N'患者呼吸面罩', N'1', N'2026-08-13 03:39:05.483')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'265', N'湿化罐', N'8', N'机械故障', N'200.00', N'1', N'个', N'加温湿化罐', N'1', N'2026-08-13 03:39:05.483')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'266', N'涡轮风机', N'8', N'机械故障', N'8000.00', N'1', N'个', N'呼吸机涡轮风机', N'1', N'2026-08-13 03:39:05.483')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'267', N'呼气阀', N'8', N'机械故障', N'1500.00', N'1', N'个', N'呼气阀组件', N'1', N'2026-08-13 03:39:05.483')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'268', N'固件升级', N'8', N'软件故障', N'1000.00', N'1', N'次', N'呼吸机固件升级', N'1', N'2026-08-13 03:39:05.483')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'269', N'参数校准服务', N'8', N'软件故障', N'2000.00', N'1', N'次', N'传感器及参数校准', N'1', N'2026-08-13 03:39:05.483')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'270', N'氧电池传感器', N'8', N'电气故障', N'800.00', N'1', N'个', N'氧气浓度传感器', N'1', N'2026-08-13 03:39:05.483')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'271', N'流量传感器', N'8', N'电气故障', N'1200.00', N'1', N'个', N'气体流量传感器', N'1', N'2026-08-13 03:39:05.483')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'272', N'电源板', N'8', N'电气故障', N'3000.00', N'1', N'块', N'呼吸机电源板', N'1', N'2026-08-13 03:39:05.483')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'273', N'除颤电极片', N'9', N'机械故障', N'300.00', N'2', N'副', N'一次性除颤电极片', N'1', N'2026-08-13 03:39:05.487')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'274', N'电池组', N'9', N'机械故障', N'2500.00', N'1', N'组', N'除颤仪内置电池组', N'1', N'2026-08-13 03:39:05.487')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'275', N'除颤软件升级', N'9', N'软件故障', N'3000.00', N'1', N'次', N'除颤仪控制软件升级', N'1', N'2026-08-13 03:39:05.487')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'276', N'高压电容', N'9', N'电气故障', N'5000.00', N'1', N'个', N'除颤高压充放电电容', N'1', N'2026-08-13 03:39:05.487')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'277', N'充电板', N'9', N'电气故障', N'3500.00', N'1', N'块', N'充电控制电路板', N'1', N'2026-08-13 03:39:05.487')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'278', N'液晶屏总成', N'3', N'机械故障', N'1200.00', N'1', N'个', N'监护仪液晶显示屏', N'1', N'2026-08-13 03:39:05.490')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'279', N'屏幕排线', N'3', N'机械故障', N'45.00', N'1', N'根', N'屏幕连接排线', N'1', N'2026-08-13 03:39:05.490')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'280', N'外壳组件', N'3', N'机械故障', N'300.00', N'1', N'套', N'监护仪外壳及支架', N'1', N'2026-08-13 03:39:05.490')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'281', N'按键面板', N'3', N'机械故障', N'200.00', N'1', N'块', N'前面板按键组件', N'1', N'2026-08-13 03:39:05.490')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'282', N'系统升级服务', N'3', N'软件故障', N'1500.00', N'1', N'次', N'监护仪系统软件升级', N'1', N'2026-08-13 03:39:05.490')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'283', N'参数配置服务', N'3', N'软件故障', N'800.00', N'1', N'次', N'监护参数配置与校准', N'1', N'2026-08-13 03:39:05.490')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'284', N'电源模块', N'3', N'电气故障', N'350.00', N'1', N'个', N'监护仪电源适配器', N'1', N'2026-08-13 03:39:05.490')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'285', N'血氧探头', N'3', N'电气故障', N'600.00', N'1', N'个', N'血氧饱和度传感器', N'1', N'2026-08-13 03:39:05.490')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'286', N'心电导联线', N'3', N'电气故障', N'200.00', N'1', N'根', N'心电导联线缆', N'1', N'2026-08-13 03:39:05.490')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'287', N'无创血压袖带', N'3', N'电气故障', N'150.00', N'1', N'个', N'血压测量袖带', N'1', N'2026-08-13 03:39:05.490')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'288', N'主板', N'3', N'电气故障', N'5000.00', N'1', N'块', N'监护仪主板', N'1', N'2026-08-13 03:39:05.490')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'289', N'进样针', N'4', N'机械故障', N'800.00', N'1', N'根', N'自动进样器进样针', N'1', N'2026-08-13 03:39:05.493')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'290', N'比色杯', N'4', N'机械故障', N'50.00', N'5', N'个', N'生化分析仪比色杯', N'1', N'2026-08-13 03:39:05.493')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'291', N'蠕动泵管', N'4', N'机械故障', N'100.00', N'2', N'根', N'液体输送蠕动泵管', N'1', N'2026-08-13 03:39:05.493')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'292', N'分析软件升级', N'4', N'软件故障', N'5000.00', N'1', N'次', N'检验分析软件版本升级', N'1', N'2026-08-13 03:39:05.493')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'293', N'LIS接口调试', N'4', N'软件故障', N'3000.00', N'1', N'次', N'检验信息系统接口调试', N'1', N'2026-08-13 03:39:05.493')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'294', N'光源灯', N'4', N'电气故障', N'1500.00', N'1', N'个', N'光学检测光源灯', N'1', N'2026-08-13 03:39:05.497')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'295', N'光电传感器', N'4', N'电气故障', N'2000.00', N'1', N'个', N'光学检测传感器', N'1', N'2026-08-13 03:39:05.497')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'296', N'电源板', N'4', N'电气故障', N'4000.00', N'1', N'块', N'设备电源板', N'1', N'2026-08-13 03:39:05.497')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'297', N'温控模块', N'4', N'电气故障', N'3000.00', N'1', N'个', N'反应盘温控模块', N'1', N'2026-08-13 03:39:05.497')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'298', N'高频电刀头', N'5', N'机械故障', N'200.00', N'2', N'个', N'高频电刀手术电极', N'1', N'2026-08-13 03:39:05.497')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'299', N'负极板', N'5', N'机械故障', N'50.00', N'2', N'片', N'高频电刀负极板', N'1', N'2026-08-13 03:39:05.497')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'300', N'脚踏开关', N'5', N'机械故障', N'800.00', N'1', N'个', N'高频电刀脚踏控制器', N'1', N'2026-08-13 03:39:05.497')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'301', N'电刀参数校准', N'5', N'软件故障', N'1500.00', N'1', N'次', N'电刀输出功率校准', N'1', N'2026-08-13 03:39:05.500')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'302', N'电源模块', N'5', N'电气故障', N'2000.00', N'1', N'个', N'高频电刀电源模块', N'1', N'2026-08-13 03:39:05.500')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'303', N'高频发生器', N'5', N'电气故障', N'8000.00', N'1', N'个', N'高频电刀发生器', N'1', N'2026-08-13 03:39:05.500')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'304', N'控制主板', N'5', N'电气故障', N'3500.00', N'1', N'块', N'高频电刀控制主板', N'1', N'2026-08-13 03:39:05.500')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'9255', N'12导联心电线缆', N'3', N'电气故障', N'850.00', N'1', N'套', N'心电图机通用导联线', N'1', N'2022-01-01 10:00:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'9256', N'医用高级润滑脂', N'1', N'机械故障', N'150.00', N'1', N'罐', N'用于牵引床、康复机等机械部件润滑', N'1', N'2023-05-12 11:00:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'9257', N'生化仪加样针', N'4', N'机械故障', N'3500.00', N'1', N'根', N'全自动生化分析仪高精度加样针', N'1', N'2024-07-20 09:30:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'9258', N'脑电电极帽', N'3', N'电气故障', N'1200.00', N'1', N'顶', N'脑电图仪专用信号采集帽', N'1', N'2025-01-15 14:20:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'20001', N'发光仪进样皮带', N'4', N'机械故障', N'1200.00', N'1', N'条', N'免疫分析仪传输履带', N'1', N'2023-02-01 10:00:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'20002', N'高精度定位光栅', N'1', N'电气故障', N'850.00', N'1', N'组', N'发药机机械臂测距定位元件', N'1', N'2024-04-15 11:00:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'20003', N'医用压缩机继电器', N'7', N'电气故障', N'250.00', N'1', N'个', N'医用冰箱温控启动保护元件', N'1', N'2025-06-20 09:30:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'30001', N'麻醉机流量传感器', N'8', N'传感器故障', N'1800.00', N'1', N'个', N'呼吸回路气体流量监测', N'1', N'2023-05-01 10:00:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'30002', N'灭菌器耐高温密封圈', N'1', N'机械故障', N'850.00', N'1', N'条', N'高压蒸汽灭菌锅门密封胶条', N'1', N'2024-06-15 11:00:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'30003', N'超声清洗机不锈钢加热管', N'7', N'电气故障', N'320.00', N'1', N'根', N'清洗液恒温加热管', N'1', N'2025-08-20 09:30:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'40001', N'医疗设备专用大容量锂电池组', N'7', N'配件老化', N'1600.00', N'1', N'块', N'适用于除颤仪、监护仪备用电源', N'1', N'2023-01-10 10:00:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'40002', N'高精度电导率传感器', N'8', N'传感器故障', N'2500.00', N'1', N'套', N'血透机透析液浓度监测', N'1', N'2024-02-15 11:00:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'40003', N'5微米PP熔喷初效滤芯', N'7', N'耗材堵塞', N'120.00', N'1', N'根', N'水处理系统前置杂质过滤', N'1', N'2025-03-20 09:30:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'50001', N'医用精密伺服电机', N'1', N'机械故障', N'8500.00', N'1', N'套', N'外骨骼机器人关节动力核心配件', N'1', N'2023-08-10 10:00:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'50002', N'2.4TB 10K RPM SAS 企业级硬盘', N'9', N'硬件故障', N'2200.00', N'1', N'块', N'服务器存储阵列替换件', N'1', N'2024-03-15 11:00:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'50003', N'10G SFP+ 万兆单模光模块', N'9', N'电气故障', N'450.00', N'1', N'个', N'核心交换机光纤链路收发器', N'1', N'2025-05-20 09:30:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'60001', N'特氟龙液流加样管', N'7', N'耗材堵塞', N'450.00', N'1', N'根', N'检验设备吸样及排废管路', N'1', N'2024-02-10 10:00:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'60002', N'医疗设备半导体制冷片 12V 10A', N'8', N'硬件故障', N'800.00', N'1', N'片', N'用于发光仪等试剂仓冷藏模块', N'1', N'2025-01-20 09:30:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'70001', N'MRI专用二级冷头 (Cold Head)', N'8', N'硬件故障', N'125000.00', N'1', N'套', N'超导磁体积氦气冷凝核心部件', N'1', N'2026-01-10 10:00:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'70002', N'Revolution CT 液态金属轴承球管', N'2', N'配件老化', N'850000.00', N'1', N'只', N'CT核心X射线发生源', N'1', N'2026-02-15 11:00:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'80001', N'医用氧气传感器(氧电池)', N'8', N'传感器故障', N'1800.00', N'1', N'个', N'麻醉机/呼吸机氧浓度监测核心耗材', N'1', N'2025-01-15 10:00:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'80002', N'呼吸机一体化可重复使用呼气阀', N'8', N'配件老化', N'3500.00', N'1', N'套', N'含加热丝及高精度硅胶膜片', N'1', N'2025-06-20 11:00:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'80003', N'ECMO主机专用医疗级UPS蓄电池', N'7', N'配件老化', N'2750.00', N'2', N'块', N'保障ECMO运转生命线', N'1', N'2026-03-10 09:30:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'100001', N'超声操作面板轨迹球组件', N'7', N'机械故障', N'650.00', N'1', N'套', N'含光电编码器及滚轮', N'1', N'2025-01-10 10:00:00.000')
GO

INSERT INTO [dbo].[Materials] ([MaterialId], [MaterialName], [CategoryId], [FaultType], [UnitPrice], [DefaultQuantity], [Unit], [Description], [IsActive], [CreatedAt]) VALUES (N'100002', N'飞利浦 S5-1 相控阵探头', N'2', N'硬件故障', N'68000.00', N'1', N'把', N'心脏专科核心探头', N'1', N'2025-05-15 11:00:00.000')
GO

SET IDENTITY_INSERT [dbo].[Materials] OFF
GO


-- ----------------------------
-- Table structure for OperationLogs
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[OperationLogs]') AND type IN ('U'))
	DROP TABLE [dbo].[OperationLogs]
GO

-- 系统操作审计日志表：记录用户在系统中的关键操作行为（登录、增删改、审批等），
-- 用于操作留痕与安全审计追溯。
CREATE TABLE [dbo].[OperationLogs] (
  [LogId] bigint  IDENTITY(1,1) NOT NULL,  -- 日志主键，自增（bigint，量大）
  [UserId] int  NULL,  -- 操作人用户ID，外键 -> Users.UserId
  [ActionType] nvarchar(30) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,  -- 操作类型，如 Login/Create/Update/Approve/Delete
  [TargetTable] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 被操作的目标数据表名
  [TargetId] int  NULL,  -- 被操作记录在目标表中的主键值
  [Detail] nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 操作详情描述文本
  [CreatedAt] datetime DEFAULT getdate() NOT NULL  -- 操作发生时间，默认当前时间
)
GO

ALTER TABLE [dbo].[OperationLogs] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of OperationLogs
-- ----------------------------
SET IDENTITY_INSERT [dbo].[OperationLogs] ON
GO

INSERT INTO [dbo].[OperationLogs] ([LogId], [UserId], [ActionType], [TargetTable], [TargetId], [Detail], [CreatedAt]) VALUES (N'1', N'1', N'Login', N'Users', N'1', N'管理员登录系统', N'2026-07-28 08:30:00.000')
GO

INSERT INTO [dbo].[OperationLogs] ([LogId], [UserId], [ActionType], [TargetTable], [TargetId], [Detail], [CreatedAt]) VALUES (N'2', N'1', N'Create', N'Equipment', N'8', N'新增设备: 免疫分析仪 MY-2026-088', N'2026-07-28 08:35:00.000')
GO

INSERT INTO [dbo].[OperationLogs] ([LogId], [UserId], [ActionType], [TargetTable], [TargetId], [Detail], [CreatedAt]) VALUES (N'3', N'1', N'Create', N'InboundRecords', N'5', N'新增入库单: RK-2026-07-005', N'2026-07-28 08:36:00.000')
GO

INSERT INTO [dbo].[OperationLogs] ([LogId], [UserId], [ActionType], [TargetTable], [TargetId], [Detail], [CreatedAt]) VALUES (N'4', N'2', N'Approve', N'BorrowRecords', N'1', N'审批借用: 生化分析仪 JY-2026-045', N'2026-07-28 09:00:00.000')
GO

INSERT INTO [dbo].[OperationLogs] ([LogId], [UserId], [ActionType], [TargetTable], [TargetId], [Detail], [CreatedAt]) VALUES (N'5', N'2', N'Update', N'Equipment', N'5', N'更新设备状态: 生化分析仪 -> Borrowed', N'2026-07-28 09:01:00.000')
GO

INSERT INTO [dbo].[OperationLogs] ([LogId], [UserId], [ActionType], [TargetTable], [TargetId], [Detail], [CreatedAt]) VALUES (N'6', N'5', N'Update', N'MaintenanceRecords', N'2', N'完成维修: 心电监护仪 XD-2026-022', N'2026-07-15 10:30:00.000')
GO

INSERT INTO [dbo].[OperationLogs] ([LogId], [UserId], [ActionType], [TargetTable], [TargetId], [Detail], [CreatedAt]) VALUES (N'7', N'5', N'Update', N'Equipment', N'3', N'设备维修完成恢复使用: 心电监护仪', N'2026-07-15 10:31:00.000')
GO

INSERT INTO [dbo].[OperationLogs] ([LogId], [UserId], [ActionType], [TargetTable], [TargetId], [Detail], [CreatedAt]) VALUES (N'8', N'4', N'Update', N'BorrowRecords', N'2', N'归还设备: 呼吸机 HX-2024-112', N'2026-07-28 16:00:00.000')
GO

INSERT INTO [dbo].[OperationLogs] ([LogId], [UserId], [ActionType], [TargetTable], [TargetId], [Detail], [CreatedAt]) VALUES (N'9', N'4', N'Update', N'Equipment', N'7', N'设备状态更新: 呼吸机 -> Idle', N'2026-07-28 16:01:00.000')
GO

INSERT INTO [dbo].[OperationLogs] ([LogId], [UserId], [ActionType], [TargetTable], [TargetId], [Detail], [CreatedAt]) VALUES (N'10', N'3', N'Create', N'MaintenanceRecords', N'1', N'申报故障: 呼吸机 HX-2025-088 屏幕无显示', N'2026-07-15 14:30:00.000')
GO

INSERT INTO [dbo].[OperationLogs] ([LogId], [UserId], [ActionType], [TargetTable], [TargetId], [Detail], [CreatedAt]) VALUES (N'11', N'2', N'Approve', N'ScrapRecords', N'4', N'审批报废: 超声诊断仪 CS-2019-034', N'2026-07-10 10:00:00.000')
GO

INSERT INTO [dbo].[OperationLogs] ([LogId], [UserId], [ActionType], [TargetTable], [TargetId], [Detail], [CreatedAt]) VALUES (N'12', N'1', N'Delete', N'Equipment', N'11', N'软删除设备: X光机 XG-2018-022', N'2026-07-20 15:00:00.000')
GO

SET IDENTITY_INSERT [dbo].[OperationLogs] OFF
GO


-- ----------------------------
-- Table structure for SmsCodes
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[SmsCodes]') AND type IN ('U'))
	DROP TABLE [dbo].[SmsCodes]
GO

-- 短信验证码表：用于手机号登录/找回密码等场景的短信验证码校验，
-- Used 标记验证码是否已被使用，ExpireAt 控制验证码有效期。
CREATE TABLE [dbo].[SmsCodes] (
  [Id] int  IDENTITY(1,1) NOT NULL,  -- 主键，自增
  [Phone] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,  -- 接收验证码的手机号
  [Code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,  -- 验证码内容
  [ExpireAt] datetime  NOT NULL,  -- 验证码过期时间
  [Used] bit DEFAULT 0 NOT NULL,  -- 是否已被使用，默认0（未使用）
  [CreatedAt] datetime DEFAULT getdate() NOT NULL  -- 发送/创建时间，默认当前时间
)
GO

ALTER TABLE [dbo].[SmsCodes] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of SmsCodes
-- ----------------------------
SET IDENTITY_INSERT [dbo].[SmsCodes] ON
GO

SET IDENTITY_INSERT [dbo].[SmsCodes] OFF
GO


-- ----------------------------
-- Table structure for Suppliers
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Suppliers]') AND type IN ('U'))
	DROP TABLE [dbo].[Suppliers]
GO

-- 设备供应商字典表：记录设备/物资供应商的基础资料及联系方式，
-- 与 Equipment、InboundRecords 关联，用于追溯设备采购来源。
CREATE TABLE [dbo].[Suppliers] (
  [SupplierId] int  IDENTITY(1,1) NOT NULL,  -- 供应商主键，自增
  [SupplierName] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,  -- 供应商名称
  [ContactPerson] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 联系人姓名
  [Phone] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 联系电话
  [IsActive] bit DEFAULT 1 NOT NULL,  -- 是否启用，默认1
  [CreatedAt] datetime DEFAULT getdate() NOT NULL,  -- 创建时间，默认当前时间
  [SupplierCode] nvarchar(30) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 供应商编码
  [Email] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 联系邮箱
  [Address] nvarchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 供应商地址
  [Website] nvarchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 供应商官网
  [Remark] nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL  -- 备注信息
)
GO

ALTER TABLE [dbo].[Suppliers] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Suppliers
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Suppliers] ON
GO

INSERT INTO [dbo].[Suppliers] ([SupplierId], [SupplierName], [ContactPerson], [Phone], [IsActive], [CreatedAt], [SupplierCode], [Email], [Address], [Website], [Remark]) VALUES (N'1', N'国药器械', N'赵经理', N'13800001111', N'1', N'2026-07-30 07:23:46.333', N'GY123456', N'', N'', N'', N'')
GO

INSERT INTO [dbo].[Suppliers] ([SupplierId], [SupplierName], [ContactPerson], [Phone], [IsActive], [CreatedAt], [SupplierCode], [Email], [Address], [Website], [Remark]) VALUES (N'2', N'西门子医疗', N'孙经理', N'13800003333', N'1', N'2026-07-30 07:23:46.333', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Suppliers] ([SupplierId], [SupplierName], [ContactPerson], [Phone], [IsActive], [CreatedAt], [SupplierCode], [Email], [Address], [Website], [Remark]) VALUES (N'3', N'迈瑞医疗', N'吴经理', N'13800005555', N'1', N'2026-07-30 07:23:46.333', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Suppliers] ([SupplierId], [SupplierName], [ContactPerson], [Phone], [IsActive], [CreatedAt], [SupplierCode], [Email], [Address], [Website], [Remark]) VALUES (N'4', N'飞利浦医疗', N'周经理', N'13800004444', N'1', N'2026-07-30 07:23:46.333', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [dbo].[Suppliers] ([SupplierId], [SupplierName], [ContactPerson], [Phone], [IsActive], [CreatedAt], [SupplierCode], [Email], [Address], [Website], [Remark]) VALUES (N'5', N'罗氏诊断', N'陈经理', N'13800006666', N'1', N'2026-07-30 07:23:46.333', N'LS1111', N'', N'', N'', N'')
GO

INSERT INTO [dbo].[Suppliers] ([SupplierId], [SupplierName], [ContactPerson], [Phone], [IsActive], [CreatedAt], [SupplierCode], [Email], [Address], [Website], [Remark]) VALUES (N'6', N'河西大药房', N'魏经理', N'15845541261', N'1', N'2026-08-05 06:57:40.477', N'HX123456', N'hexidayaofang@163.com', N'弗兰省常山市露露县', N'', N'测试')
GO

INSERT INTO [dbo].[Suppliers] ([SupplierId], [SupplierName], [ContactPerson], [Phone], [IsActive], [CreatedAt], [SupplierCode], [Email], [Address], [Website], [Remark]) VALUES (N'7', N'111', N'1', N'11', N'0', N'2026-08-07 08:10:01.990', N'11', N'1', N'1', N'1', N'1')
GO

INSERT INTO [dbo].[Suppliers] ([SupplierId], [SupplierName], [ContactPerson], [Phone], [IsActive], [CreatedAt], [SupplierCode], [Email], [Address], [Website], [Remark]) VALUES (N'8', N'22', N'2', N'', N'0', N'2026-08-07 08:28:07.747', N'2', N'', N'', N'', N'')
GO

INSERT INTO [dbo].[Suppliers] ([SupplierId], [SupplierName], [ContactPerson], [Phone], [IsActive], [CreatedAt], [SupplierCode], [Email], [Address], [Website], [Remark]) VALUES (N'9', N'魏什么', N'杨', N'12356985423', N'1', N'2026-08-14 01:35:58.873', N'WXS-21313', N'无', N'长沙', N'无', N'测试')
GO

SET IDENTITY_INSERT [dbo].[Suppliers] OFF
GO


-- ----------------------------
-- Table structure for Users
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type IN ('U'))
	DROP TABLE [dbo].[Users]
GO

-- 系统用户表：医院内使用本系统的人员账号，涵盖管理员(admin)、医生/护士(doctor)、
-- 维修工程师(repair)等角色，Role 字段区分权限，DeptId 关联所属科室。
CREATE TABLE [dbo].[Users] (
  [UserId] int  IDENTITY(1,1) NOT NULL,  -- 用户主键，自增
  [Username] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,  -- 登录用户名，唯一
  [PasswordHash] nvarchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,  -- 密码（生产环境应存储哈希值，示例数据中为明文，仅供演示）
  [RealName] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,  -- 真实姓名
  [Role] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT 'doctor' NOT NULL,  -- 角色：admin(管理员)/doctor(医护人员)/repair(维修工程师)等，默认 doctor
  [DeptId] int  NULL,  -- 所属科室ID，外键 -> Departments.DeptId
  [Phone] nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 联系电话
  [Title] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,  -- 职称/岗位，如“护士长”“维修工程师”
  [IsActive] bit DEFAULT 1 NOT NULL,  -- 账号是否启用，默认1
  [LastLoginAt] datetime  NULL,  -- 最后一次登录时间
  [CreatedAt] datetime DEFAULT getdate() NOT NULL,  -- 账号创建时间，默认当前时间
  [AvatarUrl] nvarchar(120) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL  -- 头像图片URL
)
GO

ALTER TABLE [dbo].[Users] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Users
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Users] ON
GO

INSERT INTO [dbo].[Users] ([UserId], [Username], [PasswordHash], [RealName], [Role], [DeptId], [Phone], [Title], [IsActive], [LastLoginAt], [CreatedAt], [AvatarUrl]) VALUES (N'1', N'admin', N'123456', N'系统管理员', N'admin', N'1', N'13800001001', N'系统工程师', N'1', N'2026-08-14 07:43:00.560', N'2026-07-30 07:23:45.703', N'https://bucket.054215.xyz/avatars/202608/cd40b59c776d4e9a842b69827bd7a643.jpg')
GO

INSERT INTO [dbo].[Users] ([UserId], [Username], [PasswordHash], [RealName], [Role], [DeptId], [Phone], [Title], [IsActive], [LastLoginAt], [CreatedAt], [AvatarUrl]) VALUES (N'2', N'zhangwei', N'123456', N'张伟', N'admin', N'1', N'13800001002', N'设备科主任', N'1', N'2026-08-14 07:01:01.327', N'2026-07-30 07:23:45.703', N'https://bucket.054215.xyz/HospitalEquipmentSystem/%E5%A4%B4%E5%83%8F/zhangwei.jpg')
GO

INSERT INTO [dbo].[Users] ([UserId], [Username], [PasswordHash], [RealName], [Role], [DeptId], [Phone], [Title], [IsActive], [LastLoginAt], [CreatedAt], [AvatarUrl]) VALUES (N'3', N'linurse', N'123456', N'李护士', N'doctor', N'2', N'13800002001', N'主管护师', N'1', N'2026-08-14 07:12:43.603', N'2026-07-30 07:23:45.703', N'https://bucket.054215.xyz/HospitalEquipmentSystem/%E5%A4%B4%E5%83%8F/linurse.jpg')
GO

INSERT INTO [dbo].[Users] ([UserId], [Username], [PasswordHash], [RealName], [Role], [DeptId], [Phone], [Title], [IsActive], [LastLoginAt], [CreatedAt], [AvatarUrl]) VALUES (N'4', N'wangfang', N'123456', N'王芳', N'doctor', N'3', N'13800003001', N'ICU护士长', N'1', N'2026-08-10 06:44:20.897', N'2026-07-30 07:23:45.703', N'https://bucket.054215.xyz/HospitalEquipmentSystem/%E5%A4%B4%E5%83%8F/wangfang.jpg')
GO

INSERT INTO [dbo].[Users] ([UserId], [Username], [PasswordHash], [RealName], [Role], [DeptId], [Phone], [Title], [IsActive], [LastLoginAt], [CreatedAt], [AvatarUrl]) VALUES (N'5', N'wanggong', N'123456', N'王工', N'repair', N'1', N'13800001003', N'维修工程师', N'1', N'2026-08-14 07:10:14.930', N'2026-07-30 07:23:45.703', N'https://bucket.054215.xyz/HospitalEquipmentSystem/%E5%A4%B4%E5%83%8F/wanggong.jpg')
GO

INSERT INTO [dbo].[Users] ([UserId], [Username], [PasswordHash], [RealName], [Role], [DeptId], [Phone], [Title], [IsActive], [LastLoginAt], [CreatedAt], [AvatarUrl]) VALUES (N'6', N'ligong', N'123456', N'李工', N'repair', N'2', N'111000011', N'维修工程师', N'1', N'2026-08-14 07:15:34.670', N'2026-07-31 08:42:29.687', N'https://bucket.054215.xyz/HospitalEquipmentSystem/%E5%A4%B4%E5%83%8F/ligong.jpg')
GO

INSERT INTO [dbo].[Users] ([UserId], [Username], [PasswordHash], [RealName], [Role], [DeptId], [Phone], [Title], [IsActive], [LastLoginAt], [CreatedAt], [AvatarUrl]) VALUES (N'10', N'lihan', N'123456', N'我爱罗1', N'admin', N'5', N'16680252327', N'系统管理员', N'1', N'2026-08-13 08:24:40.957', N'2026-08-11 06:17:17.917', N'https://bucket.054215.xyz/HospitalEquipmentSystem/%E5%A4%B4%E5%83%8F/lihan.jpg')
GO

INSERT INTO [dbo].[Users] ([UserId], [Username], [PasswordHash], [RealName], [Role], [DeptId], [Phone], [Title], [IsActive], [LastLoginAt], [CreatedAt], [AvatarUrl]) VALUES (N'11', N'何专家', N'123456', N'何博达', N'admin', N'6', N'13970038985', N'系统管理员', N'1', N'2026-08-14 07:37:36.467', N'2026-08-13 08:27:50.210', NULL)
GO

INSERT INTO [dbo].[Users] ([UserId], [Username], [PasswordHash], [RealName], [Role], [DeptId], [Phone], [Title], [IsActive], [LastLoginAt], [CreatedAt], [AvatarUrl]) VALUES (N'12', N'admin1', N'123456', N'张伟', N'admin', N'1', N'16680252327', N'系统管理员', N'1', NULL, N'2026-08-14 03:39:44.033', NULL)
GO

INSERT INTO [dbo].[Users] ([UserId], [Username], [PasswordHash], [RealName], [Role], [DeptId], [Phone], [Title], [IsActive], [LastLoginAt], [CreatedAt], [AvatarUrl]) VALUES (N'13', N'111', N'123456', N'hhh', N'admin', N'1', N'16680252333', N'系统管理员', N'1', NULL, N'2026-08-14 05:19:29.493', NULL)
GO

INSERT INTO [dbo].[Users] ([UserId], [Username], [PasswordHash], [RealName], [Role], [DeptId], [Phone], [Title], [IsActive], [LastLoginAt], [CreatedAt], [AvatarUrl]) VALUES (N'14', N'1112', N'123456', N'lil', N'admin', N'1', N'16682523274', N'系统管理员', N'1', N'2026-08-16 05:01:13.200', N'2026-08-14 07:19:13.887', NULL)
GO

SET IDENTITY_INSERT [dbo].[Users] OFF
GO


-- ----------------------------
-- View structure for v_CategoryUsageRate
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[v_CategoryUsageRate]') AND type IN ('V'))
	DROP VIEW [dbo].[v_CategoryUsageRate]
GO

-- 视图：各设备分类的使用率统计
-- 用途：按分类统计“该分类下有效设备总数(TotalCount)”与“正在使用中的设备数(UseCount，
--      含 InUse 使用中 / Borrowed 借出两种状态)”，并计算出使用率百分比 UsageRate。
-- 口径说明：
--   - 仅统计 IsActive=1（未被软删除）且 Status<>'Scrapped'（未报废）的设备；
--   - 使用 LEFT JOIN，因此没有任何设备的分类也会显示 TotalCount=0、UsageRate=0.0；
--   - UsageRate 四舍五入保留 1 位小数（DECIMAL(5,1)）。
-- 注意（原始注释保留）：此处并未按 c.ParentId IS NOT NULL 过滤，所以顶级分类（父分类）
-- 也会一并统计在内，而不是只统计末级子分类，使用该视图时需留意这一点。
CREATE VIEW [dbo].[v_CategoryUsageRate] AS SELECT 
    c.CategoryName,
    COUNT(e.EquipmentId) AS TotalCount,
    SUM(CASE WHEN e.Status IN ('InUse', 'Borrowed') THEN 1 ELSE 0 END) AS UseCount,
    CAST(
        CASE 
            WHEN COUNT(e.EquipmentId) = 0 THEN 0.0
            ELSE SUM(CASE WHEN e.Status IN ('InUse', 'Borrowed') THEN 1.0 ELSE 0 END) * 100.0 / COUNT(e.EquipmentId)
        END AS DECIMAL(5, 1)
    ) AS UsageRate
FROM EquipmentCategories c
LEFT JOIN Equipment e ON c.CategoryId = e.CategoryId AND e.IsActive = 1 AND e.Status != 'Scrapped'
-- 删掉了 WHERE c.ParentId IS NOT NULL，这样所有分类都会被查出来！
GROUP BY c.CategoryId, c.CategoryName;
GO


-- ----------------------------
-- Primary Key structure for table BorrowNoCounter
-- ----------------------------
ALTER TABLE [dbo].[BorrowNoCounter] ADD CONSTRAINT [PK_BorrowNoCounter] PRIMARY KEY CLUSTERED ([YearMonth])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for BorrowRecords
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[BorrowRecords]', RESEED, 100003)
GO


-- ----------------------------
-- Indexes structure for table BorrowRecords
-- ----------------------------
-- 按设备ID查询借用记录时加速（如查某设备的历史借用记录）
CREATE NONCLUSTERED INDEX [IX_Borrow_EquipmentId]
ON [dbo].[BorrowRecords] (
  [EquipmentId] ASC
)
GO

-- 按状态筛选借用记录时加速（如查询所有“Overdue逾期”记录）
CREATE NONCLUSTERED INDEX [IX_Borrow_Status]
ON [dbo].[BorrowRecords] (
  [Status] ASC
)
GO


-- ----------------------------
-- Uniques structure for table BorrowRecords
-- ----------------------------
-- 借用单号业务唯一，防止重复生成
ALTER TABLE [dbo].[BorrowRecords] ADD CONSTRAINT [UQ_BorrowRecords_BorrowNo] UNIQUE NONCLUSTERED ([BorrowNo] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table BorrowRecords
-- ----------------------------
ALTER TABLE [dbo].[BorrowRecords] ADD CONSTRAINT [PK__BorrowRe__4295F83FD95D1471] PRIMARY KEY CLUSTERED ([BorrowId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Departments
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Departments]', RESEED, 100001)
GO


-- ----------------------------
-- Primary Key structure for table Departments
-- ----------------------------
ALTER TABLE [dbo].[Departments] ADD CONSTRAINT [PK__Departme__014881AE415B15A6] PRIMARY KEY CLUSTERED ([DeptId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for DeptRevenue
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[DeptRevenue]', RESEED, 100008)
GO


-- ----------------------------
-- Indexes structure for table DeptRevenue
-- ----------------------------
-- 按月份统计查询加速
CREATE NONCLUSTERED INDEX [IX_DeptRevenue_Period]
ON [dbo].[DeptRevenue] (
  [Period] ASC
)
GO


-- ----------------------------
-- Uniques structure for table DeptRevenue
-- ----------------------------
-- 同一科室同一月份只能有一条营收记录，防止重复统计
ALTER TABLE [dbo].[DeptRevenue] ADD CONSTRAINT [UQ_DeptRevenue_Period_Dept] UNIQUE NONCLUSTERED ([Period] ASC, [DeptId] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table DeptRevenue
-- ----------------------------
ALTER TABLE [dbo].[DeptRevenue] ADD CONSTRAINT [PK__DeptReve__275F16DD1C5A96D6] PRIMARY KEY CLUSTERED ([RevenueId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Equipment
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Equipment]', RESEED, 100008)
GO


-- ----------------------------
-- Indexes structure for table Equipment
-- ----------------------------
-- 按科室查询设备清单时加速
CREATE NONCLUSTERED INDEX [IX_Equipment_DeptId]
ON [dbo].[Equipment] (
  [DeptId] ASC
)
GO

-- 按设备状态筛选（如查询所有闲置/借出设备）时加速
CREATE NONCLUSTERED INDEX [IX_Equipment_Status]
ON [dbo].[Equipment] (
  [Status] ASC
)
GO


-- ----------------------------
-- Uniques structure for table Equipment
-- ----------------------------
-- 设备编号业务唯一
ALTER TABLE [dbo].[Equipment] ADD CONSTRAINT [UQ__Equipmen__34475C139007A28B] UNIQUE NONCLUSTERED ([EquipmentNo] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Equipment
-- ----------------------------
ALTER TABLE [dbo].[Equipment] ADD CONSTRAINT [PK__Equipmen__34474479C787D266] PRIMARY KEY CLUSTERED ([EquipmentId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for EquipmentCategories
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[EquipmentCategories]', RESEED, 46)
GO


-- ----------------------------
-- Primary Key structure for table EquipmentCategories
-- ----------------------------
ALTER TABLE [dbo].[EquipmentCategories] ADD CONSTRAINT [PK__Equipmen__19093A0B4E26B77F] PRIMARY KEY CLUSTERED ([CategoryId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for InboundRecords
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[InboundRecords]', RESEED, 50002)
GO


-- ----------------------------
-- Primary Key structure for table InboundRecords
-- ----------------------------
ALTER TABLE [dbo].[InboundRecords] ADD CONSTRAINT [PK__InboundR__B4DB7AB5907C4CFD] PRIMARY KEY CLUSTERED ([InboundId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for MaintenanceMaterials
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[MaintenanceMaterials]', RESEED, 100014)
GO


-- ----------------------------
-- Indexes structure for table MaintenanceMaterials
-- ----------------------------
-- 按维修工单查询其所有用料明细时加速
CREATE NONCLUSTERED INDEX [IX_MaintenanceMaterials_RecordId]
ON [dbo].[MaintenanceMaterials] (
  [RecordId] ASC
)
GO


-- ----------------------------
-- Checks structure for table MaintenanceMaterials
-- ----------------------------
-- 用量必须大于0
ALTER TABLE [dbo].[MaintenanceMaterials] ADD CONSTRAINT [CK_MM_Quantity] CHECK ([Quantity]>(0))
GO

-- 单价不能为负
ALTER TABLE [dbo].[MaintenanceMaterials] ADD CONSTRAINT [CK_MM_UnitPrice] CHECK ([UnitPrice]>=(0))
GO

-- 小计金额不能为负
ALTER TABLE [dbo].[MaintenanceMaterials] ADD CONSTRAINT [CK_MM_Subtotal] CHECK ([Subtotal]>=(0))
GO


-- ----------------------------
-- Primary Key structure for table MaintenanceMaterials
-- ----------------------------
ALTER TABLE [dbo].[MaintenanceMaterials] ADD CONSTRAINT [PK_MaintenanceMaterials] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for MaintenanceRecords
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[MaintenanceRecords]', RESEED, 100006)
GO


-- ----------------------------
-- Indexes structure for table MaintenanceRecords
-- ----------------------------
-- 按设备查询其历史维修工单时加速
CREATE NONCLUSTERED INDEX [IX_Maintenance_EquipmentId]
ON [dbo].[MaintenanceRecords] (
  [EquipmentId] ASC
)
GO

-- 按工单状态筛选（如查询所有待处理/进行中工单）时加速
CREATE NONCLUSTERED INDEX [IX_Maintenance_Status]
ON [dbo].[MaintenanceRecords] (
  [Status] ASC
)
GO


-- ----------------------------
-- Checks structure for table MaintenanceRecords
-- ----------------------------
-- AI 识别置信度必须为空或落在 [0,1] 区间内
ALTER TABLE [dbo].[MaintenanceRecords] ADD CONSTRAINT [CK_MaintenanceRecords_AiConfidence] CHECK ([AiConfidence] IS NULL OR [AiConfidence]>=(0) AND [AiConfidence]<=(1))
GO


-- ----------------------------
-- Primary Key structure for table MaintenanceRecords
-- ----------------------------
ALTER TABLE [dbo].[MaintenanceRecords] ADD CONSTRAINT [PK__Maintena__FBDF78E969763BD7] PRIMARY KEY CLUSTERED ([RecordId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Materials
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Materials]', RESEED, 100002)
GO


-- ----------------------------
-- Indexes structure for table Materials
-- ----------------------------
-- 按“分类+故障类型”联合查询推荐备件时加速；仅索引启用中的备件(IsActive=1)，
-- 并通过 INCLUDE 覆盖常用查询列，避免回表
CREATE NONCLUSTERED INDEX [IX_Materials_CategoryId_FaultType]
ON [dbo].[Materials] (
  [CategoryId] ASC,
  [FaultType] ASC
)
INCLUDE ([MaterialName], [UnitPrice], [DefaultQuantity], [Unit])
WHERE ([IsActive]=(1))
GO


-- ----------------------------
-- Checks structure for table Materials
-- ----------------------------
-- 备件单价必须大于0
ALTER TABLE [dbo].[Materials] ADD CONSTRAINT [CK_Materials_UnitPrice] CHECK ([UnitPrice]>(0))
GO

-- 默认用量必须大于0
ALTER TABLE [dbo].[Materials] ADD CONSTRAINT [CK_Materials_DefaultQuantity] CHECK ([DefaultQuantity]>(0))
GO


-- ----------------------------
-- Primary Key structure for table Materials
-- ----------------------------
ALTER TABLE [dbo].[Materials] ADD CONSTRAINT [PK_Materials] PRIMARY KEY CLUSTERED ([MaterialId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for OperationLogs
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[OperationLogs]', RESEED, 12)
GO


-- ----------------------------
-- Indexes structure for table OperationLogs
-- ----------------------------
-- 按时间倒序查询最近操作日志时加速
CREATE NONCLUSTERED INDEX [IX_OperationLogs_Time]
ON [dbo].[OperationLogs] (
  [CreatedAt] DESC
)
GO


-- ----------------------------
-- Primary Key structure for table OperationLogs
-- ----------------------------
ALTER TABLE [dbo].[OperationLogs] ADD CONSTRAINT [PK__Operatio__5E548648786D00CF] PRIMARY KEY CLUSTERED ([LogId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for SmsCodes
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[SmsCodes]', RESEED, 1)
GO


-- ----------------------------
-- Indexes structure for table SmsCodes
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_SmsCodes_Phone]
ON [dbo].[SmsCodes] (
  [Phone] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table SmsCodes
-- ----------------------------
ALTER TABLE [dbo].[SmsCodes] ADD CONSTRAINT [PK__SmsCodes__3214EC07DCBDF9FD] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Suppliers
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Suppliers]', RESEED, 9)
GO


-- ----------------------------
-- Primary Key structure for table Suppliers
-- ----------------------------
ALTER TABLE [dbo].[Suppliers] ADD CONSTRAINT [PK__Supplier__4BE666B4D7CBDA10] PRIMARY KEY CLUSTERED ([SupplierId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Users
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Users]', RESEED, 14)
GO


-- ----------------------------
-- Uniques structure for table Users
-- ----------------------------
-- 登录用户名唯一
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [UQ__Users__536C85E4B0499163] UNIQUE NONCLUSTERED ([Username] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Users
-- ----------------------------
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [PK__Users__1788CC4C2B25FDF4] PRIMARY KEY CLUSTERED ([UserId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Foreign Keys structure for table BorrowRecords
-- ----------------------------
ALTER TABLE [dbo].[BorrowRecords] ADD CONSTRAINT [FK__BorrowRec__Equip__1F98B2C1] FOREIGN KEY ([EquipmentId]) REFERENCES [dbo].[Equipment] ([EquipmentId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- 借用记录 -> 设备：一台设备可对应多条借用记录
GO

ALTER TABLE [dbo].[BorrowRecords] ADD CONSTRAINT [FK__BorrowRec__Appli__208CD6FA] FOREIGN KEY ([ApplicantId]) REFERENCES [dbo].[Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- 借用记录 -> 用户：借用申请人
GO

ALTER TABLE [dbo].[BorrowRecords] ADD CONSTRAINT [FK__BorrowRec__Appli__2180FB33] FOREIGN KEY ([ApplicantDeptId]) REFERENCES [dbo].[Departments] ([DeptId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- 借用记录 -> 科室：借用申请科室
GO

ALTER TABLE [dbo].[BorrowRecords] ADD CONSTRAINT [FK__BorrowRec__Appro__22751F6C] FOREIGN KEY ([ApproverId]) REFERENCES [dbo].[Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- 借用记录 -> 用户：审批人
GO


-- ----------------------------
-- Foreign Keys structure for table DeptRevenue
-- ----------------------------
ALTER TABLE [dbo].[DeptRevenue] ADD CONSTRAINT [FK__DeptReven__DeptI__03F0984C] FOREIGN KEY ([DeptId]) REFERENCES [dbo].[Departments] ([DeptId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- 科室营收 -> 科室：营收所属科室
GO


-- ----------------------------
-- Foreign Keys structure for table Equipment
-- ----------------------------
ALTER TABLE [dbo].[Equipment] ADD CONSTRAINT [FK__Equipment__Suppl__04E4BC85] FOREIGN KEY ([SupplierId]) REFERENCES [dbo].[Suppliers] ([SupplierId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- 设备 -> 供应商：设备来源供应商
GO

ALTER TABLE [dbo].[Equipment] ADD CONSTRAINT [FK__Equipment__Categ__05D8E0BE] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[EquipmentCategories] ([CategoryId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- 设备 -> 设备分类
GO

ALTER TABLE [dbo].[Equipment] ADD CONSTRAINT [FK__Equipment__DeptI__06CD04F7] FOREIGN KEY ([DeptId]) REFERENCES [dbo].[Departments] ([DeptId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- 设备 -> 科室：设备当前所属科室
GO

ALTER TABLE [dbo].[Equipment] ADD CONSTRAINT [FK__Equipment__Respo__07C12930] FOREIGN KEY ([ResponsibleUserId]) REFERENCES [dbo].[Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- 设备 -> 用户：设备责任人
GO

ALTER TABLE [dbo].[Equipment] ADD CONSTRAINT [FK_Equipment_Suppliers] FOREIGN KEY ([SupplierId]) REFERENCES [dbo].[Suppliers] ([SupplierId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- （与上面 FK__Equipment__Suppl__04E4BC85 重复的历史遗留外键，同样指向 SupplierId）
GO


-- ----------------------------
-- Foreign Keys structure for table EquipmentCategories
-- ----------------------------
ALTER TABLE [dbo].[EquipmentCategories] ADD CONSTRAINT [FK__Equipment__Paren__7A672E12] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[EquipmentCategories] ([CategoryId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- 设备分类 -> 设备分类（自关联）：实现分类的父子级树形结构
GO


-- ----------------------------
-- Foreign Keys structure for table InboundRecords
-- ----------------------------
ALTER TABLE [dbo].[InboundRecords] ADD CONSTRAINT [FK__InboundRe__Equip__0D7A0286] FOREIGN KEY ([EquipmentId]) REFERENCES [dbo].[Equipment] ([EquipmentId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- 入库记录 -> 设备：本次入库对应的设备
GO

ALTER TABLE [dbo].[InboundRecords] ADD CONSTRAINT [FK__InboundRe__Opera__10566F31] FOREIGN KEY ([OperatorId]) REFERENCES [dbo].[Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- 入库记录 -> 用户：入库经办人
GO


-- ----------------------------
-- Foreign Keys structure for table MaintenanceMaterials
-- ----------------------------
ALTER TABLE [dbo].[MaintenanceMaterials] ADD CONSTRAINT [FK_MM_MaintenanceRecords] FOREIGN KEY ([RecordId]) REFERENCES [dbo].[MaintenanceRecords] ([RecordId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- 维修用料明细 -> 维修工单：一个工单可关联多条用料明细
GO

ALTER TABLE [dbo].[MaintenanceMaterials] ADD CONSTRAINT [FK_MM_Materials] FOREIGN KEY ([MaterialId]) REFERENCES [dbo].[Materials] ([MaterialId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- 维修用料明细 -> 备件字典：本条明细所使用的备件
GO


-- ----------------------------
-- Foreign Keys structure for table MaintenanceRecords
-- ----------------------------
ALTER TABLE [dbo].[MaintenanceRecords] ADD CONSTRAINT [FK__Maintenan__Equip__151B244E] FOREIGN KEY ([EquipmentId]) REFERENCES [dbo].[Equipment] ([EquipmentId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- 维修工单 -> 设备：故障设备
GO

ALTER TABLE [dbo].[MaintenanceRecords] ADD CONSTRAINT [FK__Maintenan__Repor__160F4887] FOREIGN KEY ([ReporterId]) REFERENCES [dbo].[Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- 维修工单 -> 用户：报修人
GO

ALTER TABLE [dbo].[MaintenanceRecords] ADD CONSTRAINT [FK__Maintenan__Repor__17036CC0] FOREIGN KEY ([ReportDeptId]) REFERENCES [dbo].[Departments] ([DeptId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- 维修工单 -> 科室：报修科室
GO

ALTER TABLE [dbo].[MaintenanceRecords] ADD CONSTRAINT [FK__Maintenan__Assig__19DFD96B] FOREIGN KEY ([AssignedTo]) REFERENCES [dbo].[Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- 维修工单 -> 用户：被指派的维修工程师
GO


-- ----------------------------
-- Foreign Keys structure for table Materials
-- ----------------------------
ALTER TABLE [dbo].[Materials] ADD CONSTRAINT [FK_Materials_EquipmentCategories] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[EquipmentCategories] ([CategoryId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- 备件字典 -> 设备分类：该备件适用的设备分类
GO


-- ----------------------------
-- Foreign Keys structure for table OperationLogs
-- ----------------------------
ALTER TABLE [dbo].[OperationLogs] ADD CONSTRAINT [FK__Operation__UserI__2739D489] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- 操作日志 -> 用户：执行操作的用户
GO


-- ----------------------------
-- Foreign Keys structure for table Users
-- ----------------------------
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [FK__Users__DeptId__75A278F5] FOREIGN KEY ([DeptId]) REFERENCES [dbo].[Departments] ([DeptId]) ON DELETE NO ACTION ON UPDATE NO ACTION  -- 用户 -> 科室：用户所属科室
GO