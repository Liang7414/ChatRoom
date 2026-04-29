ChatRoom (Solution)空白方案
│ 
│ 
├── 📂 ChatRoom.Domain                 # 核心：不依賴任何外部套件 (POCOs)
│   ├── 📂 Entities                    # 資料庫實體
│   │
│   └── 📂 RepositoryInterfaces        # 定義資料庫操作介面，不寫ORM查詢，會被Application.Service引用
│
├── 📂 ChatRoom.Application            # 應用邏輯：DTOs、AutoMapper、Services
│   ├── 📂 DTOs                        # 資料傳輸物件(前端能夠使用的I/O)
│   │
│   ├── 📂 Mappings                    # AutoMapper 設定
│   │   └── MappingProfiles.cs         # (前端輸入資料)  將DTO轉換成Entity物件
│   │                                    (結果回傳給前端)將運算結果轉換成DTO
│   │ 
│   └── 📂 Services
│       ├── 📂 Interfaces              # 應用層服務介面
│       │
│       └── 📂 Implementations         # 具體邏輯 (呼叫 RepositoryInterfaces)   
│
│
├── 📂 ChatRoom.Infrastructure         # 基礎設施：資料庫實作、外部服務
│   ├── 📂 Data                        # Entity Framework Core
│   │   └── ChatRoomDbContext.cs        # DbContext
│   │
│   ├── 📂 Migrations                  # 資料庫遷移紀錄
│   │
│   └── 📂 RepositoryImplementations   # 負責ORM資料庫查詢
│
└── 📂 ChatRoom.API                    # Controllers、Middleware、Settings
    ├── 📂 Controllers                 #負責接收前端Request並呼叫Application.Services.Interfaces
    │
    ├── appsettings.json               # ConnectionStrings設定
    ├── docker-compose.yml             # Docker 設定
    ├── Dockerfile                     # API 映像檔定義
    └── Program.cs                     # 註冊 Implementations與Interface(DI) AutoMapper, DbContext, JWT 等