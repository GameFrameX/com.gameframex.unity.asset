<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Asset

[![GitHub release](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.asset?style=flat-square)](https://github.com/GameFrameX/com.gameframex.unity.asset/releases)
[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.asset?style=flat-square)](https://github.com/GameFrameX/com.gameframex.unity.asset/blob/main/LICENSE.md)
[![Documentation](https://img.shields.io/badge/Documentation-Online-blue?style=flat-square)](https://gameframex.doc.alianblank.com)

**All-in-One Solution for Indie Game Development · Empowering Indie Developers' Dreams**

[Documentation](https://gameframex.doc.alianblank.com) · [Quick Start](#quick-start) · [QQ Group](https://qm.qq.com/q/5s5e1e6e6e)

**Language**: **English** | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)


</div>

---

## Project Overview

Game Frame X Asset is a Unity resource management package based on the GameFrameX framework, providing resource management functionality for easier and more efficient asset handling.

**Asset Component** - Provides resource-related interfaces.

## Quick Start

### System Requirements

- Unity 2019.4 or higher
- GameFrameX framework 1.1.1 or higher

### Installation

Choose one of the following methods:

1. Add the following to the `dependencies` section in your project's `manifest.json`:
   ```json
   {"com.gameframex.unity.asset": "https://github.com/GameFrameX/com.gameframex.unity.asset.git"}
   ```

2. Use `Git URL` in Unity's Package Manager:
   ```
   https://github.com/GameFrameX/com.gameframex.unity.asset.git
   ```

3. Download the repository and place it in your Unity project's `Packages` directory. It will be loaded automatically.

## Usage Examples

```csharp
// Standard: via GameEntry (no dependency on com.gameframex.unity.entry)
var assetComponent = GameEntry.GetComponent<AssetComponent>();
assetComponent.LoadAsset("AssetPath");
```

## Dependencies

- `com.gameframex.unity`: GameFrameX core framework

## Documentation & Resources

- Documentation: https://gameframex.doc.alianblank.com
- Repository: https://github.com/gameframex/com.gameframex.unity.asset
- Issues: https://github.com/gameframex/com.gameframex.unity.asset/issues

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE.md) for details.
