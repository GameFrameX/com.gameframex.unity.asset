using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameFrameX.Runtime;
using UnityEngine;
using YooAsset;

namespace GameFrameX.Asset.Runtime
{
    /// <summary>
    /// 资源组件。
    /// </summary>
    /// <remarks>
    /// Asset component that provides resource loading, unloading, and package management capabilities.
    /// </remarks>
    [DisallowMultipleComponent]
    [AddComponentMenu("GameFrameX/Asset")]
    [RequireComponent(typeof(GameFrameXAssetCroppingHelper))]
    [UnityEngine.Scripting.Preserve]
    public sealed class AssetComponent : GameFrameworkComponent
    {
        [Tooltip("当目标平台为Web平台时，将会强制设置为" + nameof(EPlayMode.WebPlayMode))] [SerializeField]
        private EPlayMode m_GamePlayMode;

        /// <summary>
        /// 获取或设置资源的运行模式。
        /// </summary>
        /// <remarks>
        /// Gets or sets the asset play mode.
        /// </remarks>
        /// <value>资源的运行模式 / Asset play mode</value>
        [UnityEngine.Scripting.Preserve]
        public EPlayMode GamePlayMode
        {
            get { return m_GamePlayMode; }
            set { m_GamePlayMode = value; }
        }
#if UNITY_EDITOR
        [SerializeField] private List<AssetResourcePackageInfo> m_assetResourcePackages = new List<AssetResourcePackageInfo>();
#endif

        /// <summary>
        /// 内置资源包的默认名称。
        /// </summary>
        /// <remarks>
        /// The default name of the built-in asset package.
        /// </remarks>
        /// <value>内置资源包名称 / Built-in package name</value>
        public const string BuildInPackageName = "DefaultPackage";
        private InitializationOperation _initializationOperation;

        private IAssetManager _assetManager;

        [UnityEngine.Scripting.Preserve]
        protected override void Awake()
        {
#if !UNITY_EDITOR
            if (GamePlayMode == EPlayMode.EditorSimulateMode)
            {
                GamePlayMode = EPlayMode.HostPlayMode;
            }
#if UNITY_WEBGL
            GamePlayMode = EPlayMode.WebPlayMode;
#endif
#endif
            ImplementationComponentType = Utility.Assembly.GetType(componentType);
            InterfaceComponentType = typeof(IAssetManager);
            base.Awake();
            _assetManager = GameFrameworkEntry.GetModule<IAssetManager>();
            if (_assetManager == null)
            {
                Log.Fatal("Asset manager is invalid.");
                return;
            }

            _assetManager.SetPlayMode(GamePlayMode);
        }

        [UnityEngine.Scripting.Preserve]
        private void Start()
        {
            _assetManager.Initialize();
        }

        /// <summary>
        /// 异步初始化资源包。
        /// </summary>
        /// <remarks>
        /// Asynchronously initializes an asset package.
        /// </remarks>
        /// <param name="packageName">资源包名称 / Asset package name</param>
        /// <param name="host">主下载地址 / Primary download URL</param>
        /// <param name="fallbackHostServer">备用下载地址 / Fallback download URL</param>
        /// <param name="isDefaultPackage">是否为默认资源包 / Whether this is the default package</param>
        /// <returns>初始化是否成功 / Whether initialization succeeded</returns>
        [UnityEngine.Scripting.Preserve]
        public async Task<bool> InitPackageAsync(string packageName, string host, string fallbackHostServer, bool isDefaultPackage = false)
        {
#if UNITY_EDITOR
            var assetResourcePackageInfo = new AssetResourcePackageInfo()
            {
                PackageName = packageName,
                DownloadURL = host,
                FallbackDownloadURL = fallbackHostServer
            };
            if (!m_assetResourcePackages.Exists(m => m.PackageName == packageName))
            {
                m_assetResourcePackages.Add(assetResourcePackageInfo);
            }
#endif
            return await _assetManager.InitPackageAsync(packageName, host, fallbackHostServer, isDefaultPackage);
        }

