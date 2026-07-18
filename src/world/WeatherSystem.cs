using Godot;
using System;
using System.Collections.Generic;

namespace GTA.World;

public partial class WeatherSystem : Node3D
{
    public enum Weather { Clear, Rain, Storm, Fog }
    
    [Export]
    public float WeatherChangeInterval = 60.0f;
    
    [Export]
    public float TransitionSpeed = 0.5f;

    private Weather _currentWeather = Weather.Clear;
    private float _weatherTimer = 0;
    private WorldEnvironment _environment;
    private float _targetLightIntensity = 1.0f;
    private float _currentLightIntensity = 1.0f;

    public override void _Ready()
    {
        _environment = GetNode<WorldEnvironment>(".");
        GD.Print($"Weather: {_currentWeather}");
    }

    public override void _PhysicsProcess(double delta)
    {
        _weatherTimer -= (float)delta;

        if (_weatherTimer <= 0)
        {
            ChangeWeather();
            _weatherTimer = WeatherChangeInterval;
        }

        // Smooth light transition
        _currentLightIntensity = Mathf.Lerp(_currentLightIntensity, _targetLightIntensity, (float)delta * TransitionSpeed);
    }

    private void ChangeWeather()
    {
        var random = new RandomNumberGenerator();
        int weatherIndex = random.Randi() % 4;
        _currentWeather = (Weather)weatherIndex;

        switch (_currentWeather)
        {
            case Weather.Clear:
                _targetLightIntensity = 1.0f;
                GD.Print("Weather: Clear");
                break;
            case Weather.Rain:
                _targetLightIntensity = 0.7f;
                GD.Print("Weather: Rain");
                break;
            case Weather.Storm:
                _targetLightIntensity = 0.4f;
                GD.Print("Weather: Storm");
                break;
            case Weather.Fog:
                _targetLightIntensity = 0.6f;
                GD.Print("Weather: Fog");
                break;
        }
    }

    public Weather GetCurrentWeather() => _currentWeather;
}
