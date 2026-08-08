using Godot;
using System;

namespace ProjectManager
{
    public partial class ProjectItem : ColorRect
    {
        private string ProjectDir { get; set; } = string.Empty;

        // 项目所在路径
        public string ProjectPath { get; set; } = string.Empty;
        // 项目名称
        private string ProjectName { get; set; } = string.Empty;

        

        // 默认构造函数
        public ProjectItem()
        {
            ProjectName = "新项目";

            ProjectDir = "res://ProjectManager/Projects/";
            ProjectPath = ProjectDir + ProjectName + "/";

            this.Color = new Color("#1C1F26");
            this.SizeFlagsHorizontal = SizeFlags.ExpandFill;
            
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            base._Process(delta);
        }

        public override string ToString()
        {
            return string.Empty;
        }

        // 默认析构函数
        ~ProjectItem()
        {

        }
       
    }
}