        #region 异步加载子资源对象

        /// <summary>
        /// 异步加载子资源对象。
        /// </summary>
        /// <remarks>
        /// Asynchronously loads sub-asset objects.
        /// </remarks>
        /// <param name="assetInfo">资源信息 / Asset information</param>
        /// <returns>子资源操作句柄的异步任务 / Async task of the sub-assets handle</returns>
        [UnityEngine.Scripting.Preserve]
        public Task<SubAssetsHandle> LoadSubAssetsAsync(AssetInfo assetInfo)
        {
            return _assetManager.LoadSubAssetsAsync(assetInfo);
        }

        /// <summary>
        /// 异步加载子资源对象。
        /// </summary>
        /// <remarks>
        /// Asynchronously loads sub-asset objects.
        /// </remarks>
        /// <param name="path">资源路径 / Asset path</param>
        /// <param name="type">子资源类型 / Sub-asset type</param>
        /// <returns>子资源操作句柄的异步任务 / Async task of the sub-assets handle</returns>
        [UnityEngine.Scripting.Preserve]
        public Task<SubAssetsHandle> LoadSubAssetsAsync(string path, Type type)
        {
            return _assetManager.LoadSubAssetsAsync(path, type);
        }

        /// <summary>
        /// 异步加载子资源对象。
        /// </summary>
        /// <remarks>
        /// Asynchronously loads sub-asset objects.
        /// </remarks>
        /// <typeparam name="T">子资源类型 / Sub-asset type</typeparam>
        /// <param name="path">资源路径 / Asset path</param>
        /// <returns>子资源操作句柄的异步任务 / Async task of the sub-assets handle</returns>
        [UnityEngine.Scripting.Preserve]
        public Task<SubAssetsHandle> LoadSubAssetsAsync<T>(string path) where T : UnityEngine.Object
        {
            return _assetManager.LoadSubAssetsAsync<T>(path);
        }

        #endregion

        #region 同步加载子资源对象

        /// <summary>
        /// 同步加载子资源对象。
        /// </summary>
        /// <remarks>
        /// Synchronously loads sub-asset objects.
        /// </remarks>
        /// <param name="assetInfo">资源信息 / Asset information</param>
        /// <returns>子资源操作句柄 / Sub-assets handle</returns>
        [UnityEngine.Scripting.Preserve]
        public SubAssetsHandle LoadSubAssetSync(AssetInfo assetInfo)
        {
            return _assetManager.LoadSubAssetSync(assetInfo);
        }

        /// <summary>
        /// 同步加载子资源对象。
        /// </summary>
        /// <remarks>
        /// Synchronously loads sub-asset objects.
        /// </remarks>
        /// <param name="path">资源路径 / Asset path</param>
        /// <param name="type">子资源类型 / Sub-asset type</param>
        /// <returns>子资源操作句柄 / Sub-assets handle</returns>
        [UnityEngine.Scripting.Preserve]
        public SubAssetsHandle LoadSubAssetSync(string path, Type type)
        {
            return _assetManager.LoadSubAssetSync(path, type);
        }

        /// <summary>
        /// 同步加载子资源对象。
        /// </summary>
        /// <remarks>
        /// Synchronously loads sub-asset objects.
        /// </remarks>
        /// <typeparam name="T">子资源类型 / Sub-asset type</typeparam>
        /// <param name="path">资源路径 / Asset path</param>
        /// <returns>子资源操作句柄 / Sub-assets handle</returns>
        [UnityEngine.Scripting.Preserve]
        public SubAssetsHandle LoadSubAssetSync<T>(string path) where T : UnityEngine.Object
        {
            return _assetManager.LoadSubAssetSync<T>(path);
        }

        #endregion

        #region 异步加载原生文件

