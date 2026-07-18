using Godot;
using System;

namespace GTA.Player;

public partial class Player : CharacterBody3D
{
    [Export]
    public float Speed = 5.0f;
    
    [Export]
    public float JumpForce = 4.5f;
    
    [Export]
    public float Sensitivity = 0.002f;

    private float _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    private Node3D _head;
    private Camera3D _camera;
    private float _yaw = 0;
    private float _pitch = 0;

    public override void _Ready()
    {
        _head = GetNode<Node3D>("Head");
        _camera = _head.GetNode<Camera3D>("Camera3D");
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _PhysicsProcess(double delta)
    {
        HandleInput(delta);
        HandleMovement(delta);
        MoveAndSlide();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion)
        {
            HandleMouseLook(mouseMotion);
        }
    }

    private void HandleInput(double delta)
    {
        Vector3 input = Vector3.Zero;

        if (Input.IsActionPressed("ui_up"))
            input.Z -= 1;
        if (Input.IsActionPressed("ui_down"))
            input.Z += 1;
        if (Input.IsActionPressed("ui_left"))
            input.X -= 1;
        if (Input.IsActionPressed("ui_right"))
            input.X += 1;

        input = input.Normalized();
        
        // Transform input to world space based on camera direction
        var forward = -Transform.Basis.Z;
        var right = Transform.Basis.X;
        
        Velocity = (forward * input.Z + right * input.X) * Speed;
    }

    private void HandleMovement(double delta)
    {
        // Gravity
        if (!IsOnFloor())
            Velocity += Vector3.Down * _gravity * (float)delta;
        
        // Jump
        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
            Velocity += Vector3.Up * JumpForce;
    }

    private void HandleMouseLook(InputEventMouseMotion mouseMotion)
    {
        _yaw -= mouseMotion.Relative.X * Sensitivity;
        _pitch -= mouseMotion.Relative.Y * Sensitivity;
        _pitch = Mathf.Clamp(_pitch, -Mathf.Pi / 2, Mathf.Pi / 2);

        var rot = Transform.Rotation;
        rot.Y = _yaw;
        Transform = new Transform3D(Transform.Basis.FromEuler(rot), Transform.Origin);

        _head.Rotation = new Vector3(_pitch, 0, 0);
    }
}
