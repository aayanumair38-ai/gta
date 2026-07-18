# GTA 3D - API Reference

## Player Classes

### Player
- `Velocity: Vector3` - Current velocity
- `Speed: float` - Movement speed
- `JumpForce: float` - Jump strength
- `Move()` - Handle player movement
- `Jump()` - Execute jump

### PlayerStats
- `GetHealth(): float` - Current health
- `GetArmor(): float` - Current armor
- `GetLevel(): int` - Player level
- `TakeDamage(damage)` - Reduce health
- `Heal(amount)` - Restore health
- `AddExperience(amount)` - Gain XP

### PlayerInteraction
- `Interact()` - Interact with nearby objects
- `GetNearbyObjects(): List` - Get interactable objects

## Combat Classes

### Combat
- `GetHealth(): float` - Current health
- `GetCurrentWeapon(): Weapon` - Active weapon
- `TakeDamage(damage)` - Take damage
- `Heal(amount)` - Restore health
- `Die()` - Handle death

### Weapon
- `Damage: float` - Damage per shot
- `FireRate: float` - Shots per second
- `Fire()` - Shoot weapon
- `Reload()` - Reload weapon
- `GetCurrentAmmo(): int` - Current ammunition

## NPC Classes

### NPC
- `CurrentState: NPCState` - AI state
- `TakeDamage(damage)` - Damage NPC
- `SetState(state)` - Change AI state

### NPCManager
- `GetNPCs(): List<NPC>` - All active NPCs
- `SpawnNPCs()` - Create NPC instances

## Vehicle Class

### Vehicle
- `Acceleration: float` - Speed up rate
- `MaxSpeed: float` - Top speed
- `PlayerEnter()` - Player enters vehicle
- `PlayerExit()` - Player leaves vehicle
- `IsPlayerInside(): bool` - Check occupancy

## Gameplay Systems

### GameManager (Singleton)
- `GetGameTime(): float` - Elapsed time
- `GetStats(): Dictionary` - Game statistics
- `AddKill()` - Increment kill counter
- `SetPaused(paused)` - Pause game

### QuestSystem
- `AddQuest(quest)` - Create new quest
- `ActivateQuest(id)` - Start quest
- `CompleteQuest(id)` - Mark quest done
- `GetActiveQuests(): List` - Active quest list

### InventorySystem
- `AddItem(item)` - Add to inventory
- `RemoveItem(id, qty)` - Remove from inventory
- `GetItem(id): InventoryItem` - Get item details
- `GetCurrentWeight(): float` - Inventory weight

### SaveSystem
- `SaveGame(name, data)` - Save game state
- `LoadGame(name): GameSave` - Load game
- `GetSaveFiles(): List` - List saves

## World Classes

### WeatherSystem
- `GetCurrentWeather(): Weather` - Active weather
- `ChangeWeather()` - Change weather state

### TerrainGenerator
- `GridSize: int` - Map dimensions
- `TileSize: float` - Tile size
- `GenerateTerrain()` - Create map

### EnemySpawner
- `MaxEnemies: int` - Max enemies
- `SpawnRadius: float` - Spawn distance
- `SpawnEnemy()` - Spawn enemy

### LevelManager
- `GetCurrentLevel(): int` - Current level
- `GetDifficultyMultiplier(): float` - Difficulty factor
- `AdvanceLevel()` - Go to next level

## UI Classes

### HUD
- `UpdateHealth(current, max)` - Update health display
- `UpdateAmmo(current, max)` - Update ammo display
- `UpdateSpeed(speed)` - Update speed display
- `ShowMessage(msg, duration)` - Show notification

### DialogueSystem
- `StartDialogue(node)` - Begin dialogue
- `EndDialogue()` - End dialogue
- `IsDialogueActive(): bool` - Check if in dialogue

### NotificationSystem
- `ShowNotification(msg, duration)` - Display notification

## Audio System

### AudioManager (Singleton)
- `PlaySFX(name, pitch)` - Play sound effect
- `PlayMusic(name, fade)` - Play music
- `SetMasterVolume(volume)` - Set volume (0-1)
- `GetMasterVolume(): float` - Get volume

## Utility Classes

### Constants
- `GAME_NAME: string` - Game title
- `VERSION: string` - Game version
- `DEFAULT_SPEED: float` - Base speed
- `DEFAULT_JUMP_FORCE: float` - Jump height

### PerformanceMonitor
- Displays FPS, memory usage, and performance stats

## Enums

### Weapon.WeaponType
- `Pistol`
- `Rifle`
- `Shotgun`
- `Melee`

### NPC.NPCState
- `Idle`
- `Walking`
- `Talking`
- `Fleeing`

### AIBehavior.AIState
- `Patrol`
- `Chase`
- `Attack`
- `Flee`
- `Idle`

### PickupItem.ItemType
- `Health`
- `Ammo`
- `Armor`
- `Money`

### LevelManager.GameDifficulty
- `Easy` (0.5x)
- `Normal` (1.0x)
- `Hard` (1.5x)
- `Nightmare` (2.0x)

## Common Usage Examples

### Get Player Health
```csharp
var playerStats = GetNode<PlayerStats>("Player/PlayerStats");
float health = playerStats.GetHealth();
```

### Play Sound
```csharp
AudioManager.Instance.PlaySFX("gunshot");
```

### Show Quest
```csharp
GameManager.Instance.ShowNotification("New Quest!");
```

### Save Game
```csharp
var saveData = new SaveSystem.GameSave();
GetNode<SaveSystem>("SaveSystem").SaveGame("save1", saveData);
```