        /// <summary>
        /// 异步加载原生文件。
        /// </summary>
        /// <remarks>
        /// Asynchronously loads a raw file.
        /// </remarks>
        /// <param name="assetInfo">资源信息 / Asset information</param>
        /// <returns>原生文件操作句柄的异步任务 / Async task of the raw file handle</returns>
        [UnityEngine.Scripting.Preserve]
        public Task<RawFileHandle> LoadRawFileAsync(AssetInfo assetInfo)
        {
            return _assetManager.LoadRawFileAsync(assetInfo);
        }

        /// <summary>
        /// 异步加载原生文件。
        /// </summary>
        /// <remarks>
        /// Asynchronously loads a raw file.
        /// </remarks>
        /// <param name="path">资源路径 / Asset path</param>
        /// <returns>原生文件操作句柄的异步任务 / Async task of the raw file handle</returns>
        [UnityEngine.Scripting.Preserve]
        public Task<RawFileHandle> LoadRawFileAsync(string path)
        {
            return _assetManager.LoadRawFileAsync(path);
        }

        #endregion

        #region 同步加载原生文件

        /// <summary>
        /// 同步加载原生文件。
        /// </summary>
        /// <remarks>
        /// Synchronously loads a raw file.
        /// </remarks>
        /// <param name="assetInfo">资源信息 / Asset information</param>
        /// <returns>原生文件操作句柄 / Raw file handle</returns>
        [UnityEngine.Scripting.Preserve]
        public RawFileHandle LoadRawFileSync(AssetInfo assetInfo)
        {
            return _assetManager.LoadRawFileSync(assetInfo);
        }

        /// <summary>
        /// 同步加载原生文件。
        /// </summary>
        /// <remarks>
        /// Synchronously loads a raw file.
        /// </remarks>
        /// <param name="path">资源路径 / Asset path</param>
        /// <returns>原生文件操作句柄 / Raw file handle</returns>
        [UnityEngine.Scripting.Preserve]
        public RawFileHandle LoadRawFileSync(string path)
        {
            return _assetManager.LoadRawFileSync(path);
        }

        #endregion


        #region 异步加载资源

        /// <summary>
        /// 异步加载资源。
        /// </summary>
        /// <remarks>
        /// Asynchronously loads an asset.
        /// </remarks>
        /// <param name="assetInfo">资源信息 / Asset information</param>
        /// <returns>资源操作句柄的异步任务 / Async task of the asset handle</returns>
        [UnityEngine.Scripting.Preserve]
        public Task<AssetHandle> LoadAssetAsync(AssetInfo assetInfo)
        {
            return _assetManager.LoadAssetAsync(assetInfo);
        }

        /// <summary>
        /// 异步加载资源。
        /// </summary>
        /// <remarks>
        /// Asynchronously loads an asset.
        /// </remarks>
        /// <param name="path">资源路径 / Asset path</param>
        /// <param name="type">资源类型 / Asset type</param>
        /// <returns>资源操作句柄的异步任务 / Async task of the asset handle</returns>
        [UnityEngine.Scripting.Preserve]
        public Task<AssetHandle> LoadAssetAsync(string path, Type type)
        {
            return _assetManager.LoadAssetAsync(path, type);
        }

        /// <summary>
        /// 异步加载资源。
        /// </summary>
        /// <remarks>
        /// Asynchronously loads an asset.
        /// </remarks>
        /// <typeparam name="T">资源类型 / Asset type</typeparam>
        /// <param name="path">资源路径 / Asset path</param>
        /// <returns>资源操作句柄的异步任务 / Async task of the asset handle</returns>
        [UnityEngine.Scripting.Preserve]
        public Task<AssetHandle> LoadAssetAsync<T>(string path) where T : UnityEngine.Object
        {
            return _assetManager.LoadAssetAsync<T>(path);
        }

