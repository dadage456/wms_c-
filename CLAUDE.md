# CLAUDE.md

此文件为 Claude Code (claude.ai/code) 在此仓库中工作提供指导。

## 项目概述

这是**金风科技 WMS PDA 系统** - 专为运行 Windows CE 的 PDA（个人数字助理）设备设计的仓库管理系统。系统处理仓库操作，包括收货、上架、拣货、盘点和 AGV 集成。

**重要说明：** 这是一个在工业仓库环境中使用的**生产系统**。所有更改必须经过彻底测试并保持向后兼容性。

## 技术栈

- **平台**: Windows CE 5.0 / Windows Mobile 6.0
- **框架**: .NET Compact Framework 2.0
- **语言**: C# 2.0
- **UI**: Windows Forms (.NET Compact Framework)
- **数据库**: SQLite（本地存储）
- **通信**: SOAP WebServices + Socket 备份
- **压缩**: SharpZipLib (GZIP, BZIP2, ZIP)
- **目标设备**: 带有条码扫描器的工业 PDA/HHT 设备

## 架构概述

### 三层架构
```
表示层 (PDA/)
├── 40+ 个不同操作的 Windows 窗体
├── 用户交互和条码扫描
└── 数据输入和显示窗体

业务逻辑层 (BizLayer/)
├── Management.cs（业务逻辑协调）
├── MiddleService.cs（服务集成 - 1818 行代码）
├── Config.cs（配置管理）
└── Web 服务适配器

数据层 (Entity/)
├── Stock.cs（主要业务实体）
├── DownCollectData.cs（采集数据管理）
├── User.cs（用户会话管理）
└── 数据契约和模型

服务集成 (agvtxws/)
├── AGV 系统通信
└── 外部服务同步
```

### 关键设计模式
- **单例模式**: DownCollectData、User、MiddleService、Config、Management
- **工厂模式**: 窗体创建和导航
- **数据传输对象**: Stock、UserInfo 和各种信息类
- **模态对话框模式**: 窗体交互

## 构建和开发命令

### 先决条件
- Visual Studio 2008+ 支持 Windows CE
- .NET Compact Framework 2.0 SDK
- Windows CE 5.0/Windows Mobile 6.0 设备/模拟器

### 构建命令
```bash
# 构建解决方案（Debug）
msbuild P100141PDA.sln /t:Rebuild /p:Configuration=Debug /p:Platform=AnyCPU

# 构建解决方案（Release）
msbuild P100141PDA.sln /t:Rebuild /p:Configuration=Release /p:Platform=AnyCPU

# 使用 Visual Studio
devenv P100141PDA.sln
# 然后：生成 → 生成解决方案 或 生成 → 部署 PDA
```

### 部署
```bash
# 部署到设备（构建后）
# 将这些文件复制到 Windows CE 设备：
# - PDA/bin/Debug/PDA.exe
# - PDA/bin/Debug/BizLayer.dll
# - PDA/bin/Debug/Entity.dll
# - PDA/bin/Debug/ICSHARPCODE.SHARPZIPLIB.DLL
# - PDA/App.cfg（配置文件）
```

## 核心业务逻辑

### 三步采集流程
所有采集操作都遵循此模式：
1. **库位扫描** - 扫描库位条码（$KW$ 格式）
2. **物料二维码扫描** - 扫描物料编码（包含 MC 标识）
3. **数量输入** - 输入数字数量

### 物料控制系统
- **批次控制**: 对具有批次控制的物料验证批次号
- **序列号控制**: 对受控物料强制序列号跟踪
- **库位匹配**: 确保物料在正确的库位
- **ERP 子库一致性**: 验证 ERP 子库代码

### 关键业务操作
- **出库 (Goods Down)**: 出库拣货和发货
- **入库 (Goods Up)**: 收货和上架
- **立库操作 (ASWH)**: 自动化仓库系统集成
- **盘点 (Inventory)**: 库存计数和核对
- **托盘管理 (Tray)**: 托盘和料盘操作
- **转移操作 (Transfer)**: 物料在不同位置间的移动

## 配置管理

### 服务器配置
**文件**: `PDA/App.cfg`
```xml
<appSettings>
  <add key="ServerUrl" value="http://wms.goldwind.com.cn/P100141PDA"/>
  <add key="ServerUrl_bk" value="http://10.12.8.123:8081/P100141PDA"/>
  <add key="ServerUrl_local" value="http://localhost:18923"/>
  <add key="Station" value=""/>
</appSettings>
```

### 配置访问
```csharp
// 通过单例访问配置
Config.ServerUrl     // 主服务器 URL
Config.ServerUrl_bk  // 备用服务器 URL
Config.Station       // 工站标识符
```

## 代码标准和约定

