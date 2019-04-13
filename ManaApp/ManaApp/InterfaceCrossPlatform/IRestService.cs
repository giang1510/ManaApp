using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManaApp.InterfaceCrossPlatform
{
    interface IRestService
    {
        Task<string> RefreshDataAsync();
        Task<string> getPage(string relativePath);
        Task<string> login(string username, string password);
        //Task<string> register(User user);
    }
}
