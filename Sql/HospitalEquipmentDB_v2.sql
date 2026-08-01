-- ============================================================
-- 智能医院设备管理系统 数据库建表脚本 v2.0
-- 目标: SQL Server 2017+
-- 六大模块: 设备台账/入库管理/维修管理/借用管理/仪表盘监控/系统设置
-- ============================================================

-- ============================ 1. 科室表 ============================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Departments' AND xtype='U')
CREATE TABLE Departments (
    DeptId INT IDENTITY(1,1) PRIMARY KEY,
    DeptName NVARCHAR(50) NOT NULL,
    DeptCode NVARCHAR(20) NULL,
    Location NVARCHAR(100) NULL,
    Phone NVARCHAR(20) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- ============================ 2. 用户表 ============================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' AND xtype='U')
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    RealName NVARCHAR(50) NOT NULL,
    Role NVARCHAR(20) NOT NULL DEFAULT 'doctor',
    DeptId INT NULL REFERENCES Departments(DeptId),
    Phone NVARCHAR(20) NULL,
    Title NVARCHAR(50) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    LastLoginAt DATETIME NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- ============================ 3. 设备分类表 ============================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='EquipmentCategories' AND xtype='U')
CREATE TABLE EquipmentCategories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(50) NOT NULL,
    CategoryCode NVARCHAR(20) NULL,
    ParentId INT NULL REFERENCES EquipmentCategories(CategoryId),
    SortOrder INT NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- ============================ 4. 供应商表 ============================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Suppliers' AND xtype='U')
CREATE TABLE Suppliers (
    SupplierId INT IDENTITY(1,1) PRIMARY KEY,
    SupplierName NVARCHAR(100) NOT NULL,
    ContactPerson NVARCHAR(50) NULL,
    Phone NVARCHAR(20) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- ============================ 5. 设备表（核心） ============================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Equipment' AND xtype='U')
CREATE TABLE Equipment (
    EquipmentId INT IDENTITY(1,1) PRIMARY KEY,
    EquipmentNo NVARCHAR(50) NOT NULL UNIQUE,
    EquipmentName NVARCHAR(100) NOT NULL,
    Model NVARCHAR(100) NULL,
    Manufacturer NVARCHAR(100) NULL,
    SupplierId INT NULL REFERENCES Suppliers(SupplierId),
    CategoryId INT NULL REFERENCES EquipmentCategories(CategoryId),
    DeptId INT NULL REFERENCES Departments(DeptId),
    Location NVARCHAR(200) NULL,
    ResponsibleUserId INT NULL REFERENCES Users(UserId),
    Price DECIMAL(18,2) NULL,
    PurchaseDate DATE NULL,
    WarrantyMonths INT NULL,
    ServiceLife INT NULL,
    LastMaintainDate DATE NULL,
    NextMaintainDate DATE NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Idle',
    Remarks NVARCHAR(500) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL
);
GO
CREATE NONCLUSTERED INDEX IX_Equipment_DeptId ON Equipment(DeptId);
CREATE NONCLUSTERED INDEX IX_Equipment_Status ON Equipment(Status);
GO

-- ============================ 6. 入库记录表 ============================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='InboundRecords' AND xtype='U')
CREATE TABLE InboundRecords (
    InboundId INT IDENTITY(1,1) PRIMARY KEY,
    EquipmentId INT NOT NULL REFERENCES Equipment(EquipmentId),
    InboundNo NVARCHAR(50) NOT NULL,
    Supplier NVARCHAR(100) NULL,
    PurchasePrice DECIMAL(18,2) NULL,
    Quantity INT NOT NULL DEFAULT 1,
    InboundDate DATETIME NOT NULL DEFAULT GETDATE(),
    OperatorId INT NULL REFERENCES Users(UserId),
    AuditStatus NVARCHAR(20) NOT NULL DEFAULT 'Pending',
    Remarks NVARCHAR(500) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- ============================ 7. 维修工单表 ============================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='MaintenanceRecords' AND xtype='U')
