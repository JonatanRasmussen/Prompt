using Godot;
using System;

namespace GlobalNameSpace
{
    public partial class MsgBlock : VBoxContainer
    {
        private PromptTab? _parentTab;
        private Button? _moveMsgBlockUpButton;
        private Button? _moveMsgBlockDownButton;
        private Button? _removeMsgBlockButton;

        public override void _Ready()
        {
            FindParentTab();
            InitMoveMsgBlockUpButton();
            InitMoveMsgBlockDownButton();
            InitRemoveMsgBlockButton();
        }

        private void FindParentTab()
        {
            _parentTab = GetParent().GetParent().GetParent().GetParent().GetParent().GetParent() as PromptTab;
        }

        private void InitMoveMsgBlockUpButton()
        {
            string path = MyNodePaths.MSG_BLOCK_SCENE_MOVE_BLOCK_UP_BUTTON;
            _moveMsgBlockUpButton = GetNode<Button>(path);
            if (_moveMsgBlockUpButton == null)
            {
                GD.PrintErr($"Node not found at path {path}.");
                return;
            }
            _moveMsgBlockUpButton.Pressed += MoveMsgBlockButtonPressedUp;
        }

        private void InitMoveMsgBlockDownButton()
        {
            string path = MyNodePaths.MSG_BLOCK_SCENE_MOVE_BLOCK_DOWN_BUTTON;
            _moveMsgBlockDownButton = GetNode<Button>(path);
            if (_moveMsgBlockDownButton == null)
            {
                GD.PrintErr($"Node not found at path {path}.");
                return;
            }
            _moveMsgBlockDownButton.Pressed += MoveMsgBlockButtonPressedDown;
        }

        private void InitRemoveMsgBlockButton()
        {
            string path = MyNodePaths.MSG_BLOCK_SCENE_REMOVE_BLOCK_BUTTON;
            _removeMsgBlockButton = GetNode<Button>(path);
            if (_removeMsgBlockButton == null)
            {
                GD.PrintErr($"Node not found at path {path}.");
                return;
            }
            _removeMsgBlockButton.Pressed += RemoveMsgBlockButtonPressed;
        }

        private void MoveMsgBlockButtonPressedUp()
        {
            if (_parentTab == null)
            {
                GD.PrintErr("Parent is null.");
                return;
            }
            bool moveTowardsTop = true;
            _parentTab.MoveMsgBlock(this, moveTowardsTop);
        }

        private void MoveMsgBlockButtonPressedDown()
        {
            if (_parentTab == null)
            {
                GD.PrintErr("Parent is null.");
                return;
            }
            bool moveTowardsTop = false;
            _parentTab.MoveMsgBlock(this, moveTowardsTop);
        }

        private void RemoveMsgBlockButtonPressed()
        {
            if (_parentTab == null)
            {
                GD.PrintErr("Parent is null.");
                return;
            }
            _parentTab.RemoveMsgBlock(this);
        }
    }
}
