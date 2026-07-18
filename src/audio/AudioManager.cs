using Godot;
using System;
using System.Collections.Generic;

namespace GTA.Audio;

public partial class AudioManager : Node
{
    public static AudioManager Instance { get; private set; }
    
    private Dictionary<string, AudioStream> _soundEffects = new();
    private Dictionary<string, AudioStreamPlayer> _activeSounds = new();
    private AudioStreamPlayer _musicPlayer;
    private float _masterVolume = 1.0f;
    private float _sfxVolume = 0.8f;
    private float _musicVolume = 0.6f;

    public override void _Ready()
    {
        if (Instance != null && Instance != this)
        {
            QueueFree();
            return;
        }
        
        Instance = this;
        _musicPlayer = new AudioStreamPlayer();
        AddChild(_musicPlayer);
        GD.Print("AudioManager initialized");
    }

    public void PlaySFX(string soundName, float pitch = 1.0f)
    {
        var player = new AudioStreamPlayer();
        player.Bus = "SFX";
        player.PitchScale = pitch;
        player.VolumeDb = Mathf.LinearToDb(_sfxVolume * _masterVolume);
        AddChild(player);
        player.Play();
        
        // Remove after playback
        await ToSignal(player, AudioStreamPlayer.SignalName.Finished);
        player.QueueFree();
    }

    public void PlayMusic(string musicName, float fadeTime = 2.0f)
    {
        _musicPlayer.VolumeDb = Mathf.LinearToDb(_musicVolume * _masterVolume);
        GD.Print($"Playing music: {musicName}");
    }

    public void SetMasterVolume(float volume)
    {
        _masterVolume = Mathf.Clamp(volume, 0, 1);
        AudioServer.MasterVolume = _masterVolume;
    }

    public float GetMasterVolume() => _masterVolume;
}
