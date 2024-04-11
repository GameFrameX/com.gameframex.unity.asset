//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFrameX.Editor;
using GameFrameX.Asset.Runtime;
using UnityEditor;

namespace GameFrameX.Asset.Editor
{
    [CustomEditor(typeof(AssetComponent))]
    internal sealed class AssetComponentInspector : ComponentTypeComponentInspector
    {
        protected override void RefreshTypeNames()
        {
            RefreshComponentTypeNames(typeof(IAssetManager));
        }
    }
}