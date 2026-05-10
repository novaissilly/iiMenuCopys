using ShibaGTGenesis.Classes;
using static ShibaGTGenesis.Menu.Menu;

namespace ShibaGTGenesis.Menu
{
    internal class Buttons
    {
        public static ButtonInfo[][] buttons = new ButtonInfo[][]
        {
            new ButtonInfo[] { // Main Stuff [0]
                new ButtonInfo { buttonText = "Save Enabled Buttons", isTogglable = false, toolTip = "Opens the settings menu."},
                new ButtonInfo { buttonText = "Settings", isTogglable = false, toolTip = "Opens the settings menu."},
            },
        };
    }
}