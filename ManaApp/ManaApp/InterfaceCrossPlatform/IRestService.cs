using ManaApp.Model;
using System.Threading.Tasks;

namespace ManaApp.InterfaceCrossPlatform
{
    public interface IRestService
    {
        Task<string> RefreshDataAsync();
        Task<string> GetPage(string relativePath);
        Task<string> Login(string username, string password);
        Task<string> Register(User user);
    }
}
