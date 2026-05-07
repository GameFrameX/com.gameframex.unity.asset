using System.Threading.Tasks;
using UnityEngine;
using YooAsset;

namespace GameFrameX.Asset.Runtime
{
    /// <summary>
    /// AssetComponent 的 AnimationClip 资源加载扩展。
    /// </summary>
    /// <remarks>
    /// AnimationClip asset loading extensions for AssetComponent.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class AssetComponentAnimationClipExtensions
    {
        /// <summary>
        /// 异步加载 AnimationClip 资源。
        /// </summary>
        /// <remarks>
        /// Asynchronously loads an AnimationClip asset.
        /// </remarks>
        /// <param name="assetComponent">资源组件实例 / AssetComponent instance</param>
        /// <param name="path">资源路径 / Asset path</param>
        /// <returns>资源操作句柄的异步任务 / Async task of the asset handle</returns>
        [UnityEngine.Scripting.Preserve]
        public static Task<AssetHandle> LoadAnimationClipAsync(this AssetComponent assetComponent, string path)
        {
            return assetComponent.LoadAssetAsync<AnimationClip>(path);
        }

        /// <summary>
        /// 同步加载 AnimationClip 资源。
        /// </summary>
        /// <remarks>
        /// Synchronously loads an AnimationClip asset.
        /// </remarks>
        /// <param name="assetComponent">资源组件实例 / AssetComponent instance</param>
        /// <param name="path">资源路径 / Asset path</param>
        /// <returns>资源操作句柄 / Asset handle</returns>
        [UnityEngine.Scripting.Preserve]
        public static AssetHandle LoadAnimationClipSync(this AssetComponent assetComponent, string path)
        {
            return assetComponent.LoadAssetSync<AnimationClip>(path);
        }
    }
}