CREATE TABLE MaintenanceRecords (
    RecordId INT IDENTITY(1,1) PRIMARY KEY,
    EquipmentId INT NOT NULL REFERENCES Equipment(EquipmentId),
    RepairNo NVARCHAR(50) NOT NULL,
    ReporterId INT NULL REFERENCES Users(UserId),
    ReportDeptId INT NULL REFERENCES Departments(DeptId),
    FaultDesc NVARCHAR(500) NOT NULL,
    FaultType NVARCHAR(50) NULL,
    Urgency NVARCHAR(20) NOT NULL DEFAULT 'Normal',
    ProgressStage NVARCHAR(20) NOT NULL DEFAULT 'Pending',
    AssignedTo INT NULL REFERENCES Users(UserId),
    RepairResult NVARCHAR(500) NULL,
    RepairCost DECIMAL(18,2) NULL,
    DowntimeHours DECIMAL(6,2) NULL,
    ReportTime DATETIME NOT NULL DEFAULT GETDATE(),
    CompleteTime DATETIME NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Pending',
    Remarks NVARCHAR(500) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);
GO
CREATE NONCLUSTERED INDEX IX_Maintenance_EquipmentId ON MaintenanceRecords(EquipmentId);
CREATE NONCLUSTERED INDEX IX_Maintenance_Status ON MaintenanceRecords(Status);
GO

-- ============================ 8. 借用记录表 ============================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='BorrowRecords' AND xtype='U')
CREATE TABLE BorrowRecords (
    BorrowId INT IDENTITY(1,1) PRIMARY KEY,
    BorrowNo NVARCHAR(50) NOT NULL,
    EquipmentId INT NOT NULL REFERENCES Equipment(EquipmentId),
    ApplicantId INT NOT NULL REFERENCES Users(UserId),
    ApplicantDeptId INT NULL REFERENCES Departments(DeptId),
    Purpose NVARCHAR(200) NULL,
    ExpectedReturnDate DATE NOT NULL,
    ApproverId INT NULL REFERENCES Users(UserId),
    ApproveDate DATETIME NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Pending',
    ActualReturnDate DATE NULL,
    ReturnNote NVARCHAR(200) NULL,
    Remarks NVARCHAR(500) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL
);
GO
CREATE NONCLUSTERED INDEX IX_Borrow_EquipmentId ON BorrowRecords(EquipmentId);
CREATE NONCLUSTERED INDEX IX_Borrow_Status ON BorrowRecords(Status);
GO

-- ============================ 9. 操作日志表 ============================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='OperationLogs' AND xtype='U')
CREATE TABLE OperationLogs (
    LogId BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NULL REFERENCES Users(UserId),
    ActionType NVARCHAR(30) NOT NULL,
    TargetTable NVARCHAR(50) NULL,
    TargetId INT NULL,
    Detail NVARCHAR(500) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);
GO
CREATE NONCLUSTERED INDEX IX_OperationLogs_Time ON OperationLogs(CreatedAt DESC);
GO

-- ============================================================
--                     示例数据插入
-- ============================================================

-- 1. 科室数据
IF NOT EXISTS (SELECT 1 FROM Departments)
BEGIN
    SET IDENTITY_INSERT Departments ON;
    INSERT INTO Departments (DeptId, DeptName, DeptCode, Location, Phone) VALUES
    (1, N'设备科', N'Dept-001', N'行政楼3层', N'1001'),
    (2, N'急诊科', N'Dept-002', N'急诊楼1层', N'2001'),
    (3, N'ICU', N'Dept-003', N'住院楼5层', N'3001'),
    (4, N'放射科', N'Dept-004', N'医技楼2层', N'4001'),
    (5, N'检验科', N'Dept-005', N'医技楼3层', N'5001'),
    (6, N'妇产科', N'Dept-006', N'住院楼8层', N'6001'),
    (7, N'手术室', N'Dept-007', N'住院楼4层', N'7001');
    SET IDENTITY_INSERT Departments OFF;
