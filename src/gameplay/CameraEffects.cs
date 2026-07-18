using Godot;
using System;

namespace GTA.Gameplay;

public partial class CameraEffects : Node3D
{
    [Export]
    public float ShakeDuration = 0.1f;
    
    [Export]
    public float ShakeIntensity = 0.3f;
    
    [Export]
    public float ZoomSpeed = 5.0f;

    private Camera3D _camera;
    private float _shakeTimer = 0;
    private float _targetFOV = 75.0f;

    public override void _Ready()
    {
        _camera = GetNode<Camera3D>(".");
    }

    public override void _PhysicsProcess(double delta)
    {
        UpdateShake(delta);
        UpdateZoom(delta);
    }

    private void UpdateShake(double delta)
    {
        if (_shakeTimer > 0)
        {
            _shakeTimer -= (float)delta;
            var random = new RandomNumberGenerator();
            
            Vector3 shakeOffset = new Vector3(
                (random.Randf() - 0.5f) * ShakeIntensity,
                (random.Randf() - 0.5f) * ShakeIntensity,
                (random.Randf() - 0.5f) * ShakeIntensity
            );
            
            _camera.Position += shakeOffset;
        }
    }

    private void UpdateZoom(double delta)
    {
        if (_camera.Fov != _targetFOV)
        {
            _camera.Fov = Mathf.Lerp(_camera.Fov, _targetFOV, (float)delta * ZoomSpeed);
        }
    }

    public void Shake()
    {
        _shakeTimer = ShakeDuration;
    }

    public void SetZoom(float fov)
    {
        _targetFOV = Mathf.Clamp(fov, 10.0f, 110.0f);
    }
}
