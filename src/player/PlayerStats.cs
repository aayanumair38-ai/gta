using Godot;
using System;

namespace GTA.Player;

public partial class PlayerStats : Node
{
    [Export]
    public float MaxHealth = 100.0f;
    
    [Export]
    public float MaxArmor = 100.0f;

    private float _currentHealth;
    private float _currentArmor;
    private float _money = 0;
    private float _experience = 0;
    private int _level = 1;

    public override void _Ready()
    {
        _currentHealth = MaxHealth;
        _currentArmor = MaxArmor;
    }

    public void TakeDamage(float damage)
    {
        float armorDamage = damage * 0.5f; // Armor absorbs 50% damage
        
        if (_currentArmor > 0)
        {
            _currentArmor -= armorDamage;
            if (_currentArmor < 0)
            {
                _currentHealth += _currentArmor; // Transfer overflow to health
                _currentArmor = 0;
            }
        }
        else
        {
            _currentHealth -= damage;
        }
        
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        _currentHealth = Mathf.Min(_currentHealth + amount, MaxHealth);
    }

    public void AddArmor(float amount)
    {
        _currentArmor = Mathf.Min(_currentArmor + amount, MaxArmor);
    }

    public void AddMoney(float amount)
    {
        _money += amount;
    }

    public void AddExperience(float amount)
    {
        _experience += amount;
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        if (_experience >= _level * 100)
        {
            _level++;
            MaxHealth += 10;
            MaxArmor += 10;
            GD.Print($"Level Up! Now level {_level}");
        }
    }

    private void Die()
    {
        GD.Print("Player died");
        GetTree().ReloadCurrentScene();
    }

    public float GetHealth() => _currentHealth;
    public float GetArmor() => _currentArmor;
    public float GetMoney() => _money;
    public int GetLevel() => _level;
}
