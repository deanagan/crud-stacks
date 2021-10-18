using System;

namespace TodoBackend.Api.Data.ViewModels
{
    public class AuthDataViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public long TokenExpirationTime { get; set; }
    }
}