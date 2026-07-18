# GTA 3D - Gameplay Systems

## Player System
- **Movement**: WASD for forward/backward and strafe
- **Camera**: Mouse look with smooth rotation
- **Jump**: Spacebar to jump with gravity
- **Interaction**: E key to interact with objects/NPCs

## Combat System
- **Health**: 100 HP default
- **Weapons**: Pistol, Rifle, Shotgun with different damage values
- **Ammo**: Limited ammunition per magazine
- **Reload**: R key to reload current weapon
- **Damage**: Raycast-based hit detection
- **Death**: Respawns when health reaches 0

## NPC System
- **Wandering AI**: NPCs patrol with random waypoints
- **States**: Idle, Walking, Talking, Fleeing
- **Animation**: Play walking/idle animations
- **Interaction**: Can talk to NPCs (WIP)
- **Combat**: NPCs flee when shot

## Vehicle System
- **Driving**: WASD to drive, left/right to steer
- **Speed**: Acceleration/braking mechanics
- **Physics**: RigidBody3D with smooth turning
- **Enter/Exit**: E to enter/exit vehicles

## UI/HUD
- **Health Display**: Current HP / Max HP
- **Ammo Counter**: Current ammo / Magazine size
- **Speed Meter**: Current movement speed
- **FPS Counter**: Frame rate display
- **Crosshair**: Center screen aiming reticle
- **Pause Menu**: ESC to pause game

## Controls Summary

| Key | Action |
|-----|--------|
| W/A/S/D | Move |
| Mouse | Look around |
| Space | Jump |
| E | Interact/Enter vehicle |
| Left Mouse | Fire weapon |
| R | Reload |
| 1/2/3 | Switch weapons |
| ESC | Pause |

## Future Features
- Vehicle upgrades
- More sophisticated NPC AI
- Dialogue system
- Missions/Quests
- More weapon types
- Character customization
- Save/Load system
