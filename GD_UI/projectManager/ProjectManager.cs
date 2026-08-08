using Godot;
using System;
using System.Collections.Generic;

namespace ProjectManager 
{
    public partial class ProjectManager : VBoxContainer
    {
        public List<ProjectItem> m_projectItems { get; private set; }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();

            this.SizeFlagsHorizontal = SizeFlags.ExpandFill;
            this.SizeFlagsVertical = SizeFlags.ExpandFill;
            m_projectItems = new List<ProjectItem>();

            ProjectItem projectItem = new ProjectItem();
            projectItem.Color = Colors.White;
            projectItem.SizeFlagsHorizontal = SizeFlags.ExpandFill; // 宽度铺满 projectManager
            projectItem.SizeFlagsVertical = SizeFlags.Fill;         // 固定高度，多个项目时不膨胀
            projectItem.Visible = true;
            m_projectItems.Add(projectItem);
            projectItem.CustomMinimumSize = new Vector2(0, 50);     // 固定高度 50
            this.AddChild(projectItem);

            var fc = FileController.Instantiate();
            fc.CreateFile("user://test.txt");
            if (fc.FileExists("user://test.txt"))
            {
                // File created successfully
                GD.Print("File created successfully.");
            }
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            base._Process(delta);
        }
    }
}


