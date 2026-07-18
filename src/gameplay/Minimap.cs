using Godot;
using System;
using System.Collections.Generic;

namespace GTA.Gameplay;

public partial class Minimap : Control
{
    [Export]
    public float Scale = 100.0f;
    
    [Export]
    public Vector2 MapSize = new(200, 200);
    
    [Export]
    public Color PlayerMarkerColor = Colors.Blue;
    
    [Export]
    public Color EnemyMarkerColor = Colors.Red;
    
    [Export]
    public Color ObjectiveMarkerColor = Colors.Yellow;

    private Player.Player _player;
    private List<Vector3> _enemyPositions = new();
    private List<Vector3> _objectivePositions = new();

    public override void _Ready()
    {
        _player = GetTree().Root.GetChild(0).FindChild("Player", true, false) as Player.Player;
        CustomMinimumSize = MapSize;
    }

    public override void _Draw()
    {
        // Draw background
        DrawRect(new Rect2(Vector2.Zero, MapSize), new Color(0, 0, 0, 0.7f));
        
        // Draw border
        DrawRect(new Rect2(Vector2.Zero, MapSize), new Color(1, 1, 1, 0.3f), false);

        if (_player != null)
        {
            Vector2 playerMapPos = WorldToMapPos(_player.GlobalPosition);
            DrawCircle(playerMapPos, 5, PlayerMarkerColor);
        }

        // Draw enemies
        foreach (var enemyPos in _enemyPositions)
        {
            Vector2 enemyMapPos = WorldToMapPos(enemyPos);
            DrawCircle(enemyMapPos, 3, EnemyMarkerColor);
        }

        // Draw objectives
        foreach (var objPos in _objectivePositions)
        {
            Vector2 objMapPos = WorldToMapPos(objPos);
            DrawRect(new Rect2(objMapPos - Vector2.One * 3, Vector2.One * 6), ObjectiveMarkerColor);
        }
    }

    private Vector2 WorldToMapPos(Vector3 worldPos)
    {
        return new Vector2(
            (MapSize.X / 2) + (worldPos.X / Scale),
            (MapSize.Y / 2) + (worldPos.Z / Scale)
        );
    }

    public void AddEnemyMarker(Vector3 position)
    {
        _enemyPositions.Add(position);
    }

    public void RemoveEnemyMarker(Vector3 position)
    {
        _enemyPositions.Remove(position);
    }

    public void AddObjectiveMarker(Vector3 position)
    {
        _objectivePositions.Add(position);
    }
}