END
GO

-- 2. 用户数据（3种角色）
IF NOT EXISTS (SELECT 1 FROM Users WHERE Username = 'admin')
BEGIN
    SET IDENTITY_INSERT Users ON;
    INSERT INTO Users (UserId, Username, PasswordHash, RealName, Role, DeptId, Phone, Title) VALUES
    (1, N'admin', N'123456', N'系统管理员', N'admin', 1, N'13800001001', N'系统工程师'),
    (2, N'zhangwei', N'123456', N'张伟', N'admin', 1, N'13800001002', N'设备科主任'),
    (3, N'linurse', N'123456', N'李护士', N'doctor', 2, N'13800002001', N'主管护师'),
    (4, N'wangfang', N'123456', N'王芳', N'doctor', 3, N'13800003001', N'ICU护士长'),
    (5, N'wanggong', N'123456', N'王工', N'repair', 1, N'13800001003', N'维修工程师');
    SET IDENTITY_INSERT Users OFF;
END
GO

-- 3. 设备分类数据（5大类+4子类）
IF NOT EXISTS (SELECT 1 FROM EquipmentCategories)
BEGIN
    SET IDENTITY_INSERT EquipmentCategories ON;
    INSERT INTO EquipmentCategories (CategoryId, CategoryName, CategoryCode, ParentId, SortOrder) VALUES
    (1, N'影像设备', N'CAT-IMG', NULL, 1),
    (2, N'急救设备', N'CAT-EMR', NULL, 2),
    (3, N'监护设备', N'CAT-MON', NULL, 3),
    (4, N'检验设备', N'CAT-LAB', NULL, 4),
    (5, N'手术设备', N'CAT-OPT', NULL, 5),
    (6, N'CT设备', N'CAT-CT', 1, 1),
    (7, N'超声设备', N'CAT-US', 1, 2),
    (8, N'呼吸设备', N'CAT-VEN', 2, 1),
    (9, N'除颤设备', N'CAT-DEF', 2, 2);
    SET IDENTITY_INSERT EquipmentCategories OFF;
END
GO

-- 4. 供应商数据
IF NOT EXISTS (SELECT 1 FROM Suppliers)
BEGIN
    SET IDENTITY_INSERT Suppliers ON;
    INSERT INTO Suppliers (SupplierId, SupplierName, ContactPerson, Phone) VALUES
    (1, N'国药器械', N'赵经理', N'13800001111'),
    (2, N'西门子医疗', N'孙经理', N'13800003333'),
    (3, N'迈瑞医疗', N'吴经理', N'13800005555'),
    (4, N'飞利浦医疗', N'周经理', N'13800004444'),
    (5, N'罗氏诊断', N'陈经理', N'13800006666');
    SET IDENTITY_INSERT Suppliers OFF;
END
GO

