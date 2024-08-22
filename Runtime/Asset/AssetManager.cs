using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameFrameX.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using YooAsset;
using Object = UnityEngine.Object;

namespace GameFrameX.Asset.Runtime
{
    /// <summary>
    /// 资源组件。
    /// </summary>
    public partial class AssetManager : GameFrameworkModule, IAssetManager
    {
        public string DefaultPackageName { get; set; } = "DefaultPackage";


        public int DownloadingMaxNum { get; set; }
        public int FailedTryAgain { get; set; }


        public EFileVerifyLevel VerifyLevel { get; set; }
        public long Milliseconds { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public void Initialize()
        {
            BetterStreamingAssets.Initialize();
            Log.Info($"资源系统运行模式：{PlayMode}");
            YooAssets.Initialize();
            YooAssets.SetOperationSystemMaxTimeSlice(30);
            // YooAssets.SetCacheSystemCachedFileVerifyLevel(EVerifyLevel.High);
            // YooAssets.SetDownloadSystemBreakpointResumeFileSize(4096 * 8);

            Log.Info("Asset Init Over");
        }


        /// <summary>
        /// 初始化操作。
        /// </summary>
        /// <param name="packageName">包名称</param>
        /// <param name="hostServerURL">热更链接URL。</param>
        /// <param name="fallbackHostServerURL">备用热更链接URL</param>
        /// <param name="isDefaultPackage">是否是默认包</param>
        /// <returns></returns>
        public UniTask<bool> InitPackageAsync(string packageName, string hostServerURL, string fallbackHostServerURL, bool isDefaultPackage = false)
        {
            var taskCompletionSource = new UniTaskCompletionSource<bool>();
            GameFrameworkGuard.NotNull(packageName, nameof(packageName));
            GameFrameworkGuard.NotNull(hostServerURL, nameof(hostServerURL));
            GameFrameworkGuard.NotNull(fallbackHostServerURL, nameof(fallbackHostServerURL));

            // 创建默认的资源包
            var resourcePackage = YooAssets.TryGetPackage(packageName);
            if (resourcePackage == null)
            {
                resourcePackage = YooAssets.CreatePackage(packageName);
                if (isDefaultPackage)
                {
                    // 设置该资源包为默认的资源包，可以使用YooAssets相关加载接口加载该资源包内容。
                    YooAssets.SetDefaultPackage(resourcePackage);
                }
            }

            var initializationOperationHandler = CreateInitializationOperationHandler(resourcePackage, hostServerURL, fallbackHostServerURL);
            initializationOperationHandler.Completed += asyncOperationBase =>
            {
                if (asyncOperationBase.Error == null && asyncOperationBase.Status == EOperationStatus.Succeed && asyncOperationBase.IsDone)
                {
                    taskCompletionSource.TrySetResult(true);
                }
                else
                {
                    taskCompletionSource.TrySetException(new Exception(asyncOperationBase.Error));
                }
            };
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// 卸载资源
        /// </summary>
        /// <param name="assetPath"></param>
        public void UnloadAsset(string assetPath)
        {
            GameFrameworkGuard.NotNull(assetPath, nameof(assetPath));
            var package = YooAssets.GetPackage(DefaultPackageName);
            package.TryUnloadUnusedAsset(assetPath);
        }


        /// <summary>
        /// 强制回收所有资源
        /// </summary>
        /// <param name="packageName">资源包名称</param>
        public void UnloadAllAssetsAsync(string packageName)
        {
            GameFrameworkGuard.NotNull(packageName, nameof(packageName));
            var package = YooAssets.GetPackage(packageName);
            package.UnloadAllAssetsAsync();
        }

        /// <summary>
        /// 卸载无用资源
        /// </summary>
        /// <param name="packageName">资源包名称</param>
        public void UnloadUnusedAssetsAsync(string packageName)
        {
            GameFrameworkGuard.NotNull(packageName, nameof(packageName));
            var package = YooAssets.GetPackage(packageName);
            package.UnloadUnusedAssetsAsync();
        }

        /// <summary>
        /// 清理所有资源
        /// </summary>
        /// <param name="packageName">资源包名称</param>
        public void ClearAllBundleFilesAsync(string packageName)
        {
            GameFrameworkGuard.NotNull(packageName, nameof(packageName));
            var package = YooAssets.GetPackage(packageName);
            package.ClearAllBundleFilesAsync();
        }

        /// <summary>
        /// 清理无用资源
        /// </summary>
        /// <param name="packageName">资源包名称</param>
        public void ClearUnusedBundleFilesAsync(string packageName)
        {
            GameFrameworkGuard.NotNull(packageName, nameof(packageName));
            var package = YooAssets.GetPackage(packageName);
            package.ClearUnusedBundleFilesAsync();
        }


        #region 异步加载子资源对象

        /// <summary>
        /// 异步加载子资源对象
        /// </summary>
        /// <param name="assetInfo">资源信息</param>
        /// <returns></returns>
        public UniTask<SubAssetsHandle> LoadSubAssetsAsync(AssetInfo assetInfo)
        {
            var taskCompletionSource = new UniTaskCompletionSource<SubAssetsHandle>();
            var assetHandle = YooAssets.LoadSubAssetsAsync(assetInfo);
            assetHandle.Completed += handle => { taskCompletionSource.TrySetResult(handle); };
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// 异步加载子资源对象
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public UniTask<SubAssetsHandle> LoadSubAssetsAsync(string path, Type type)
        {
            var taskCompletionSource = new UniTaskCompletionSource<SubAssetsHandle>();
            var assetHandle = YooAssets.LoadSubAssetsAsync(path, type);
            assetHandle.Completed += handle => { taskCompletionSource.TrySetResult(handle); };
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// 异步加载子资源对象
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <returns></returns>
        public UniTask<SubAssetsHandle> LoadSubAssetsAsync<T>(string path) where T : Object
        {
            var taskCompletionSource = new UniTaskCompletionSource<SubAssetsHandle>();
            var assetHandle = YooAssets.LoadSubAssetsAsync<T>(path);
            assetHandle.Completed += handle => { taskCompletionSource.TrySetResult(handle); };
            return taskCompletionSource.Task;
        }

        #endregion

        #region 异步加载子资源对象

        /// <summary>
        /// 同步加载子资源对象
        /// </summary>
        /// <param name="assetInfo">资源信息</param>
        /// <returns></returns>
        public SubAssetsHandle LoadSubAssetSync(AssetInfo assetInfo)
        {
            return YooAssets.LoadSubAssetsSync(assetInfo);
        }

        /// <summary>
        /// 同步加载子资源对象
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public SubAssetsHandle LoadSubAssetSync(string path, Type type)
        {
            return YooAssets.LoadSubAssetsSync(path, type);
        }

        /// <summary>
        /// 同步加载子资源对象
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <returns></returns>
        public SubAssetsHandle LoadSubAssetSync<T>(string path) where T : Object
        {
            return YooAssets.LoadSubAssetsSync<T>(path);
        }

        #endregion

        #region 异步加载原生文件

        /// <summary>
        /// 异步加载原生文件
        /// </summary>
        /// <param name="assetInfo">资源信息</param>
        /// <returns></returns>
        public UniTask<RawFileHandle> LoadRawFileAsync(AssetInfo assetInfo)
        {
            var taskCompletionSource = new UniTaskCompletionSource<RawFileHandle>();
            var assetHandle = YooAssets.LoadRawFileAsync(assetInfo);
            assetHandle.Completed += handle => { taskCompletionSource.TrySetResult(handle); };
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// 异步加载原生文件
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <returns></returns>
        public UniTask<RawFileHandle> LoadRawFileAsync(string path)
        {
            var taskCompletionSource = new UniTaskCompletionSource<RawFileHandle>();
            var assetHandle = YooAssets.LoadRawFileAsync(path);
            assetHandle.Completed += handle => { taskCompletionSource.TrySetResult(handle); };
            return taskCompletionSource.Task;
        }

        #endregion

        #region 同步加载原生文件

        /// <summary>
        /// 同步加载原生文件
        /// </summary>
        /// <param name="assetInfo">资源信息</param>
        /// <returns></returns>
        public RawFileHandle LoadRawFileSync(AssetInfo assetInfo)
        {
            return YooAssets.LoadRawFileSync(assetInfo);
        }

        /// <summary>
        /// 同步加载原生文件
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <returns></returns>
        public RawFileHandle LoadRawFileSync(string path)
        {
            return YooAssets.LoadRawFileSync(path);
        }

        #endregion


        #region 异步加载资源

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="assetInfo">资源信息</param>
        /// <returns></returns>
        public UniTask<AssetHandle> LoadAssetAsync(AssetInfo assetInfo)
        {
            var taskCompletionSource = new UniTaskCompletionSource<AssetHandle>();
            var assetHandle = YooAssets.LoadAssetAsync(assetInfo);
            assetHandle.Completed += handle => { taskCompletionSource.TrySetResult(handle); };
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <param name="type">资源类型</param>
        /// <returns></returns>
        public UniTask<AssetHandle> LoadAssetAsync(string path, Type type)
        {
            var taskCompletionSource = new UniTaskCompletionSource<AssetHandle>();
            var assetHandle = YooAssets.LoadAssetAsync(path, type);
            assetHandle.Completed += handle => { taskCompletionSource.TrySetResult(handle); };
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// 异步加载全部资源
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <returns></returns>
        public UniTask<AllAssetsHandle> LoadAllAssetsAsync<T>(string path) where T : Object
        {
            var taskCompletionSource = new UniTaskCompletionSource<AllAssetsHandle>();
            var assetHandle = YooAssets.LoadAllAssetsAsync<T>(path);
            assetHandle.Completed += handle => { taskCompletionSource.TrySetResult(handle); };
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// 异步加载全部资源
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <param name="type">资源类型</param>
        /// <returns></returns>
        public UniTask<AllAssetsHandle> LoadAllAssetsAsync(string path, Type type)
        {
            var taskCompletionSource = new UniTaskCompletionSource<AllAssetsHandle>();
            var assetHandle = YooAssets.LoadAllAssetsAsync(path, type);
            assetHandle.Completed += handle => { taskCompletionSource.TrySetResult(handle); };
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// 异步加载资源包内所有资源对象
        /// </summary>
        /// <param name="path">资源的定位地址</param>
        public UniTask<AllAssetsHandle> LoadAllAssetsAsync(string path)
        {
            var taskCompletionSource = new UniTaskCompletionSource<AllAssetsHandle>();
            var assetHandle = YooAssets.LoadAllAssetsAsync(path);
            assetHandle.Completed += handle => { taskCompletionSource.TrySetResult(handle); };
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// 异步加载资源包内所有资源对象
        /// </summary>
        /// <param name="assetInfo">资源信息</param>
        public UniTask<AllAssetsHandle> LoadAllAssetsAsync(AssetInfo assetInfo)
        {
            var taskCompletionSource = new UniTaskCompletionSource<AllAssetsHandle>();
            var assetHandle = YooAssets.LoadAllAssetsAsync(assetInfo);
            assetHandle.Completed += handle => { taskCompletionSource.TrySetResult(handle); };
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// 异步加载子资源对象
        /// </summary>
        /// <param name="path">资源的定位地址</param>
        public SubAssetsHandle LoadSubAssetsAsync(string path)
        {
            return YooAssets.LoadSubAssetsAsync(path);
        }


        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <returns></returns>
        public UniTask<AssetHandle> LoadAssetAsync(string path)
        {
            var taskCompletionSource = new UniTaskCompletionSource<AssetHandle>();
            var assetHandle = YooAssets.LoadAssetAsync(path);
            assetHandle.Completed += handle => { taskCompletionSource.TrySetResult(handle); };
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <typeparam name="T">资源类型</typeparam>
        /// <returns></returns>
        public UniTask<AssetHandle> LoadAssetAsync<T>(string path) where T : Object
        {
            var taskCompletionSource = new UniTaskCompletionSource<AssetHandle>();
            var assetHandle = YooAssets.LoadAssetAsync<T>(path);
            assetHandle.Completed += handle => { taskCompletionSource.TrySetResult(handle); };
            return taskCompletionSource.Task;
        }

        /*
        public async Task<TObject> LoadAssetTaskAsync<TObject>(string assetPath) where TObject : Object
        {
            ResourcePackage assetPackage = YooAssets.TryGetPackage(DefaultPackageName);
            var handle = assetPackage.LoadAssetAsync<TObject>(assetPath);
            await handle.Task;
            if (handle == null || handle.AssetObject == null || handle.Status == EOperationStatus.Failed)
            {
                string errorMessage = Utility.Text.Format("Can not load asset '{0}'.", assetPath);
                throw new GameFrameworkException(errorMessage);
            }

            var result = handle.AssetObject as TObject;
            if (result == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("TObject '{0}' is invalid.", typeof(TObject).FullName));
            }

            return result;
        }*/

        #endregion

        #region 同步加载资源

        /// <summary>
        /// 同步加载资源包内所有资源对象
        /// </summary>
        /// <param name="path">资源的定位地址</param>
        public AllAssetsHandle LoadAllAssetsSync(string path)
        {
            return YooAssets.LoadAllAssetsSync(path);
        }

        /// <summary>
        /// 同步加载资源包内所有资源对象
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="path">资源的定位地址</param>
        public AllAssetsHandle LoadAllAssetsSync<T>(string path) where T : Object
        {
            return YooAssets.LoadAllAssetsSync<T>(path);
        }

        /// <summary>
        /// 同步加载资源包内所有资源对象
        /// </summary>
        /// <param name="path">资源的定位地址</param>
        /// <param name="type">子对象类型</param>
        public AllAssetsHandle LoadAllAssetsSync(string path, Type type)
        {
            return YooAssets.LoadAllAssetsSync(path, type);
        }

        /// <summary>
        /// 同步加载包内全部资源对象
        /// </summary>
        /// <param name="assetInfo">资源信息</param>
        /// <returns></returns>
        public AllAssetsHandle LoadAllAssetsSync(AssetInfo assetInfo)
        {
            return YooAssets.LoadAllAssetsSync(assetInfo);
        }

        /// <summary>
        /// 同步加载子资源
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <returns></returns>
        public SubAssetsHandle LoadSubAssetSync(string path)
        {
            return YooAssets.LoadSubAssetsSync(path);
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <returns></returns>
        public AssetHandle LoadAssetSync(string path)
        {
            return YooAssets.LoadAssetSync(path);
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public AssetHandle LoadAssetSync(string path, Type type)
        {
            return YooAssets.LoadAssetSync(path, type);
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <param name="assetInfo">资源信息</param>
        /// <returns></returns>
        public AssetHandle LoadAssetSync(AssetInfo assetInfo)
        {
            return YooAssets.LoadAssetSync(assetInfo);
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <returns></returns>
        public AssetHandle LoadAssetSync<T>(string path) where T : Object
        {
            return YooAssets.LoadAssetSync<T>(path);
        }

        #endregion

        #region 加载场景

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <param name="sceneMode">场景模式</param>
        /// <param name="activateOnLoad">是否加载完成自动激活</param>
        /// <returns></returns>
        public UniTask<SceneHandle> LoadSceneAsync(string path, LoadSceneMode sceneMode, bool activateOnLoad = true)
        {
            var taskCompletionSource = new UniTaskCompletionSource<SceneHandle>();
            var sceneHandle = YooAssets.LoadSceneAsync(path, sceneMode, LocalPhysicsMode.None, !activateOnLoad);
            sceneHandle.Completed += handle => { taskCompletionSource.TrySetResult(handle); };
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="assetInfo">资源路径</param>
        /// <param name="sceneMode">场景模式</param>
        /// <param name="activateOnLoad">是否加载完成自动激活</param>
        /// <returns></returns>
        public UniTask<SceneHandle> LoadSceneAsync(AssetInfo assetInfo, LoadSceneMode sceneMode, bool activateOnLoad = true)
        {
            var taskCompletionSource = new UniTaskCompletionSource<SceneHandle>();
            var sceneHandle = YooAssets.LoadSceneAsync(assetInfo, sceneMode, LocalPhysicsMode.None, !activateOnLoad);
            sceneHandle.Completed += handle => { taskCompletionSource.TrySetResult(handle); };
            return taskCompletionSource.Task;
        }

        #endregion

        #region 资源包

        /// <summary>
        /// 创建资源包
        /// </summary>
        /// <param name="packageName">资源包名称</param>
        /// <returns></returns>
        public ResourcePackage CreateAssetsPackage(string packageName)
        {
            return YooAssets.CreatePackage(packageName);
        }

        /// <summary>
        /// 尝试获取资源包
        /// </summary>
        /// <param name="packageName">资源包名称</param>
        /// <returns></returns>
        public ResourcePackage TryGetAssetsPackage(string packageName)
        {
            return YooAssets.TryGetPackage(packageName);
        }

        /// <summary>
        /// 检查资源包是否存在
        /// </summary>
        /// <param name="packageName">资源包名称</param>
        /// <returns></returns>
        public bool HasAssetsPackage(string packageName)
        {
            return YooAssets.TryGetPackage(packageName) != null;
        }

        /// <summary>
        /// 获取资源包
        /// </summary>
        /// <param name="packageName">资源包名称</param>
        /// <returns></returns>
        public ResourcePackage GetAssetsPackage(string packageName)
        {
            return YooAssets.GetPackage(packageName);
        }

        #endregion

        /// <summary>
        /// 是否需要下载
        /// </summary>
        /// <param name="assetInfo">资源信息</param>
        /// <returns></returns>
        public bool IsNeedDownload(AssetInfo assetInfo)
        {
            return YooAssets.IsNeedDownloadFromRemote(assetInfo);
        }

        /// <summary>
        /// 是否需要下载
        /// </summary>
        /// <param name="path">资源地址</param>
        /// <returns></returns>
        public bool IsNeedDownload(string path)
        {
            return YooAssets.IsNeedDownloadFromRemote(path);
        }

        /// <summary>
        /// 获取资源信息
        /// </summary>
        /// <param name="assetTags">资源标签列表</param>
        /// <returns></returns>
        public AssetInfo[] GetAssetInfos(string[] assetTags)
        {
            return YooAssets.GetAssetInfos(assetTags);
        }

        /// <summary>
        /// 获取资源信息
        /// </summary>
        /// <param name="assetTag">资源标签</param>
        /// <returns></returns>
        public AssetInfo[] GetAssetInfos(string assetTag)
        {
            return YooAssets.GetAssetInfos(assetTag);
        }

        /// <summary>
        /// 获取资源信息
        /// </summary>
        public AssetInfo GetAssetInfo(string path)
        {
            return YooAssets.GetAssetInfo(path);
        }


        /// <summary>
        /// 设置默认资源包
        /// </summary>
        /// <param name="resourcePackage">资源信息</param>
        /// <returns></returns>
        public void SetDefaultAssetsPackage(ResourcePackage resourcePackage)
        {
            YooAssets.SetDefaultPackage(resourcePackage);
        }


        protected override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        protected override void Shutdown()
        {
        }


        /// <summary>
        /// 获取或设置运行模式。
        /// </summary>
        public EPlayMode PlayMode { get; private set; }

        /// <summary>
        /// 设置运行模式
        /// </summary>
        /// <param name="playMode">运行模式</param>
        public void SetPlayMode(EPlayMode playMode)
        {
            PlayMode = playMode;
        }

        /// <summary>
        /// 获取资源只读区路径。
        /// </summary>
        public string ReadOnlyPath { get; private set; }

        /// <summary>
        /// 设置资源只读区路径。
        /// </summary>
        /// <param name="readOnlyPath">资源只读区路径。</param>
        public void SetReadOnlyPath(string readOnlyPath)
        {
            GameFrameworkGuard.NotNull(readOnlyPath, nameof(readOnlyPath));
            ReadOnlyPath = readOnlyPath;
        }

        /// <summary>
        /// 获取资源读写区路径。
        /// </summary>
        public string ReadWritePath { get; private set; }

        /// <summary>
        /// 设置资源读写区路径。
        /// </summary>
        /// <param name="readWritePath">资源读写区路径。</param>
        public void SetReadWritePath(string readWritePath)
        {
            GameFrameworkGuard.NotNull(readWritePath, nameof(readWritePath));
            ReadWritePath = readWritePath;
        }
    }
}