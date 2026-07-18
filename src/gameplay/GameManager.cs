using Godot;
using System;
using System.Collections.Generic;

namespace GTA.Gameplay;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; }
    
    [Export]
    public bool EnableDebugMode = false;
    
    private float _gameTime = 0;
    private bool _isGamePaused = false;
    private Dictionary<string, int> _gameStats = new();
    private Player.Player _player;
    private HUD _hud;

    public override void _Ready()
    {
        if (Instance != null && Instance != this)
        {
            QueueFree();
            return;
        }
        
        Instance = this;
        _player = GetNode<Player.Player>("/root/Main/Player");
        _hud = GetNode<HUD>("/root/Main/HUD");
        InitializeStats();
        GD.Print("GameManager initialized");
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!_isGamePaused)
        {
            _gameTime += (float)delta;
            UpdateGameState(delta);
        }
    }

    private void InitializeStats()
    {
        _gameStats["kills"] = 0;
        _gameStats["npcs_encountered"] = 0;
        _gameStats["vehicles_used"] = 0;
        _gameStats["health_restored"] = 0;
    }

    private void UpdateGameState(double delta)
    {
        // Update player HUD with game data
        if (_player != null && _hud != null)
        {
            _hud.UpdateSpeed(_player.Velocity.Length());
        }
    }

    public void AddKill()
    {
        _gameStats["kills"]++;
        GD.Print($"Kills: {_gameStats["kills"]}");
    }

    public void SetPaused(bool paused)
    {
        _isGamePaused = paused;
        GetTree().Paused = paused;
    }

    public float GetGameTime() => _gameTime;
    public Dictionary<string, int> GetStats() => _gameStats;
}
