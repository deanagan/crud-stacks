
// Note string nullable. This is due to C#7.4 and up's
// default project file setting: <Nullable>enable</Nullable>
//See documentation here: https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references#nullable-contexts
namespace RealTimeChat.Service
{
    public class UserConnection
    {
        public string? User { get; set; }
        public string? Room { get; set; }
    }
}