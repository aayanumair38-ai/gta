using Godot;
using System;
using System.Collections.Generic;

namespace GTA.NPC;

public partial class NPCManager : Node3D
{
    [Export]
    public int NPCCount = 10;
    
    [Export]
    public float SpawnRadius = 30.0f;

    private List<NPC> _npcs = new();
    private PackedScene _npcScene;

    public override void _Ready()
    {
        _npcScene = GD.Load<PackedScene>("res://scenes/NPC.tscn");
        SpawnNPCs();
    }

    private void SpawnNPCs()
    {
        var random = new RandomNumberGenerator();
        
        for (int i = 0; i < NPCCount; i++)
        {
            if (_npcScene != null)
            {
                var npc = _npcScene.Instantiate<NPC>();
                
                float angle = random.Randf() * Mathf.Tau;
                float distance = random.Randf() * SpawnRadius;
                
                npc.GlobalPosition = GlobalPosition + new Vector3(
                    Mathf.Cos(angle) * distance,
                    0,
                    Mathf.Sin(angle) * distance
                );
                
                AddChild(npc);
                _npcs.Add(npc);
            }
        }
        
        GD.Print($"Spawned {_npcs.Count} NPCs");
    }

    public List<NPC> GetNPCs() => _npcs;
}
