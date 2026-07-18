using Godot;
using System;

namespace GTA.Gameplay;

public partial class ParticleEffects : Node3D
{
    [Export]
    public float MuzzleFlashDuration = 0.1f;
    
    [Export]
    public float BloodSplatterScale = 1.0f;

    private GPUParticles3D _muzzleFlash;
    private GPUParticles3D _explosionEffect;
    private GPUParticles3D _bloodEffect;

    public override void _Ready()
    {
        CreateParticleEffects();
    }

    private void CreateParticleEffects()
    {
        _muzzleFlash = new GPUParticles3D();
        _muzzleFlash.Name = "MuzzleFlash";
        AddChild(_muzzleFlash);

        _explosionEffect = new GPUParticles3D();
        _explosionEffect.Name = "ExplosionEffect";
        AddChild(_explosionEffect);

        _bloodEffect = new GPUParticles3D();
        _bloodEffect.Name = "BloodEffect";
        AddChild(_bloodEffect);
    }

    public void PlayMuzzleFlash()
    {
        if (_muzzleFlash != null)
        {
            _muzzleFlash.Restart();
        }
    }

    public void PlayExplosion(Vector3 position)
    {
        _explosionEffect.GlobalPosition = position;
        _explosionEffect.Restart();
    }

    public void PlayBloodSplatter(Vector3 position)
    {
        _bloodEffect.GlobalPosition = position;
        _bloodEffect.Scale *= BloodSplatterScale;
        _bloodEffect.Restart();
    }
}
