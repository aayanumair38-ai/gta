using Godot;
using System;
using System.Collections.Generic;

namespace GTA.World;

public partial class EnemySpawner : Node3D
{
    [Export]
    public int MaxEnemies = 5;
    
    [Export]
    public float SpawnRadius = 50.0f;
    
    [Export]
    public float SpawnInterval = 10.0f;

    private List<NPC.NPC> _activeEnemies = new();
    private float _spawnTimer = 0;
    private PackedScene _enemyScene;

    public override void _Ready()
    {
        _enemyScene = GD.Load<PackedScene>("res://scenes/NPC.tscn");
    }

    public override void _PhysicsProcess(double delta)
    {
        _spawnTimer -= (float)delta;

        if (_spawnTimer <= 0 && _activeEnemies.Count < MaxEnemies)
        {
            SpawnEnemy();
            _spawnTimer = SpawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        if (_enemyScene == null) return;

        var enemy = _enemyScene.Instantiate<NPC.NPC>();
        var random = new RandomNumberGenerator();
        
        float angle = random.Randf() * Mathf.Tau;
        float distance = random.Randf() * SpawnRadius;
        
        enemy.GlobalPosition = GlobalPosition + new Vector3(
            Mathf.Cos(angle) * distance,
            0,
            Mathf.Sin(angle) * distance
        );
        
        AddChild(enemy);
        _activeEnemies.Add(enemy);
        GD.Print($"Enemy spawned. Total: {_activeEnemies.Count}");
    }
}
