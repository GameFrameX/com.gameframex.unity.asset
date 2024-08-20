using GameFrameX.Event.Runtime;

namespace GameFrameX.Asset.Runtime
{
    /// <summary>
    /// 补丁清单更新失败
    /// </summary>
    public sealed class AssetPatchManifestUpdateFailedEventArgs : GameEventArgs
    {
        public override void Clear()
        {
        }

        /// <summary>
        /// 补丁清单更新失败事件编号
        /// </summary>
        public static readonly string EventId = nameof(AssetPatchManifestUpdateFailedEventArgs);

        public override string Id
        {
            get { return EventId; }
        }

        /// <summary>
        /// 包名称
        /// </summary>
        public string PackageName { get; private set; }

        public string Error { get; private set; }

        /// <summary>
        /// 创建补丁清单更新失败
        /// </summary>
        /// <param name="packageName">包名称</param>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public static AssetPatchManifestUpdateFailedEventArgs Create(string packageName, string error)
        {
            var assetPatchManifestUpdateFailed = ReferencePool.Acquire<AssetPatchManifestUpdateFailedEventArgs>();
            assetPatchManifestUpdateFailed.PackageName = packageName;
            assetPatchManifestUpdateFailed.Error       = error;
            return assetPatchManifestUpdateFailed;
        }
    }
}