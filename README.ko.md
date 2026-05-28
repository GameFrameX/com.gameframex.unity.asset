<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Asset

[![GitHub release](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.asset?style=flat-square)](https://github.com/GameFrameX/com.gameframex.unity.asset/releases)
[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.asset?style=flat-square)](https://github.com/GameFrameX/com.gameframex.unity.asset/blob/main/LICENSE.md)
[![Documentation](https://img.shields.io/badge/Documentation-Online-blue?style=flat-square)](https://gameframex.doc.alianblank.com)

**인디 게임 개발자를 위한 올인원 솔루션 · 인디 개발자의 꿈을 실현**

[문서](https://gameframex.doc.alianblank.com) · [빠른 시작](#빠른-시작) · [QQ 그룹](https://qm.qq.com/q/5s5e1e6e6e)

**언어**: [English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | **한국어**


</div>

---

## 프로젝트 개요

Game Frame X Asset은 GameFrameX 프레임워크 기반의 Unity 리소스 관리 패키지로, 리소스 관리 기능을 더 간단하고 효율적으로 사용할 수 있게 합니다.

**Asset 컴포넌트 (Asset Component)** - 리소스 관련 인터페이스를 제공합니다.

## 빠른 시작

### 시스템 요구 사항

- Unity 2019.4 이상
- GameFrameX 프레임워크 1.1.1 이상

### 설치

다음 방법 중 하나를 선택하세요:

1. 프로젝트의 `manifest.json` 파일의 `dependencies` 섹션에 다음 내용을 추가:
   ```json
   {"com.gameframex.unity.asset": "https://github.com/AlianBlank/com.gameframex.unity.asset.git"}
   ```

2. Unity의 Package Manager에서 `Git URL`을 사용하여 추가:
   ```
   https://github.com/AlianBlank/com.gameframex.unity.asset.git
   ```

3. 저장소를 다운로드하여 Unity 프로젝트의 `Packages` 디렉토리에 배치하면 자동으로 로드됩니다.

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

## 라이선스

이 프로젝트는 MIT 라이선스에 따라 배포됩니다. 자세한 내용은 [LICENSE](LICENSE.md)를 참조하세요.
