using Godot;
using System;
using System.Collections.Generic;

namespace GlobalNameSpace
{
    public partial class Main : Node
    {
        private PackedScene _promptTabScene = GD.Load<PackedScene>(MyNodePaths.SCENE_PATH_PROMPT_TAB);
        private Container? _promptTabContainer;
        private readonly List<Node> _promptTabInstances = new();

        public override void _Ready()
        {
            InitPromptTabContainer();
            CreateNewPromptTab("NewTab");
        }

        public void CreateNewPromptTab(string tabName)
        {
            var promptTabInst = _promptTabScene.Instantiate();
            promptTabInst.Name = tabName;
            _promptTabInstances.Add(promptTabInst);
            _promptTabContainer?.AddChild(promptTabInst);
        }

        public void ClosePromptTab(int index)
        {
            if (index < 0 || index >= _promptTabInstances.Count)
            {
                GD.PrintErr("Invalid tab index.");
                return;
            }

            var promptTabInst = _promptTabInstances[index];
            _promptTabContainer?.RemoveChild(promptTabInst);
            promptTabInst.QueueFree();
            _promptTabInstances.RemoveAt(index);
        }

        private void InitPromptTabContainer()
        {
            string path = "WindowMargin/TitleAndTabs/PromptTabs";
            _promptTabContainer = GetNode<Container>(path);
            if (_promptTabContainer == null)
            {
                GD.PrintErr($"Node not found at path {path}.");
                return;
            }
        }
    }
}