        /// <summary>
        /// 异步加载全部资源。
        /// </summary>
        /// <remarks>
        /// Asynchronously loads all assets.
        /// </remarks>
        /// <typeparam name="T">资源类型 / Asset type</typeparam>
        /// <param name="path">资源路径 / Asset path</param>
        /// <returns>全部资源操作句柄的异步任务 / Async task of the all-assets handle</returns>
        [UnityEngine.Scripting.Preserve]
        public Task<AllAssetsHandle> LoadAllAssetsAsync<T>(string path) where T : UnityEngine.Object
        {
            return _assetManager.LoadAllAssetsAsync<T>(path);
        }

        /// <summary>
        /// 异步加载全部资源。
        /// </summary>
        /// <remarks>
        /// Asynchronously loads all assets.
        /// </remarks>
        /// <param name="path">资源路径 / Asset path</param>
        /// <param name="type">资源类型 / Asset type</param>
        /// <returns>全部资源操作句柄的异步任务 / Async task of the all-assets handle</returns>
        [UnityEngine.Scripting.Preserve]
        public Task<AllAssetsHandle> LoadAllAssetsAsync(string path, Type type)
        {
            return _assetManager.LoadAllAssetsAsync(path, type);
        }

        /// <summary>
        /// 异步加载资源包内所有资源对象。
        /// </summary>
        /// <remarks>
        /// Asynchronously loads all asset objects within the asset bundle.
        /// </remarks>
        /// <param name="path">资源的定位地址 / Asset location address</param>
        /// <returns>全部资源操作句柄的异步任务 / Async task of the all-assets handle</returns>
        [UnityEngine.Scripting.Preserve]
        public Task<AllAssetsHandle> LoadAllAssetsAsync(string path)
        {
            return _assetManager.LoadAllAssetsAsync(path);
        }

        /// <summary>
        /// 异步加载资源包内所有资源对象。
        /// </summary>
        /// <remarks>
        /// Asynchronously loads all asset objects within the asset bundle.
        /// </remarks>
        /// <param name="assetInfo">资源信息 / Asset information</param>
        /// <returns>全部资源操作句柄的异步任务 / Async task of the all-assets handle</returns>
        [UnityEngine.Scripting.Preserve]
        public Task<AllAssetsHandle> LoadAllAssetsAsync(AssetInfo assetInfo)
        {
            return _assetManager.LoadAllAssetsAsync(assetInfo);
        }

        /// <summary>
        /// 异步加载资源。
        /// </summary>
        /// <remarks>
        /// Asynchronously loads an asset.
        /// </remarks>
        /// <param name="path">资源路径 / Asset path</param>
        /// <returns>资源操作句柄的异步任务 / Async task of the asset handle</returns>
        [UnityEngine.Scripting.Preserve]
        public Task<AssetHandle> LoadAssetAsync(string path)
        {
            return _assetManager.LoadAssetAsync(path);
        }

        /// <summary>
        /// 异步加载子资源对象。
        /// </summary>
        /// <remarks>
        /// Asynchronously loads sub-asset objects.
        /// </remarks>
        /// <param name="path">资源的定位地址 / Asset location address</param>
        /// <returns>子资源操作句柄 / Sub-assets handle</returns>
        [UnityEngine.Scripting.Preserve]
        public SubAssetsHandle LoadSubAssetsAsync(string path)
        {
            return _assetManager.LoadSubAssetsAsync(path);
        }

        #endregion

        #region 同步加载资源

        /// <summary>
        /// 同步加载资源包内所有资源对象。
        /// </summary>
        /// <remarks>
        /// Synchronously loads all asset objects within the asset bundle.
        /// </remarks>
        /// <param name="path">资源的定位地址 / Asset location address</param>
        /// <returns>全部资源操作句柄 / All-assets handle</returns>
        [UnityEngine.Scripting.Preserve]
        public AllAssetsHandle LoadAllAssetsSync(string path)
        {
            return _assetManager.LoadAllAssetsSync(path);
        }

