# GlodWind WMS App API 接口文档

## 概述

本文档基于实际代码分析，提供了 GlodWind WMS App 中所有 API 接口的准确描述。所有接口定义均从源代码中提取，确保与实际实现一致。

## API接口文档格式规范

本文档中所有API接口均采用以下统一格式：

### 接口文档结构模板

```
### X.X 接口名称

**基本信息**
- 接口名称：接口的中文名称
- 接口地址：完整的API路径
- 请求方法：GET/POST/PUT/DELETE
- 使用页面：调用该接口的页面文件
- 业务场景：接口的主要用途和应用场景

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| paramName | string/number/boolean | 是/否 | 默认值 | 参数说明 |

**入参示例**
```javascript
{
  param1: "值1",  // 参数说明
  param2: "值2"   // 参数说明
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    // 响应数据结构
  }
}
```

**业务说明**
- 接口的核心功能和业务逻辑
- 数据处理方式和规则
- 与其他接口的关联关系

**使用场景**
- 具体的使用场景和条件
- 前端页面中的应用方式
- 用户操作流程
```

### 格式要求
1. **基本信息**：包含接口的核心标识信息
2. **请求参数**：使用表格形式，清晰展示参数属性
3. **入参示例**：提供JavaScript格式的参数示例，便于理解
4. **响应格式**：展示标准的JSON响应结构
5. **业务说明**：描述接口的业务逻辑和功能
6. **使用场景**：说明接口的实际应用场景

### 注意事项
- 不包含函数定义代码
- 不包含调用示例代码
- 保持格式统一和简洁
- 重点关注参数和响应结构

## 基础配置

### 请求基础信息
- **基础URL**: `http://10.12.8.123:8086`
- **认证方式**: Bearer Token
- **请求格式**: JSON
- **响应格式**: JSON

### 请求工具
所有API请求通过 `/utils/request.js` 统一处理，包含：
- 自动添加认证token
- 统一错误处理
- 请求/响应日志

---

## 1. 登录认证接口

### 1.1 获取验证码图片

**基本信息**
- 接口名称：获取验证码图片
- 接口地址：/captchaImage
- 请求方法：GET
- 使用页面：登录页面
- 业务场景：获取登录验证码图片，用于用户登录验证

**请求参数**
无

**入参示例**
无

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "uuid": "验证码唯一标识",
  "img": "base64编码的验证码图片"
}
```

**业务说明**
- 生成验证码图片和对应的UUID
- 验证码用于登录时的安全验证
- 返回base64格式的图片数据

**使用场景**
- 用户登录页面显示验证码
- 登录时验证用户输入的验证码
- 防止恶意登录和机器人攻击

### 1.2 用户登录

**基本信息**
- 接口名称：用户登录
- 接口地址：/login
- 请求方法：POST
- 使用页面：登录页面
- 业务场景：用户通过用户名、密码和验证码进行系统登录

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| username | string | 是 | - | 用户名 |
| password | string | 是 | - | 密码 |
| code | string | 是 | - | 验证码 |
| uuid | string | 是 | - | 验证码唯一标识 |
| clientid | string | 是 | - | 客户端标识 |

**入参示例**
```javascript
{
  username: "admin",     // 用户名
  password: "123456",    // 密码
  code: "ABC123",        // 验证码
  uuid: "uuid123",       // 验证码唯一标识
  clientid: "client001"  // 客户端标识
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "token": "Bearer认证令牌"
}
```

**业务说明**
- 验证用户名和密码的正确性
- 验证验证码的有效性
- 生成并返回JWT令牌用于后续接口认证
- 记录用户登录状态和设备信息

**使用场景**
- 用户在登录页面输入账号密码
- 系统验证用户身份和权限
- 登录成功后获取访问令牌
- 用于系统的身份认证和授权

### 1.3 获取用户信息

**基本信息**
- 接口名称：获取用户信息
- 接口地址：/getInfo
- 请求方法：GET
- 使用页面：所有需要用户信息的页面
- 业务场景：获取当前登录用户的详细信息、角色和权限

**请求参数**
无

**入参示例**
无

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "user": {
    "userId": 1,
    "userName": "用户名",
    "nickName": "昵称"
  },
  "roles": ["admin"],
  "permissions": ["*:*:*"]
}
```

**业务说明**
- 获取当前登录用户的完整信息
- 包含用户基本信息、角色和权限数据
- 用于前端页面权限控制和用户信息显示
- 需要在请求头中携带有效的认证令牌

**使用场景**
- 用户登录后获取个人信息
- 页面权限控制和菜单显示
- 用户信息展示和个性化设置
- 系统权限管理和访问控制

### 1.4 用户登出

**基本信息**
- 接口名称：用户登出
- 接口地址：/logout
- 请求方法：POST
- 使用页面：登出按钮所在页面
- 业务场景：用户安全退出系统，清除登录状态

**请求参数**
无

**入参示例**
无

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功"
}
```

**业务说明**
- 清除用户的登录状态和会话信息
- 使当前认证令牌失效
- 在服务端记录用户登出日志
- 清理相关的缓存和临时数据

**使用场景**
- 用户主动点击登出按钮
- 令牌过期或失效时重新登录
- 安全要求下的强制登出
- 用户切换账号时使用

---

## 2. 用户管理接口

### 2.1 修改用户密码

**基本信息**
- 接口名称：修改用户密码
- 接口地址：/system/user/profile/updatePwd
- 请求方法：PUT
- 使用页面：用户个人中心
- 业务场景：用户修改自己的登录密码

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| oldPassword | string | 是 | - | 旧密码 |
| newPassword | string | 是 | - | 新密码 |

**入参示例**
```javascript
{
  oldPassword: "old123",   // 旧密码
  newPassword: "new456"    // 新密码
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "密码修改成功"
}
```

**业务说明**
- 验证用户输入的旧密码是否正确
- 更新用户密码为新密码
- 密码修改后需要重新登录
- 记录密码修改日志

**使用场景**
- 用户定期修改密码
- 密码泄露或安全风险时修改
- 用户忘记密码后重置
- 系统安全要求强制修改

### 2.2 获取用户详细信息

**基本信息**
- 接口名称：获取用户详细信息
- 接口地址：/system/user/profile
- 请求方法：GET
- 使用页面：用户个人中心
- 业务场景：获取当前用户的详细个人信息

**请求参数**
无

**入参示例**
无

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "userId": 1,
    "userName": "用户名",
    "nickName": "昵称",
    "email": "邮箱",
    "phonenumber": "手机号",
    "sex": "性别",
    "avatar": "头像地址"
  }
}
```

**业务说明**
- 获取用户的完整个人信息
- 包含基本信息、联系方式等
- 用于个人中心页面展示
- 需要登录认证

**使用场景**
- 个人中心页面信息展示
- 用户资料查看和编辑
- 头像和个人信息显示
- 用户身份确认

### 2.3 更新用户信息

**基本信息**
- 接口名称：更新用户信息
- 接口地址：/system/user/profile
- 请求方法：PUT
- 使用页面：用户个人中心
- 业务场景：更新用户的个人信息

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| nickName | string | 否 | - | 昵称 |
| email | string | 否 | - | 邮箱 |
| phonenumber | string | 否 | - | 手机号 |
| sex | string | 否 | - | 性别 |

**入参示例**
```javascript
{
  nickName: "新昵称",    // 昵称
  email: "new@email.com", // 邮箱
  phonenumber: "13800138000", // 手机号
  sex: "1"              // 性别：0-女，1-男
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "修改成功"
}
```

**业务说明**
- 更新用户的基本信息
- 支持昵称、邮箱、手机号、性别等字段
- 自动验证数据格式和有效性
- 记录信息修改日志

**使用场景**
- 个人资料更新
- 用户信息维护
- 联系方式变更
- 个人信息完善

### 2.4 更新用户头像

**基本信息**
- 接口名称：更新用户头像
- 接口地址：/system/user/profile/avatar
- 请求方法：POST
- 使用页面：用户个人中心
- 业务场景：用户更新个人头像图片

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| avatarfile | file | 是 | - | 头像图片文件 |

**入参示例**
```javascript
{
  avatarfile: "图片文件对象"  // 头像图片文件
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "imgUrl": "头像图片地址"
}
```

