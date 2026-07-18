using Godot;
using System;
using System.Collections.Generic;

namespace GTA.World;

public partial class LevelManager : Node
{
    public enum GameDifficulty { Easy, Normal, Hard, Nightmare }
    
    [Export]
    public GameDifficulty CurrentDifficulty = GameDifficulty.Normal;

    private Dictionary<GameDifficulty, float> _difficultyMultipliers = new()
    {
        { GameDifficulty.Easy, 0.5f },
        { GameDifficulty.Normal, 1.0f },
        { GameDifficulty.Hard, 1.5f },
        { GameDifficulty.Nightmare, 2.0f }
    };

    private int _currentLevel = 1;
    private float _levelProgress = 0;

    public override void _Ready()
    {
        GD.Print($"Level {_currentLevel} - Difficulty: {CurrentDifficulty}");
    }

    public void AdvanceLevel()
    {
        _currentLevel++;
        _levelProgress = 0;
        GetTree().ReloadCurrentScene();
        GD.Print($"Advanced to Level {_currentLevel}");
    }

    public void SetDifficulty(GameDifficulty difficulty)
    {
        CurrentDifficulty = difficulty;
        GD.Print($"Difficulty set to: {difficulty}");
    }

    public float GetDifficultyMultiplier() => _difficultyMultipliers[CurrentDifficulty];
    public int GetCurrentLevel() => _currentLevel;
    public void UpdateProgress(float amount) => _levelProgress += amount;
}
