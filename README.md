# TowerDefend

## Overview

TowerDefend is a 2D tower-defense prototype built with Unity. Players place turrets on nodes to stop enemy waves that follow waypoints. The project uses a modular architecture with managers, object pooling, ScriptableObject configs, and a small event system to drive UI and gameplay.

## Key Features

- Wave-based enemy spawning (configurable via `LevelConfig`).
- Turret types with levels, upgrades and sell mechanics.
- Health for base (base HP) that triggers lose state.
- Win screen when all waves complete.
- Object pooling for enemies, bullets, effects.
- Sound manager (SFX + music) with enum-based IDs.
- Simple tutorial hooks and analytics events.

## Tech / Dependencies

- Unity (2019.4+ recommended, confirm project settings)
- DOTween (for UI/tweening)
- TextMeshPro
- Project uses a custom `PoolManager`, `SingletonMono<T>`, and ScriptableObjects for configs.

## Getting Started

1. Open the project in Unity.
2. Ensure required packages (DOTween, TextMeshPro) are imported.
3. In the scene, ensure the following manager GameObjects exist and are active:
   - `UIManager` (handles UIs)
   - `LevelManager` (level flow)
   - `EnemySpawner` (spawn logic)
   - `EnemyManager` (tracking active enemies)
   - `EconomyManager` (currency)
   - `SoundManager` (attach the `SoundManager` script and assign clips)
   - `PoolManager` (object pooling registry)

4. Assign AudioClips to `SoundManager.sounds` (use `SoundId` enum keys). Common IDs:
   - `Music`, `Click`, `StartWave`, `Fire`, `Hit`, `Explosion`, `Build`, `Sell`, `Win`, `Lose`, `SkillCast`, `SkillImpact`, `BaseHit`.

5. Configure your `LevelConfig` ScriptableObjects (waves, base HP, CostBase, WayPoint references).

6. Open the main level scene and press Play. Use the turret UI to place turrets and the Wave button to spawn enemies.

## Project Structure (important folders)

- `Assets/_GAME/01_Scripts/Manager` — core managers: `LevelManager`, `EnemyManager`, `EconomyManager`, `SoundManager`, etc.
- `Assets/_GAME/01_Scripts/Control` — gameplay controls: turrets, enemies, bullets, skills.
- `Assets/_GAME/01_Scripts/UI` — UI canvases and UI elements.
- `Assets/_GAME/01_Scripts/Data` — ScriptableObject configs (LevelConfig, TurretConfig, EnemyConfig).

## Important Implementation Notes

- Level flow: `LevelManager.StartLevel()` initializes `EnemySpawner` then opens `CanvasGamePlay`. The `CanvasGamePlay` listens to events for gold and base HP.
- Base HP: configured via `LevelConfig.BaseHp`. `EnemyBase.ReachEndPoint()` calls `LevelManager.DamageBase(1)` (damage amount configurable).
- Money flow: `GameEventManager.UseMoneyUpdated` and `AddMoneyUpdated` are broadcast; `EconomyManager` updates the canonical `CurrentEconomy` value.
- Turrets: created by `TurretCard` and managed by `NodeBase`. Selling a turret calls `TurretBase.SellTurretBase()` and refunds via events.
- Object pool: bullets, enemies and effects are spawned via `PoolManager.Instance.Spawn(poolName, data)` and released with `Despawn/ReleaseToPool`.

## Common Tasks / Where to Change

- Add new turret: create `TurretConfig` ScriptableObject and a prefab deriving from `TurretBase`.
- Add new enemy: create `EnemyConfig`, register prefab in `PoolManager`, add to `WaveEnemyConfig`.
- Change base damage per enemy: modify call in `EnemyBase.ReachEndPoint()` or add configurable field to `EnemyConfig`/`LevelConfig`.
- Add/replace sounds: assign clips in `SoundManager`, use `SoundManager.Instance.Play(SoundManager.SoundId.<ID>)`.

## Debugging Tips

- If bullets spawn but are invisible: check pool prefab has a SpriteRenderer, scale != 0 and sorting layer visible.
- If gold display is incorrect: `CanvasGamePlay` reads `EconomyManager.Instance.CurrentEconomy` on open and listens to money events.
- If turrets don't reset on restart: `LevelManager.ResetGameState()` is responsible for clearing turrets/enemies; ensure `StartLevel()` is called for restart.

## Contributing

- Follow existing patterns: singleton managers, event-driven updates via `GameEventManager`, ScriptableObject configs.
- Keep changes isolated: add new systems under `Assets/_GAME/01_Scripts` and update registration in `PoolManager` where required.

## License

Specify your license here (e.g. MIT) or add a `LICENSE` file at repository root.