**业务说明**
- 上传并更新用户头像
- 自动处理图片文件格式转换
- 生成头像URL地址
- 删除旧头像文件

**使用场景**
- 用户更换个人头像
- 头像上传和处理
- 个人资料更新
- 用户界面个性化

---

## 3. 出库管理接口

### 3.1 获取出库任务列表

**基本信息**
- 接口名称：获取出库任务列表
- 接口地址：/system/terminal/outList
- 请求方法：GET
- 使用页面：goodsDown.nvue
- 业务场景：获取用户需要处理的出库任务列表

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| sortType | string | 否 | "" | 排序类型 |
| sortColumn | string | 否 | "" | 排序字段 |
| searchKey | string | 否 | "" | 搜索关键词 |
| userId | number | 是 | - | 用户ID |
| roleoRuserId | number | 是 | - | 角色或用户ID |
| roomTag | string | 否 | "0" | 库房标记 |
| batchflag | string | 否 | "0" | 批次标记 |
| transferType | string | 否 | "0" | 转移类型 |
| beatflag | string | 否 | "N" | 节拍标记 |
| PageIndex | string | 是 | "1" | 页码 |
| PageSize | string | 是 | "100" | 页大小 |
| finshFlg | string | 否 | "0" | 完成标记 |

**入参示例**
```javascript
{
  sortType: "",          // 排序类型
  sortColumn: "",        // 排序字段
  searchKey: "",        // 搜索关键词
  userId: 123456,        // 用户ID
  roleoRuserId: 789012,   // 角色或用户ID
  roomTag: "0",         // 库房标记
  batchflag: "0",       // 批次标记
  transferType: "0",    // 转移类型
  beatflag: "N",        // 节拍标记
  PageIndex: "1",       // 页码
  PageSize: "100",      // 页大小
  finshFlg: "0"         // 完成标记：0-未完成，1-已完成
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "total": 100,
    "rows": [
      {
        "outtaskid": "出库任务ID",
        "taskcode": "任务编号",
        "matcode": "物料编码",
        "matname": "物料名称",
        "quantity": "数量",
        "status": "状态"
      }
    ]
  }
}
```

**业务说明**
- 根据用户ID查询出库任务
- 支持分页查询功能
- 可按完成状态过滤
- 支持排序和搜索功能
- 返回任务的基本信息
- 用于任务列表展示和选择

**使用场景**
- 出库任务列表页面
- 用户查看待处理任务
- 任务状态跟踪
- 任务选择和接收

### 3.2 获取出库任务明细

**基本信息**
- 接口名称：获取出库任务明细
- 接口地址：/system/terminal/outTaskitemList
- 请求方法：GET
- 使用页面：goodsDownDetail.nvue
- 业务场景：获取指定出库任务的明细信息

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| sortType | string | 否 | "" | 排序类型 |
| sortColumn | string | 否 | "" | 排序字段 |
| searchKey | string | 否 | "" | 搜索关键词 |
| outtaskid | string | 是 | - | 出库任务ID |
| workstation | string | 是 | - | 工作站 |
| userId | number | 是 | - | 用户ID |
| roleoRuserId | number | 是 | - | 角色或用户ID |
| roomTag | string | 否 | "0" | 库房标签 |
| batchflag | string | 否 | "0" | 批次标志 |
| transferType | string | 否 | "0" | 转移类型 |
| beatflag | string | 否 | "N" | 节拍标志 |
| PageIndex | string | 是 | "1" | 页码 |
| PageSize | string | 是 | "100" | 页大小 |

**入参示例**
```javascript
{
  sortType: "",          // 排序类型
  sortColumn: "",        // 排序字段
  searchKey: "",         // 搜索关键词
  outtaskid: "出库任务ID", // 出库任务ID
  workstation: "工作站",    // 工作站
  userId: 123456,        // 用户ID
  roleoRuserId: 789012,   // 角色或用户ID
  roomTag: "0",          // 库房标签
  batchflag: "0",        // 批次标志
  transferType: "0",     // 转移类型
  beatflag: "N",         // 节拍标志
  PageIndex: "1",        // 页码
  PageSize: "100"        // 页大小
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "total": 50,
    "rows": [
      {
        "outtaskitemid": "任务明细ID",
        "matcode": "物料编码",
        "matname": "物料名称",
        "quantity": "数量",
        "storesiteno": "库位编码",
        "batchno": "批次号",
        "sn": "序列号"
      }
    ]
  }
}
```

**业务说明**
- 根据任务ID获取明细列表
- 支持按物料编码和库位搜索
- 分页返回明细数据
- 包含物料、库位、批次等信息
- 用于明细展示和采集操作

**使用场景**
- 出库任务明细页面
- 明细信息展示
- 采集操作准备
- 数据核对和确认

### 3.3 获取已接收的出库任务明细

**基本信息**
- 接口名称：获取已接收的出库任务明细
- 接口地址：/system/terminal/getOutTaskItem
- 请求方法：GET
- 使用页面：goodsDownDetail.nvue
- 业务场景：获取出库任务的采集明细信息

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| outtaskid | string | 是 | - | 出库任务ID |

**入参示例**
```javascript
{
  outtaskid: "出库任务ID"   // 出库任务ID
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "outtaskid": "出库任务ID",
    "items": [
      {
        "outtaskitemid": "任务明细ID",
        "matcode": "物料编码",
        "quantity": "数量",
        "storesiteno": "库位编码",
        "batchno": "批次号",
        "sn": "序列号",
        "status": "状态"
      }
    ]
  }
}
```

**业务说明**
- 获取出库任务的采集明细
- 包含所有需要采集的物料信息
- 提供库位、批次、序列号等详细信息
- 为采集操作提供完整数据
- 支持多种物料类型的采集

**使用场景**
- 出库采集明细展示
- 采集任务准备
- 物料信息确认
- 采集操作指导

### 3.4 校验库位

**基本信息**
- 接口名称：校验库位
- 接口地址：/system/terminal/getStoreSite
- 请求方法：GET
- 使用页面：goodsDownDetail.nvue
- 业务场景：根据库房编码和库位编码校验库位的有效性

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| storeRoomNo | string | 是 | - | 库房编码 |
| storeSiteNo | string | 是 | - | 库位编码 |

**入参示例**
```javascript
{
  storeRoomNo: "库房编码",   // 库房编码
  storeSiteNo: "库位编码"    // 库位编码
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "storeroomno": "库房编码",
    "storesiteno": "库位编码",
    "sitename": "库位名称",
    "sitetype": "库位类型",
    "status": "状态",
    "capacity": "容量",
    "usedcapacity": "已用容量"
  }
}
```

**业务说明**
- 验证库房和库位编码的有效性
- 返回库位的详细信息
- 检查库位状态和容量
- 为后续操作提供库位信息
- 确保采集操作的准确性

**使用场景**
- 出库采集前库位校验
- 库位信息确认
- 库位状态检查
- 采集操作准备

### 3.5 根据库位获取库存

**基本信息**
- 接口名称：根据库位获取库存
- 接口地址：/system/terminal/getRepertoryByStoresiteNo
- 请求方法：GET
- 使用页面：goodsDownDetail.nvue
- 业务场景：根据库位和物料信息获取库存数据

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| storesiteno | string | 是 | - | 库位编码 |
| matcode | string | 是 | - | 物料编码 |
| erpStoreroom | string | 是 | - | ERP子库 |
| batchno | string | 是 | - | 批次号 |
| sn | string | 是 | - | 序列号 |

**入参示例**
```javascript
{
  storesiteno: "库位编码",   // 库位编码
  matcode: "物料编码",      // 物料编码
  erpStoreroom: "ERP子库",  // ERP子库
  batchno: "批次号",        // 批次号
  sn: "序列号"              // 序列号
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "storesiteno": "库位编码",
    "matcode": "物料编码",
    "matname": "物料名称",
    "quantity": "库存数量",
    "batchno": "批次号",
    "sn": "序列号",
    "erpstoreroom": "ERP子库",
    "unit": "单位",
    "status": "状态"
  }
}
```

**业务说明**
- 查询指定条件的库存信息
- 验证库存是否存在和可用
- 返回详细的库存数据
- 包含物料、数量、批次等信息
- 为出库操作提供库存验证

