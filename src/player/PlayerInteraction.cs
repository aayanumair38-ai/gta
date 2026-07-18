using Godot;
using System;
using System.Collections.Generic;

namespace GTA.Player;

public partial class PlayerInteraction : Area3D
{
    [Export]
    public float InteractionRange = 2.0f;

    private List<Node3D> _interactableObjects = new();
    private Label _interactionHint;

    public override void _Ready()
    {
        _interactionHint = GetNode<Label>("InteractionHint");
        _interactionHint.Visible = false;
    }

    public override void _PhysicsProcess(double delta)
    {
        CheckForInteractables();
    }

    private void CheckForInteractables()
    {
        var overlappingAreas = GetOverlappingAreas();
        _interactableObjects.Clear();

        foreach (Area3D area in overlappingAreas)
        {
            if (area.IsInGroup("interactable"))
            {
                _interactableObjects.Add(area);
            }
        }

        if (_interactableObjects.Count > 0)
        {
            _interactionHint.Visible = true;
            _interactionHint.Text = "[E] Interact";
        }
        else
        {
            _interactionHint.Visible = false;
        }
    }

    public void Interact()
    {
        if (_interactableObjects.Count > 0)
        {
            var obj = _interactableObjects[0];
            if (obj.HasMethod("OnInteract"))
            {
                obj.Call("OnInteract");
            }
        }
    }
}
