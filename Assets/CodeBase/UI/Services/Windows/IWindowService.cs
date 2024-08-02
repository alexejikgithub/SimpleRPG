using SimpleRPG.Infrastructure.Services;

namespace SimpleRPG.UI.Services.Windows
{
    public interface IWindowService : IService
    {
        void Open(WindowId windowId);
    }
}