        /// <summary>
        /// 同步加载资源包内所有资源对象。
        /// </summary>
        /// <remarks>
        /// Synchronously loads all asset objects within the asset bundle.
        /// </remarks>
        /// <typeparam name="T">资源类型 / Asset type</typeparam>
        /// <param name="path">资源的定位地址 / Asset location address</param>
        /// <returns>全部资源操作句柄 / All-assets handle</returns>
        [UnityEngine.Scripting.Preserve]
        public AllAssetsHandle LoadAllAssetsSync<T>(string path) where T : UnityEngine.Object
        {
            return _assetManager.LoadAllAssetsSync<T>(path);
        }

        /// <summary>
        /// 同步加载资源包内所有资源对象。
        /// </summary>
        /// <remarks>
        /// Synchronously loads all asset objects within the asset bundle.
        /// </remarks>
        /// <param name="path">资源的定位地址 / Asset location address</param>
        /// <param name="type">子对象类型 / Sub-object type</param>
        /// <returns>全部资源操作句柄 / All-assets handle</returns>
        [UnityEngine.Scripting.Preserve]
        public AllAssetsHandle LoadAllAssetsSync(string path, Type type)
        {
            return _assetManager.LoadAllAssetsSync(path, type);
        }

        /// <summary>
        /// 同步加载包内全部资源对象。
        /// </summary>
        /// <remarks>
        /// Synchronously loads all asset objects within the package.
        /// </remarks>
        /// <param name="assetInfo">资源信息 / Asset information</param>
        /// <returns>全部资源操作句柄 / All-assets handle</returns>
        [UnityEngine.Scripting.Preserve]
        public AllAssetsHandle LoadAllAssetsSync(AssetInfo assetInfo)
        {
            return _assetManager.LoadAllAssetsSync(assetInfo);
        }

        /// <summary>
        /// 同步加载子资源。
        /// </summary>
        /// <remarks>
        /// Synchronously loads sub-assets.
        /// </remarks>
        /// <param name="path">资源路径 / Asset path</param>
        /// <returns>子资源操作句柄 / Sub-assets handle</returns>
        [UnityEngine.Scripting.Preserve]
        public SubAssetsHandle LoadSubAssetsSync(string path)
        {
            return _assetManager.LoadSubAssetSync(path);
        }

        /// <summary>
        /// 同步加载资源。
        /// </summary>
        /// <remarks>
        /// Synchronously loads an asset.
        /// </remarks>
        /// <param name="path">资源路径 / Asset path</param>
        /// <returns>资源操作句柄 / Asset handle</returns>
        [UnityEngine.Scripting.Preserve]
        public AssetHandle LoadAssetsSync(string path)
        {
            return _assetManager.LoadAssetSync(path);
        }

        /// <summary>
        /// 同步加载资源。
        /// </summary>
        /// <remarks>
        /// Synchronously loads an asset.
        /// </remarks>
        /// <param name="path">资源路径 / Asset path</param>
        /// <param name="type">资源类型 / Asset type</param>
        /// <returns>资源操作句柄 / Asset handle</returns>
        [UnityEngine.Scripting.Preserve]
        public AssetHandle LoadAssetSync(string path, Type type)
        {
            return _assetManager.LoadAssetSync(path, type);
        }

        /// <summary>
        /// 同步加载资源。
        /// </summary>
        /// <remarks>
        /// Synchronously loads an asset.
        /// </remarks>
        /// <param name="assetInfo">资源信息 / Asset information</param>
        /// <returns>资源操作句柄 / Asset handle</returns>
        [UnityEngine.Scripting.Preserve]
        public AssetHandle LoadAssetSync(AssetInfo assetInfo)
        {
            return _assetManager.LoadAssetSync(assetInfo);
        }

        /// <summary>
        /// 同步加载资源。
        /// </summary>
        /// <remarks>
        /// Synchronously loads an asset.
        /// </remarks>
        /// <typeparam name="T">资源类型 / Asset type</typeparam>
        /// <param name="path">资源路径 / Asset path</param>
        /// <returns>资源操作句柄 / Asset handle</returns>
        [UnityEngine.Scripting.Preserve]
        public AssetHandle LoadAssetSync<T>(string path) where T : UnityEngine.Object
        {
            return _assetManager.LoadAssetSync<T>(path);
        }

