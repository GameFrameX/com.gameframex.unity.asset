//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFrameX.Editor;
using GameFrameX.Asset.Runtime;
using UnityEditor;
using UnityEngine;

namespace GameFrameX.Asset.Editor
{
    [CustomEditor(typeof(AssetComponent))]
    internal sealed class AssetComponentInspector : ComponentTypeComponentInspector
    {
        private SerializedProperty m_GamePlayMode;

        private GUIContent m_GamePlayModeGUIContent = new GUIContent("资源运行模式");
        private GUIContent m_AssetResourcePackagesGUIContent = new GUIContent("包列表");
        private SerializedProperty m_AssetResourcePackages;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                EditorGUILayout.PropertyField(m_GamePlayMode, m_GamePlayModeGUIContent);
                GUI.enabled = false;
                EditorGUILayout.PropertyField(m_AssetResourcePackages, m_AssetResourcePackagesGUIContent);
                GUI.enabled = true;
            }
            EditorGUI.EndDisabledGroup();

            serializedObject.ApplyModifiedProperties();

            Repaint();
        }

        protected override void RefreshTypeNames()
        {
            RefreshComponentTypeNames(typeof(IAssetManager));
        }

        protected override void Enable()
        {
            m_GamePlayMode = serializedObject.FindProperty("m_GamePlayMode");
            m_AssetResourcePackages = serializedObject.FindProperty("m_assetResourcePackages");
            // m_fallbackHostServer = serializedObject.FindProperty("m_fallbackHostServer");
        }
    }
}