**使用场景**
- 出库采集前库存验证
- 库存信息查询
- 数量确认
- 出库资格验证

### 3.6 根据库位获取库存（带序列号）

**基本信息**
- 接口名称：根据库位获取库存（带序列号）
- 接口地址：/system/terminal/getRepertoryByStoresiteNoSn
- 请求方法：GET
- 使用页面：goodsDownDetail.nvue
- 业务场景：获取含序列号的库存信息

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| storesiteno | string | 是 | - | 库位编码 |
| matcode | string | 是 | - | 物料编码 |
| erpStoreroom | string | 是 | - | ERP子库 |
| batchno | string | 是 | - | 批次号 |
| sn | string | 是 | - | 序列号 |

**入参示例**
```javascript
{
  storesiteno: "库位编码",   // 库位编码
  matcode: "物料编码",      // 物料编码
  erpStoreroom: "ERP子库",  // ERP子库
  batchno: "批次号",        // 批次号
  sn: "序列号"              // 序列号
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "storesiteno": "库位编码",
    "matcode": "物料编码",
    "matname": "物料名称",
    "quantity": "库存数量",
    "batchno": "批次号",
    "sn": "序列号",
    "erpstoreroom": "ERP子库",
    "unit": "单位",
    "status": "状态",
    "sncontrol": "序列号控制"
  }
}
```

**业务说明**
- 专门处理序列号控制的库存查询
- 验证序列号的有效性
- 提供序列号相关的详细信息
- 支持序列号管理的物料
- 确保序列号唯一性

**使用场景**
- 序列号物料出库
- 序列号验证
- 序列号管理
- 质量追溯需求

### 3.7 根据库位获取库存（ERP）

**基本信息**
- 接口名称：根据库位获取库存（ERP）
- 接口地址：/system/terminal/getRepertoryByStoresiteNoErp
- 请求方法：GET
- 使用页面：goodsDownDetail.nvue
- 业务场景：基于ERP子库获取库存信息

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| storesiteno | string | 是 | - | 库位编码 |
| matcode | string | 是 | - | 物料编码 |

**入参示例**
```javascript
{
  storesiteno: "库位编码",   // 库位编码
  matcode: "物料编码"       // 物料编码
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "storesiteno": "库位编码",
    "matcode": "物料编码",
    "matname": "物料名称",
    "quantity": "库存数量",
    "erpstoreroom": "ERP子库",
    "unit": "单位",
    "status": "状态"
  }
}
```

**业务说明**
- 简化的库存查询接口
- 基于库位和物料编码查询
- 返回ERP子库相关信息
- 提供基础库存数据
- 支持快速库存验证

**使用场景**
- 快速库存查询
- ERP子库验证
- 基础库存信息获取
- 库存状态确认

### 3.8 提交下架采集

**基本信息**
- 接口名称：提交下架采集
- 接口地址：/system/terminal/commitDownShelves
- 请求方法：POST
- 使用页面：goodsDownDetail.nvue
- 业务场景：提交平库下架采集的数据

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| downShelvesInfos | array | 是 | - | 下架信息数组 |
| itemListInfos | array | 是 | - | 明细信息数组 |

**入参示例**
```javascript
{
  downShelvesInfos: [      // 下架信息数组
    {
      outtaskid: "任务ID",   // 出库任务ID
      storesiteno: "库位编码", // 库位编码
      quantity: "数量",      // 数量
      unit: "单位",          // 单位
      batchno: "批次号",     // 批次号
      sn: "序列号"           // 序列号
    }
  ],
  itemListInfos: [         // 明细信息数组
    {
      outtaskitemid: "任务明细ID", // 出库任务明细ID
      quantity: "数量"             // 数量
    }
  ]
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "success": true,
    "taskId": "任务ID",
    "processedItems": 10
  }
}
```

**业务说明**
- 提交完整的下架采集数据
- 包含下架信息和明细信息
- 自动处理数据验证和业务逻辑
- 更新库存和任务状态
- 生成操作记录

**使用场景**
- 平库下架数据提交
- 采集结果保存
- 库存更新
- 任务状态更新

### 3.9 提交出库任务项

**基本信息**
- 接口名称：提交出库任务项
- 接口地址：/system/terminal/commitRCOutTaskItem
- 请求方法：POST
- 使用页面：goodsDownDetail.nvue
- 业务场景：提交或撤销出库任务明细

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| outtaskitemids | array | 是 | - | 出库任务明细ID数组 |
| roomTag | string | 是 | - | 库房标签 |
| isCanel | string | 是 | - | 是否取消 |

**入参示例**
```javascript
{
  outtaskitemids: ["明细ID1", "明细ID2"], // 出库任务明细ID数组
  roomTag: "0",                          // 库房标签
  isCanel: "false"                        // 是否取消：true-取消，false-提交
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "success": true,
    "processedCount": 2,
    "action": "提交/撤销"
  }
}
```

**业务说明**
- 支持任务明细的提交和撤销
- 批量处理多个明细
- 更新任务状态
- 记录操作日志
- 支持库房标签管理

**使用场景**
- 任务明细提交
- 任务撤销操作
- 批量处理
- 状态管理

### 3.10 获取物料控制信息

**基本信息**
- 接口名称：获取物料控制信息
- 接口地址：/system/terminal/getMatControl
- 请求方法：GET
- 使用页面：goodsDownDetail.nvue
- 业务场景：获取物料的控制属性信息

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| matCode | string | 是 | - | 物料编码 |

**入参示例**
```javascript
{
  matCode: "物料编码"   // 物料编码
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "matcode": "物料编码",
    "batchcontrol": "批次控制标志",
    "sncontrol": "序列号控制标志",
    "qualitycontrol": "质量控制标志",
    "expirationcontrol": "有效期控制标志"
  }
}
```

**业务说明**
- 获取物料的各种控制属性
- 包括批次控制、序列号控制等
- 确定物料的库存管理方式
- 为采集操作提供控制依据
- 影响出库采集的操作流程

**使用场景**
- 出库采集前确认物料控制属性
- 确定是否需要批次号或序列号
- 质量控制和有效期管理的依据
- 库存管理规则的制定

### 3.11 获取库房物料控制信息

**基本信息**
- 接口名称：获取库房物料控制信息
- 接口地址：/system/terminal/getRoomMatControl
- 请求方法：GET
- 使用页面：goodsDownDetail.nvue
- 业务场景：获取特定库房中物料的控制信息

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| taskId | string | 是 | - | 任务ID |

**入参示例**
```javascript
{
  taskId: "任务ID"  // 任务ID
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "taskid": "任务ID",
    "roomno": "库房编码",
    "matcontrol": "物料控制信息",
    "batchrequired": "批次要求",
    "snrequired": "序列号要求"
  }
}
```

**业务说明**
- 获取任务相关的库房物料控制信息
- 基于任务ID查询对应的控制规则
- 提供库房特定的物料管理要求
- 为采集操作提供具体指导
- 包含批次和序列号的具体要求

**使用场景**
- 任务执行前确认控制要求
- 库房特定的物料管理
- 采集操作规则的制定
- 任务执行条件的验证

### 3.12 获取出入库位置

**基本信息**
- 接口名称：获取出入库位置
- 接口地址：/system/terminal/getInOutLocation
- 请求方法：GET
- 使用页面：goodsDownDetail.nvue
- 业务场景：获取仓库的出入库位置信息

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| locationType | string | 是 | - | 位置类型（入库/出库） |

**入参示例**
```javascript
{
  locationType: "OUT"  // 位置类型：OUT-出库，IN-入库
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "locationid": "位置ID",
    "locationname": "位置名称",
    "locationtype": "位置类型",
    "status": "状态",
    "description": "描述"
  }
}
```

**业务说明**
- 获取仓库的出入库位置信息
- 支持入库和出库位置查询
- 提供位置的状态和描述
- 为仓库操作提供位置指导
- 支持多位置管理

**使用场景**
- 出入库位置确认
- 仓库位置管理
- 操作位置指导
- 位置状态查询

### 3.13 提交立库下架采集

**基本信息**
- 接口名称：提交立库下架采集
- 接口地址：/system/terminal/commitASWHDownShelves
- 请求方法：POST
- 使用页面：aswhDown.nvue
- 业务场景：提交自动化仓库的下架采集数据

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| downShelvesInfos | array | 是 | - | 下架信息数组 |
| itemListInfos | array | 是 | - | 明细信息数组 |
| invCheckInfos | array | 是 | - | 盘点信息数组 |

