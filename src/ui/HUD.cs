using Godot;
using System;

namespace GTA.UI;

public partial class HUD : CanvasLayer
{
    private Label _healthLabel;
    private Label _ammoLabel;
    private Label _speedLabel;
    private Label _fpsLabel;
    private ProgressBar _healthBar;
    private TextureProgressBar _ammoBar;
    private Label _crosshair;

    public override void _Ready()
    {
        _healthLabel = GetNode<Label>("MarginContainer/VBoxContainer/HealthLabel");
        _ammoLabel = GetNode<Label>("MarginContainer/VBoxContainer/AmmoLabel");
        _speedLabel = GetNode<Label>("MarginContainer/VBoxContainer/SpeedLabel");
        _fpsLabel = GetNode<Label>("MarginContainer/VBoxContainer/FPSLabel");
        _healthBar = GetNode<ProgressBar>("MarginContainer/VBoxContainer/HealthBar");
        _ammoBar = GetNode<TextureProgressBar>("MarginContainer/VBoxContainer/AmmoBar");
        _crosshair = GetNode<Label>("CenterContainer/Crosshair");
    }

    public override void _PhysicsProcess(double delta)
    {
        UpdateFPS();
    }

    public void UpdateHealth(float current, float max)
    {
        _healthLabel.Text = $"Health: {current:F0}/{max:F0}";
        _healthBar.Value = (current / max) * 100;
    }

    public void UpdateAmmo(int current, int max)
    {
        _ammoLabel.Text = $"Ammo: {current}/{max}";
        _ammoBar.Value = (current / (float)max) * 100;
    }

    public void UpdateSpeed(float speed)
    {
        _speedLabel.Text = $"Speed: {speed:F1} m/s";
    }

    private void UpdateFPS()
    {
        _fpsLabel.Text = $"FPS: {Engine.GetFramesPerSecond()}";
    }

    public void ShowMessage(string message, float duration = 3.0f)
    {
        var messageLabel = new Label();
        messageLabel.Text = message;
        messageLabel.AddThemeStyleOverride("normal", new StyleBoxFlat());
        AddChild(messageLabel);
        
        var timer = new Timer();
        timer.WaitTime = duration;
        timer.OneShot = true;
        AddChild(timer);
        timer.Timeout += () => messageLabel.QueueFree();
        timer.Start();
    }
}
