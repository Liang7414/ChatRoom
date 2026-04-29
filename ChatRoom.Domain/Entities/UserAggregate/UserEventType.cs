using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom.Domain.Entities.UserAggregate
{
    public class UserEventType
    {
        public enum UserEventCodes
        {
            Login = 1,
            Logout = 2,
            UpdateProfile = 3,
            Register = 4
        }
        public int Id { get; set; }
        public string EventType { get; set; } = string.Empty;
        public virtual ICollection<UserLog> Logs { get; set; } = new List<UserLog>();
    }
}
