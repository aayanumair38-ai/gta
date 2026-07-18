using Godot;
using System;

namespace GTA.World;

public partial class World : Node3D
{
    private MeshInstance3D _ground;
    private StaticBody3D _groundBody;
    private CollisionShape3D _groundCollision;

    public override void _Ready()
    {
        CreateGround();
    }

    private void CreateGround()
    {
        // Create ground mesh
        _groundBody = new StaticBody3D();
        AddChild(_groundBody);

        _ground = new MeshInstance3D();
        _groundBody.AddChild(_ground);

        var mesh = new BoxMesh();
        mesh.Size = new Vector3(100, 1, 100);
        _ground.Mesh = mesh;

        // Add material
        var material = new StandardMaterial3D();
        material.AlbedoColor = new Color(0.2f, 0.5f, 0.2f, 1.0f);
        _ground.SetSurfaceOverrideMaterial(0, material);

        // Add collision
        _groundCollision = new CollisionShape3D();
        _groundBody.AddChild(_groundCollision);
        _groundCollision.Shape = new BoxShape3D() { Size = new Vector3(100, 1, 100) };
    }
}
