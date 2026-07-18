using Godot;
using System;

namespace GTA.UI;

public partial class PauseMenu : CanvasLayer
{
    private Control _pausePanel;
    private Button _resumeButton;
    private Button _settingsButton;
    private Button _quitButton;
    private bool _isPaused = false;

    public override void _Ready()
    {
        _pausePanel = GetNode<Control>("PausePanel");
        _resumeButton = _pausePanel.GetNode<Button>("VBoxContainer/ResumeButton");
        _settingsButton = _pausePanel.GetNode<Button>("VBoxContainer/SettingsButton");
        _quitButton = _pausePanel.GetNode<Button>("VBoxContainer/QuitButton");

        _resumeButton.Pressed += Resume;
        _settingsButton.Pressed += () => GD.Print("Settings");
        _quitButton.Pressed += () => GetTree().ChangeSceneToFile("res://scenes/MainMenu.tscn");
        
        _pausePanel.Visible = false;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("pause"))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        _isPaused = !_isPaused;
        _pausePanel.Visible = _isPaused;
        GetTree().Paused = _isPaused;
    }

    private void Resume()
    {
        TogglePause();
    }
}
