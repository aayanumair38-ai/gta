using Godot;
using System;
using System.Collections.Generic;

namespace GTA.Utils;

public partial class EventBus : Node
{
    public static EventBus Instance { get; private set; }

    private Dictionary<string, List<Callable>> _eventListeners = new();

    public override void _Ready()
    {
        if (Instance != null && Instance != this)
        {
            QueueFree();
            return;
        }
        
        Instance = this;
        GD.Print("EventBus initialized");
    }

    public void Subscribe(string eventName, Callable callback)
    {
        if (!_eventListeners.ContainsKey(eventName))
        {
            _eventListeners[eventName] = new List<Callable>();
        }
        
        _eventListeners[eventName].Add(callback);
    }

    public void Unsubscribe(string eventName, Callable callback)
    {
        if (_eventListeners.ContainsKey(eventName))
        {
            _eventListeners[eventName].Remove(callback);
        }
    }

    public void Emit(string eventName, params Variant[] args)
    {
        if (_eventListeners.ContainsKey(eventName))
        {
            foreach (var listener in _eventListeners[eventName])
            {
                listener.Call(args);
            }
        }
    }
}
