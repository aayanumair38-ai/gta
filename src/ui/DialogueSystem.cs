using Godot;
using System;

namespace GTA.UI;

public partial class DialogueSystem : Control
{
    public class DialogueNode
    {
        public string Speaker { get; set; }
        public string Text { get; set; }
        public string[] Options { get; set; }
        public Action<int> OnChoice { get; set; }
    }

    private DialogueNode _currentDialogue;
    private Label _speakerLabel;
    private Label _dialogueLabel;
    private VBoxContainer _optionsContainer;
    private Panel _dialoguePanel;
    private bool _isDialogueActive = false;

    public override void _Ready()
    {
        _dialoguePanel = GetNode<Panel>("DialoguePanel");
        _speakerLabel = _dialoguePanel.GetNode<Label>("VBoxContainer/Speaker");
        _dialogueLabel = _dialoguePanel.GetNode<Label>("VBoxContainer/DialogueText");
        _optionsContainer = _dialoguePanel.GetNode<VBoxContainer>("VBoxContainer/Options");
        _dialoguePanel.Visible = false;
    }

    public void StartDialogue(DialogueNode dialogue)
    {
        _currentDialogue = dialogue;
        _speakerLabel.Text = dialogue.Speaker;
        _dialogueLabel.Text = dialogue.Text;
        _isDialogueActive = true;
        _dialoguePanel.Visible = true;

        // Clear previous options
        foreach (Node child in _optionsContainer.GetChildren())
            child.QueueFree();

        // Create option buttons
        if (dialogue.Options != null)
        {
            for (int i = 0; i < dialogue.Options.Length; i++)
            {
                var button = new Button();
                button.Text = dialogue.Options[i];
                int optionIndex = i;
                button.Pressed += () => SelectOption(optionIndex);
                _optionsContainer.AddChild(button);
            }
        }
    }

    private void SelectOption(int index)
    {
        _currentDialogue.OnChoice?.Invoke(index);
        EndDialogue();
    }

    public void EndDialogue()
    {
        _isDialogueActive = false;
        _dialoguePanel.Visible = false;
    }

    public bool IsDialogueActive() => _isDialogueActive;
}