        #endregion

        #region 加载场景

        /// <summary>
        /// 异步加载场景。
        /// </summary>
        /// <remarks>
        /// Asynchronously loads a scene.
        /// </remarks>
        /// <param name="path">资源路径 / Asset path</param>
        /// <param name="sceneMode">场景加载模式 / Scene load mode</param>
        /// <param name="activateOnLoad">加载完成后是否自动激活 / Whether to activate automatically after loading</param>
        /// <returns>场景操作句柄的异步任务 / Async task of the scene handle</returns>
        [UnityEngine.Scripting.Preserve]
        public Task<SceneHandle> LoadSceneAsync(string path, UnityEngine.SceneManagement.LoadSceneMode sceneMode, bool activateOnLoad = true)
        {
            return _assetManager.LoadSceneAsync(path, sceneMode, activateOnLoad);
        }

        /// <summary>
        /// 异步加载场景。
        /// </summary>
        /// <remarks>
        /// Asynchronously loads a scene.
        /// </remarks>
        /// <param name="assetInfo">资源信息 / Asset information</param>
        /// <param name="sceneMode">场景加载模式 / Scene load mode</param>
        /// <param name="activateOnLoad">加载完成后是否自动激活 / Whether to activate automatically after loading</param>
        /// <returns>场景操作句柄的异步任务 / Async task of the scene handle</returns>
        [UnityEngine.Scripting.Preserve]
        public Task<SceneHandle> LoadSceneAsync(AssetInfo assetInfo, UnityEngine.SceneManagement.LoadSceneMode sceneMode, bool activateOnLoad = true)
        {
            return _assetManager.LoadSceneAsync(assetInfo, sceneMode, activateOnLoad);
        }

        #endregion

        #region 资源包

        /// <summary>
        /// 创建资源包。
        /// </summary>
        /// <remarks>
        /// Creates an asset package.
        /// </remarks>
        /// <param name="packageName">资源包名称 / Asset package name</param>
        /// <returns>创建的资源包实例 / Created resource package instance</returns>
        [UnityEngine.Scripting.Preserve]
        public ResourcePackage CreateAssetsPackage(string packageName)
        {
            return _assetManager.CreateAssetsPackage(packageName);
        }

        /// <summary>
        /// 尝试获取资源包。
        /// </summary>
        /// <remarks>
        /// Tries to get an asset package.
        /// </remarks>
        /// <param name="packageName">资源包名称 / Asset package name</param>
        /// <returns>资源包实例；如果不存在则返回 null / Resource package instance; null if not found</returns>
        [UnityEngine.Scripting.Preserve]
        public ResourcePackage TryGetAssetsPackage(string packageName)
        {
            return _assetManager.TryGetAssetsPackage(packageName);
        }

        /// <summary>
        /// 检查资源包是否存在。
        /// </summary>
        /// <remarks>
        /// Checks whether an asset package exists.
        /// </remarks>
        /// <param name="packageName">资源包名称 / Asset package name</param>
        /// <returns>如果存在则返回 <c>true</c>；否则返回 <c>false</c> / <c>true</c> if the package exists; otherwise <c>false</c></returns>
        [UnityEngine.Scripting.Preserve]
        public bool HasAssetsPackage(string packageName)
        {
            return _assetManager.HasAssetsPackage(packageName);
        }

        /// <summary>
        /// 获取资源包。
        /// </summary>
        /// <remarks>
        /// Gets an asset package.
        /// </remarks>
        /// <param name="packageName">资源包名称 / Asset package name</param>
        /// <returns>资源包实例 / Resource package instance</returns>
        [UnityEngine.Scripting.Preserve]
        public ResourcePackage GetAssetsPackage(string packageName)
        {
            return _assetManager.GetAssetsPackage(packageName);
        }

