using Godot;
using System;

namespace GTA.NPC;

public partial class NPC : CharacterBody3D
{
    [Export]
    public float WalkSpeed = 3.0f;
    
    [Export]
    public float WanderRadius = 20.0f;
    
    [Export]
    public float WanderWaitTime = 3.0f;

    private Vector3 _targetPosition;
    private float _waitTimer = 0;
    private float _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    private AnimationPlayer _animationPlayer;
    private bool _isWalking = false;

    public enum NPCState { Idle, Walking, Talking, Fleeing }
    public NPCState CurrentState { get; set; } = NPCState.Idle;

    public override void _Ready()
    {
        _targetPosition = GlobalPosition;
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        SetRandomTarget();
    }

    public override void _PhysicsProcess(double delta)
    {
        ApplyGravity(delta);
        
        switch (CurrentState)
        {
            case NPCState.Idle:
                HandleIdle(delta);
                break;
            case NPCState.Walking:
                HandleWalking(delta);
                break;
            case NPCState.Fleeing:
                HandleFleeing(delta);
                break;
        }
        
        MoveAndSlide();
    }

    private void HandleIdle(double delta)
    {
        _waitTimer -= (float)delta;
        
        if (_waitTimer <= 0)
        {
            CurrentState = NPCState.Walking;
            SetRandomTarget();
        }
        
        PlayAnimation("idle");
    }

    private void HandleWalking(double delta)
    {
        Vector3 direction = (_targetPosition - GlobalPosition).Normalized();
        direction.Y = 0; // Keep walking on ground
        
        float distanceToTarget = GlobalPosition.DistanceTo(_targetPosition);
        
        if (distanceToTarget < 1.0f)
        {
            CurrentState = NPCState.Idle;
            _waitTimer = WanderWaitTime;
            Velocity = Vector3.Zero;
            PlayAnimation("idle");
        }
        else
        {
            Velocity = direction * WalkSpeed;
            LookAt(_targetPosition + Vector3.Up, Vector3.Up);
            PlayAnimation("walk");
        }
    }

    private void HandleFleeing(double delta)
    {
        // Run away from player
        Velocity = (GlobalPosition - GetTree().Root.GetChild(0).GlobalPosition).Normalized() * (WalkSpeed * 1.5f);
        PlayAnimation("run");
    }

    private void ApplyGravity(double delta)
    {
        if (!IsOnFloor())
            Velocity += Vector3.Down * _gravity * (float)delta;
    }

    private void SetRandomTarget()
    {
        var random = new RandomNumberGenerator();
        float angle = random.Randf() * Mathf.Tau;
        float distance = random.Randf() * WanderRadius;
        
        _targetPosition = GlobalPosition + new Vector3(
            Mathf.Cos(angle) * distance,
            0,
            Mathf.Sin(angle) * distance
        );
    }

    private void PlayAnimation(string animName)
    {
        if (_animationPlayer != null && _animationPlayer.CurrentAnimation != animName)
        {
            _animationPlayer.Play(animName);
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentState = NPCState.Fleeing;
        GD.Print($"NPC took {damage} damage!");
    }
}
