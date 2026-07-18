using Godot;
using System;

namespace GTA.Player;

public partial class InputHandler : Node
{
    [Signal]
    public delegate void ActionPressedEventHandler(string action);

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent && keyEvent.Pressed)
        {
            HandleKeyInput(keyEvent);
        }

        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
        {
            HandleMouseInput(mouseEvent);
        }
    }

    private void HandleKeyInput(InputEventKey keyEvent)
    {
        if (keyEvent.Keycode == Key.W)
            EmitSignal(SignalName.ActionPressed, "move_forward");
        if (keyEvent.Keycode == Key.A)
            EmitSignal(SignalName.ActionPressed, "move_left");
        if (keyEvent.Keycode == Key.S)
            EmitSignal(SignalName.ActionPressed, "move_backward");
        if (keyEvent.Keycode == Key.D)
            EmitSignal(SignalName.ActionPressed, "move_right");
        if (keyEvent.Keycode == Key.Space)
            EmitSignal(SignalName.ActionPressed, "jump");
        if (keyEvent.Keycode == Key.E)
            EmitSignal(SignalName.ActionPressed, "interact");
    }

    private void HandleMouseInput(InputEventMouseButton mouseEvent)
    {
        if (mouseEvent.ButtonIndex == MouseButton.Left)
            EmitSignal(SignalName.ActionPressed, "fire");
    }
}
