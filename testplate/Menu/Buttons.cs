using PolaroidMenu.Classes;
using PolaroidMenu.Mods;
using static PolaroidMenu.Settings;

namespace PolaroidMenu.Menu
{
    internal class Buttons
    {
        public static ButtonInfo[][] buttons = new ButtonInfo[][]
        {
            new ButtonInfo[] { // Main Mods
                new ButtonInfo { buttonText = "Settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "Opens the main settings page for the menu."},
                new ButtonInfo { buttonText = "Platforms", isTogglable = true , enabled=false,toolTip="A Basic platform mod" ,method=() => Movement.Platforms(false,true)},
                new ButtonInfo { buttonText = "Trigger Platforms", isTogglable = true , enabled=false,toolTip="A Basic trigger platform mod" ,method=() => Movement.TriggerPlatforms(false,true)},
                new ButtonInfo { buttonText = "Fly [A]", isTogglable = true , enabled=false,toolTip="A Basic fly mod with speed optimization in settings" ,method=() => Movement.Fly()},
                new ButtonInfo { buttonText = "Trigger Fly", isTogglable = true , enabled=false,toolTip="A Basic trigger fly mod with speed optimization in settings" ,method=() => Movement.TriggerFly()},
                new ButtonInfo { buttonText = "Up And Down", isTogglable = true , enabled=false,toolTip="A Basic Up And Down Mod" ,method=() => Movement.UpAndDown()},
                new ButtonInfo { buttonText = "Iron Monke", isTogglable = true , enabled=false,toolTip="A Basic Iron Monke Mod" ,method=() => Movement.IronMonke()},
                new ButtonInfo { buttonText = "ESP", isTogglable = true , enabled=false,toolTip="A Basic ESP Mod" ,method=() => Visual.ESP(), disableMethod=() => Visual.FixESP()},
                new ButtonInfo { buttonText = "Activate AntiBan", isTogglable = false , enabled=false,toolTip="A OP AntiBan Mod" ,method=() => Overpowered.AntiBan()},
                new ButtonInfo { buttonText = "Set Master", isTogglable = false , enabled=false,toolTip="A OP Set Master Mod" ,method=() => Overpowered.SetMaster()},
                new ButtonInfo { buttonText = "Tag All [M]", isTogglable = false , enabled=false,toolTip="A OP Tag All Mod" ,method=() => Overpowered.TagAll()},
                new ButtonInfo { buttonText = "Untag All [M]", isTogglable = false , enabled=false,toolTip="A OP Untag All Mod" ,method=() => Overpowered.UntagAll()},
                new ButtonInfo { buttonText = "Anti-Tag [M]", isTogglable = true , enabled=false,toolTip="A OP Anti Tag Mod" ,method=() => Overpowered.AntiTag()},
                new ButtonInfo { buttonText = "Kill All [<color=orange>BRAWL</color>] [M]", isTogglable = false , enabled=false,toolTip="A OP Kill All Mod" ,method=() => Overpowered.KillAll()},
                new ButtonInfo { buttonText = "Reive All [<color=orange>BRAWL</color>] [M]", isTogglable = false , enabled=false,toolTip="A OP Revive All Mod" ,method=() => Overpowered.ReviveAll()},
                new ButtonInfo { buttonText = "Reive Self [<color=orange>BRAWL</color>] [M]", isTogglable = false , enabled=false,toolTip="A OP Revive All Mod" ,method=() => Overpowered.ReviveSelf()},
                new ButtonInfo { buttonText = "Anti-Kill  [<color=orange>BRAWL</color>] [M]", isTogglable = true , enabled=false,toolTip="A OP AntiKill Mod" ,method=() => Overpowered.AntiKill()},
                new ButtonInfo { buttonText = "Kill Spam  [<color=orange>BRAWL</color>] [M]", isTogglable = true , enabled=false,toolTip="A OP Paintbrawl balloon spam Mod" ,method=() => Overpowered.PaintbrawlSpam()},
                new ButtonInfo { buttonText = "Unacid All  [<color=orange>SCIENCE</color>]", isTogglable = false , enabled=false,toolTip="A OP Unacid All Mod" ,method=() => Overpowered.UnacidAll()},

            },

            new ButtonInfo[] { // Settings
                new ButtonInfo { buttonText = "Return to Main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "Menu", method =() => SettingsMods.MenuSettings(), isTogglable = false, toolTip = "Opens the settings for the menu."},
                new ButtonInfo { buttonText = "Movement", method =() => SettingsMods.MovementSettings(), isTogglable = false, toolTip = "Opens the movement settings for the menu."},
                new ButtonInfo { buttonText = "Anti-Report", method =() => SettingsMods.AntiReport(), isTogglable = true, toolTip = "AntiReport."},
                //new ButtonInfo { buttonText = "Projectile", method =() => SettingsMods.ProjectileSettings(), isTogglable = false, toolTip = "Opens the projectile settings for the menu."},
            },

            new ButtonInfo[] { // Menu Settings
                new ButtonInfo { buttonText = "Return to Settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "Returns to the main settings page for the menu."},
                new ButtonInfo { buttonText = "Right Hand", enableMethod =() => SettingsMods.RightHand(), disableMethod =() => SettingsMods.LeftHand(), toolTip = "Puts the menu on your right hand."},
                new ButtonInfo { buttonText = "Notifications", enableMethod =() => SettingsMods.EnableNotifications(), disableMethod =() => SettingsMods.DisableNotifications(), enabled = !disableNotifications, toolTip = "Toggles the notifications."},
                new ButtonInfo { buttonText = "FPS Counter", enableMethod =() => SettingsMods.EnableFPSCounter(), disableMethod =() => SettingsMods.DisableFPSCounter(), enabled = fpsCounter, toolTip = "Toggles the FPS counter."},
                new ButtonInfo { buttonText = "Disconnect Button", enableMethod =() => SettingsMods.EnableDisconnectButton(), disableMethod =() => SettingsMods.DisableDisconnectButton(), enabled = disconnectButton, toolTip = "Toggles the disconnect button."},
                new ButtonInfo { buttonText = "Theme" ,overlapText="Theme [<color=green>Rainbow</color>]", method =() => SettingsMods.MenuButtonTypeAdd(), isTogglable = false, toolTip = "changes menu theme."},
                new ButtonInfo { buttonText = "Page Type" ,overlapText="Page Type [<color=green>Buttons</color>]", method =() => SettingsMods.ChangeNavigationType(), isTogglable = false, toolTip = "changes menu button navigation type."},
            },

            new ButtonInfo[] { // Movement Settings
                new ButtonInfo { buttonText = "Return to Settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "Returns to the main settings page for the menu."},
                new ButtonInfo { buttonText = "Fly Speed" ,overlapText="Fly Speed [<color=green>Normal</color>]", method =() => SettingsMods.AddFlySpeed(), isTogglable = false, toolTip = "changes fly speed."},
            },

            new ButtonInfo[] { // Projectile Settings
                new ButtonInfo { buttonText = "Return to Settings", method =() => SettingsMods.MenuSettings(), isTogglable = false, toolTip = "Opens the settings for the menu."},
            },
        };
    }
}