-- 5. 设备数据（20台，覆盖5种状态和7个科室）
IF NOT EXISTS (SELECT 1 FROM Equipment)
BEGIN
    SET IDENTITY_INSERT Equipment ON;
    INSERT INTO Equipment (EquipmentId, EquipmentNo, EquipmentName, Model, Manufacturer, SupplierId, CategoryId, DeptId, Location, ResponsibleUserId, Price, PurchaseDate, WarrantyMonths, ServiceLife, LastMaintainDate, NextMaintainDate, Status, Remarks) VALUES
    (1, N'CT-2026-001', N'多层螺旋CT', N'Revolution CT', N'GE医疗', 2, 6, 4, N'放射科CT室1', 2, 5800000.00, '2026-03-15', 36, 10, '2026-06-15', '2026-09-15', N'InUse', N'2026年新购高端CT'),
    (2, N'HX-2025-088', N'呼吸机', N'V60 Plus', N'迈瑞', 3, 8, 2, N'急诊科抢救室', 3, 180000.00, '2025-09-20', 24, 8, '2026-06-20', '2026-09-20', N'Maintenance', N'屏幕故障已送维修部'),
    (3, N'XD-2026-022', N'心电监护仪', N'BeneView T8', N'迈瑞', 3, 3, 3, N'ICU 3床', 4, 45000.00, '2026-01-10', 24, 6, '2026-07-10', '2026-10-10', N'InUse', NULL),
    (4, N'CS-2019-034', N'超声诊断仪', N'Voluson E10', N'GE医疗', 2, 7, 6, N'妇产科超声室', 2, 1200000.00, '2019-11-05', 36, 8, '2026-01-10', NULL, N'Scrapped', N'设备老化，已批准报废'),
    (5, N'JY-2026-045', N'生化分析仪', N'AU5800', N'贝克曼', 1, 4, 5, N'检验科1室', 2, 850000.00, '2026-05-08', 24, 10, NULL, NULL, N'Borrowed', N'借出给急诊科，预计8月3日归还'),
    (6, N'CC-2026-005', N'除颤仪', N'Lifepak 20', N'飞利浦', 4, 9, 2, N'急诊科抢救室', 3, 95000.00, '2026-02-01', 24, 8, '2026-07-05', '2026-10-05', N'InUse', NULL),
    (7, N'HX-2024-112', N'呼吸机(备用)', N'V60', N'迈瑞', 3, 8, 3, N'ICU库房', 4, 165000.00, '2024-12-01', 24, 8, '2026-06-28', '2026-09-28', N'Idle', N'ICU备用设备'),
    (8, N'MY-2026-088', N'免疫分析仪', N'Cobas 8000', N'罗氏', 5, 4, 5, N'检验科2室', 2, 580000.00, '2026-07-28', 24, 10, NULL, NULL, N'Idle', N'新入库待分配科室'),
    (9, N'MR-2025-033', N'核磁共振成像仪', N'Signa Pioneer', N'GE医疗', 2, 1, 4, N'放射科MR室', 2, 8500000.00, '2025-06-01', 48, 12, '2026-06-01', '2026-09-01', N'InUse', N'3.0T高场强MRI'),
    (10, N'DR-2026-011', N'DR拍片机', N'DigitalDiagnost', N'飞利浦', 4, 1, 4, N'放射科DR室', 2, 680000.00, '2026-04-01', 36, 10, NULL, NULL, N'InUse', NULL),
    (11, N'XG-2018-022', N'X光机', N'R-200', N'万东医疗', 1, 1, 4, N'放射科X光室', 2, 350000.00, '2018-03-12', 24, 8, '2025-12-01', NULL, N'Scrapped', N'技术落后已停用待报废'),
    (12, N'SS-2025-015', N'麻醉机', N'Fabius GS', N'德尔格', 1, 5, 7, N'手术室3号间', 2, 420000.00, '2025-08-15', 24, 10, '2026-07-15', '2026-10-15', N'InUse', NULL),
    (13, N'SS-2024-008', N'手术床', N'OT-8800', N'迈瑞', 3, 5, 7, N'手术室1号间', 2, 185000.00, '2024-03-20', 36, 12, '2026-06-20', '2026-12-20', N'Idle', N'备用手术床'),
    (14, N'JY-2025-055', N'血液分析仪', N'BC-6800', N'迈瑞', 3, 4, 5, N'检验科3室', 2, 320000.00, '2025-11-01', 24, 8, '2026-07-01', '2026-10-01', N'InUse', NULL),
    (15, N'JY-2024-042', N'凝血分析仪', N'CS-5100', N'希森美康', 1, 4, 5, N'检验科1室', 2, 280000.00, '2024-07-15', 24, 8, '2026-05-15', '2026-08-15', N'Maintenance', N'加样臂故障维修中'),
    (16, N'XD-2025-018', N'多参数监护仪', N'PM-9000', N'迈瑞', 3, 3, 2, N'急诊科观察室', 3, 35000.00, '2025-03-10', 24, 6, '2026-06-10', '2026-09-10', N'Borrowed', N'借给ICU临时使用'),
    (17, N'CC-2024-003', N'除颤仪', N'Lifepak 15', N'飞利浦', 4, 9, 3, N'ICU抢救室', 4, 120000.00, '2024-08-01', 24, 8, '2026-06-01', '2026-09-01', N'InUse', NULL),
    (18, N'HX-2023-066', N'呼吸机', N'V60 Classic', N'迈瑞', 3, 8, 2, N'急诊科库房', 3, 150000.00, '2023-04-10', 24, 8, '2025-10-10', NULL, N'Scrapped', N'设备老旧性能下降已报废'),
    (19, N'ZS-2026-012', N'注射泵', N'BYZ-810', N'迈瑞', 3, 2, 2, N'急诊科5床', 3, 8500.00, '2026-06-15', 24, 6, NULL, NULL, N'InUse', NULL),
    (20, N'XW-2025-033', N'洗胃机', N'XW-III', N'凯达科技', 1, 2, 2, N'急诊科处置室', 3, 12000.00, '2025-05-20', 24, 6, '2026-05-20', '2026-08-20', N'Idle', NULL);
    SET IDENTITY_INSERT Equipment OFF;
