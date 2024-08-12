using System.Threading.Tasks;
using SimpleRPG.Infrastructure.AssetManagement;
using SimpleRPG.Infrastructure.Services.Ads;
using SimpleRPG.Services.PersistantProgress;
using SimpleRPG.StaticData;
using SimpleRPG.StaticData.Windows;
using SimpleRPG.UI.Services.Windows;
using SimpleRPG.UI.Windows.Shop;
using UnityEngine;

namespace SimpleRPG.UI.Services.Factory
{
    public class UiFactory : IUiFactory
    {
        private const string _UiRootPath = "UIRoot";
        
        private IStaticDataService _staticData;
        private IAssetProvider _asset;


        private Transform _uiRoot;
        private IPersistantProgressService _progressService;
        private IAdsService _adsService;

        public UiFactory(IAssetProvider asset,IStaticDataService staticData, IPersistantProgressService progressService,IAdsService adsService )
        {
            _staticData = staticData;
            _asset = asset;
            _progressService = progressService;
            _adsService = adsService;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Shop);
            ShopWindow window = Object.Instantiate(config.Prefab, _uiRoot) as ShopWindow;
            window.Construct(_adsService,_progressService);
        }

        public async Task CreateUIRoot()
        {
            GameObject instantiate = await _asset.Instantiate(_UiRootPath);
            _uiRoot =  instantiate.transform;
        }
    }
}