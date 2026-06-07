<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Asset

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.asset)](https://github.com/GameFrameX/com.gameframex.unity.asset/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.asset)](https://github.com/GameFrameX/com.gameframex.unity.asset/releases)
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

### 系统要求

- Unity 2019.4 或更高版本
- GameFrameX 框架 1.1.1 或更高版本

### 安装

任选以下方式之一：

1. 直接在 `manifest.json` 的文件中的 `dependencies` 节点下添加以下内容：
   ```json
   {"com.gameframex.unity.asset": "https://github.com/GameFrameX/com.gameframex.unity.asset.git"}
   ```

2. 在 Unity 的 `Packages Manager` 中使用 `Git URL` 的方式添加库，地址为：
   ```
   https://github.com/GameFrameX/com.gameframex.unity.asset.git
   ```

3. 直接下载仓库放置到 Unity 项目的 `Packages` 目录下，会自动加载识别。

## 使用示例

```csharp
// 标准方式：通过 GameEntry（不依赖 com.gameframex.unity.entry）
var assetComponent = GameEntry.GetComponent<AssetComponent>();
assetComponent.LoadAsset("AssetPath");
```

## 依赖项

- `com.gameframex.unity`: GameFrameX 核心框架

## 文档与资源

- 文档地址: https://gameframex.doc.alianblank.com
- 仓库地址: https://github.com/gameframex/com.gameframex.unity.asset
- 问题反馈: https://github.com/gameframex/com.gameframex.unity.asset/issues

## 开源协议

本项目遵循 MIT 许可证。详细信息请查看 [LICENSE](LICENSE.md) 文件。
