using Godot;
using System;

namespace GlobalNameSpace
{
    public partial class NewTestButtonPress : Node
    {
        public override void _Ready()
        {
    /*         // Margin
            var marginContainer = new MarginContainer();
            marginContainer.SetAnchorsPreset(Control.LayoutPreset.FullRect);
            int marginValue = 100;
            marginContainer.AddThemeConstantOverride("margin_top", marginValue);
            marginContainer.AddThemeConstantOverride("margin_left", marginValue);
            marginContainer.AddThemeConstantOverride("margin_bottom", marginValue);
            marginContainer.AddThemeConstantOverride("margin_right", marginValue);
            AddChild(marginContainer);

            // Horizontal Split
            var hSplitContainer = new HSplitContainer();
            marginContainer.AddChild(hSplitContainer);

            // PanelContainer
            var pamelContainer1 = new PanelContainer();
            var pamelContainer2 = new PanelContainer();
            hSplitContainer.AddChild(pamelContainer1);
            hSplitContainer.AddChild(pamelContainer2); */


    /*         // Button
            var button1 = new Button
            {
                Text = "Click me"
            };
            button1.Pressed += ButtonPressed;
            vBoxContainer.AddChild(button1);


            var textEdit = new TextEdit
            {
                Text = "Click me"
            };
            centerContainer.AddChild(textEdit); */
        }

        private void ButtonPressed()
        {
            GD.Print("Hello world!");
            Program.NewMain();
        }
    }
}

/* namespace GlobalNameSpace
{
    public partial class TestButtonPress : Node
    {
        private Button testButton;

        public override void _Ready()
        {
            // Get the button node from the scene
            testButton = GetNode<Button>("Path/To/TestButton");

            // Set the button text (optional, since the text can also be set from the editor)
            testButton.Text = "Click me";

            // Connect the button's pressed signal to the ButtonPressed method
            testButton.Pressed += ButtonPressed;
        }

        private void ButtonPressed()
        {
            GD.Print("Hello world!");
            Program.NewMain();
        }
    }
} */