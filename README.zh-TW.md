<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Asset

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.asset)](https://github.com/GameFrameX/com.gameframex.unity.asset/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.asset)](https://github.com/GameFrameX/com.gameframex.unity.asset/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

獨立遊戲前後端一體化解決方案 · 獨立遊戲開發者的圓夢大使

<br />

[文檔](https://gameframex.doc.alianblank.com) · [快速開始](#快速開始) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | **繁體中文** | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## 項目簡介

Game Frame X Asset 是一個基於 GameFrameX 框架的 Unity 資源管理功能包，提供資源管理功能，使資源管理功能的使用更加簡單高效。

**Asset 資源元件 (Asset Component)** - 提供資源相關的介面。

## 快速開始

### 安裝

選擇以下任一方式：

1. 編輯 Unity 專案的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：
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

   `scopes` 控制哪些套件透過此註冊表解析。只有以 `com.gameframex` 開頭的套件才會從這個註冊表取得。

2. 直接在 `manifest.json` 的 `dependencies` 節點下添加以下內容：
   ```json
   {
      "com.gameframex.unity.asset": "https://github.com/gameframex/com.gameframex.unity.asset.git"
   }
   ```
3. 在 Unity 的 `Package Manager` 中使用 `Git URL` 的方式添加庫，地址為：`https://github.com/gameframex/com.gameframex.unity.asset.git`
4. 直接下載倉庫放置到 Unity 專案的 `Packages` 目錄下，會自動載入識別。
## 使用範例

```csharp
// 標準方式：透過 GameEntry（不依賴 com.gameframex.unity.entry）
var assetComponent = GameEntry.GetComponent<AssetComponent>();
assetComponent.LoadAsset("AssetPath");
```

## 依賴項

- `com.gameframex.unity`: GameFrameX 核心框架
- `com.gameframex.unity.tuyoogame.yooasset`（≥ 3.1.0）：YooAsset 資源管理主包

## 註冊即分發機制

自 4.1.0 起，Web 運行模式的初始化收斂為一行 `WebPlayModeFileSystemProviderRegistry.CreateFileSystemParameters(hostServerURL, fallbackHostServerURL)` 呼叫，平台相關的條件分支已全部清零。各小遊戲平台適配套件（微信、抖音等）會在場景載入前（早於首次 Web 初始化）自註冊到 YooAsset 主包的註冊表，註冊表按 Priority 最高者優先互斥解析，每次初始化只會選中一個平台。未安裝任何適配套件的 WebGL 專案回落預設 WebGL 檔案系統，行為保持不變。

## 升級說明

- 自 4.1.0 起，必須安裝 YooAsset 主包 `com.gameframex.unity.tuyoogame.yooasset` ≥ 3.1.0（此前僅靠宿主專案恰好直接安裝了該包才能運作）。

## 文檔與資源

- 文檔地址: https://gameframex.doc.alianblank.com
- 倉庫地址: https://github.com/gameframex/com.gameframex.unity.asset
- 問題回報: https://github.com/gameframex/com.gameframex.unity.asset/issues


## 依賴

| 套件 | 說明 |
|------|------|
| `com.gameframex.unity` | 1.1.1 |
| `com.gameframex.unity.tuyoogame.yooasset` | ≥ 3.1.0 |

## 社區與支援

- QQ群: 467608841 / 233840761

## 更新日誌

查看 [Releases](https://github.com/GameFrameX/gameframex/com.gameframex.unity.asset/releases) 了解更新日誌。
## 開源協議

本專案遵循 MIT 許可證。詳細資訊請查看 [LICENSE](LICENSE.md) 檔案。
