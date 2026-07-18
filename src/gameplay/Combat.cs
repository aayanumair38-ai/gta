using Godot;
using System;
using System.Collections.Generic;

namespace GTA.Gameplay;

public partial class Combat : Node3D
{
    [Export]
    public float MaxHealth = 100.0f;
    
    private float _currentHealth;
    private Dictionary<Weapon.WeaponType, Weapon> _weapons = new();
    private Weapon _currentWeapon;
    private ProgressBar _healthBar;

    public override void _Ready()
    {
        _currentHealth = MaxHealth;
        SetupWeapons();
        _healthBar = GetNode<ProgressBar>("UI/HealthBar");
        UpdateHealthBar();
    }

    private void SetupWeapons()
    {
        // Load weapons from scene
        foreach (Node child in GetChildren())
        {
            if (child is Weapon weapon)
            {
                _weapons[weapon.Type] = weapon;
                if (_currentWeapon == null)
                    _currentWeapon = weapon;
            }
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("weapon_1"))
            SwitchWeapon(Weapon.WeaponType.Pistol);
        if (@event.IsActionPressed("weapon_2"))
            SwitchWeapon(Weapon.WeaponType.Rifle);
        if (@event.IsActionPressed("weapon_3"))
            SwitchWeapon(Weapon.WeaponType.Shotgun);
    }

    private void SwitchWeapon(Weapon.WeaponType type)
    {
        if (_weapons.ContainsKey(type))
        {
            _currentWeapon.Visible = false;
            _currentWeapon = _weapons[type];
            _currentWeapon.Visible = true;
            GD.Print($"Switched to {type}");
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        UpdateHealthBar();
        
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        _currentHealth = Mathf.Min(_currentHealth + amount, MaxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (_healthBar != null)
        {
            _healthBar.Value = _currentHealth / MaxHealth * 100;
        }
    }

    private void Die()
    {
        GD.Print("Player died!");
        GetTree().ReloadCurrentScene();
    }

    public float GetHealth() => _currentHealth;
    public Weapon GetCurrentWeapon() => _currentWeapon;
}
