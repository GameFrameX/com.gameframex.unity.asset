using System.Threading.Tasks;
using UnityEngine;
using YooAsset;

namespace GameFrameX.Asset.Runtime
{
    /// <summary>
    /// AssetComponent 的 Material 资源加载扩展。
    /// </summary>
    /// <remarks>
    /// Material asset loading extensions for AssetComponent.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class AssetComponentMaterialExtensions
    {
        /// <summary>
        /// 异步加载 Material 资源。
        /// </summary>
        /// <remarks>
        /// Asynchronously loads a Material asset.
        /// </remarks>
        /// <param name="assetComponent">资源组件实例 / AssetComponent instance</param>
        /// <param name="path">资源路径 / Asset path</param>
        /// <returns>资源操作句柄的异步任务 / Async task of the asset handle</returns>
        [UnityEngine.Scripting.Preserve]
        public static Task<AssetHandle> LoadMaterialAsync(this AssetComponent assetComponent, string path)
        {
            return assetComponent.LoadAssetAsync<Material>(path);
        }

        /// <summary>
        /// 同步加载 Material 资源。
        /// </summary>
        /// <remarks>
        /// Synchronously loads a Material asset.
        /// </remarks>
        /// <param name="assetComponent">资源组件实例 / AssetComponent instance</param>
        /// <param name="path">资源路径 / Asset path</param>
        /// <returns>资源操作句柄 / Asset handle</returns>
        [UnityEngine.Scripting.Preserve]
        public static AssetHandle LoadMaterialSync(this AssetComponent assetComponent, string path)
        {
            return assetComponent.LoadAssetSync<Material>(path);
        }
    }
}
