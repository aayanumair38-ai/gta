using Godot;
using System;
using System.Collections.Generic;

namespace GTA.Gameplay;

public partial class QuestSystem : Node3D
{
    public class Quest
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public List<string> Objectives { get; set; } = new();
    }

    private Dictionary<string, Quest> _quests = new();
    private List<string> _activeQuests = new();

    public override void _Ready()
    {
        InitializeQuests();
    }

    private void InitializeQuests()
    {
        // Tutorial quest
        var tutorialQuest = new Quest
        {
            Id = "tutorial_01",
            Title = "Explore the World",
            Description = "Get familiar with the game controls and environment",
            Objectives = new List<string>
            {
                "Walk around the map",
                "Find an NPC",
                "Find a vehicle"
            }
        };

        AddQuest(tutorialQuest);
        ActivateQuest(tutorialQuest.Id);
    }

    public void AddQuest(Quest quest)
    {
        _quests[quest.Id] = quest;
        GD.Print($"Quest added: {quest.Title}");
    }

    public void ActivateQuest(string questId)
    {
        if (_quests.ContainsKey(questId) && !_activeQuests.Contains(questId))
        {
            _activeQuests.Add(questId);
            GD.Print($"Quest activated: {_quests[questId].Title}");
        }
    }

    public void CompleteQuest(string questId)
    {
        if (_quests.ContainsKey(questId))
        {
            _quests[questId].IsCompleted = true;
            _activeQuests.Remove(questId);
            GD.Print($"Quest completed: {_quests[questId].Title}");
        }
    }

    public List<string> GetActiveQuests() => _activeQuests;
    public Quest GetQuestInfo(string questId) => _quests.ContainsKey(questId) ? _quests[questId] : null;
}
