using GameFrameX.Asset.Runtime;
using GameFrameX.Runtime;
using NUnit.Framework;
using YooAsset;

namespace GameFrameX.Asset.Tests
{
    internal sealed class AssetOptionsUnitTests
    {
        [TearDown]
        public void TearDown()
        {
            GameFrameXOptions.Reset(); // 清除 Options 注册，避免跨用例静态泄漏
        }

        [Test]
        public void AssetOptions_ConfigureAndTryGet_RoundTripsDiffs()
        {
            GameFrameXOptions.Reset();
            GameFrameXOptions.Configure(new AssetOptions { GamePlayMode = EPlayMode.HostPlayMode });

            Assert.IsTrue(GameFrameXOptions.TryGet<AssetOptions>(out var options));
            Assert.That(options.GamePlayMode, Is.EqualTo(EPlayMode.HostPlayMode));

            GameFrameXOptions.Reset();
            var defaults = new AssetOptions();
            Assert.That(defaults.GamePlayMode, Is.EqualTo(EPlayMode.EditorSimulateMode));
#if UNITY_EDITOR
            Assert.NotNull(defaults.AssetResourcePackages);
            Assert.That(defaults.AssetResourcePackages, Is.Empty);
#endif
        }
    }
}