        #endregion

        /// <summary>
        /// 检查指定资源是否需要下载。
        /// </summary>
        /// <remarks>
        /// Checks whether the specified asset needs to be downloaded.
        /// </remarks>
        /// <param name="assetInfo">资源信息 / Asset information</param>
        /// <returns>如果需要下载则返回 <c>true</c>；否则返回 <c>false</c> / <c>true</c> if download is needed; otherwise <c>false</c></returns>
        [UnityEngine.Scripting.Preserve]
        public bool IsNeedDownload(AssetInfo assetInfo)
        {
            return _assetManager.IsNeedDownload(assetInfo);
        }

        /// <summary>
        /// 检查指定资源是否需要下载。
        /// </summary>
        /// <remarks>
        /// Checks whether the specified asset needs to be downloaded.
        /// </remarks>
        /// <param name="path">资源地址 / Asset path</param>
        /// <returns>如果需要下载则返回 <c>true</c>；否则返回 <c>false</c> / <c>true</c> if download is needed; otherwise <c>false</c></returns>
        [UnityEngine.Scripting.Preserve]
        public bool IsNeedDownload(string path)
        {
            return _assetManager.IsNeedDownload(path);
        }

        /// <summary>
        /// 根据资源标签列表获取资源信息数组。
        /// </summary>
        /// <remarks>
        /// Gets asset information array by asset tag list.
        /// </remarks>
        /// <param name="assetTags">资源标签列表 / Asset tag list</param>
        /// <returns>匹配的资源信息数组 / Array of matching asset information</returns>
        [UnityEngine.Scripting.Preserve]
        public AssetInfo[] GetAssetInfos(string[] assetTags)
        {
            return _assetManager.GetAssetInfos(assetTags);
        }

        /// <summary>
        /// 根据资源标签获取资源信息数组。
        /// </summary>
        /// <remarks>
        /// Gets asset information array by asset tag.
        /// </remarks>
        /// <param name="assetTag">资源标签 / Asset tag</param>
        /// <returns>匹配的资源信息数组 / Array of matching asset information</returns>
        [UnityEngine.Scripting.Preserve]
        public AssetInfo[] GetAssetInfos(string assetTag)
        {
            return _assetManager.GetAssetInfos(assetTag);
        }

        /// <summary>
        /// 根据资源路径获取资源信息。
        /// </summary>
        /// <remarks>
        /// Gets asset information by asset path.
        /// </remarks>
        /// <param name="path">资源路径 / Asset path</param>
        /// <returns>资源信息 / Asset information</returns>
        [UnityEngine.Scripting.Preserve]
        public AssetInfo GetAssetInfo(string path)
        {
            return _assetManager.GetAssetInfo(path);
        }

        /// <summary>
        /// 检查指定的资源路径是否存在。
        /// </summary>
        /// <remarks>
        /// Checks whether the specified asset path exists.
        /// </remarks>
        /// <param name="assetPath">要检查的资源路径 / Asset path to check</param>
        /// <returns>如果存在则返回 <c>true</c>；否则返回 <c>false</c> / <c>true</c> if the asset path exists; otherwise <c>false</c></returns>
        [UnityEngine.Scripting.Preserve]
        public bool HasAssetPath(string assetPath)
        {
            return _assetManager.HasAssetPath(assetPath);
        }

        /// <summary>
        /// 设置默认资源包。
        /// </summary>
        /// <remarks>
        /// Sets the default asset package.
        /// </remarks>
        /// <param name="assetsPackage">资源包实例 / Resource package instance</param>
        [UnityEngine.Scripting.Preserve]
        public void SetDefaultAssetsPackage(ResourcePackage assetsPackage)
        {
            _assetManager.SetDefaultAssetsPackage(assetsPackage);
        }

