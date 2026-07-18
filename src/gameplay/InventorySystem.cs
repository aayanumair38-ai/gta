using Godot;
using System;
using System.Collections.Generic;

namespace GTA.Gameplay;

public partial class InventorySystem : Node
{
    public class InventoryItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public float Weight { get; set; }
        public string Description { get; set; }
    }

    [Export]
    public float MaxInventoryWeight = 100.0f;

    private Dictionary<string, InventoryItem> _items = new();
    private float _currentWeight = 0;

    public override void _Ready()
    {
        GD.Print("Inventory System initialized");
    }

    public void AddItem(InventoryItem item)
    {
        if (_currentWeight + item.Weight > MaxInventoryWeight)
        {
            GD.Print("Inventory full!");
            return;
        }

        if (_items.ContainsKey(item.Id))
        {
            _items[item.Id].Quantity += item.Quantity;
        }
        else
        {
            _items[item.Id] = item;
        }

        _currentWeight += item.Weight * item.Quantity;
        GD.Print($"Added {item.Name} x{item.Quantity}");
    }

    public void RemoveItem(string itemId, int quantity = 1)
    {
        if (_items.ContainsKey(itemId))
        {
            _items[itemId].Quantity -= quantity;
            if (_items[itemId].Quantity <= 0)
            {
                _currentWeight -= _items[itemId].Weight * _items[itemId].Quantity;
                _items.Remove(itemId);
            }
            GD.Print($"Removed item: {itemId}");
        }
    }

    public InventoryItem GetItem(string itemId) => _items.ContainsKey(itemId) ? _items[itemId] : null;
    public Dictionary<string, InventoryItem> GetAllItems() => _items;
    public float GetCurrentWeight() => _currentWeight;
}
