using UnityEngine;

namespace GameFrameX.Asset.Runtime
{
    [UnityEngine.Scripting.Preserve]
    public class GameFrameXAssetCroppingHelper : MonoBehaviour
    {
        [UnityEngine.Scripting.Preserve]
        private void Start()
        {
            _ = typeof(AssetManager);
            _ = typeof(Constant);
            _ = typeof(IAssetManager);
            _ = typeof(AssetComponent);
        }
    }
}