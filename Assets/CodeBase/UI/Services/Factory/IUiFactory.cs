using System.Threading.Tasks;
using SimpleRPG.Infrastructure.Services;

namespace SimpleRPG.UI.Services.Factory
{
    public interface IUiFactory : IService
    {
        void CreateShop();
        Task CreateUIRoot();
    }
}