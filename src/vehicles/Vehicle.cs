using Godot;
using System;

namespace GTA.Vehicles;

public partial class Vehicle : RigidBody3D
{
    [Export]
    public float Acceleration = 50.0f;
    
    [Export]
    public float MaxSpeed = 30.0f;
    
    [Export]
    public float Braking = 30.0f;
    
    [Export]
    public float SteerSensitivity = 2.0f;
    
    [Export]
    public float TurnSpeed = 1.5f;

    private float _currentSpeed = 0;
    private float _steerAngle = 0;
    private bool _isPlayerInside = false;
    private Node3D _enterPoint;

    public override void _Ready()
    {
        LockRotation = true; // Lock X and Z axis rotation
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_isPlayerInside)
        {
            HandleInput(delta);
            UpdateMovement(delta);
        }
    }

    private void HandleInput(double delta)
    {
        float throttle = 0;
        float brake = 0;
        float steer = 0;

        if (Input.IsActionPressed("ui_up"))
            throttle = 1;
        if (Input.IsActionPressed("ui_down"))
            brake = 1;
        if (Input.IsActionPressed("ui_left"))
            steer = -1;
        if (Input.IsActionPressed("ui_right"))
            steer = 1;

        // Acceleration and braking
        if (throttle > 0)
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, MaxSpeed, (float)delta * Acceleration / MaxSpeed);
        }
        else if (brake > 0)
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, 0, (float)delta * Braking / MaxSpeed);
        }
        else
        {
            _currentSpeed *= 0.95f; // Gradual slowdown
        }

        // Steering
        _steerAngle = Mathf.Lerp(_steerAngle, steer * SteerSensitivity, (float)delta * 5);
        RotateY((float)(_steerAngle * delta * TurnSpeed));
    }

    private void UpdateMovement(double delta)
    {
        Vector3 movement = -GlobalTransform.Basis.Z * _currentSpeed * (float)delta;
        GlobalPosition += movement;
    }

    public void PlayerEnter()
    {
        _isPlayerInside = true;
        GD.Print("Player entered vehicle");
    }

    public void PlayerExit()
    {
        _isPlayerInside = false;
        _currentSpeed = 0;
        GD.Print("Player exited vehicle");
    }

    public bool IsPlayerInside() => _isPlayerInside;
}
