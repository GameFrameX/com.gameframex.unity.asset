<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Asset

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.asset)](https://github.com/GameFrameX/com.gameframex.unity.asset/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.asset)](https://github.com/GameFrameX/com.gameframex.unity.asset/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

インディゲーム開発者向けオールインワンソリューション · インディ開発者の夢を支援

<br />

[ドキュメント](https://gameframex.doc.alianblank.com) · [クイックスタート](#クイックスタート) · QQグループ: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | **日本語** | [한국어](README.ko.md)

</div>

## プロジェクト概要

Game Frame X Asset は、GameFrameX フレームワークに基づく Unity リソース管理パッケージで、リソース管理機能をより簡単かつ効率的に使用できるようにします。

**Asset コンポーネント (Asset Component)** - リソース関連のインターフェースを提供します。

## クイックスタート

### 動作環境

- Unity 2019.4 以上
- GameFrameX フレームワーク 1.1.1 以上

### インストール

以下のいずれかの方法を選択してください：

1. プロジェクトの `manifest.json` の `dependencies` セクションに以下を追加：
   ```json
   {"com.gameframex.unity.asset": "https://github.com/GameFrameX/com.gameframex.unity.asset.git"}
   ```

2. Unity の Package Manager で `Git URL` を使用：
   ```
   https://github.com/GameFrameX/com.gameframex.unity.asset.git
   ```

3. リポジトリをダウンロードして Unity プロジェクトの `Packages` ディレクトリに配置。自動的にロードされます。

## 使用例

```csharp
// 標準: GameEntry 経由（com.gameframex.unity.entry 非依存）
var assetComponent = GameEntry.GetComponent<AssetComponent>();
assetComponent.LoadAsset("AssetPath");
```

## 依存関係

- `com.gameframex.unity`: GameFrameX コアフレームワーク

## ドキュメントとリソース

- ドキュメント: https://gameframex.doc.alianblank.com
- リポジトリ: https://github.com/gameframex/com.gameframex.unity.asset
- イシュー: https://github.com/gameframex/com.gameframex.unity.asset/issues

## ライセンス

このプロジェクトは MIT ライセンスの下で公開されています。詳細は [LICENSE](LICENSE.md) をご覧ください。
