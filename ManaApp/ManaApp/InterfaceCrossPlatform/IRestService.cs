using ManaApp.Model;
using System.Threading.Tasks;

namespace ManaApp.InterfaceCrossPlatform
{
    public interface IRestService
    {
        Task<string> RefreshDataAsync();
        Task<string> getPage(string relativePath);
        Task<string> login(string username, string password);
        Task<string> register(User user);
    }
}