**入参示例**
```javascript
{
  downShelvesInfos: [      // 下架信息数组
    {
      outtaskid: "任务ID",   // 出库任务ID
      palletno: "托盘号",    // 托盘号
      storesiteno: "库位编码", // 库位编码
      quantity: "数量",      // 数量
      unit: "单位"           // 单位
    }
  ],
  itemListInfos: [         // 明细信息数组
    {
      outtaskitemid: "任务明细ID", // 出库任务明细ID
      quantity: "数量"             // 数量
    }
  ],
  invCheckInfos: [         // 盘点信息数组
    {
      matcode: "物料编码",  // 物料编码
      quantity: "数量"      // 数量
    }
  ]
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "success": true,
    "taskId": "任务ID",
    "palletProcessed": true
  }
}
```

**业务说明**
- 提交立库下架的完整采集数据
- 包含下架信息、明细信息和盘点信息
- 支持托盘化管理
- 自动处理盘点数据
- 更新立库系统状态

**使用场景**
- 立库下架数据提交
- 托盘化管理
- 盘点数据处理
- 自动化仓库操作

### 3.14 下架指令写入WCS

**基本信息**
- 接口名称：下架指令写入WCS
- 接口地址：/system/terminal/commitDownWmsToWcs
- 请求方法：GET
- 使用页面：aswhDown.nvue
- 业务场景：向下架WCS系统发送指令

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| taskId | string | 是 | - | 任务ID |
| taskNo | string | 是 | - | 任务编号 |
| trayNo | string | 是 | - | 托盘号 |
| startAddr | string | 是 | - | 起始地址 |
| endAddr | string | 是 | - | 结束地址 |
| singleFlag | string | 是 | - | 单次标志 |

**入参示例**
```javascript
{
  taskId: "任务ID",      // 任务ID
  taskNo: "任务编号",    // 任务编号
  trayNo: "托盘号",      // 托盘号
  startAddr: "起始地址",  // 起始地址
  endAddr: "结束地址",    // 结束地址
  singleFlag: "1"        // 单次标志
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "success": true,
    "commandId": "指令ID",
    "taskStatus": "已发送"
  }
}
```

**业务说明**
- 向WCS系统发送下架指令
- 包含任务和托盘的完整信息
- 指定操作的起始和结束地址
- 支持单次和连续操作
- 实现WMS与WCS系统集成

**使用场景**
- 立库下架指令发送
- WCS系统集成
- 自动化设备控制
- 任务执行指令

### 3.15 盘库下架指令写入WCS

**基本信息**
- 接口名称：盘库下架指令写入WCS
- 接口地址：/system/terminal/commitInvDownWmsToWcs
- 请求方法：GET
- 使用页面：aswhInventorytask.nvue
- 业务场景：向盘点WCS系统发送指令

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| taskId | string | 是 | - | 任务ID |
| taskNo | string | 是 | - | 任务编号 |
| trayNo | string | 是 | - | 托盘号 |
| startAddr | string | 是 | - | 起始地址 |
| endAddr | string | 是 | - | 结束地址 |
| singleFlag | string | 是 | - | 单次标志 |

**入参示例**
```javascript
{
  taskId: "任务ID",      // 任务ID
  taskNo: "任务编号",    // 任务编号
  trayNo: "托盘号",      // 托盘号
  startAddr: "起始地址",  // 起始地址
  endAddr: "结束地址",    // 结束地址
  singleFlag: "1"        // 单次标志
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "success": true,
    "commandId": "指令ID",
    "taskStatus": "盘点中"
  }
}
```

**业务说明**
- 向WCS系统发送盘点指令
- 包含盘点任务的完整信息
- 指定盘点操作的地址范围
- 支持盘点设备的自动化控制
- 实现盘点系统集成

**使用场景**
- 立库盘点指令发送
- 自动化盘点设备控制
- WCS系统集成
- 盘点任务执行

### 3.16 空托盘指令写入WCS

**基本信息**
- 接口名称：空托盘指令写入WCS
- 接口地址：/system/terminal/commitEmptyTrayWmsToWcs
- 请求方法：GET
- 使用页面：aswhDown.nvue
- 业务场景：向空托盘WCS系统发送指令

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| taskId | string | 是 | - | 任务ID |
| taskNo | string | 是 | - | 任务编号 |
| trayNo | string | 是 | - | 托盘号 |
| startAddr | string | 是 | - | 起始地址 |
| endAddr | string | 是 | - | 结束地址 |
| singleFlag | string | 是 | - | 单次标志 |

**入参示例**
```javascript
{
  taskId: "任务ID",      // 任务ID
  taskNo: "任务编号",    // 任务编号
  trayNo: "托盘号",      // 托盘号
  startAddr: "起始地址",  // 起始地址
  endAddr: "结束地址",    // 结束地址
  singleFlag: "1"        // 单次标志
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "success": true,
    "commandId": "指令ID",
    "taskStatus": "空托盘处理中"
  }
}
```

**业务说明**
- 向WCS系统发送空托盘处理指令
- 包含空托盘移动的完整信息
- 指定空托盘的起始和目标位置
- 支持空托盘的自动化管理
- 实现空托盘调度优化

**使用场景**
- 空托盘移动指令
- 托盘调度管理
- WCS系统集成
- 空托盘回收

### 3.17 获取盘库任务

**基本信息**
- 接口名称：获取盘库任务
- 接口地址：/system/terminal/getInventoryTask
- 请求方法：GET
- 使用页面：Inventorytask.nvue, aswhInventorytask.nvue
- 业务场景：获取用户需要处理的盘点任务列表

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| userId | number | 是 | - | 用户ID |
| taskType | string | 否 | - | 任务类型 |
| status | string | 否 | - | 状态 |

**入参示例**
```javascript
{
  userId: 123456,     // 用户ID
  taskType: "平库",   // 任务类型
  status: "进行中"    // 状态
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "total": 20,
    "rows": [
      {
        "taskid": "盘点任务ID",
        "taskcode": "任务编号",
        "taskname": "任务名称",
        "tasktype": "任务类型",
        "status": "状态",
        "createdate": "创建日期"
      }
    ]
  }
}
```

**业务说明**
- 根据用户ID查询盘点任务
- 支持按任务类型和状态过滤
- 返回任务的基本信息
- 支持平库和立库盘点
- 用于盘点任务管理

**使用场景**
- 盘点任务列表展示
- 任务分配和接收
- 盘点任务跟踪
- 库存管理

### 3.18 提交盘库任务

**基本信息**
- 接口名称：提交盘库任务
- 接口地址：/system/terminal/commitInventoryTask
- 请求方法：GET
- 使用页面：Inventorytask.nvue, aswhInventorytask.nvue
- 业务场景：提交或取消盘点任务

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| taskcomment | string | 是 | - | 任务备注 |
| userId | number | 是 | - | 用户ID |
| isCanel | string | 是 | - | 是否取消 |

**入参示例**
```javascript
{
  taskcomment: "任务备注",  // 任务备注
  userId: 123456,         // 用户ID
  isCanel: "false"        // 是否取消：true-取消，false-提交
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "success": true,
    "taskId": "任务ID",
    "action": "提交/取消"
  }
}
```

**业务说明**
- 支持盘点任务的提交和取消
- 记录任务备注信息
- 更新任务状态
- 记录操作用户
- 支持任务生命周期管理

**使用场景**
- 盘点任务提交
- 任务取消操作
- 任务状态管理
- 用户操作记录

### 3.19 获取盘库任务明细

**基本信息**
- 接口名称：获取盘库任务明细
- 接口地址：/system/terminal/getInventoryTaskItem
- 请求方法：GET
- 使用页面：Inventorytask.nvue, aswhInventorytask.nvue
- 业务场景：获取盘点任务的明细信息

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| taskId | string | 是 | - | 任务ID |

**入参示例**
```javascript
{
  taskId: "任务ID"  // 任务ID
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "taskid": "任务ID",
    "items": [
      {
        "itemid": "明细ID",
        "matcode": "物料编码",
        "matname": "物料名称",
        "storesiteno": "库位编码",
        "systemquantity": "系统数量",
        "actualquantity": "实际数量",
        "difference": "差异数量"
      }
    ]
  }
}
```