### 命名约定
- **类**: PascalCase（例如：`GoodsDownTaskItemFrm`）
- **方法**: PascalCase（例如：`PerformingBarcode()`）
- **变量**: camelCase（例如：`barcodeContent`）
- **常量**: UPPER_CASE（例如：`MAX_QUANTITY`）
- **窗体**: 后缀 `Frm`（例如：`LoginFrm`）

### 文件组织
- **窗体**: `OperationTypeFrm.cs` + `OperationTypeFrm.Designer.cs` + `OperationTypeFrm.resx`
- **业务逻辑**: `BizLayer/` 使用描述性名称
- **数据模型**: `Entity/` 使用实体名称
- **将设计器生成的代码**仅保存在 `.designer.cs` 文件中

### 编码标准
- **缩进**: 4 个空格
- **大括号**: K&R 风格（开始大括号在同一行）
- **注释**: 业务逻辑使用中文注释，技术代码使用英文注释
- **错误处理**: 全面的 try-catch 块，带有用户友好的消息

## 关键文件和组件

### 入口点和主窗体
- `PDA/Program.cs` - 应用程序入口点
- `PDA/MainFrm.cs` - 主导航窗体
- `PDA/LoginFrm.cs` - 用户认证
- `PDA/GoodsDownTaskItemFrm.cs` - 主要出库采集窗体

### 业务逻辑核心
- `BizLayer/Management.cs` - 业务逻辑协调（单例）
- `BizLayer/MiddleService.cs` - 服务集成（1818 行代码）
- `BizLayer/Config.cs` - 配置管理（单例）

### 数据模型
- `Entity/Stock.cs` - 主要业务实体（19 个属性）
- `Entity/DownCollectData.cs` - 采集数据管理（单例）
- `Entity/User.cs` - 用户会话管理（单例）

### 配置
- `PDA/App.cfg` - 应用程序配置（构建时复制到输出）

## 开发指南

### 添加新操作
1. 按照命名约定创建新窗体：`NewOperationFrm.cs`
2. 在 `MainFrm.cs` 中添加导航按钮
3. 在 `BizLayer/Management.cs` 中实现业务逻辑
4. 在 `BizLayer/MiddleService.cs` 中添加服务方法
5. 在 `Entity/` 中创建相应的实体类

### 测试要求
- **没有自动化测试套件**
- **需要在 Windows CE 设备或模拟器上进行手动测试**
- **测试所有受影响的工作流**：登录、扫描、数据采集、同步
- **验证 `App.cfg` 中的配置端点**
- **尽可能在实际硬件上测试**（条码扫描器、设备限制）

### 窗体开发
- 使用**模态对话框**进行窗体交互
- 实现**适当的资源释放**
- 处理**设备限制**（屏幕尺寸、内存、性能）
- 通过设备特定的 API 支持**条码扫描器集成**
- 在适当的地方为**手指/触摸交互**设计

## 集成模式

### Web 服务通信
- **主要**: 通过 `MiddleService.cs` 使用 SOAP WebServices
- **备份**: 用于回退的 Socket 通信
- **错误处理**: 带有用户友好消息的自定义异常
- **数据格式**: 复杂数据结构使用 ADO.NET DataSets

### 数据验证
- **服务调用前的客户端验证**
- **Management.cs 中的业务规则验证**
- **物料控制验证**（批次、序列号、库位）
- **所有操作的 ERP 一致性检查**

## 常见问题和解决方案

### 设备特定问题
- **内存限制**: 最小化窗体复杂性和数据保留
- **屏幕尺寸**: 为小屏幕设计（典型 240x320）
- **条码扫描器**: 使用实际设备硬件测试
- **网络连接**: 在可能的地方实现离线功能

### 配置问题
- **服务器 URL 更改**: 更新 `App.cfg` 并重新部署
- **工作站配置**: 设置适当的工作站标识符
- **备份服务器**: 确保备用 URL 可访问

### 性能考虑
- **最小化数据传输**: 对大型数据集使用压缩
- **优化窗体加载**: 在适当的地方使用延迟加载
- **减少内存使用**: 适当的资源释放
- **缓存常用数据**: 参考数据的内存缓存

## API 文档

全面的 API 文档可在 `/api.md` 中找到，包含所有仓库操作的详细接口规范：
- 认证和用户管理
- 出库和入库操作
- 库存管理
- 自动化仓库集成
- 物料控制验证

## 业务规则文档

详细的业务规则记录在：
- `出库采集业务规则文档.md` - 出库采集业务规则
- `下架采集页面业务流程图.md` - 业务流程图
- `AGENTS.md` - 开发指南和最佳实践

## 重要说明

1. **生产系统**: 这是在工业环境中使用的实时系统
2. **遗留技术**: Windows CE 和 .NET CF 2.0 需要专门的知识
3. **无自动化测试**: 所有更改都需要手动测试
4. **设备限制**: 始终考虑 PDA 硬件限制
5. **中文界面**: 所有面向用户的文本都是中文
6. **条码集成**: 核心功能依赖于条码扫描器硬件