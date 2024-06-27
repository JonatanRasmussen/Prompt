using Godot;
using System;
using System.Collections.Generic;

namespace GlobalNameSpace
{
    public partial class PromptTab : HSplitContainer
    {
        private PackedScene _msgBlockScene = GD.Load<PackedScene>(MyNodePaths.SCENE_PATH_MSG_BLOCK);
        private Container? _msgBlockContainer;
        private Button? _createNewMsgBlockButton;
        private readonly List<Node> _msgBlockInstances = new();

        public override void _Ready()
        {
            InitMsgBlockContainer();
            InitNewMsgBlockButton();
            CreateNewMsgBlock();
        }

        public void CreateNewMsgBlock()
        {
            var msgBlockInst = _msgBlockScene.Instantiate();
            _msgBlockInstances.Add(msgBlockInst);
            _msgBlockContainer?.AddChild(msgBlockInst);
        }

        public void MoveMsgBlock(MsgBlock msgBlock, bool moveTowardsTop)
        {
            int index = _msgBlockInstances.IndexOf(msgBlock);
            if (index == -1)
            {
                GD.PrintErr("Message block not found in the list.");
                return;
            }

            int newIndex = index + 1;
            if (moveTowardsTop)
            {
                newIndex = index - 1;
            }

            if (newIndex < 0 || newIndex >= _msgBlockInstances.Count)
            {
                GD.Print("Cannot move MsgBlock to an index out of bounds.");
                return;
            }

            _msgBlockContainer?.MoveChild(msgBlock, newIndex);

            _msgBlockInstances.RemoveAt(index);
            _msgBlockInstances.Insert(newIndex, msgBlock);

            GD.Print($"Moved MsgBlock from index {index} to {newIndex}");
        }

        public void RemoveMsgBlock(MsgBlock msgBlock)
        {
            GD.Print("Closing Msg Block...");
            int index = _msgBlockInstances.IndexOf(msgBlock);
            if (index != -1)
            {
                RemoveMsgBlockAtIndex(index);
            }
        }

        private void RemoveMsgBlockAtIndex(int index)
        {
            if (index < 0 || index >= _msgBlockInstances.Count)
            {
                GD.PrintErr("Invalid message block index.");
                return;
            }

            var msgBlockInst = _msgBlockInstances[index];
            _msgBlockContainer?.RemoveChild(msgBlockInst);
            msgBlockInst.QueueFree();
            _msgBlockInstances.RemoveAt(index);
        }

        private void InitMsgBlockContainer()
        {
            string path = MyNodePaths.PROMPT_TAB_SCENE_MSG_BLOCK_CONTAINER;
            _msgBlockContainer = GetNode<Container>(path);
            if (_msgBlockContainer == null)
            {
                GD.PrintErr($"Node not found at path {path}.");
                return;
            }
        }

        private void InitNewMsgBlockButton()
        {
            string path = MyNodePaths.PROMPT_TAB_SCENE_NEW_BLOCK_BUTTON;
            _createNewMsgBlockButton = GetNode<Button>(path);
            if (_createNewMsgBlockButton == null)
            {
                GD.PrintErr($"Node not found at path {path}.");
                return;
            }
            _createNewMsgBlockButton.Pressed += CreateNewMsgBlock;
        }
    }
}
