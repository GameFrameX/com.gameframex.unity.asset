﻿using GameFrameX.Event.Runtime;

namespace GameFrameX.Asset.Runtime
{
    /// <summary>
    /// 资源版本号更新失败
    /// </summary>
    public sealed class AssetStaticVersionUpdateFailedEventArgs : GameEventArgs
    {
        public override void Clear()
        {
        }

        /// <summary>
        /// 资源版本号更新失败事件编号
        /// </summary>
        public static readonly string EventId = nameof(AssetStaticVersionUpdateFailedEventArgs);

        public override string Id
        {
            get { return EventId; }
        }

        /// <summary>
        /// 包名称
        /// </summary>
        public string PackageName { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Error { get; private set; }

        /// <summary>
        /// 创建资源版本号更新失败
        /// </summary>
        /// <param name="packageName">包名称</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static AssetStaticVersionUpdateFailedEventArgs Create(string packageName, string error)
        {
            var assetStaticVersionUpdateFailed = ReferencePool.Acquire<AssetStaticVersionUpdateFailedEventArgs>();
            assetStaticVersionUpdateFailed.PackageName = packageName;
            assetStaticVersionUpdateFailed.Error       = error;
            return assetStaticVersionUpdateFailed;
        }
    }
}