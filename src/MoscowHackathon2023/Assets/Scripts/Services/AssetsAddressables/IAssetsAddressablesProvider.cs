using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Services.AssetsAddressables
{
    public interface IAssetsAddressablesProvider
    {
        public void Initialize();
        public Task<T> GetAsset<T>(string address) where T : Object;
        public Task<T> GetAsset<T>(AssetReference assetReference) where T : Object;
        public void CleanUp();
    }
}