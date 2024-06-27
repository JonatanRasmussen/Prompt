using Godot;
using System;
using System.Collections.Generic;

namespace GlobalNameSpace
{
    public class MyNodePaths
    {
        // Scenes
        public const string SCENE_PATH_PROMPT_TAB = "res://scenes/prompt_tab.tscn";
        public const string SCENE_PATH_MSG_BLOCK = "res://scenes/msg_block.tscn";

        // Main
        public const string MAIN_SCENE_PROMPT_TABS = "WindowMargin/TitleAndTabs/PromptTabs";

        // PromptTab
        public const string PROMPT_TAB_SCENE_MSG_BLOCK_CONTAINER = "Inputs/InputSetup/ScrollBehavior/ScrollContainer/MsgBlocks";
        public const string PROMPT_TAB_SCENE_NEW_BLOCK_BUTTON = "Inputs/InputSetup/ScrollBehavior/ScrollContainer/CenterContainer/MsgBlockButtons/NewBlockButton";

        // MsgBlock
        public const string MSG_BLOCK_SCENE_MOVE_BLOCK_UP_BUTTON = "Content/NavigationButtons/MoveUp";
        public const string MSG_BLOCK_SCENE_MOVE_BLOCK_DOWN_BUTTON = "Content/NavigationButtons/MoveDown";
        public const string MSG_BLOCK_SCENE_REMOVE_BLOCK_BUTTON = "Content/NavigationButtons/Remove";
    }
}