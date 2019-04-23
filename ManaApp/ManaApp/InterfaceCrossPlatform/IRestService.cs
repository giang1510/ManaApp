using ManaApp.Model;
using System.Threading.Tasks;

namespace ManaApp.InterfaceCrossPlatform
{
    public interface IRestService
    {
        Task<string> RefreshDataAsync();
        Task<string> GetPage(string relativePath);
        Task<LoginResponse> Login(User user);
        Task<string> Register(User user);
        Task<string> SearchProvider(SearchInput searchInput);
    }
}
