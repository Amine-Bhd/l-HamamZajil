using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Hubs
{
    public class LoginHub : Hub
    {
        private readonly string login = "test@gmail.com";
        private readonly string password = "test";
        public bool Login(string email, string pass)
        {
            return email == login && pass == password;
        }
    }
}