**业务说明**
- 获取盘点任务的明细列表
- 包含系统数量和实际数量
- 计算差异数量
- 支持多种盘点类型
- 为盘点操作提供数据支持

**使用场景**
- 盘点明细展示
- 数量核对
- 差异分析
- 盘点结果确认

### 3.20 提交盘库信息

**基本信息**
- 接口名称：提交盘库信息
- 接口地址：/system/terminal/commitInventoryInfos
- 请求方法：POST
- 使用页面：Inventorytask.nvue, aswhInventorytask.nvue
- 业务场景：提交盘点采集的数据

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| inventoryInfos | array | 是 | - | 盘点信息数组 |
| taskComment | string | 是 | - | 任务备注 |

**入参示例**
```javascript
{
  inventoryInfos: [        // 盘点信息数组
    {
      itemid: "明细ID",      // 明细ID
      matcode: "物料编码",   // 物料编码
      storesiteno: "库位编码", // 库位编码
      systemquantity: "系统数量", // 系统数量
      actualquantity: "实际数量",  // 实际数量
      difference: "差异数量"       // 差异数量
    }
  ],
  taskComment: "盘点任务备注"  // 任务备注
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "success": true,
    "taskId": "任务ID",
    "processedItems": 10
  }
}
```

**业务说明**
- 提交完整的盘点数据
- 包含系统数量和实际数量对比
- 自动计算差异数量
- 支持批量提交
- 更新库存和盘点状态

**使用场景**
- 盘点数据提交
- 库存调整
- 差异处理
- 盘点结果确认

### 3.21 重置指令写入WCS

**基本信息**
- 接口名称：重置指令写入WCS
- 接口地址：/system/terminal/commitResetWmsToWcs
- 请求方法：GET
- 使用页面：aswhDown.nvue
- 业务场景：向WCS系统发送重置指令

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| taskId | string | 是 | - | 任务ID |
| taskNo | string | 是 | - | 任务编号 |
| trayNo | string | 是 | - | 托盘号 |
| startAddr | string | 是 | - | 起始地址 |
| endAddr | string | 是 | - | 结束地址 |

**入参示例**
```javascript
{
  taskId: "任务ID",      // 任务ID
  taskNo: "任务编号",    // 任务编号
  trayNo: "托盘号",      // 托盘号
  startAddr: "起始地址",  // 起始地址
  endAddr: "结束地址"    // 结束地址
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "success": true,
    "commandId": "指令ID",
    "resetStatus": "已重置"
  }
}
```

**业务说明**
- 向WCS系统发送重置指令
- 用于系统异常或任务中断后的重置
- 包含重置任务的完整信息
- 指定重置的地址范围
- 支持系统状态恢复

**使用场景**
- 系统重置操作
- 任务中断恢复
- 异常状态处理
- WCS系统维护

### 3.22 盘库重置指令写入WCS

**基本信息**
- 接口名称：盘库重置指令写入WCS
- 接口地址：/system/terminal/commitInvResetWmsToWcs
- 请求方法：GET
- 使用页面：aswhInventorytask.nvue
- 业务场景：向盘点WCS系统发送重置指令

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| taskId | string | 是 | - | 任务ID |
| taskNo | string | 是 | - | 任务编号 |
| trayNo | string | 是 | - | 托盘号 |
| startAddr | string | 是 | - | 起始地址 |
| endAddr | string | 是 | - | 结束地址 |

**入参示例**
```javascript
{
  taskId: "任务ID",      // 任务ID
  taskNo: "任务编号",    // 任务编号
  trayNo: "托盘号",      // 托盘号
  startAddr: "起始地址",  // 起始地址
  endAddr: "结束地址"    // 结束地址
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "success": true,
    "commandId": "指令ID",
    "resetStatus": "盘点已重置"
  }
}
```

**业务说明**
- 向WCS系统发送盘点重置指令
- 用于盘点任务异常或中断后的重置
- 包含盘点重置的完整信息
- 指定重置的地址范围
- 支持盘点系统恢复

**使用场景**
- 盘点任务重置
- 盘点设备恢复
- 异常状态处理
- 盘点系统维护

### 3.23 获取托盘库位

**基本信息**
- 接口名称：获取托盘库位
- 接口地址：/system/terminal/getPalletSiteNo
- 请求方法：GET
- 使用页面：aswhDown.nvue
- 业务场景：获取托盘对应的库位信息

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| taskNo | string | 是 | - | 任务编号 |

**入参示例**
```javascript
{
  taskNo: "任务编号"  // 任务编号
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "taskno": "任务编号",
    "palletno": "托盘号",
    "storesiteno": "库位编码",
    "sitename": "库位名称",
    "status": "状态",
    "location": "位置信息"
  }
}
```

**业务说明**
- 根据任务编号查询托盘库位
- 提供托盘的当前位置信息
- 包含库位的详细描述
- 支持托盘位置跟踪
- 为任务执行提供位置信息

**使用场景**
- 托盘位置查询
- 库位信息确认
- 任务执行准备
- 托盘跟踪管理

### 3.24 提交异常上架

**基本信息**
- 接口名称：提交异常上架
- 接口地址：/system/terminal/commitExceptShelves
- 请求方法：POST
- 使用页面：exceptColl.nvue
- 业务场景：提交异常物料上架信息

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| exceptShelvesInfos | array | 是 | - | 异常上架信息数组 |

**入参示例**
```javascript
{
  exceptShelvesInfos: [     // 异常上架信息数组
    {
      matcode: "物料编码",   // 物料编码
      matname: "物料名称",   // 物料名称
      quantity: "数量",      // 数量
      unit: "单位",          // 单位
      storesiteno: "库位编码", // 库位编码
      excepttype: "异常类型",  // 异常类型
      exceptdesc: "异常描述",  // 异常描述
      operator: "操作人"      // 操作人
    }
  ]
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "success": true,
    "processedCount": 1,
    "exceptId": "异常记录ID"
  }
}
```

**业务说明**
- 处理异常物料的上架操作
- 记录异常类型和详细描述
- 支持多种异常类型处理
- 自动生成异常记录
- 更新异常物料状态

**使用场景**
- 异常物料处理
- 异常记录管理
- 特殊上架操作
- 异常跟踪

### 3.25 提交托盘下架

**基本信息**
- 接口名称：提交托盘下架
- 接口地址：/system/terminal/commitTrayDownShelves
- 请求方法：POST
- 使用页面：aswhDown.nvue
- 业务场景：提交托盘下架采集数据

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| downShelvesInfos | array | 是 | - | 下架信息数组 |
| itemListInfos | array | 是 | - | 明细信息数组 |

**入参示例**
```javascript
{
  downShelvesInfos: [      // 下架信息数组
    {
      outtaskid: "任务ID",   // 出库任务ID
      palletno: "托盘号",    // 托盘号
      storesiteno: "库位编码", // 库位编码
      quantity: "数量",      // 数量
      unit: "单位"           // 单位
    }
  ],
  itemListInfos: [         // 明细信息数组
    {
      outtaskitemid: "任务明细ID", // 出库任务明细ID
      quantity: "数量"             // 数量
    }
  ]
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "success": true,
    "taskId": "任务ID",
    "palletProcessed": true
  }
}
```

**业务说明**
- 专门处理托盘下架操作
- 支持整托盘的下架管理
- 包含托盘和明细信息
- 自动处理托盘状态更新
- 支持托盘级别的操作

**使用场景**
- 托盘下架操作
- 整托处理
- 托盘状态管理
- 自动化仓库操作

### 3.26 完成出库任务项

**基本信息**
- 接口名称：完成出库任务项
- 接口地址：/system/terminal/commitFinishOutTaskItem
- 请求方法：POST
- 使用页面：goodsDownDetail.nvue
- 业务场景：标记出库任务明细为完成状态

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| outtaskitemid | string | 是 | - | 出库任务明细ID |

**入参示例**
```javascript
{
  outtaskitemid: "任务明细ID"  // 出库任务明细ID
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "success": true,
    "itemId": "明细ID",
    "status": "已完成"
  }
}
```

