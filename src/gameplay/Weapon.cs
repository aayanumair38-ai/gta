using Godot;
using System;

namespace GTA.Gameplay;

public partial class Weapon : Node3D
{
    public enum WeaponType { Pistol, Rifle, Shotgun, Melee }
    
    [Export]
    public WeaponType Type = WeaponType.Pistol;
    
    [Export]
    public float Damage = 20.0f;
    
    [Export]
    public float FireRate = 0.1f; // Seconds between shots
    
    [Export]
    public float Range = 100.0f;
    
    [Export]
    public int AmmoCapacity = 30;

    private float _fireTimer = 0;
    private int _currentAmmo;
    private AudioStreamPlayer3D _fireSound;
    private GPUParticles3D _muzzleFlash;

    public override void _Ready()
    {
        _currentAmmo = AmmoCapacity;
        _fireSound = GetNode<AudioStreamPlayer3D>("FireSound");
        _muzzleFlash = GetNode<GPUParticles3D>("MuzzleFlash");
    }

    public override void _PhysicsProcess(double delta)
    {
        _fireTimer -= (float)delta;

        if (Input.IsActionPressed("fire") && _fireTimer <= 0 && _currentAmmo > 0)
        {
            Fire();
            _fireTimer = FireRate;
        }

        if (Input.IsActionJustPressed("reload"))
        {
            Reload();
        }
    }

    private void Fire()
    {
        _currentAmmo--;
        
        if (_fireSound != null)
            _fireSound.Play();
        
        if (_muzzleFlash != null)
            _muzzleFlash.Restart();

        // Raycast for hit detection
        var spaceState = GetWorld3D().DirectSpaceState;
        var query = PhysicsRayQueryParameters3D.Create(GlobalPosition, GlobalPosition + GlobalTransform.Basis.Z * Range);
        var result = spaceState.IntersectRay(query);

        if (result.Count > 0 && result.ContainsKey("collider"))
        {
            var collider = (Node)result["collider"];
            GD.Print($"Shot hit: {collider.Name}");
            
            if (collider.IsInGroup("enemies"))
            {
                // Deal damage to enemy
                GD.Print($"Enemy hit for {Damage} damage");
            }
        }
    }

    private void Reload()
    {
        _currentAmmo = AmmoCapacity;
        GD.Print($"Reloaded! Ammo: {_currentAmmo}");
    }

    public int GetCurrentAmmo() => _currentAmmo;
    public int GetCapacity() => AmmoCapacity;
}
