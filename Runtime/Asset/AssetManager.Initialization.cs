using YooAsset;

namespace GameFrameX.Asset.Runtime
{
    public partial class AssetManager
    {
        /// <summary>
        /// 根据运行模式创建初始化操作数据
        /// </summary>
        /// <returns></returns>
        private InitializationOperation CreateInitializationOperationHandler(ResourcePackage resourcePackage, string hostServerURL, string fallbackHostServerURL)
        {
            switch (PlayMode)
            {
                case EPlayMode.EditorSimulateMode:
                {
                    // 编辑器下的模拟模式
                    return InitializeYooAssetEditorSimulateMode(resourcePackage);
                }
                case EPlayMode.OfflinePlayMode:
                {
                    // 单机运行模式
                    return InitializeYooAssetOfflinePlayMode(resourcePackage);
                }
                case EPlayMode.HostPlayMode:
                {
                    // 联机运行模式
                    return InitializeYooAssetHostPlayMode(resourcePackage, hostServerURL, fallbackHostServerURL);
                }
                case EPlayMode.WebPlayMode:
                {
                    // WebGL运行模式
                    return InitializeYooAssetWebPlayMode(resourcePackage, hostServerURL, fallbackHostServerURL);
                }
                default:
                {
                    return null;
                }
            }
        }

        private InitializationOperation InitializeYooAssetEditorSimulateMode(ResourcePackage resourcePackage)
        {
            var initParameters = new EditorSimulateModeParameters();
            //注意：如果是原生文件系统选择EDefaultBuildPipeline.RawFileBuildPipeline
            var buildPipeline = EDefaultBuildPipeline.BuiltinBuildPipeline;
            var simulateBuildResult = EditorSimulateModeHelper.SimulateBuild(buildPipeline, DefaultPackageName);
            var editorFileSystem = FileSystemParameters.CreateDefaultEditorFileSystemParameters(simulateBuildResult);
            initParameters.EditorFileSystemParameters = editorFileSystem;
            return resourcePackage.InitializeAsync(initParameters);
        }

        private InitializationOperation InitializeYooAssetOfflinePlayMode(ResourcePackage resourcePackage)
        {
            var buildinFileSystem = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
            var initParameters = new OfflinePlayModeParameters();
            initParameters.BuildinFileSystemParameters = buildinFileSystem;
            return resourcePackage.InitializeAsync(initParameters);
        }

        private InitializationOperation InitializeYooAssetWebPlayMode(ResourcePackage resourcePackage, string hostServerURL, string fallbackHostServerURL)
        {
            var webFileSystem = FileSystemParameters.CreateDefaultWebFileSystemParameters();
            var initParameters = new WebPlayModeParameters();
            initParameters.WebFileSystemParameters = webFileSystem;
            return resourcePackage.InitializeAsync(initParameters);
        }

        private InitializationOperation InitializeYooAssetHostPlayMode(ResourcePackage resourcePackage, string hostServerURL, string fallbackHostServerURL)
        {
            IRemoteServices remoteServices = new RemoteServices(hostServerURL, fallbackHostServerURL);
            var cacheFileSystem = FileSystemParameters.CreateDefaultCacheFileSystemParameters(remoteServices);
            var buildinFileSystem = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
            var initParameters = new HostPlayModeParameters();
            initParameters.BuildinFileSystemParameters = buildinFileSystem;
            initParameters.CacheFileSystemParameters = cacheFileSystem;
            return resourcePackage.InitializeAsync(initParameters);
        }
    }
}