**业务说明**
- 标记指定的任务明细为完成
- 更新任务状态和进度
- 记录完成时间和操作人
- 支持任务进度跟踪
- 用于任务完成确认

**使用场景**
- 任务明细完成
- 状态更新
- 进度跟踪
- 任务确认

### 3.27 根据用户ID获取出库任务托盘号

**基本信息**
- 接口名称：根据用户ID获取出库任务托盘号
- 接口地址：/system/terminal/getOutTaskPalletNoByUserID
- 请求方法：GET
- 使用页面：goodsDownDetail.nvue
- 业务场景：获取用户出库任务的托盘号信息

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| userId | number | 是 | - | 用户ID |
| outTaskId | string | 是 | - | 出库任务ID |

**入参示例**
```javascript
{
  userId: 123456,        // 用户ID
  outTaskId: "出库任务ID"  // 出库任务ID
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "userid": "用户ID",
    "outtaskid": "出库任务ID",
    "palletno": "托盘号",
    "taskcode": "任务编号",
    "assignedate": "分配日期",
    "status": "状态"
  }
}
```

**业务说明**
- 获取指定用户的出库任务托盘号
- 提供托盘分配的详细信息
- 包含分配时间和状态
- 支持用户级别的托盘管理
- 为任务执行提供托盘信息

**使用场景**
- 用户托盘查询
- 托盘分配管理
- 任务执行准备
- 托盘跟踪

### 3.28 提交出库任务托盘号

**基本信息**
- 接口名称：提交出库任务托盘号
- 接口地址：/system/terminal/commitRCOutTaskPalletNo
- 请求方法：POST
- 使用页面：goodsDownDetail.nvue
- 业务场景：为出库任务分配或取消托盘号

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| outTaskId | string | 是 | - | 出库任务ID |
| palletNo | string | 是 | - | 托盘号 |
| roomTag | string | 是 | - | 库房标签 |
| isCanel | string | 是 | - | 是否取消 |

**入参示例**
```javascript
{
  outTaskId: "出库任务ID", // 出库任务ID
  palletNo: "托盘号",      // 托盘号
  roomTag: "0",            // 库房标签
  isCanel: "false"         // 是否取消：true-取消，false-分配
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "success": true,
    "taskId": "出库任务ID",
    "palletNo": "托盘号"
  }
}
```

**业务说明**
- 为出库任务分配托盘号
- 支持托盘号的取消操作
- 更新任务的托盘信息
- 记录托盘分配日志
- 支持托盘资源的管理

**使用场景**
- 出库任务的托盘分配
- 托盘资源的调度
- 任务托盘信息的更新
- 托盘使用情况的跟踪

### 3.29 获取出库任务托盘号

**基本信息**
- 接口名称：获取出库任务托盘号
- 接口地址：/system/terminal/getOutTaskPalletNo
- 请求方法：GET
- 使用页面：goodsDownDetail.nvue
- 业务场景：获取出库任务的托盘号信息

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| outTaskId | string | 是 | - | 出库任务ID |
| roomTag | string | 否 | '0' | 库房标签 |

**入参示例**
```javascript
{
  outTaskId: "出库任务ID", // 出库任务ID
  roomTag: "0"            // 库房标签
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "outtaskid": "出库任务ID",
    "palletno": "托盘号",
    "taskcode": "任务编号",
    "status": "状态",
    "assignedate": "分配日期"
  }
}
```

**业务说明**
- 获取指定出库任务的托盘号
- 提供托盘的分配信息
- 包含任务的状态信息
- 支持托盘分配历史的查询
- 为任务执行提供托盘信息

**使用场景**
- 出库任务托盘号的查询
- 托盘分配状态的确认
- 任务信息的完整展示
- 托盘使用情况的跟踪

### 3.30 提交立库托盘号下架

**基本信息**
- 接口名称：提交立库托盘号下架
- 接口地址：/system/terminal/commitASWHPalletNoDownShelves
- 请求方法：POST
- 使用页面：aswhDown.nvue
- 业务场景：提交自动化仓库的托盘下架采集数据

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| downShelvesInfos | array | 是 | - | 下架信息数组 |
| itemListInfos | array | 是 | - | 明细信息数组 |

**入参示例**
```javascript
{
  downShelvesInfos: [      // 下架信息数组
    {
      outtaskid: "任务ID",   // 出库任务ID
      palletno: "托盘号",    // 托盘号
      storesiteno: "库位编码", // 库位编码
      quantity: "数量",      // 数量
      unit: "单位"          // 单位
    }
  ],
  itemListInfos: [         // 明细信息数组
    {
      outtaskitemid: "任务明细ID", // 出库任务明细ID
      quantity: "数量"             // 数量
    }
  ]
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "success": true,
    "taskId": "出库任务ID",
    "palletProcessed": true
  }
}
```

**业务说明**
- 处理立库托盘下架操作
- 支持托盘级别的下架管理
- 包含完整的托盘和明细信息
- 自动处理立库系统更新
- 支持托盘状态跟踪

**使用场景**
- 立库托盘下架
- 自动化仓库操作
- 托盘管理
- 下架数据处理

---

## 4. 到货签收接口

### 4.1 获取到货签收任务列表

**基本信息**
- 接口名称：获取到货签收任务列表
- 接口地址：/system/terminal/upList
- 请求方法：GET
- 使用页面：arrive_sign.nvue
- 业务场景：获取用户需要处理的到货签收任务列表

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| sortType | string | 否 | "" | 排序类型 |
| sortColumn | string | 否 | "" | 排序字段 |
| searchKey | string | 否 | "" | 搜索关键词 |
| userId | number | 是 | - | 用户ID |
| roleoRuserId | number | 是 | - | 角色或用户ID |
| roomTag | string | 否 | "0" | 库房标记 |
| batchflag | string | 否 | "0" | 批次标记 |
| transferType | string | 否 | "0" | 转移类型 |
| beatflag | string | 否 | "N" | 节拍标记 |
| PageIndex | string | 是 | "1" | 页码 |
| PageSize | string | 是 | "100" | 页大小 |
| finshFlg | string | 否 | "0" | 完成标记 |

**入参示例**
```javascript
{
  sortType: "",          // 排序类型
  sortColumn: "",        // 排序字段
  searchKey: "",         // 搜索关键词
  userId: 123456,        // 用户ID
  roleoRuserId: 789012,   // 角色或用户ID
  roomTag: "0",          // 库房标记
  batchflag: "0",        // 批次标记
  transferType: "0",     // 转移类型
  beatflag: "N",         // 节拍标记
  PageIndex: "1",        // 页码
  PageSize: "100",       // 页大小
  finshFlg: "0"          // 完成标记：0-未完成，1-已完成
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "total": 25,
    "rows": [
      {
        "arrivalid": "到货ID",
        "arrivalcode": "到货单号",
        "supplier": "供应商",
        "matcode": "物料编码",
        "matname": "物料名称",
        "quantity": "数量",
        "status": "状态"
      }
    ]
  }
}
```

**业务说明**
- 根据用户ID查询到货签收任务
- 支持分页查询功能
- 可按完成状态过滤
- 返回到货单的基本信息
- 用于到货签收任务管理

**使用场景**
- 到货签收任务列表
- 任务分配和接收
- 到货状态跟踪
- 签收任务管理

### 4.2 提交签收上架

**基本信息**
- 接口名称：提交签收上架
- 接口地址：/system/terminal/commitUpShelves
- 请求方法：POST
- 使用页面：arrive_sign.nvue
- 业务场景：提交到货签收后的上架数据

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| upShelvesInfos | array | 是 | - | 上架信息数组 |
| itemListInfos | array | 是 | - | 明细信息数组 |
| filter | string | 是 | - | 过滤条件 |

**入参示例**
```javascript
{
  upShelvesInfos: [        // 上架信息数组
    {
      arrivalid: "到货ID",   // 到货ID
      storesiteno: "库位编码", // 库位编码
      quantity: "数量",      // 数量
      unit: "单位",          // 单位
      batchno: "批次号",     // 批次号
      sn: "序列号"           // 序列号
    }
  ],
  itemListInfos: [         // 明细信息数组
    {
      arrivalitemid: "到货明细ID", // 到货明细ID
      quantity: "数量"              // 数量
    }
  ],
  filter: "过滤条件"        // 过滤条件
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "success": true,
    "taskId": "到货ID",
    "processedItems": 5
  }
}
```

