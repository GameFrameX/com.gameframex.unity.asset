<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Asset

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.asset)](https://github.com/GameFrameX/com.gameframex.unity.asset/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.asset)](https://github.com/GameFrameX/com.gameframex.unity.asset/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

All-in-One Solution for Indie Game Development · Empowering Indie Developers' Dreams

<br />

[Documentation](https://gameframex.doc.alianblank.com) · [Quick Start](#quick-start) · QQ Group: 467608841 / 233840761

<br />

**English** | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## Project Overview

Game Frame X Asset is a Unity resource management package based on the GameFrameX framework, providing resource management functionality for easier and more efficient asset handling.

**Asset Component** - Provides resource-related interfaces.

## Quick Start

### Installation

Choose one of the following methods:

1. Edit your Unity project's `Packages/manifest.json` and add the `scopedRegistries` section:
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
       "com.gameframex.unity.asset": "3.0.1"
     }
   }
   ```

   `scopes` controls which packages are resolved through this registry. Only packages whose names start with `com.gameframex` will be fetched from it.

2. Add to `manifest.json` dependencies:
   ```json
   {
      "com.gameframex.unity.asset": "https://github.com/gameframex/com.gameframex.unity.asset.git"
   }
   ```
3. Use **Package Manager** in Unity with **Git URL**: `https://github.com/gameframex/com.gameframex.unity.asset.git`
4. Clone the repository into your Unity project's `Packages` directory. It will be loaded automatically.
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


## Community & Support

- QQ Group: 467608841 / 233840761

## Changelog

See [Releases](https://github.com/GameFrameX/gameframex/com.gameframex.unity.asset/releases) for changelog.
## License

See [LICENSE.md](LICENSE.md) for license information.
