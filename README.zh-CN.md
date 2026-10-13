<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Asset

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.asset)](https://github.com/GameFrameX/com.gameframex.unity.asset/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.asset)](https://github.com/GameFrameX/com.gameframex.unity.asset/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

独立游戏前后端一体化解决方案 · 独立游戏开发者的圆梦大使

<br />

[文档](https://gameframex.doc.alianblank.com) · [快速开始](#快速开始) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | **简体中文** | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## 项目简介

Game Frame X Asset 是一个基于 GameFrameX 框架的 Unity 资源管理功能包，提供资源管理功能，使资源管理功能的使用更加简单高效。

**Asset 资源组件 (Asset Component)** - 提供资源相关的接口。

## 快速开始

### 安装

选择以下任一方式：

1. 编辑 Unity 项目的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：
   ```json
   {
     "scopedRegistries": [
       {
         "name": "GameFrameX",
         "url": "https://gameframex.upm.alianblank.uk",
         "scopes": [
           "com.gameframex"
         ]
       }
     ],
     "dependencies": {
      "com.gameframex.unity.asset": "4.1.0"
     }
   }
   ```

   `scopes` 控制哪些包通过此注册表解析。只有以 `com.gameframex` 开头的包才会从这个注册表获取。

2. 直接在 `manifest.json` 的 `dependencies` 节点下添加以下内容：
   ```json
   {
      "com.gameframex.unity.asset": "https://github.com/gameframex/com.gameframex.unity.asset.git"
   }
   ```
3. 在 Unity 的 `Package Manager` 中使用 `Git URL` 的方式添加库，地址为：`https://github.com/gameframex/com.gameframex.unity.asset.git`
4. 直接下载仓库放置到 Unity 项目的 `Packages` 目录下，会自动加载识别。
## 使用示例

```csharp
// 标准方式：通过 GameEntry（不依赖 com.gameframex.unity.entry）
var assetComponent = GameEntry.GetComponent<AssetComponent>();
assetComponent.LoadAsset("AssetPath");
```

## 依赖

- `com.gameframex.unity`: GameFrameX 核心框架
- `com.gameframex.unity.tuyoogame.yooasset`（≥ 3.1.0）：YooAsset 资源管理主包

## 注册即分发机制

自 4.1.0 起，Web 运行模式的初始化收敛为一行 `WebPlayModeFileSystemProviderRegistry.CreateFileSystemParameters(hostServerURL, fallbackHostServerURL)` 调用，平台相关的条件分支已全部清零。各小游戏平台适配包（微信、抖音等）会在场景加载前（早于首次 Web 初始化）自注册到 YooAsset 主包的注册表，注册表按 Priority 最高者优先互斥解析，每次初始化只会选中一个平台。未安装任何适配包的 WebGL 工程回落默认 WebGL 文件系统，行为保持不变。

## 升级说明

- 自 4.1.0 起，必须安装 YooAsset 主包 `com.gameframex.unity.tuyoogame.yooasset` ≥ 3.1.0（此前仅靠宿主工程恰好直接安装了该包才能工作）。

- 文档地址: https://gameframex.doc.alianblank.com
- 仓库地址: https://github.com/gameframex/com.gameframex.unity.asset
- 问题反馈: https://github.com/gameframex/com.gameframex.unity.asset/issues


## 社区与支持

- QQ群: 467608841 / 233840761

## 更新日志

查看 [Releases](https://github.com/GameFrameX/gameframex/com.gameframex.unity.asset/releases) 了解更新日志。
## 开源协议

本项目遵循 MIT 许可证。详细信息请查看 [LICENSE](LICENSE.md) 文件。