**业务说明**
- 提交到货签收后的上架数据
- 包含上架信息和明细信息
- 自动处理数据验证和业务逻辑
- 更新库存和到货状态
- 生成操作记录

**使用场景**
- 到货签收后上架
- 库存更新
- 到货状态更新
- 操作记录生成

---

## 5. 入库管理接口

### 5.1 获取入库任务列表

**基本信息**
- 接口名称：获取入库任务列表
- 接口地址：/system/terminal/inList
- 请求方法：GET
- 使用页面：goodsUp.nvue
- 业务场景：获取用户需要处理的入库任务列表

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| sortType | string | 否 | "" | 排序类型 |
| sortColumn | string | 否 | "" | 排序字段 |
| searchKey | string | 否 | "" | 搜索关键词 |
| userId | number | 是 | - | 用户ID |
| roleoRuserId | number | 是 | - | 角色或用户ID |
| roomTag | string | 否 | "0" | 库房标记 |
| batchflag | string | 否 | "0" | 批次标记 |
| transferType | string | 否 | "0" | 转移类型 |
| beatflag | string | 否 | "N" | 节拍标记 |
| PageIndex | string | 是 | "1" | 页码 |
| PageSize | string | 是 | "100" | 页大小 |
| finshFlg | string | 否 | "0" | 完成标记 |

**入参示例**
```javascript
{
  sortType: "",          // 排序类型
  sortColumn: "",        // 排序字段
  searchKey: "",         // 搜索关键词
  userId: 123456,        // 用户ID
  roleoRuserId: 789012,   // 角色或用户ID
  roomTag: "0",          // 库房标记
  batchflag: "0",        // 批次标记
  transferType: "0",     // 转移类型
  beatflag: "N",         // 节拍标记
  PageIndex: "1",        // 页码
  PageSize: "100",       // 页大小
  finshFlg: "0"          // 完成标记：0-未完成，1-已完成
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "total": 50,
    "rows": [
      {
        "intaskid": "入库任务ID",
        "taskcode": "任务编号",
        "matcode": "物料编码",
        "matname": "物料名称",
        "quantity": "数量",
        "status": "状态"
      }
    ]
  }
}
```

**业务说明**
- 根据用户ID查询入库任务
- 支持分页查询功能
- 可按完成状态过滤
- 返回任务的基本信息
- 用于任务列表展示和选择

**使用场景**
- 入库任务列表页面
- 用户查看待处理任务
- 任务状态跟踪
- 任务选择和接收

### 5.2 提交上架采集

**基本信息**
- 接口名称：提交上架采集
- 接口地址：/system/terminal/commitUpShelves
- 请求方法：POST
- 使用页面：goodsUp.nvue
- 业务场景：提交平库上架采集的数据

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| upShelvesInfos | array | 是 | - | 上架信息数组 |
| itemListInfos | array | 是 | - | 明细信息数组 |
| filter | string | 是 | - | 过滤条件 |

**入参示例**
```javascript
{
  upShelvesInfos: [        // 上架信息数组
    {
      intaskid: "任务ID",   // 入库任务ID
      storesiteno: "库位编码", // 库位编码
      quantity: "数量",      // 数量
      unit: "单位",          // 单位
      batchno: "批次号",     // 批次号
      sn: "序列号"           // 序列号
    }
  ],
  itemListInfos: [         // 明细信息数组
    {
      intaskitemid: "任务明细ID", // 入库任务明细ID
      quantity: "数量"             // 数量
    }
  ],
  filter: "过滤条件"        // 过滤条件
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "success": true,
    "taskId": "任务ID",
    "processedItems": 5
  }
}
```

**业务说明**
- 提交完整的上架采集数据
- 包含上架信息和明细信息
- 自动处理数据验证和业务逻辑
- 更新库存和任务状态
- 生成操作记录

**使用场景**
- 平库上架数据提交
- 采集结果保存
- 库存更新
- 任务状态更新

---

## 6. 立库组盘接口

### 6.1 获取立库组盘任务列表

**基本信息**
- 接口名称：获取立库组盘任务列表
- 接口地址：/system/terminal/aswhInList
- 请求方法：GET
- 使用页面：aswhUp.nvue
- 业务场景：获取用户需要处理的立库组盘任务列表

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| sortType | string | 否 | "" | 排序类型 |
| sortColumn | string | 否 | "" | 排序字段 |
| searchKey | string | 否 | "" | 搜索关键词 |
| userId | number | 是 | - | 用户ID |
| roleoRuserId | number | 是 | - | 角色或用户ID |
| roomTag | string | 否 | "0" | 库房标记 |
| batchflag | string | 否 | "0" | 批次标记 |
| transferType | string | 否 | "0" | 转移类型 |
| beatflag | string | 否 | "N" | 节拍标记 |
| PageIndex | string | 是 | "1" | 页码 |
| PageSize | string | 是 | "100" | 页大小 |
| finshFlg | string | 否 | "0" | 完成标记 |

**入参示例**
```javascript
{
  sortType: "",          // 排序类型
  sortColumn: "",        // 排序字段
  searchKey: "",         // 搜索关键词
  userId: 123456,        // 用户ID
  roleoRuserId: 789012,   // 角色或用户ID
  roomTag: "0",          // 库房标记
  batchflag: "0",        // 批次标记
  transferType: "0",     // 转移类型
  beatflag: "N",         // 节拍标记
  PageIndex: "1",        // 页码
  PageSize: "100",       // 页大小
  finshFlg: "0"          // 完成标记：0-未完成，1-已完成
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "total": 35,
    "rows": [
      {
        "intaskid": "入库任务ID",
        "taskcode": "任务编号",
        "matcode": "物料编码",
        "matname": "物料名称",
        "quantity": "数量",
        "palletno": "托盘号",
        "status": "状态"
      }
    ]
  }
}
```

**业务说明**
- 根据用户ID查询立库组盘任务
- 支持分页查询功能
- 可按完成状态过滤
- 返回组盘任务的基本信息
- 包含托盘信息
- 用于立库组盘任务管理

**使用场景**
- 立库组盘任务列表
- 任务分配和接收
- 组盘状态跟踪
- 自动化仓库管理

### 6.2 提交立库上架

**基本信息**
- 接口名称：提交立库上架
- 接口地址：/system/terminal/commitASWHUpShelves
- 请求方法：POST
- 使用页面：aswhUp.nvue
- 业务场景：提交自动化仓库的上架采集数据

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| upShelvesInfos | array | 是 | - | 上架信息数组 |
| itemListInfos | array | 是 | - | 明细信息数组 |
| filter | string | 是 | - | 过滤条件 |
| weight | string | 是 | - | 重量 |
| capacity | string | 是 | - | 容量 |

**入参示例**
```javascript
{
  upShelvesInfos: [        // 上架信息数组
    {
      intaskid: "任务ID",   // 入库任务ID
      palletno: "托盘号",    // 托盘号
      storesiteno: "库位编码", // 库位编码
      quantity: "数量",      // 数量
      unit: "单位"           // 单位
    }
  ],
  itemListInfos: [         // 明细信息数组
    {
      intaskitemid: "任务明细ID", // 入库任务明细ID
      quantity: "数量"             // 数量
    }
  ],
  filter: "过滤条件",      // 过滤条件
  weight: "重量",          // 重量
  capacity: "容量"         // 容量
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "success": true,
    "taskId": "任务ID",
    "palletProcessed": true
  }
}
```

**业务说明**
- 提交立库上架的完整采集数据
- 包含上架信息、明细信息和物理属性
- 支持托盘化管理
- 包含重量和容量信息
- 更新立库系统状态

**使用场景**
- 立库上架数据提交
- 托盘化管理
- 物理属性记录
- 自动化仓库操作

---

## 7. 消息通知接口

### 7.1 获取消息列表

**基本信息**
- 接口名称：获取消息列表
- 接口地址：/system/notice/list
- 请求方法：GET
- 使用页面：msg.nvue
- 业务场景：获取系统通知消息列表

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| pageNum | string | 否 | "1" | 页码 |
| pageSize | string | 否 | "10" | 每页数量 |
| noticeTitle | string | 否 | - | 消息标题 |
| noticeType | string | 否 | - | 消息类型 |
| createBy | string | 否 | - | 创建者 |

