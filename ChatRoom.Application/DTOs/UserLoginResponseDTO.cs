using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom.Application.DTOs
{
    public class UserLoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Nickname { get; set; }
        public string? PortraitPhoto { get; set; }

    }
}
