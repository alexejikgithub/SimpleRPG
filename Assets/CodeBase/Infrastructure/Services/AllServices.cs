using SimpleRPG.Infrastructure.Factory;
using SimpleRPG.Services.Input;

namespace SimpleRPG.Infrastructure.Services
{
    public class AllServices
    {
        private static AllServices _instance;
        public static AllServices Container => _instance ?? (_instance = new AllServices());
        public IInputService InputService { get; set; }

        public void RegisterSingle<TService>(TService implementation) where TService : IService => Implementation<TService>.ServiceInstance = implementation;

        public TService Single<TService>() where TService : IService => Implementation<TService>.ServiceInstance;

        private class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}