END
GO

-- 6. 入库记录数据
IF NOT EXISTS (SELECT 1 FROM InboundRecords)
BEGIN
    SET IDENTITY_INSERT InboundRecords ON;
    INSERT INTO InboundRecords (InboundId, EquipmentId, InboundNo, Supplier, PurchasePrice, Quantity, InboundDate, OperatorId, AuditStatus) VALUES
    (1, 1, N'RK-2026-03-001', N'GE医疗', 5800000.00, 1, '2026-03-15', 1, N'Approved'),
    (2, 9, N'RK-2025-06-002', N'GE医疗', 8500000.00, 1, '2025-06-01', 1, N'Approved'),
    (3, 5, N'RK-2026-05-003', N'贝克曼', 850000.00, 1, '2026-05-08', 1, N'Approved'),
    (4, 10, N'RK-2026-04-004', N'飞利浦', 680000.00, 1, '2026-04-01', 1, N'Approved'),
    (5, 8, N'RK-2026-07-005', N'罗氏', 580000.00, 1, '2026-07-28', 2, N'Pending'),
    (6, 12, N'RK-2025-08-006', N'德尔格', 420000.00, 1, '2025-08-15', 1, N'Approved'),
    (7, 19, N'RK-2026-06-007', N'迈瑞', 8500.00, 5, '2026-06-15', 2, N'Approved'),
    (8, 14, N'RK-2025-11-008', N'迈瑞', 320000.00, 1, '2025-11-01', 1, N'Approved'),
    (9, 16, N'RK-2025-03-009', N'迈瑞', 35000.00, 3, '2025-03-10', 1, N'Approved'),
    (10, 20, N'RK-2025-05-010', N'凯达科技', 12000.00, 2, '2025-05-20', 2, N'Approved');
    SET IDENTITY_INSERT InboundRecords OFF;
END
GO

