---

# 📘 项目 C# 编码与开发规范（团队通用版）

---

## 1. 命名规范 (Naming Conventions)

命名必须使用英文，禁止使用拼音或缩写（除非是约定俗成的缩写，如 `ID`, `IP`, `Http`）。

### 1.1 大驼峰命名法 (PascalCase)
用于：**类名、结构体、枚举、接口、属性、方法、命名空间**。

*   **类/结构体**：`UserService`, `OrderHeader`
*   **接口**：必须以 `I` 开头。例如：`IUserRepository`, `IExportable`
*   **方法**：`GetActiveUsers()`, `CalculateTotalAmount()`
*   **属性**：`public string FirstName { get; set; }`
*   **枚举**：`Gender.Male`, `OrderStatus.PendingPayment`
*   **异步方法**：必须以 `Async` 结尾。例如：`GetUsersAsync()`, `SaveDataAsync()`

### 1.2 小驼峰命名法 (camelCase)
用于：**局部变量、方法参数**。

*   **局部变量**：`int itemCount = 10;`
*   **方法参数**：`public void UpdateUser(int userId, string userName)`

### 1.3 字段 (Field) 命名
*   **私有/受保护成员字段**：下划线 + 小驼峰。例如：`private readonly ILogger _logger;`
*   **静态私有字段**：`_s` 或下划线。例如：`private static int _instanceCount;`
*   **常量 (const)**：使用 **PascalCase**（注意：C# 与 Java/C++ 不同，常量**不使用**全大写）。
    *   ✅ 正确：`public const int MaxPageSize = 50;`
    *   ❌ 错误：`public const int MAX_PAGE_SIZE = 50;`

---

## 2. 代码格式与排版 (Code Style)

### 2.1 换行与括号 (Allman 风格)
大括号 `{ }` 必须独占一行。

*   ✅ **正确：**
    ```csharp
    if (isReady)
    {
        DoSomething();
    }
    ```
*   ❌ **错误（Java/JS 风格）：**
    ```csharp
    if (isReady) {
        DoSomething();
    }
    ```

### 2.2 代码空行与缩进
*   使用 **4 个空格** 缩进（Tab 键需在 IDE 中设置为 4 空格）。
*   方法与方法之间、逻辑块之间保留 **1 行空行**，禁止连续出现 2 行以上的空行。
*   单行代码长度不要超过 **120 个字符**，过长需要换行。

### 2.3 Using 引用
*   无用的 `using` 必须清理（快捷键：`Ctrl + R, Ctrl + O`）。
*   `using` 语句统一放在文件顶部（或使用 C# 10+ 的 `global using`）。

---

## 3. C# 语法最佳实践

### 3.1 变量声明 (`var` 的使用)
*   当右侧类型显而易见时，**推荐使用 `var`**：
    *   ✅ `var user = new User();`
    *   ✅ `var list = new List<string>();`
*   当右侧类型不明显时，**必须显式声明类型**：
    *   ✅ `int age = GetAge();` （不要写 `var age = GetAge();`）

### 3.2 字符串操作
*   **拼接字符串**：优先使用 **字符串插值 `$""`**，禁止使用 `+` 拼接多个变量。
    *   ✅ `string msg = $"User {userId} is logged in.";`
    *   ❌ `string msg = "User " + userId + " is logged in.";`
*   **空字符串判断**：使用 `string.IsNullOrEmpty()` 或 `string.IsNullOrWhiteSpace()`。

### 3.3 空值处理 (Null Safety)
*   判断是否为 null 优先使用 `is null` 或 `is not null`（比 `== null` 更安全）。
    *   ✅ `if (user is null) return;`
*   善用空包容运算符 `??` 和空条件运算符 `?.`：
    *   ✅ `string name = user?.Name ?? "Guest";`

### 3.4 异步编程 (`async` / `await`)
*   **绝对禁止使用 `async void`**（除非是 UI 的事件响应函数）。返回值必须是 `Task` 或 `Task<T>`。
*   **绝对禁止死锁调用**：不能使用 `.Result` 或 `.Wait()`，必须一异步到底（使用 `await`）。
    *   ✅ `var data = await GetDataAsync();`
    *   ❌ `var data = GetDataAsync().Result;`

---

## 4. 异常处理与资源释放

### 4.1 资源释放 (`IDisposable`)
凡是实现了 `IDisposable` 接口的对象（如数据库连接、文件流、HttpClient），**必须释放资源**。

*   **推荐使用 C# 8+ 的 `using` 声明：**
    ```csharp
    using var stream = new FileStream("test.txt", FileMode.Open);
    // 离开当前作用域后自动释放
    ```

### 4.2 异常捕获 (Catch)
*   **严禁捕获了 Exception 却什么都不做（吃掉异常）：**
    *   ❌ `catch (Exception) { }`
*   **严禁盲目捕获基类 `Exception`**，尽量捕获具体的异常类型（如 `FileNotFoundException`）。
*   **重新抛出异常时使用 `throw;`**，不要使用 `throw ex;`（后者会丢失堆栈追踪信息）：
    *   ✅ `catch (Exception) { _logger.LogError(...); throw; }`
    *   ❌ `catch (Exception ex) { throw ex; }`

---

## 5. 注释规范 (Documentation)

### 5.1 类和公共方法 (XML 注释)
所有 `public` 的类、接口、方法、属性，**必须编写 XML 文档注释**（在上面输入 `///` 自动生成）。

```csharp
/// <summary>
/// 根据用户ID获取用户信息
/// </summary>
/// <param name="userId">用户唯一标识</param>
/// <returns>用户信息实体，若不存在则返回 null</returns>
public async Task<User?> GetUserByIdAsync(int userId)
{
    // ...
}
```

### 5.2 行内注释
*   注释应该说明**“为什么这么写”（意图）**，而不是说明“写了什么”（代码本身就能看出来）。
*   如果代码逻辑太复杂需要写大量注释，优先考虑**重构该代码**。
