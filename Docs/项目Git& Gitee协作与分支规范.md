---

# 🐙 项目 Git & Gitee 协作与分支规范

为了防止代码被误覆盖、仓库体积虚胖以及合并冲突，全员必须严格遵守本规范。

---

## 1. 分支管理规范 (Branching Strategy)

本项目采用轻量级的 **Gitee Flow（主干 + 功能分支）** 模式。

```text
master (保护分支，禁止直接 Push)
  ├── feature/zhangsan-login   (张三的登录功能分支)
  └── bugfix/lisi-order-calc   (李四的 Bug 修复分支)
```

### 1.1 分支命名与权责

| 分支类型        | 命名格式                  | 说明                                                | 来源分支 | 合并目标 |
| :-------------- | :------------------------ | :-------------------------------------------------- | :------- | :------- |
| **主干分支**    | `master`                  | **绝对稳定的代码**，随时可发布。**严禁直接 push**！ | -        | -        |
| **功能分支**    | `feature/姓名简写-功能名` | 开发新功能。例：`feature/zs-user-login`             | `master` | `master` |
| **修 Bug 分支** | `bugfix/姓名简写-Bug描述` | 修复测试阶段的 Bug。例：`bugfix/ls-fix-null`        | `master` | `master` |
| **紧急修复**    | `hotfix/版本-描述`        | 生产环境突发紧急问题，由组长建立。                  | `master` | `master` |

*注：功能开发完毕并成功合并到 `master` 后，**必须删除**对应的 `feature/` 分支，保持仓库干净。*

---



#### 常用分支前缀 (Prefix) 速查表

无论采用上述哪种格式，前缀词库都是通用的：

| 前缀 (全称)   | 常用缩写 | 含义与使用场景                                          |
| ------------- | -------- | ------------------------------------------------------- |
| **feature/**  | feat/    | **新功能/新需求**（最常用）                             |
| **bugfix/**   | fix/     | **修复 Bug**（开发/测试阶段发现的 Bug）                 |
| **hotfix/**   | -        | **线上紧急修复**（生产环境出的严重 Bug）                |
| **refactor/** | -        | **重构**（改动了代码架构，但不改变既有功能和 Bug）      |
| **chore/**    | -        | **杂项/构建**（升级 .NET 版本、加配置文件、修改 CI/CD） |
| **docs/**     | -        | **文档**（只修改了 README 或 API 文档）                 |
| **test/**     | -        | **测试**（专门补充单元测试或自动化测试）                |

------



## 2. Commit 提交规范

禁止提交 `"111"`, `"test"`, `"修好了"` 这种无意义的 Commit！

### 2.1 提交信息格式
采用业界通用的 **Conventional Commits（约定式提交）**：

`类型(范围): 简短描述`

*   **类型 (Type)**：
    *   `feat`: 新增功能 (Feature)
    *   `fix`: 修复 Bug
    *   `docs`: 仅仅修改了文档/注释
    *   `style`: 代码格式调整（不影响逻辑，如空格、缩进）
    *   `refactor`: 重构（既不是新增功能，也不是修 Bug）
    *   `chore`: 构建过程或辅助工具的变动（如更新 NuGet 包）
*   **范围 (Scope, 可选)**：说明本次修改影响的模块。
*   **简短描述**：用一句话说明做了什么。

### 2.2 正确示范
*   `feat(user): 增加手机验证码登录接口`
*   `fix(order): 修复订单重复提交的 Bug`
*   `docs(readme): 更新本地部署说明文档`
*   `chore: 升级 EntityFrameworkCore 到 8.0`

### 2.3 Commit 原子化原则
*   **一次 Commit 只做一件事**。不要把“改登录界面”和“修复订单 Bug”混在同一个 Commit 里。
*   频繁 Commit，确保每次 Commit 的代码都能通过编译。

---

## 3. Pull Request (PR) 审查与合并规范

代码合并到 `master` 必须通过 Gitee 的 **Pull Request (PR)** 机制。

### 3.1 提交 PR 流程
1.  **本地拉取最新 master**：在提交 PR 前，先在本地将最新的 `master` 合并到你的 `feature` 分支，**并在本地解决好冲突**。
2.  **本地编译测试**：确保本地编译 100% 通过，单元测试全部通过。
3.  **Push 到 Gitee**：将你的 `feature/xxx` 分支推送到 Gitee。
4.  **新建 PR**：在 Gitee 网页端点击 **“新建 Pull Request”**：
    *   源分支：`feature/zhangsan-login`
    *   目标分支：`master`
    *   **指定审核人（Reviewer）**：选择组长或团队指定的其他成员。
5.  **审核通过**：审核人在 Gitee 检查无误后，点击 **“合并 (Merge)”**。

### 3.2 PR 审核标准（Reviewer 必看）
*   [ ] 代码是否能正常编译？
*   [ ] 是否包含 `bin/`, `obj/`, `.vs/` 等垃圾文件？
*   [ ] 是否有硬编码的数据库密码或密钥？
*   [ ] 命名是否符合 C# 规范？

---

## 4. 八大 Git 禁忌（红线规矩）

违反以下规则可能导致团队代码灾难，严格禁止：

1.  **❌ 严禁强推（Force Push）：** 严禁使用 `git push -f` 或 `--force`，这会直接覆盖别人的代码！
2.  **❌ 严禁提交二进制与编译文件：** 严禁跳过 `.gitignore` 提交 `bin/`, `obj/`, `.vs/`, `.user` 文件。
3.  **❌ 严禁提交敏感信息：** 严禁将真实数据库密码、阿里云/微信 API Key 提交到 Gitee。
4.  **❌ 严禁把报错的代码推上远程仓库：** 保证你 Push 的代码是能运行的。
5.  **❌ 严禁不沟通直接解冲突：** 遇到冲突涉及别人的代码，必须叫上对方一起确认，严禁直接删掉别人的代码！
6.  **❌ 严禁在 `master` 分支直接写代码：** 必须在自己的 `feature/` 分支上开发。
7.  **❌ 严禁一个 PR 包含上万行代码：** 大需求拆分成多个小 PR 提交，方便审核。
8.  **❌ 严禁不 Pull 就直接 Push：** 每次 Push 前，先 `Pull` 最新的代码。

---

## 5. 新手日常 Git 命令备忘单

```bash
# 1. 开始新工作：切换到 master 并拉取最新代码
git checkout master
git pull origin master

# 2. 基于 master 创建并切换到自己的新功能分支
git checkout -b feature/zs-login

# 3. 写代码过程中：查看修改状态
git status

# 4. 暂存并提交本地代码
git add .
git commit -m "feat(login): 完成登录界面UI布局"

# 5. 开发完成，准备提交 PR 前：合并最新的 master 进来（预防冲突）
git checkout master
git pull origin master
git checkout feature/zs-login
git merge master   # 如果有冲突，在这里解决

# 6. 推送功能分支到 Gitee
git push origin feature/zs-login
# (然后去 Gitee 网页上发 PR)

# 7. PR 被合并后，删除本地废弃的分支
git checkout master
git pull origin master
git branch -d feature/zs-login
```
