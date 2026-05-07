using System.Threading.Tasks;
using UnityEngine;
using YooAsset;

namespace GameFrameX.Asset.Runtime
{
    /// <summary>
    /// AssetComponent 的 Texture2D 资源加载扩展。
    /// </summary>
    /// <remarks>
    /// Texture2D asset loading extensions for AssetComponent.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class AssetComponentTexture2DExtensions
    {
        /// <summary>
        /// 异步加载 Texture2D 资源。
        /// </summary>
        /// <remarks>
        /// Asynchronously loads a Texture2D asset.
        /// </remarks>
        /// <param name="assetComponent">资源组件实例 / AssetComponent instance</param>
        /// <param name="path">资源路径 / Asset path</param>
        /// <returns>资源操作句柄的异步任务 / Async task of the asset handle</returns>
        [UnityEngine.Scripting.Preserve]
        public static Task<AssetHandle> LoadTexture2DAsync(this AssetComponent assetComponent, string path)
        {
            return assetComponent.LoadAssetAsync<Texture2D>(path);
        }

        /// <summary>
        /// 同步加载 Texture2D 资源。
        /// </summary>
        /// <remarks>
        /// Synchronously loads a Texture2D asset.
        /// </remarks>
        /// <param name="assetComponent">资源组件实例 / AssetComponent instance</param>
        /// <param name="path">资源路径 / Asset path</param>
        /// <returns>资源操作句柄 / Asset handle</returns>
        [UnityEngine.Scripting.Preserve]
        public static AssetHandle LoadTexture2DSync(this AssetComponent assetComponent, string path)
        {
            return assetComponent.LoadAssetSync<Texture2D>(path);
        }
    }
}
