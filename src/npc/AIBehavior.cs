using Godot;
using System;
using System.Collections.Generic;

namespace GTA.NPC;

public partial class AIBehavior : Node
{
    public enum AIState { Patrol, Chase, Attack, Flee, Idle }
    
    private AIState _currentState = AIState.Idle;
    private Node3D _owner;
    private Node3D _target;
    private float _detectionRange = 50.0f;
    private float _attackRange = 5.0f;

    public override void _Ready()
    {
        _owner = GetParent<Node3D>();
    }

    public override void _PhysicsProcess(double delta)
    {
        UpdateAI(delta);
    }

    private void UpdateAI(double delta)
    {
        switch (_currentState)
        {
            case AIState.Patrol:
                Patrol(delta);
                break;
            case AIState.Chase:
                Chase(delta);
                break;
            case AIState.Attack:
                Attack(delta);
                break;
            case AIState.Flee:
                Flee(delta);
                break;
        }
    }

    private void Patrol(double delta)
    {
        GD.Print("AI: Patrolling");
    }

    private void Chase(double delta)
    {
        if (_target != null)
        {
            GD.Print($"AI: Chasing target");
        }
    }

    private void Attack(double delta)
    {
        GD.Print("AI: Attacking");
    }

    private void Flee(double delta)
    {
        GD.Print("AI: Fleeing");
    }

    public void SetState(AIState newState) => _currentState = newState;
    public AIState GetState() => _currentState;
}
