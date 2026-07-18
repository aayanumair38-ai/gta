using Godot;
using System;

namespace GTA.UI;

public partial class MainMenu : Control
{
    private Button _playButton;
    private Button _settingsButton;
    private Button _quitButton;
    private VBoxContainer _mainMenuContainer;
    private VBoxContainer _settingsContainer;

    public override void _Ready()
    {
        _mainMenuContainer = GetNode<VBoxContainer>("VBoxContainer/MainMenu");
        _settingsContainer = GetNode<VBoxContainer>("VBoxContainer/Settings");
        
        _playButton = _mainMenuContainer.GetNode<Button>("PlayButton");
        _settingsButton = _mainMenuContainer.GetNode<Button>("SettingsButton");
        _quitButton = _mainMenuContainer.GetNode<Button>("QuitButton");

        _playButton.Pressed += OnPlayPressed;
        _settingsButton.Pressed += OnSettingsPressed;
        _quitButton.Pressed += OnQuitPressed;
    }

    private void OnPlayPressed()
    {
        GD.Print("Starting game...");
        GetTree().ChangeSceneToFile("res://scenes/Main.tscn");
    }

    private void OnSettingsPressed()
    {
        _mainMenuContainer.Visible = false;
        _settingsContainer.Visible = true;
    }

    private void OnQuitPressed()
    {
        GetTree().Quit();
    }
}
