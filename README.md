# GTA - 3D Open World Game

A 3D open-world exploration game built with Godot Engine 4.x.

## Features
- 3D first-person/third-person camera
- Player movement and interaction
- NPC system
- Vehicle mechanics (in progress)
- World exploration
- Combat system (in progress)

## Requirements
- Godot Engine 4.x
- .NET 8.0+ (for C# support)

## Project Structure

```
gta/
├── src/
│   ├── player/          # Player controller, movement, camera
│   ├── npc/             # NPC behaviors and interactions
│   ├── vehicles/        # Vehicle system
│   ├── world/           # World generation, terrain
│   ├── ui/              # HUD, menus, UI systems
│   ├── gameplay/        # Game mechanics, weapons, combat
│   └── utils/           # Utilities, helpers, constants
├── scenes/              # Godot scene files (.tscn)
├── assets/
│   ��── models/          # 3D models (.glb, .fbx)
│   ├── textures/        # Textures and materials
│   ├── audio/           # Music and sound effects
│   └── shaders/         # Custom shaders
├── project.godot        # Godot project configuration
└── Main.csproj          # C# project file
```

## Quick Start

1. **Install Godot 4.x** with C# support
2. **Open the project**: File → Open Project → select this folder
3. **Run the game**: F5 or Play button
4. **Build executable**: Project → Export Project

## Controls

- **W/A/S/D** - Move
- **Mouse** - Look around
- **Space** - Jump
- **E** - Interact
- **ESC** - Menu

## Development

### Building
```bash
godot --export-debug Linux bin/game.x86_64
```

### Running
```bash
./bin/game.x86_64
```

## License
MIT