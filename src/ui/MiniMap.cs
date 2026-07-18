using Godot;
using System;

namespace GTA.Gameplay;

public partial class MiniMap : Control
{
    [Export]
    public float Scale = 50.0f;
    
    [Export]
    public Color PlayerColor = Colors.Blue;
    
    [Export]
    public Color NPCColor = Colors.Red;
    
    [Export]
    public Color VehicleColor = Colors.Yellow;

    private Control _mapCanvas;
    private Player.Player _player;
    private Vector2 _mapSize;

    public override void _Ready()
    {
        _mapCanvas = GetNode<Control>("MapCanvas");
        _mapSize = _mapCanvas.Size;
        _player = GetTree().Root.GetChild(0).FindChild("Player", true, false) as Player.Player;
    }

    public override void _Process(double delta)
    {
        if (_mapCanvas != null)
        {
            _mapCanvas.QueueRedraw();
        }
    }

    private void DrawMiniMap()
    {
        if (_player == null) return;

        // Draw player position
        Vector2 playerMapPos = WorldToMapPos(_player.GlobalPosition);
        _mapCanvas.DrawCircle(playerMapPos, 5, PlayerColor);
    }

    private Vector2 WorldToMapPos(Vector3 worldPos)
    {
        return new Vector2(
            (_mapSize.X / 2) + (worldPos.X / Scale),
            (_mapSize.Y / 2) + (worldPos.Z / Scale)
        );
    }
}
