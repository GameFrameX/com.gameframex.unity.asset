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
            initParameters.SimulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild(EDefaultBuildPipeline.BuiltinBuildPipeline.ToString(), DefaultPackageName);

            return resourcePackage.InitializeAsync(initParameters);
        }

        private InitializationOperation InitializeYooAssetOfflinePlayMode(ResourcePackage resourcePackage)
        {
            var initParameters = new OfflinePlayModeParameters();
            return resourcePackage.InitializeAsync(initParameters);
        }

        private InitializationOperation InitializeYooAssetWebPlayMode(ResourcePackage resourcePackage, string hostServerURL, string fallbackHostServerURL)
        {
            var initParameters = new WebPlayModeParameters();
            initParameters.BuildinQueryServices = new QueryStreamingAssetsFileServices();
            initParameters.RemoteServices = new RemoteServices(hostServerURL, fallbackHostServerURL);
            return resourcePackage.InitializeAsync(initParameters);
        }

        private InitializationOperation InitializeYooAssetHostPlayMode(ResourcePackage resourcePackage, string hostServerURL, string fallbackHostServerURL)
        {
            var initParameters = new HostPlayModeParameters();
            initParameters.BuildinQueryServices = new QueryStreamingAssetsFileServices();
            initParameters.RemoteServices = new RemoteServices(hostServerURL, fallbackHostServerURL);
            // initParameters.DeliveryQueryServices = new WebDeliveryQueryServices();
            // initParameters.DeliveryLoadServices = new WebDeliveryLoadServices();
            return resourcePackage.InitializeAsync(initParameters);
        }
    }
}