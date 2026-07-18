using Godot;
using System;
using System.Collections.Generic;

namespace GTA.Gameplay;

public partial class NotificationSystem : CanvasLayer
{
    private VBoxContainer _notificationContainer;
    private Queue<string> _notificationQueue = new();
    private bool _isDisplaying = false;

    public override void _Ready()
    {
        _notificationContainer = new VBoxContainer();
        AddChild(_notificationContainer);
    }

    public void ShowNotification(string message, float duration = 3.0f)
    {
        _notificationQueue.Enqueue(message);
        
        if (!_isDisplaying)
        {
            DisplayNextNotification(duration);
        }
    }

    private void DisplayNextNotification(float duration)
    {
        if (_notificationQueue.Count == 0)
        {
            _isDisplaying = false;
            return;
        }

        _isDisplaying = true;
        string message = _notificationQueue.Dequeue();

        var notifLabel = new Label();
        notifLabel.Text = message;
        notifLabel.AddThemeStyleOverride("normal", new StyleBoxFlat()
        {
            BgColor = new Color(0.2f, 0.2f, 0.2f, 0.9f)
        });
        
        _notificationContainer.AddChild(notifLabel);

        var timer = new Timer();
        timer.WaitTime = duration;
        timer.OneShot = true;
        AddChild(timer);
        
        timer.Timeout += () => {
            notifLabel.QueueFree();
            DisplayNextNotification(duration);
        };
        
        timer.Start();
    }
}
