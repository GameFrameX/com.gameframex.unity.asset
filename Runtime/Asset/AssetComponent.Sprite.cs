using System.Threading.Tasks;
using UnityEngine;
using YooAsset;

namespace GameFrameX.Asset.Runtime
{
    /// <summary>
    /// AssetComponent 的 Sprite 资源加载扩展。
    /// </summary>
    /// <remarks>
    /// Sprite asset loading extensions for AssetComponent.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class AssetComponentSpriteExtensions
    {
        /// <summary>
        /// 异步加载 Sprite 资源。
        /// </summary>
        /// <remarks>
        /// Asynchronously loads a Sprite asset.
        /// </remarks>
        /// <param name="assetComponent">资源组件实例 / AssetComponent instance</param>
        /// <param name="path">资源路径 / Asset path</param>
        /// <returns>资源操作句柄的异步任务 / Async task of the asset handle</returns>
        [UnityEngine.Scripting.Preserve]
        public static Task<AssetHandle> LoadSpriteAsync(this AssetComponent assetComponent, string path)
        {
            return assetComponent.LoadAssetAsync<Sprite>(path);
        }

        /// <summary>
        /// 同步加载 Sprite 资源。
        /// </summary>
        /// <remarks>
        /// Synchronously loads a Sprite asset.
        /// </remarks>
        /// <param name="assetComponent">资源组件实例 / AssetComponent instance</param>
        /// <param name="path">资源路径 / Asset path</param>
        /// <returns>资源操作句柄 / Asset handle</returns>
        [UnityEngine.Scripting.Preserve]
        public static AssetHandle LoadSpriteSync(this AssetComponent assetComponent, string path)
        {
            return assetComponent.LoadAssetSync<Sprite>(path);
        }
    }
}
