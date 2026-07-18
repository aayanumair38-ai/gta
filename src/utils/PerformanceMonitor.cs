using Godot;
using System;
using System.Diagnostics;

namespace GTA.Utils;

public partial class PerformanceMonitor : Node
{
    private Label _statsLabel;
    private Queue<float> _fpsHistory = new(100);
    private float _updateTimer = 0;

    public override void _Ready()
    {
        _statsLabel = new Label();
        AddChild(_statsLabel);
    }

    public override void _Process(double delta)
    {
        _updateTimer += (float)delta;

        if (_updateTimer >= 0.5f)
        {
            UpdateStats();
            _updateTimer = 0;
        }
    }

    private void UpdateStats()
    {
        int fps = Engine.GetFramesPerSecond();
        var memUsage = OS.GetStaticMemoryUsage() / (1024 * 1024); // MB
        var memPeak = OS.GetStaticMemoryPeakUsage() / (1024 * 1024); // MB

        _statsLabel.Text = $@"
FPS: {fps}
Memory: {memUsage} MB
Peak Memory: {memPeak} MB
Physics FPS: {Engine.PhysicsTicksPerSecond}
";
    }
}
