using System;
using System.IO;
using UnityEngine;
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

        private const string ASSET_BUNDLE_PACKAGE_ROOT_KEY = "T1_ASSET_BUNDLE_PACKAGE_ROOT_KEY";

        private InitializationOperation InitializeYooAssetEditorSimulateMode(ResourcePackage resourcePackage)
        {
            var initParameters = new EditorSimulateModeParameters();
            var buildResult = EditorSimulateModeHelper.SimulateBuild(resourcePackage.PackageName);
            var packageRoot = buildResult.PackageRootDirectory;
            var editorFileSystem = FileSystemParameters.CreateDefaultEditorFileSystemParameters(packageRoot);
            initParameters.EditorFileSystemParameters = editorFileSystem;
            initParameters.AutoUnloadBundleWhenUnused = true;
            return resourcePackage.InitializeAsync(initParameters);
        }

        private InitializationOperation InitializeYooAssetOfflinePlayMode(ResourcePackage resourcePackage)
        {
            var buildinFileSystem = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
            var initParameters = new OfflinePlayModeParameters();
            initParameters.BuildinFileSystemParameters = buildinFileSystem;
            initParameters.AutoUnloadBundleWhenUnused = true;
            return resourcePackage.InitializeAsync(initParameters);
        }

        private InitializationOperation InitializeYooAssetWebPlayMode(ResourcePackage resourcePackage, string hostServerURL, string fallbackHostServerURL)
        {
            var initParameters = new WebPlayModeParameters();
            FileSystemParameters webRemoteFileSystemParams = null;
            IRemoteServices remoteServices = new RemoteServices(hostServerURL, fallbackHostServerURL);
            // var webServerFileSystemParams = FileSystemParameters.CreateDefaultWebServerFileSystemParameters();
            // webServerFileSystemParams.AddParameter(FileSystemParametersDefine.DISABLE_CATALOG_FILE, true);

#if UNITY_WEBGL
#if ENABLE_DOUYIN_MINI_GAME
            // 创建字节小游戏文件系统
            // https: //www.yooasset.com/docs/MiniGame#%E6%8A%96%E9%9F%B3%E5%B0%8F%E6%B8%B8%E6%88%8F
            string packageRoot = YooAssetSettingsData.GetDefaultYooFolderName();
            webRemoteFileSystemParams = TiktokFileSystemCreater.CreateFileSystemParameters(packageRoot, remoteServices);
#elif ENABLE_WECHAT_MINI_GAME
            //https://www.yooasset.com/docs/MiniGame#%E5%BE%AE%E4%BF%A1%E5%B0%8F%E6%B8%B8%E6%88%8F
            WeChatWASM.WXBase.PreloadConcurrent(10);
            string packageRoot = $"{WeChatWASM.WXBase.env.USER_DATA_PATH}/__GAME_FILE_CACHE/{YooAssetSettingsData.GetDefaultYooFolderName()}";
            // 创建微信小游戏文件系统

            webRemoteFileSystemParams = WechatFileSystemCreater.CreateFileSystemParameters(packageRoot, remoteServices, null);
            webRemoteFileSystemParams.AddParameter(FileSystemParametersDefine.DISABLE_CATALOG_FILE, true);
#else
            // 创建默认WebGL文件系统
            webRemoteFileSystemParams = FileSystemParameters.CreateDefaultWebRemoteFileSystemParameters(remoteServices); //支持跨域下载
#endif
#else
            webRemoteFileSystemParams = FileSystemParameters.CreateDefaultWebRemoteFileSystemParameters(remoteServices); //支持跨域下载
#endif
            initParameters.WebServerFileSystemParameters = null;
            initParameters.WebRemoteFileSystemParameters = webRemoteFileSystemParams;
            initParameters.AutoUnloadBundleWhenUnused = true;
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
            initParameters.AutoUnloadBundleWhenUnused = true;
            return resourcePackage.InitializeAsync(initParameters);
        }
    }
}