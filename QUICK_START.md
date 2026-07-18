# GTA 3D - Quick Start Guide

## 🚀 Getting Started

### Prerequisites
- Godot 4.x (with C# support)
- .NET 8.0+
- Git (optional)

### Installation

1. **Clone or Download**
   ```bash
   git clone https://github.com/aayanumair38-ai/gta.git
   cd gta
   ```

2. **Open in Godot**
   - Launch Godot 4.x
   - Select "Open Project"
   - Navigate to the gta folder
   - Click "Select Current Folder"

3. **Wait for Compilation**
   - Godot will compile C# scripts
   - Watch the Output window for status
   - This may take 1-2 minutes

4. **Run the Game**
   - Press **F5** or click Play button
   - Game should launch in viewport
   - Use WASD to move, mouse to look

## 🎮 First Time Setup

### Configure Controls
1. Go to **Project → Project Settings → Input Map**
2. Verify these actions exist:
   - ui_up, ui_down, ui_left, ui_right (movement)
   - fire (left mouse button)
   - reload (R key)
   - pause (ESC key)

### Adjust Graphics
1. **Project → Project Settings → Rendering**
2. Adjust shadow quality for performance
3. Set texture compression if needed
4. Configure LOD distances

## 🎯 Basic Controls Reference

| Control | Action |
|---------|--------|
| **W** | Move forward |
| **A** | Strafe left |
| **S** | Move backward |
| **D** | Strafe right |
| **Mouse** | Look around |
| **Space** | Jump |
| **E** | Interact |
| **Left Click** | Fire weapon |
| **R** | Reload |
| **1/2/3** | Switch weapons |
| **ESC** | Pause game |

## 📝 Creating Your First Quest

```csharp
// Create a new quest
var myQuest = new QuestSystem.Quest
{
    Id = "tutorial_explore",
    Title = "Explore the Area",
    Description = "Discover all locations on the map",
    Objectives = new List<string>
    {
        "Visit the town center",
        "Find the hidden location",
        "Return to starting point"
    }
};

// Add and activate
QuestSystem.AddQuest(myQuest);
QuestSystem.ActivateQuest(myQuest.Id);
```

## 🎵 Playing Sound Effects

```csharp
// Play a simple sound
AudioManager.Instance.PlaySFX("explosion");

// Play with pitch variation
AudioManager.Instance.PlaySFX("footstep", 0.9f);
```

## 👥 Spawning an NPC

```csharp
// NPCs are automatically spawned by NPCManager
// Customize in Editor:
// 1. Select NPCManager node
// 2. Set NPCCount in Inspector
// 3. Set SpawnRadius in Inspector
```

## 🚗 Driving a Vehicle

```csharp
// Press E near a vehicle to enter
// Press E again to exit
// Controls while driving:
// W/A/S/D - Drive forward/left/backward/right
// Left/Right Arrow - Steer
```

## 💾 Saving and Loading

```csharp
// Create save data
var saveData = new SaveSystem.GameSave
{
    GameTime = GameManager.Instance.GetGameTime(),
    PlayerPosition = player.GlobalPosition,
    PlayerHealth = 100,
    Money = 500
};

// Save
GetNode<SaveSystem>("SaveSystem").SaveGame("mysave", saveData);

// Load
var loadedData = GetNode<SaveSystem>("SaveSystem").LoadGame("mysave");
```

## 🐛 Debugging Tips

### Enable Debug Mode
1. Open **project.godot**
2. Add: `debug/gdscript/warnings/return_value_discarded=warn`

### View Performance
1. Go to **Debug → Monitor** while playing
2. Watch FPS, memory, physics

### Check Errors
1. Open **View → Output** to see console
2. Look for red error messages
3. Check line numbers for quick fix

## 📚 Documentation Files

- **README.md** - Project overview
- **GAMEPLAY.md** - Feature documentation
- **DEVELOPMENT.md** - Architecture guide
- **API_REFERENCE.md** - API documentation
- **CONTRIBUTING.md** - Contribution guidelines
- **QUICK_START.md** - This file

## 🆘 Common Issues

### C# Won't Compile
- Check .NET 8.0 is installed
- Go to Project → Tools → C# → Build Solution
- Restart Godot if still failing

### Game Runs Slow
- Reduce NPC count in NPCManager
- Lower shadow quality
- Disable real-time shadows if needed

### Black Screen on Startup
- Check console for errors
- Verify Main.tscn exists
- Ensure all paths are correct

### Missing Assets
- Create assets/models, assets/audio, assets/textures directories
- Add your 3D models and textures
- Reference in scene files

## 🎓 Next Steps

1. **Explore Existing Code** - Review src/ structure
2. **Try the DEVELOPMENT.md** - Learn architecture
3. **Modify Scenes** - Open .tscn files in editor
4. **Add Your Content** - Create new quests/NPCs
5. **Build & Export** - Create executable

## 🤝 Need Help?

- Check DEVELOPMENT.md for architecture
- Review API_REFERENCE.md for classes
- See CONTRIBUTING.md for guidelines
- Check Godot documentation: docs.godotengine.org

## 🎉 You're Ready!

Your GTA 3D game is ready to play and customize. Have fun developing!