-- 7. 维修工单数据（12条，覆盖各进度阶段）
IF NOT EXISTS (SELECT 1 FROM MaintenanceRecords)
BEGIN
    SET IDENTITY_INSERT MaintenanceRecords ON;
    INSERT INTO MaintenanceRecords (RecordId, EquipmentId, RepairNo, ReporterId, ReportDeptId, FaultDesc, FaultType, Urgency, ProgressStage, AssignedTo, RepairResult, RepairCost, DowntimeHours, ReportTime, CompleteTime, Status) VALUES
    (1, 2, N'BX-2026-0715-01', 3, 2, N'开机后屏幕无显示，无法正常使用', N'电气故障', N'Urgent', N'InProgress', 5, NULL, NULL, NULL, '2026-07-15 14:30', NULL, N'InProgress'),
    (2, 3, N'BX-2026-0714-02', 4, 3, N'心率监测数据异常，数值波动较大', N'软件故障', N'Normal', N'Done', 5, N'重新校准传感器，重启后恢复正常', 0.00, 1.5, '2026-07-14 09:15', '2026-07-15 10:30', N'Completed'),
    (3, 1, N'BX-2026-0713-03', 2, 4, N'扫描过程中断，报错代码E-045', N'机械故障', N'Urgent', N'InProgress', 5, NULL, NULL, NULL, '2026-07-13 11:20', NULL, N'InProgress'),
    (4, 5, N'BX-2026-0712-04', 3, 2, N'试剂针堵塞，加样不准确', N'机械故障', N'Normal', N'Done', 5, N'清洗试剂针，更换密封圈', 350.00, 3.0, '2026-07-12 16:40', '2026-07-13 14:00', N'Completed'),
    (5, 9, N'BX-2026-0710-05', 2, 4, N'磁体冷却系统报警，温度偏高', N'机械故障', N'Urgent', N'Assigned', 5, NULL, NULL, NULL, '2026-07-10 08:20', NULL, N'InProgress'),
    (6, 15, N'BX-2026-0708-06', 3, 5, N'加样臂定位偏移，测试结果不准确', N'机械故障', N'Urgent', N'InProgress', 5, NULL, NULL, NULL, '2026-07-08 10:00', NULL, N'InProgress'),
    (7, 6, N'BX-2026-0705-07', 3, 2, N'除颤仪电池无法充电，电量显示异常', N'电气故障', N'Normal', N'Done', 5, N'更换电池组，测试正常', 1200.00, 24.0, '2026-07-05 15:10', '2026-07-06 15:10', N'Completed'),
    (8, 16, N'BX-2026-0703-08', 4, 3, N'血氧饱和度监测数值不准', N'软件故障', N'Normal', N'Done', 5, N'更换血氧探头，校准成功', 450.00, 2.0, '2026-07-03 09:30', '2026-07-03 15:00', N'Completed'),
    (9, 12, N'BX-2026-0628-09', 2, 7, N'麻醉机回路泄漏，气压不稳定', N'机械故障', N'Urgent', N'Done', 5, N'更换回路密封圈，检修单向阀', 2800.00, 8.0, '2026-06-28 14:00', '2026-06-29 10:00', N'Completed'),
    (10, 7, N'BX-2026-0620-10', 4, 3, N'呼吸机管路连接处漏气', N'机械故障', N'Normal', N'Pending', NULL, NULL, NULL, NULL, '2026-06-20 11:00', NULL, N'Pending'),
    (11, 19, N'BX-2026-0615-11', 3, 2, N'注射泵推进速度不准', N'机械故障', N'Low', N'Pending', NULL, NULL, NULL, NULL, '2026-06-15 08:30', NULL, N'Pending'),
    (12, 11, N'BX-2026-0510-12', 2, 4, N'X光机曝光时图像模糊', N'电气故障', N'Normal', N'Done', 5, N'球管老化无法修复，建议报废', 0.00, 48.0, '2026-05-10 09:00', '2026-05-12 09:00', N'Completed');
    SET IDENTITY_INSERT MaintenanceRecords OFF;
END
GO

