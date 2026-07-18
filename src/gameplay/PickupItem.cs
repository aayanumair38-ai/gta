using Godot;
using System;

namespace GTA.Gameplay;

public partial class PickupItem : Area3D
{
    public enum ItemType { Health, Ammo, Armor, Money }
    
    [Export]
    public ItemType Type = ItemType.Health;
    
    [Export]
    public float Value = 25.0f;
    
    [Export]
    public float RotationSpeed = 2.0f;

    private Node3D _model;
    private bool _isCollected = false;

    public override void _Ready()
    {
        _model = GetNode<Node3D>("Model");
        AddToGroup("pickups");
    }

    public override void _Process(double delta)
    {
        if (!_isCollected && _model != null)
        {
            _model.RotateY((float)delta * RotationSpeed);
        }
    }

    public void Collect()
    {
        if (_isCollected) return;
        
        _isCollected = true;
        GD.Print($"Collected {Type}: +{Value}");
        QueueFree();
    }

    public ItemType GetItemType() => Type;
    public float GetValue() => Value;
}
