using Godot;
using System;
using System.Collections.Generic;

namespace GTA.World;

public partial class TerrainGenerator : Node3D
{
    [Export]
    public int GridSize = 10;
    
    [Export]
    public float TileSize = 10.0f;

    private List<MeshInstance3D> _terrainTiles = new();

    public override void _Ready()
    {
        GenerateTerrain();
    }

    private void GenerateTerrain()
    {
        for (int x = 0; x < GridSize; x++)
        {
            for (int z = 0; z < GridSize; z++)
            {
                CreateTile(x, z);
            }
        }
        GD.Print($"Terrain generated: {GridSize}x{GridSize} tiles");
    }

    private void CreateTile(int x, int z)
    {
        var tile = new MeshInstance3D();
        var mesh = new BoxMesh();
        mesh.Size = new Vector3(TileSize, 0.5f, TileSize);
        tile.Mesh = mesh;

        // Alternate colors
        var material = new StandardMaterial3D();
        if ((x + z) % 2 == 0)
            material.AlbedoColor = new Color(0.3f, 0.6f, 0.2f, 1.0f);
        else
            material.AlbedoColor = new Color(0.4f, 0.7f, 0.2f, 1.0f);

        tile.SetSurfaceOverrideMaterial(0, material);
        tile.Position = new Vector3(x * TileSize, 0, z * TileSize);

        // Add collision
        var collisionShape = new CollisionShape3D();
        collisionShape.Shape = new BoxShape3D() { Size = new Vector3(TileSize, 0.5f, TileSize) };

        var staticBody = new StaticBody3D();
        staticBody.AddChild(tile);
        staticBody.AddChild(collisionShape);
        AddChild(staticBody);

        _terrainTiles.Add(tile);
    }
}
