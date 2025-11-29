using GameFrameX.Runtime;
using YooAsset;

namespace GameFrameX.Asset.Runtime
{
    public partial class AssetManager
    {
        [UnityEngine.Scripting.Preserve]
        private class RemoteServices : IRemoteServices
        {
            [UnityEngine.Scripting.Preserve] public string HostServer { get; }
            [UnityEngine.Scripting.Preserve] public string FallbackHostServer { get; }

            [UnityEngine.Scripting.Preserve]
            public RemoteServices(string hostServer, string fallbackHostServer)
            {
                HostServer = hostServer;
                FallbackHostServer = fallbackHostServer;
            }

            [UnityEngine.Scripting.Preserve]
            public string GetRemoteMainURL(string fileName)
            {
                return HostServer + fileName;
            }

            [UnityEngine.Scripting.Preserve]
            public string GetRemoteFallbackURL(string fileName)
            {
                return FallbackHostServer + fileName;
            }
        }

        /*/// <summary>
        /// 内置文件查询服务类
        /// </summary>
        private class QueryStreamingAssetsFileServices : IBuildinQueryServices
        {
            public bool Query(string packageName, string fileName)
            {
#if UNITY_WEBGL
                return false;
#endif

                // 注意：使用了BetterStreamingAssets插件，使用前需要初始化该插件！
                string buildinFolderName = PathHelper.AppResPath;
                return BetterStreamingAssets.FileExists($"{buildinFolderName}/{fileName}");
            }
        }*/
    }
}