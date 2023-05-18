using System.Threading.Tasks;
using Services.AssetsAddressables;
using UnityEngine;
using Zenject;

namespace Services.Factories.AbstractFactory
{
    public class AbstractFactory : IAbstractFactory
    {
        private readonly IAssetsAddressablesProvider _assetsAddressablesProvider;

        public AbstractFactory(IAssetsAddressablesProvider assetsAddressablesProvider)
        {
            _assetsAddressablesProvider = assetsAddressablesProvider;
        }
        
        public async Task<T> CreateInstance<T>(string path) where T : Object
        {
            var prefab = await _assetsAddressablesProvider.GetAsset<T>(path);

            var instance = Object.Instantiate(prefab);

            return instance;
        }
    }
}