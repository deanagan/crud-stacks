using System;

namespace TodoBackend.Api.Data.ViewModels
{
    public class AuthDataView
    {
        public Guid UniqueId { get; set; }
        public string Token { get; set; }
        public long TokenExpirationTime { get; set; }
    }
}