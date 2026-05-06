using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom.Domain.Entities.UserAggregate
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // 關聯：一對一 個人檔案

        public virtual UserProfile Profile { get; set; } = null!;
        // 關聯：一對多 行為日誌
        public virtual ICollection<UserLog> Logs { get; set; } = new List<UserLog>();
    }
}