        /// <summary>
        /// 强制回收指定资源包内的所有资源。
        /// </summary>
        /// <remarks>
        /// Forces unloading of all assets in the specified package.
        /// </remarks>
        /// <param name="packageName">资源包名称 / Asset package name</param>
        [UnityEngine.Scripting.Preserve]
        public void UnloadAllAssetsAsync(string packageName)
        {
            _assetManager.UnloadAllAssetsAsync(packageName);
        }

        /// <summary>
        /// 卸载指定资源包内的资源。
        /// </summary>
        /// <remarks>
        /// Unloads an asset in the specified package.
        /// </remarks>
        /// <param name="packageName">资源包名称 / Asset package name</param>
        /// <param name="assetPath">资源路径 / Asset path</param>
        [UnityEngine.Scripting.Preserve]
        public void UnloadAsset(string packageName, string assetPath)
        {
            _assetManager.UnloadAsset(packageName, assetPath);
        }

        /// <summary>
        /// 卸载资源。
        /// </summary>
        /// <remarks>
        /// Unloads an asset.
        /// </remarks>
        /// <param name="assetPath">资源路径 / Asset path</param>
        [UnityEngine.Scripting.Preserve]
        public void UnloadAsset(string assetPath)
        {
            _assetManager.UnloadAsset(assetPath);
        }

        /// <summary>
        /// 卸载资源句柄。
        /// </summary>
        /// <remarks>
        /// Unloads an asset handle.
        /// </remarks>
        /// <param name="assetHandle">资源句柄 / Asset handle</param>
        [UnityEngine.Scripting.Preserve]
        public void UnloadAssetHandle(object assetHandle)
        {
            if (assetHandle is AssetHandle handle)
            {
                handle.Release();
            }
        }

        /// <summary>
        /// 卸载无用资源。
        /// </summary>
        /// <remarks>
        /// Unloads unused assets.
        /// </remarks>
        /// <param name="packageName">资源包名称，为 null 时卸载默认资源包 / Asset package name; unloads the default package when null</param>
        [UnityEngine.Scripting.Preserve]
        public void UnloadUnusedAssetsAsync(string packageName = null)
        {
            _assetManager.UnloadUnusedAssetsAsync(packageName);
        }

        /// <summary>
        /// 清理所有资源包文件。
        /// </summary>
        /// <remarks>
        /// Clears all bundle files.
        /// </remarks>
        /// <param name="packageName">资源包名称，为 null 时清理默认资源包 / Asset package name; clears the default package when null</param>
        [UnityEngine.Scripting.Preserve]
        public void ClearAllBundleFilesAsync(string packageName = null)
        {
            _assetManager.ClearAllBundleFilesAsync(packageName);
        }

        /// <summary>
        /// 清理无用资源包文件。
        /// </summary>
        /// <remarks>
        /// Clears unused bundle files.
        /// </remarks>
        /// <param name="packageName">资源包名称，为 null 时清理默认资源包 / Asset package name; clears the default package when null</param>
        [UnityEngine.Scripting.Preserve]
        public void ClearUnusedBundleFilesAsync(string packageName = null)
        {
            _assetManager.ClearUnusedBundleFilesAsync(packageName);
        }
    }
#if UNITY_EDITOR
    /// <summary>
    /// 资源包配置信息（仅编辑器使用）。
    /// </summary>
    /// <remarks>
    /// Asset resource package configuration info (editor only).
    /// </remarks>
    [Serializable]
    public sealed class AssetResourcePackageInfo
    {
        /// <summary>
        /// 资源包名称。
        /// </summary>
        /// <remarks>
        /// Asset package name.
        /// </remarks>
        [SerializeField] public string PackageName;

        /// <summary>
        /// 主下载地址。
        /// </summary>
        /// <remarks>
        /// Primary download URL.
        /// </remarks>
        [SerializeField] public string DownloadURL;

        /// <summary>
        /// 备用下载地址。
        /// </summary>
        /// <remarks>
        /// Fallback download URL.
        /// </remarks>
        [SerializeField] public string FallbackDownloadURL;
    }
#endif
}
