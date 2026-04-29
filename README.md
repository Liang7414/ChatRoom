# ChatRoom

## Project Structure
<img width="1036" height="882" alt="image" src="https://github.com/user-attachments/assets/d0c0f9b9-2d14-4718-ada3-1bca67e87c83" />

## 專案亮點

此專案忠於「單一職責原則」開發模式，將業務邏輯以及資料庫搜索完全分離，在Application層完全不會看到ORM操作的部分，當然也不會在API層看到任何業務邏輯，因此每一層只會負責自己職責的任務


# 🛠 目前實作功能
## 1. 用戶管理 (User Management)

安全驗證：整合 BCrypt 雜湊演算法處理密碼加密。

身分授權：基於 JWT (JSON Web Token) 的 Bearer 認證機制。

個人檔案：支援用戶更新暱稱與頭像，並在註冊時自動建立預設檔案。

## 2. 行為日誌 (User Logs)
實時記錄：自動追蹤用戶的重要操作。

關聯查詢：利用 EF Core 的 Include 語法實現 Eager Loading，直接在 API 回傳友善的事件名稱（如 "Login"）而非原始代碼。

# ⚙️ 開發環境配置
技術棧
Framework: .NET 8

ORM: Entity Framework Core

Mapping: AutoMapper

Database: SQL Server

Security: BCrypt.Net, JWT

# 開發進度

2026/4/29 完成使用者資料建檔，之後會正式加入傳訊息的功能，並加入Redis克服請求量龐大的問題
