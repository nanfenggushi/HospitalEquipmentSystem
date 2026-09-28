# 医院设备管理系统

## 项目简介

**HospitalEquipmentSystem** 是一款基于 Windows Forms（WinForms）的智能医院设备管理系统，专为医院的设备借还、保养、维修、监控等场景设计。系统采用分层架构（DAL + BLL + UI），结合人脸识别登录、AI 故障识别、云端存储等现代功能，旨在提升医院设备管理的智能化、规范化和高效性。

该系统支持多角色用户（管理员、医生、维修员等），通过人脸识别实现安全登录，并提供完整的设备生命周期管理方案。

## 功能列表

### 核心功能
- **用户与角色管理**
  - 支持管理员、医生、维修员等多个角色
  - 用户分页查询、数据实时加载
  - 系统参数配置与可视化表格展示

- **设备借还管理**
  - 设备借出申请
  - 借还审批流程
  - 借还记录管理
  - 归还操作

- **设备维护与保养**
  - 维修记录提交
  - 维护物料管理
  - 维护记录查询
  - 故障识别（支持模拟 + AI/OpenAI 模式）

- **仓库管理**
  - 入库、出库操作
  - 物料与供应商管理
  - 分类与库存监控

- **统计分析**
  - 设备使用统计
  - 维护趋势分析
  - 设备生命周期预测
  - 月度/年度报表

- **监控中心**
  - 实时设备监控
  - 报警提示
  - 健康状态展示

- **人脸识别登录**
  - 基于虹软 ArcFace SDK 的本地人脸识别
  - 支持 RGB + IR 双摄像头
  - 活体检测与相似度阈值配置

### 其他功能
- 部门收入统计
- 故障上报与识别
- 订单编辑与工程安排
- 仪表盘（Dashboard）概览
- 角色工作台（不同角色切换工作界面）

## 技术架构

### 分层架构
- **HospitalEquipmentSystem.UI**：WinForms 界面层（所有表单、控件、页面切换）
- **HospitalEquipmentSystem.BLL**：业务逻辑层（封装数据库操作与业务规则）
- **HospitalEquipmentSystem.DAL**：数据访问层（SQL 查询、实体映射）
- **HospitalEquipmentSystem.Model**：数据模型层（DTO、实体类）
- **HospitalEquipmentSystem.Common**：通用工具类（数据库助手、R2 存储帮助类、页面管理、数据映射等）

### 技术栈
- **开发语言**：C#（.NET Framework 4.7.2）
- **界面框架**：WinForms + SunnyUI（自定义控件、数据网格、分页控件等）
- **数据库**：Microsoft SQL Server
- **人脸识别**：虹软 ArcFace SDK（本地部署）
- **存储**：Cloudflare R2 对象存储（FaceTemplates 等资源）
- **其他**：OpenTK（可能用于 3D 设备展示）、System.Data.SqlClient

## 运行环境要求

### 开发环境
- Visual Studio 2022/2025（建议 17.14+）
- .NET Framework 4.7.2 运行时
- 虹软 ArcFace SDK（需下载并配置 SDKKEY）
- 摄像头（RGB + IR，建议 USB 摄像头）
- Cloudflare R2 账号（可选，用于模板存储）

### 部署要求
- Windows 10/11（推荐）
- SQL Server 数据库
- 网络访问（R2 存储服务）
- 管理员权限（运行程序）

## 快速开始

### 1. 克隆仓库
```bash
git clone <仓库地址>
cd HospitalEquipmentSystem
```

### 2. 配置项目
1. 打开 `HospitalEquipmentSystem.UI\HospitalEquipmentSystem.UI.csproj`
2. 将 `ArcFaceSDK` 项目引用添加到 UI 项目中
3. 配置数据库连接：
   - 打开 `HospitalEquipmentSystem.UI\App.config`
   - 修改 `connectionStrings` 中的 `connStr`（示例数据库地址为 `8.163.70.157`）

### 3. 配置人脸识别（必须）
1. 前往虹软官网申请 **APPID** 和 **SDKKEY**（64位/32位）
2. 放入 `App.config`：
   ```xml
   <appSettings>
     <add key="APPID" value="你的APPID" />
     <add key="SDKKEY64" value="你的64位KEY" />
     <add key="SDKKEY32" value="你的32位KEY" />
     <!-- 摄像头索引 -->
     <add key="RGB_CAMERA_INDEX" value="0" />
     <add key="IR_CAMERA_INDEX" value="1" />
     <!-- 阈值 -->
     <add key="FACE_SIMILARITY_THRESHOLD" value="0.8" />
   </appSettings>
   ```
3. 确保 FaceTemplates 文件夹存在或修改路径

### 4. 运行项目
1. 在 Visual Studio 中打开解决方案 `HospitalEquipmentSystem.sln`
2. 构建并运行 `Program.cs`（入口在 `HospitalEquipmentSystem.UI` 命名空间）
3. 首次运行会弹出登录界面（默认使用测试账号）

## 配置说明

### 数据库配置
- 连接字符串在 `App.config` 中定义
- 必须包含 `HospitalEquipmentDB` 数据库
- 推荐使用 SQL Server Management Studio 管理数据库

### R2 对象存储配置
- 账号信息在 `App.config` 中的 `R2_*` 配置项
- 主要用于存放人脸模板（FaceTemplates）
- 建议创建 Bucket 并设置公共访问

### 虹软 ArcFace 配置
- SDK 版本建议使用最新稳定版
- 摄像头索引根据实际硬件调整
- 相似度阈值建议从 0.7~0.9 开始测试

## 项目结构

```
HospitalEquipmentSystem/
├── HospitalEquipmentSystem.UI/          # 界面层（WinForms）
│   ├── Login/           # 登录相关
│   ├── management/      # 设备管理
│   ├── RepairerMaintain/ # 维修员工作台
│   ├── DoctorMaintain/  # 医生工作台
│   ├── Borrow/          # 借还模块
│   ├── Statistics/      # 统计分析
│   ├── MonitoringCenter/ # 监控中心
│   └── DashboardForm.cs # 主仪表盘
├── HospitalEquipmentSystem.BLL/     # 业务逻辑层
├── HospitalEquipmentSystem.DAL/     # 数据访问层
├── HospitalEquipmentSystem.Model/   # 数据模型
├── HospitalEquipmentSystem.Common/  # 通用类库
├── ArcFaceSDK/                    # 人脸识别 SDK 引用
├── App.config                     # 全局配置文件
├── HospitalEquipmentSystem.sln    # 解决方案文件
└── LICENSE                        # Mulan PSL 2.0 许可证
```

## 许可证

本项目采用 **木兰宽松许可证，第 2 版 (Mulan PSL 2.0)**。

详细条款请参阅 [LICENSE](LICENSE) 文件。

## 贡献

欢迎贡献代码！请遵循以下步骤：
1. Fork 项目
2. 创建功能分支
3. 提交 PR（请确保代码符合代码规范）

## 联系方式

如有问题或建议，请通过项目仓库 Issues 提出。

**注意**：本系统连接到外部数据库和云服务，请确保网络安全和合规使用。