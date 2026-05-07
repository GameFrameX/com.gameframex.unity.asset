using System.Threading.Tasks;
using UnityEngine;
using YooAsset;

namespace GameFrameX.Asset.Runtime
{
    /// <summary>
    /// AssetComponent 的 PhysicsMaterial2D 资源加载扩展。
    /// </summary>
    /// <remarks>
    /// PhysicsMaterial2D asset loading extensions for AssetComponent.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class AssetComponentPhysicsMaterial2DExtensions
    {
        /// <summary>
        /// 异步加载 PhysicsMaterial2D 资源。
        /// </summary>
        /// <remarks>
        /// Asynchronously loads a PhysicsMaterial2D asset.
        /// </remarks>
        /// <param name="assetComponent">资源组件实例 / AssetComponent instance</param>
        /// <param name="path">资源路径 / Asset path</param>
        /// <returns>资源操作句柄的异步任务 / Async task of the asset handle</returns>
        [UnityEngine.Scripting.Preserve]
        public static Task<AssetHandle> LoadPhysicsMaterial2DAsync(this AssetComponent assetComponent, string path)
        {
            return assetComponent.LoadAssetAsync<PhysicsMaterial2D>(path);
        }

        /// <summary>
        /// 同步加载 PhysicsMaterial2D 资源。
        /// </summary>
        /// <remarks>
        /// Synchronously loads a PhysicsMaterial2D asset.
        /// </remarks>
        /// <param name="assetComponent">资源组件实例 / AssetComponent instance</param>
        /// <param name="path">资源路径 / Asset path</param>
        /// <returns>资源操作句柄 / Asset handle</returns>
        [UnityEngine.Scripting.Preserve]
        public static AssetHandle LoadPhysicsMaterial2DSync(this AssetComponent assetComponent, string path)
        {
            return assetComponent.LoadAssetSync<PhysicsMaterial2D>(path);
        }
    }
}