**入参示例**
```javascript
{
  pageNum: "1",           // 页码
  pageSize: "10",         // 每页数量
  noticeTitle: "消息标题",  // 消息标题
  noticeType: "1",         // 消息类型
  createBy: "创建者"       // 创建者
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "total": 100,
    "rows": [
      {
        "noticeId": "消息ID",
        "noticeTitle": "消息标题",
        "noticeType": "消息类型",
        "noticeContent": "消息内容",
        "status": "状态",
        "createBy": "创建者",
        "createTime": "创建时间"
      }
    ]
  }
}
```

**业务说明**
- 获取系统通知消息列表
- 支持分页查询功能
- 可按标题、类型等条件过滤
- 返回消息的详细信息
- 用于消息通知管理

**使用场景**
- 消息中心页面
- 系统通知查看
- 消息分类管理
- 通知状态跟踪

### 7.2 获取未读消息数量

**基本信息**
- 接口名称：获取未读消息数量
- 接口地址：/system/notice/unReadCount
- 请求方法：GET
- 使用页面：msg.nvue
- 业务场景：获取当前用户的未读消息数量

**请求参数**
无

**入参示例**
无

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "unReadCount": 5
  }
}
```

**业务说明**
- 统计当前用户的未读消息数量
- 用于消息提醒显示
- 支持实时更新
- 包含所有类型的未读消息

**使用场景**
- 消息提醒红点显示
- 未读消息数量统计
- 用户消息状态监控
- 消息中心提醒

### 7.3 标记消息为已读

**基本信息**
- 接口名称：标记消息为已读
- 接口地址：/system/notice/readNotice
- 请求方法：PUT
- 使用页面：msg.nvue
- 业务场景：将指定消息标记为已读状态

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| noticeId | string | 是 | - | 消息ID |

**入参示例**
```javascript
{
  noticeId: "消息ID"  // 消息ID
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功"
}
```

**业务说明**
- 将指定消息标记为已读状态
- 更新消息的阅读时间
- 支持批量标记操作
- 记录消息阅读状态

**使用场景**
- 消息已读标记
- 消息状态更新
- 阅读记录管理
- 消息中心状态同步

---

## 8. 字典数据接口

### 8.1 获取字典类型列表

**基本信息**
- 接口名称：获取字典类型列表
- 接口地址：/system/dict/type/list
- 请求方法：GET
- 使用页面：所有需要字典数据的页面
- 业务场景：获取系统字典类型列表

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| pageNum | string | 否 | "1" | 页码 |
| pageSize | string | 否 | "10" | 每页数量 |
| dictName | string | 否 | - | 字典名称 |
| dictType | string | 否 | - | 字典类型 |
| status | string | 否 | - | 状态 |

**入参示例**
```javascript
{
  pageNum: "1",           // 页码
  pageSize: "10",         // 每页数量
  dictName: "字典名称",    // 字典名称
  dictType: "字典类型",    // 字典类型
  status: "0"             // 状态：0-正常，1-停用
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "total": 50,
    "rows": [
      {
        "dictId": "字典ID",
        "dictName": "字典名称",
        "dictType": "字典类型",
        "status": "状态",
        "remark": "备注"
      }
    ]
  }
}
```

**业务说明**
- 获取系统字典类型列表
- 支持分页查询和条件过滤
- 返回字典类型的基本信息
- 用于字典数据管理

**使用场景**
- 字典管理页面
- 字典类型查询
- 系统配置管理
- 数据字典维护

### 8.2 获取字典数据列表

**基本信息**
- 接口名称：获取字典数据列表
- 接口地址：/system/dict/data/list
- 请求方法：GET
- 使用页面：所有需要字典数据的页面
- 业务场景：获取指定字典类型的数据列表

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| pageNum | string | 否 | "1" | 页码 |
| pageSize | string | 否 | "10" | 每页数量 |
| dictType | string | 是 | - | 字典类型 |
| dictLabel | string | 否 | - | 字典标签 |
| dictValue | string | 否 | - | 字典值 |
| status | string | 否 | - | 状态 |

**入参示例**
```javascript
{
  pageNum: "1",           // 页码
  pageSize: "10",         // 每页数量
  dictType: "字典类型",    // 字典类型
  dictLabel: "字典标签",   // 字典标签
  dictValue: "字典值",     // 字典值
  status: "0"             // 状态：0-正常，1-停用
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": {
    "total": 20,
    "rows": [
      {
        "dictCode": "字典编码",
        "dictLabel": "字典标签",
        "dictValue": "字典值",
        "dictType": "字典类型",
        "cssClass": "样式类",
        "listClass": "列表样式",
        "isDefault": "是否默认",
        "status": "状态"
      }
    ]
  }
}
```

**业务说明**
- 获取指定字典类型的数据列表
- 支持分页查询和条件过滤
- 返回字典数据的详细信息
- 包含样式和默认值信息

**使用场景**
- 字典数据管理
- 下拉框数据源
- 系统配置数据
- 页面选项数据

### 8.3 根据字典类型获取字典数据

**基本信息**
- 接口名称：根据字典类型获取字典数据
- 接口地址：/system/dict/data/type/{dictType}
- 请求方法：GET
- 使用页面：所有需要字典数据的页面
- 业务场景：根据字典类型获取对应的字典数据

**请求参数**
| 参数名 | 类型 | 必填 | 默认值 | 说明 |
|--------|------|------|--------|------|
| dictType | string | 是 | - | 字典类型（路径参数） |

**入参示例**
```javascript
{
  dictType: "sys_user_sex"  // 字典类型
}
```

**响应格式**
```json
{
  "code": 200,
  "msg": "操作成功",
  "data": [
    {
      "dictCode": "0",
      "dictLabel": "女",
      "dictValue": "0",
      "dictType": "sys_user_sex",
      "cssClass": "",
      "listClass": "",
      "isDefault": "Y",
      "status": "0"
    },
    {
      "dictCode": "1",
      "dictLabel": "男",
      "dictValue": "1",
      "dictType": "sys_user_sex",
      "cssClass": "",
      "listClass": "",
      "isDefault": "N",
      "status": "0"
    }
  ]
}
```

**业务说明**
- 根据字典类型直接获取字典数据
- 返回该类型下的所有有效字典项
- 包含完整的字典项信息
- 支持前端快速获取字典数据

**使用场景**
- 下拉框数据填充
- 页面选项渲染
- 数据转换映射
- 系统配置获取

---

## 9. 响应码说明

### 9.1 通用响应码
| 状态码 | 说明 |
|--------|------|
| 200 | 操作成功 |
| 401 | 认证失败或token过期 |
| 403 | 权限不足 |
| 404 | 资源不存在 |
| 500 | 服务器内部错误 |

### 9.2 业务响应码
| 状态码 | 说明 |
|--------|------|
| 200 | 成功 |
| 500 | 失败 |

---

## 10. 错误处理

### 10.1 网络错误
- **后端接口连接异常**: 检查网络连接和服务器状态
- **系统接口请求超时**: 检查请求超时设置
- **系统接口XXX异常**: 根据具体状态码处理

### 10.2 认证错误
- **401**: 跳转到登录页面重新登录
- **403**: 显示权限不足提示

### 10.3 业务错误
- **500**: 显示服务器返回的错误信息

---

## 11. 注意事项

1. **认证token**: 所有API请求（除登录和获取验证码外）都需要在请求头中携带Bearer token
2. **参数格式**: GET请求使用params，POST请求使用data
3. **JSON序列化**: 部分POST接口需要手动JSON.stringify序列化请求体
4. **错误处理**: 统一通过request.js处理，会自动显示错误提示
5. **分页参数**: 所有列表接口都支持分页，使用PageIndex和PageSize参数
6. **数据格式**: 日期时间格式请使用ISO 8601标准格式

---

## 12. 更新日志

### 2025-09-19
- 重新整理API文档，基于实际代码分析
- 修正所有接口的参数和响应结构
- 删除不准确的接口描述
- 按照新的格式规范重新整理所有接口
- 添加业务说明和使用场景
- 移除函数定义和调用示例代码块

---

*本文档基于实际代码生成，确保与实现一致。如有疑问请参考源代码实现。*