-- 8. 借用记录数据（8条，覆盖各状态）
IF NOT EXISTS (SELECT 1 FROM BorrowRecords)
BEGIN
    SET IDENTITY_INSERT BorrowRecords ON;
    INSERT INTO BorrowRecords (BorrowId, BorrowNo, EquipmentId, ApplicantId, ApplicantDeptId, Purpose, ExpectedReturnDate, ApproverId, ApproveDate, Status, ActualReturnDate, ReturnNote) VALUES
    (1, N'B-2026-07-001', 5, 3, 2, N'急诊科夜间值班急需生化检测设备', '2026-08-03', 2, '2026-07-28 09:00', N'Approved', NULL, NULL),
    (2, N'B-2026-07-002', 7, 4, 3, N'ICU临时增加床位需要备用呼吸机', '2026-07-30', 2, '2026-07-25 10:30', N'Returned', '2026-07-28 16:00', N'设备已归还，运行正常'),
    (3, N'B-2026-07-003', 16, 4, 3, N'ICU监护仪不够用，临时借用', '2026-08-10', 2, '2026-07-26 14:00', N'Approved', NULL, NULL),
    (4, N'B-2026-06-004', 3, 3, 2, N'急诊科抢救室需要心电监护', '2026-06-25', 2, '2026-06-18 09:00', N'Returned', '2026-06-24 17:30', N'使用完毕，已归还'),
    (5, N'B-2026-06-005', 19, 4, 3, N'ICU患者增加需注射泵', '2026-06-30', 2, '2026-06-20 11:00', N'Returned', '2026-06-29 10:00', N'已归还'),
    (6, N'B-2026-07-006', 13, 2, 7, N'手术室临时加台需要备用手术床', '2026-07-31', 1, '2026-07-27 08:00', N'Approved', NULL, NULL),
    (7, N'B-2026-05-007', 17, 3, 2, N'急诊科除颤仪送修期间临时使用', '2026-05-20', 2, '2026-05-10 09:30', N'Returned', '2026-05-19 15:00', N'已归还，设备完好'),
    (8, N'B-2026-07-008', 20, 3, 2, N'急诊科洗胃机故障，临时借用备用', '2026-07-28', 1, NULL, N'Pending', NULL, NULL);
    SET IDENTITY_INSERT BorrowRecords OFF;
END
GO

-- 9. 操作日志数据
IF NOT EXISTS (SELECT 1 FROM OperationLogs)
BEGIN
    SET IDENTITY_INSERT OperationLogs ON;
    INSERT INTO OperationLogs (LogId, UserId, ActionType, TargetTable, TargetId, Detail, CreatedAt) VALUES
    (1, 1, N'Login', N'Users', 1, N'管理员登录系统', '2026-07-28 08:30:00'),
    (2, 1, N'Create', N'Equipment', 8, N'新增设备: 免疫分析仪 MY-2026-088', '2026-07-28 08:35:00'),
    (3, 1, N'Create', N'InboundRecords', 5, N'新增入库单: RK-2026-07-005', '2026-07-28 08:36:00'),
    (4, 2, N'Approve', N'BorrowRecords', 1, N'审批借用: 生化分析仪 JY-2026-045', '2026-07-28 09:00:00'),
    (5, 2, N'Update', N'Equipment', 5, N'更新设备状态: 生化分析仪 -> Borrowed', '2026-07-28 09:01:00'),
    (6, 5, N'Update', N'MaintenanceRecords', 2, N'完成维修: 心电监护仪 XD-2026-022', '2026-07-15 10:30:00'),
    (7, 5, N'Update', N'Equipment', 3, N'设备维修完成恢复使用: 心电监护仪', '2026-07-15 10:31:00'),
    (8, 4, N'Update', N'BorrowRecords', 2, N'归还设备: 呼吸机 HX-2024-112', '2026-07-28 16:00:00'),
    (9, 4, N'Update', N'Equipment', 7, N'设备状态更新: 呼吸机 -> Idle', '2026-07-28 16:01:00'),
    (10, 3, N'Create', N'MaintenanceRecords', 1, N'申报故障: 呼吸机 HX-2025-088 屏幕无显示', '2026-07-15 14:30:00'),
    (11, 2, N'Approve', N'ScrapRecords', 4, N'审批报废: 超声诊断仪 CS-2019-034', '2026-07-10 10:00:00'),
    (12, 1, N'Delete', N'Equipment', 11, N'软删除设备: X光机 XG-2018-022', '2026-07-20 15:00:00');
    SET IDENTITY_INSERT OperationLogs OFF;
END
GO

PRINT N'===== v2.0 建表完成! 9张表 + 完整示例数据 =====';
GO
