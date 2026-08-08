using Godot;
using System;

public partial class StartMainContainer : HSplitContainer
{
    // 左侧和右侧的 ColorRect 节点
    public ColorRect leftRect;
	public ColorRect rightRect;

    // 左侧的 VBoxContainer 节点
    public VBoxContainer leftContainer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		base._Ready();
		leftRect = GetNode<ColorRect>("leftRect");
        rightRect = GetNode<ColorRect>("rightRect");

		leftRect.SizeFlagsHorizontal = SizeFlags.ExpandFill;
		rightRect.SizeFlagsHorizontal = SizeFlags.ExpandFill;

		leftRect.SizeFlagsStretchRatio = 1;
		rightRect.SizeFlagsStretchRatio = 2;

		leftContainer = GetNode<VBoxContainer>("leftRect/leftContainer");
		leftContainer.SizeFlagsHorizontal = SizeFlags.ExpandFill;
		var projectManagerContainer = GetNode<ScrollContainer>("leftRect/leftContainer/projectManagerContainer");
		projectManagerContainer.SizeFlagsHorizontal = SizeFlags.ExpandFill;
		projectManagerContainer.SizeFlagsVertical = SizeFlags.ExpandFill;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
