using Godot;
using System;
using System.Collections.Generic;

namespace GTA.World;

public partial class SaveSystem : Node
{
    private static readonly string SAVE_PATH = "user://gta_saves/";

    public class GameSave
    {
        public float GameTime { get; set; }
        public Vector3 PlayerPosition { get; set; }
        public float PlayerHealth { get; set; }
        public int PlayerLevel { get; set; }
        public float Money { get; set; }
        public Dictionary<string, bool> CompletedQuests { get; set; }
    }

    public override void _Ready()
    {
        // Create save directory if it doesn't exist
        if (!DirAccess.Exists(SAVE_PATH))
        {
            DirAccess.MakeDirAbsoluteError(SAVE_PATH);
        }
    }

    public void SaveGame(string saveName, GameSave data)
    {
        var json = Json.Stringify(data);
        var file = FileAccess.Open($"{SAVE_PATH}{saveName}.save", FileAccess.ModeFlags.Write);
        file.StoreString(json);
        GD.Print($"Game saved: {saveName}");
    }

    public GameSave LoadGame(string saveName)
    {
        var filePath = $"{SAVE_PATH}{saveName}.save";
        if (!FileAccess.FileExists(filePath))
        {
            GD.PrintErr($"Save file not found: {saveName}");
            return null;
        }

        var file = FileAccess.Open(filePath, FileAccess.ModeFlags.Read);
        var json = file.GetAsText();
        GD.Print($"Game loaded: {saveName}");
        
        return null; // Parse JSON to GameSave object
    }

    public static List<string> GetSaveFiles()
    {
        var saves = new List<string>();
        if (!DirAccess.Exists(SAVE_PATH))
            return saves;

        var dir = DirAccess.Open(SAVE_PATH);
        if (dir != null)
        {
            dir.ListDirBegin();
            string fileName = dir.GetNextFile();
            while (fileName != "")
            {
                if (fileName.EndsWith(".save"))
                {
                    saves.Add(fileName.TrimSuffix(".save"));
                }
                fileName = dir.GetNextFile();
            }
        }
        
        return saves;
    }
}
