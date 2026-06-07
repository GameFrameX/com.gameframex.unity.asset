<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Asset

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.asset)](https://github.com/GameFrameX/com.gameframex.unity.asset/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.asset)](https://github.com/GameFrameX/com.gameframex.unity.asset/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

인디 게임 개발자를 위한 올인원 솔루션 · 인디 개발자의 꿈을 실현

<br />

[문서](https://gameframex.doc.alianblank.com) · [빠른 시작](#빠른-시작) · QQ 그룹: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | **한국어**

</div>

## 프로젝트 개요

Game Frame X Asset은 GameFrameX 프레임워크 기반의 Unity 리소스 관리 패키지로, 리소스 관리 기능을 더 간단하고 효율적으로 사용할 수 있게 합니다.

**Asset 컴포넌트 (Asset Component)** - 리소스 관련 인터페이스를 제공합니다.

## 빠른 시작

### 설치

다음 방법 중 하나를 선택하세요:

1. Unity 프로젝트의 `Packages/manifest.json`을 편집하여 `scopedRegistries` 섹션을 추가하세요:
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

   `scopes`는 이 레지스트리를 통해 어떤 패키지를 해석할지 제어합니다. `com.gameframex`로 시작하는 패키지만 이 레지스트리에서 가져옵니다.

2. `manifest.json`의 `dependencies`에 직접 추가:
   ```json
   {
      "com.gameframex.unity.asset": "https://github.com/gameframex/com.gameframex.unity.asset.git"
   }
   ```
3. Unity의 **Package Manager**에서 **Git URL**을 사용하여 추가: `https://github.com/gameframex/com.gameframex.unity.asset.git`
4. 리포지토리를 Unity 프로젝트의 `Packages` 디렉토리에 클론하세요. 자동으로 로드됩니다.
## 사용 예시

```csharp
// 표준: GameEntry를 통해 (com.gameframex.unity.entry 비의존)
var assetComponent = GameEntry.GetComponent<AssetComponent>();
assetComponent.LoadAsset("AssetPath");
```

## 의존성

- `com.gameframex.unity`: GameFrameX 코어 프레임워크

## 문서 및 자료

- 문서: https://gameframex.doc.alianblank.com
- 저장소: https://github.com/gameframex/com.gameframex.unity.asset
- 이슈: https://github.com/gameframex/com.gameframex.unity.asset/issues


## 커뮤니티 및 지원

- QQ 그룹: 467608841 / 233840761

## 변경 로그

[Releases](https://github.com/GameFrameX/gameframex/com.gameframex.unity.asset/releases)에서 변경 로그를 확인하세요.
## 라이선스

자세한 내용은 [LICENSE.md](LICENSE.md) 파일을 참조하세요.
