using PolaroidMenu.Classes;
using PolaroidMenu.Mods;
using UnityEngine;
using static PolaroidMenu.Settings;

namespace PolaroidMenu.Menu
{
    internal class Buttons 
    {
        public static ButtonInfo[][] buttons = new ButtonInfo[][]
        {
            new ButtonInfo[] { // Main Mods
                new ButtonInfo { buttonText = "Settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "Opens the main settings page for the menu."},
                new ButtonInfo { buttonText = "Movement Mods", method =() => Global.OpenMovement(), isTogglable = false, toolTip = "Opens the Movement Mods page."},
                new ButtonInfo { buttonText = "Visual Mods", method =() => Global.OpenPlayer(), isTogglable = false, toolTip = "Opens the Player Mods page."},
                new ButtonInfo { buttonText = "Overpowered Mods", method =() => Global.OpenOverpowered(), isTogglable = false, toolTip = "Opens the Overpowered Mods page."},
                //new ButtonInfo { buttonText = "ESP", isTogglable = true , enabled=false,toolTip="A Basic ESP Mod" ,method=() => Visual.ESP(), disableMethod=() => Visual.FixESP()},
                //new ButtonInfo { buttonText = "Maniac", isTogglable = true , enabled=false,toolTip="swings your arms everywhere" ,method=() => Player.Maniac()},
               // new ButtonInfo { buttonText = "Swim Everywhere", isTogglable = true , enabled=false,toolTip="lets you swim anywhere" ,method=() => Water.SwimEverywhere(), disableMethod=() => Water.FixSwimEverywhere()},
                //new ButtonInfo { buttonText = "Fast Swim", isTogglable = true , enabled=false,toolTip="makes it so you can swim very fast" ,method=() => Water.FastSwim()},

               // new ButtonInfo { buttonText = "No Clip", isTogglable = true , enabled=false,toolTip="No Clip" ,method=() => Player.NoClip()},
                //new ButtonInfo { buttonText = "Bone ESP [NW]", isTogglable = true , enabled=false,toolTip="it looks like fortnite aimbot" ,method=() => Visual.BoneESP()},
               
                //new ButtonInfo { buttonText = "Tracers", isTogglable = true , enabled=false,toolTip="A Basic Tracer Mod" ,method=() => Visual.Tracers()},
               // new ButtonInfo { buttonText = "No Trees", isTogglable = true , enabled=false,toolTip="makes all trees disappear" ,method=() => Visual.NoTrees(),disableMethod=() => Visual.Fixtrees() },
                //new ButtonInfo { buttonText = "Copy Movement Gun", isTogglable = true , enabled=false,toolTip="A Working Copy Movement" ,method=() => Player.CopyPlayerGun()},
                 //new ButtonInfo { buttonText = "Change Name All  [<color=green>AntiBan</color> [<color=orange>SS</color>]", isTogglable = false , enabled=false,toolTip="Changes everyone names server sided" ,method=() => Overpowered.SetNameAllSS()},
                 //new ButtonInfo { buttonText = "Projectile Spammer  [<color=green>UND</color>]", isTogglable = true , enabled=false,toolTip="spams projectiles" ,method=() => Projectiles.ProjectileSpammer()},
                //new ButtonInfo { buttonText = "Projectile Spammer [<color=green>UND</color>]", isTogglable = true , enabled=false,toolTip="A Working Projectile Spammer" ,method=() => Projectiles.ProjectileSpammer()},
                //new ButtonInfo { buttonText = "RGB [<color=green>UND</color>]", isTogglable = true , enabled=false,toolTip="A Working RGB" ,method=() => Player.RGB()},

            },

            new ButtonInfo[] { // Settings
                new ButtonInfo { buttonText = "Return to Main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "Menu", method =() => SettingsMods.MenuSettings(), isTogglable = false, toolTip = "Opens the settings for the menu."},
                new ButtonInfo { buttonText = "Movement", method =() => SettingsMods.MovementSettings(), isTogglable = false, toolTip = "Opens the movement settings for the menu."},
                new ButtonInfo { buttonText = "Time",overlapText="Time : Night", isTogglable = false , enabled=false,toolTip="switchs time" ,method=() => SettingsMods.AddToTImeType()},
                //new ButtonInfo { buttonText = "Anti-Report", method =() => SettingsMods.AntiReport(), isTogglable = true, toolTip = "AntiReport."},
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

            new ButtonInfo[] { // Overpowered Buttons
                new ButtonInfo { buttonText = "Return to Home", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Opens the home for the menu."},
                new ButtonInfo { buttonText = "Execute AntiBan", isTogglable = false , enabled=false,toolTip="execute order 66" ,method=() => Overpowered.AntiBan()},
                new ButtonInfo { buttonText = "Set Master", isTogglable = false , enabled=false,toolTip="A OP Set Master Mod" ,method=() => Overpowered.SetMaster()},
                new ButtonInfo { buttonText = "Tag All [M]", isTogglable = false , enabled=false,toolTip="A OP Tag All Mod" ,method=() => Overpowered.TagAll()},
                new ButtonInfo { buttonText = "Tag Self [M]", isTogglable = false , enabled=false,toolTip="A OP Tag Self Mod" ,method=() => Overpowered.TagSelf()},
                new ButtonInfo { buttonText = "Tag Gun [M]", isTogglable = true , enabled=false,toolTip="tags the person you land the gun on" ,method=() => Overpowered.TagGun()},
                new ButtonInfo { buttonText = "Untag All [M]", isTogglable = false , enabled=false,toolTip="A OP Untag All Mod" ,method=() => Overpowered.UntagAll()},
                new ButtonInfo { buttonText = "Untag Self [M]", isTogglable = false , enabled=false,toolTip="A OP Untag self Mod" ,method=() => Overpowered.UntagSelf()},
                new ButtonInfo { buttonText = "Anti-Tag [M]", isTogglable = true , enabled=false,toolTip="A OP Anti Tag Mod" ,method=() => Overpowered.AntiTag()},
                new ButtonInfo { buttonText = "Kill All [<color=orange>BRAWL</color>] [M]", isTogglable = false , enabled=false,toolTip="A OP Kill All Mod" ,method=() => Overpowered.KillAll()},
                new ButtonInfo { buttonText = "Kill Gun [<color=orange>BRAWL</color>] [M]", isTogglable = false , enabled=false,toolTip="A OP Kill Gun Mod" ,method=() => Overpowered.KillGun()},
                new ButtonInfo { buttonText = "Reive All [<color=orange>BRAWL</color>] [M]", isTogglable = false , enabled=false,toolTip="A OP Revive All Mod" ,method=() => Overpowered.ReviveAll()},
                new ButtonInfo { buttonText = "Reive Self [<color=orange>BRAWL</color>] [M]", isTogglable = false , enabled=false,toolTip="A OP Revive All Mod" ,method=() => Overpowered.ReviveSelf()},
                new ButtonInfo { buttonText = "Anti-Kill  [<color=orange>BRAWL</color>] [M]", isTogglable = true , enabled=false,toolTip="A OP AntiKill Mod" ,method=() => Overpowered.AntiKill()},
                new ButtonInfo { buttonText = "Kill Spam  [<color=orange>BRAWL</color>] [M]", isTogglable = true , enabled=false,toolTip="A OP Paintbrawl balloon spam Mod" ,method=() => Overpowered.PaintbrawlSpam()},
                new ButtonInfo { buttonText = "Kill Spam Gun [<color=orange>BRAWL</color>] [M]", isTogglable = true , enabled=false,toolTip="a gun that makes peoples balloons spam" ,method=() => Overpowered.KillSpamGun()},
                new ButtonInfo { buttonText = "Acid All [<color=red>M</color>]", isTogglable = false , enabled=false,toolTip="A OP Acid All Mod" ,method=() => Overpowered.AcidAll()},
                new ButtonInfo { buttonText = "Unacid All", isTogglable = false , enabled=false,toolTip="A OP Unacid All Mod" ,method=() => Overpowered.UnacidAll()},
                //new ButtonInfo { buttonText = "Lag All [ANTIBAN]  [<color=green>UND</color>]", isTogglable = true , enabled=false,toolTip="lags everyone" ,method=() => Overpowered.LagAll()},
                 //new ButtonInfo { buttonText = "Lag Gun [ANTIBAN]  [<color=green>UND</color>]", isTogglable = true , enabled=false,toolTip="gun that makes people lag" ,method=() => Overpowered.LagGun()},
                 new ButtonInfo { buttonText = "Destroy All  [<color=green>UND</color>]", isTogglable = false , enabled=false,toolTip="makes every new player see nobody" ,method=() => Overpowered.DestroyAllFix()},
                 new ButtonInfo { buttonText = "Freeze All  [<color=green>AntiBan</color>]", isTogglable = true , enabled=false,toolTip="freezes all vrrigs on all interfaces" ,method=() => Overpowered.FreezeAll()},
                 new ButtonInfo { buttonText = "Freeze Gun  [<color=green>AntiBan</color>]", isTogglable = true , enabled=false,toolTip="freezes the gunned vrrig on all interfaces" ,method=() => Overpowered.FreezeGun()},
                 new ButtonInfo { buttonText = "Crash All  [<color=green>AntiBan</color>]", isTogglable = true , enabled=false,toolTip="crashes all" ,method=() => Overpowered.FreezeAll()},
                  new ButtonInfo { buttonText = "Crash Gun  [<color=green>AntiBan</color>]", isTogglable = true , enabled=false,toolTip="crash gun" ,method=() => Overpowered.CrashGun()},
                 /*/new ButtonInfo { buttonText = "Spaz Lava  [<color=red>M</color>]", isTogglable = true , enabled=false,toolTip="spams forcing lava to every state" ,method=() => Overpowered.SpazLava()},
                 new ButtonInfo { buttonText = "Force Errupt Lava  [<color=red>M</color>]", isTogglable = false , enabled=false,toolTip="forces the lava to erupt" ,method=() => Overpowered.ForceEruptLava()},
                 new ButtonInfo { buttonText = "Force UnErrupt Lava  [<color=red>M</color>]", isTogglable = false , enabled=false,toolTip="forces the lava to unerupt" ,method=() => Overpowered.ForceUneruptLava()},
                 new ButtonInfo { buttonText = "Force Rise Lava  [<color=red>M</color>]", isTogglable = false , enabled=false,toolTip="forces the lava to Rise" ,method=() => Overpowered.ForceRiseLava()},
                 new ButtonInfo { buttonText = "Force Drain Lava  [<color=red>M</color>]", isTogglable = false , enabled=false,toolTip="forces the lava to Drain" ,method=() => Overpowered.ForceDrainLava()},/*/
                 new ButtonInfo { buttonText = "Daisy09 Name All [<color=green>AntiBan</color>]", isTogglable = true , enabled=false,toolTip="changes all names to daisy09" ,method=() => Overpowered.DaisyTroll()},
                 new ButtonInfo { buttonText = "PBBV Name All [<color=green>AntiBan</color>]", isTogglable = true , enabled=false,toolTip="changes all names to pbbv" ,method=() => Overpowered.PBBVTroll()},
                 new ButtonInfo { buttonText = "Banshee Name All [<color=green>AntiBan</color>]", isTogglable = true , enabled=false,toolTip="changes all names to Banshee" ,method=() => Overpowered.BanSheeTroll()},
                 new ButtonInfo { buttonText = "Echo Name All [<color=green>AntiBan</color>]", isTogglable = true , enabled=false,toolTip="changes all names to Echo" ,method=() => Overpowered.EchoTroll()},
                 new ButtonInfo { buttonText = "SS Name All [<color=green>AntiBan</color>]", isTogglable = true , enabled=false,toolTip="changes all names to your name" ,method=() => Overpowered.NameChangeAll()},
                 new ButtonInfo { buttonText = "Glitch Leaderboard [<color=green>AntiBan</color>]", isTogglable = true , enabled=false,toolTip="Swaps out names" ,method=() => Overpowered.GlitchLeaderboard()},
                  new ButtonInfo { buttonText = "Float Player Gun [<color=red>M</color>]", isTogglable = true , enabled=false,toolTip="floats the player you use it on" ,method=() => Overpowered.FloatGun()},
                  new ButtonInfo { buttonText = "Get Honey Comb [<color=magenta>LG & RG</color>]", isTogglable = true , enabled=false,toolTip="grabs the honey combs" ,method=() => Overpowered.GetHoneyComb()},
                  new ButtonInfo { buttonText = "Gamemode > Casual [<color=green>AntiBan</color>]", isTogglable = false , enabled=false,toolTip="changes gamemode" ,method=() => Overpowered.ChangeGamemode("CASUAL")},
                  new ButtonInfo { buttonText = "Gamemode > Infection [<color=green>AntiBan</color>]", isTogglable = false , enabled=false,toolTip="changes gamemode" ,method=() => Overpowered.ChangeGamemode("INFECTION")},
                  new ButtonInfo { buttonText = "Gamemode > Hunt [<color=green>AntiBan</color>]", isTogglable = false , enabled=false,toolTip="changes gamemode" ,method=() => Overpowered.ChangeGamemode("HUNT")},
                  new ButtonInfo { buttonText = "Gamemode > Brawl [<color=green>AntiBan</color>]", isTogglable = false , enabled=false,toolTip="changes gamemode" ,method=() => Overpowered.ChangeGamemode("BRAWL")},
                  
                
                
                 //new ButtonInfo { buttonText = "RGB All  [<color=green>AntiBan & Stump</color>]", isTogglable = true , enabled=false,toolTip="rgb all" ,method=() => Overpowered.RGBAll()},
                 //new ButtonInfo { buttonText = "Mat Spam [<color=green>AntiBan</color>]", isTogglable = true , enabled=false,toolTip="spams all materials on players" ,method=() => Overpowered.MatSpam()},
                // new ButtonInfo { buttonText = "Kick All [<color=green>AntiBan</color>]", isTogglable = false , enabled=false,toolTip="Kick All" ,method=() => Overpowered.KickAll()},
            },

            new ButtonInfo[] { // Movement Buttons
                new ButtonInfo { buttonText = "Return to Home", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Opens the home for the menu."},
                                new ButtonInfo { buttonText = "Platforms", isTogglable = true , enabled=false,toolTip="A Basic platform mod" ,method=() => Movement.Platforms(false,false)},
                                new ButtonInfo { buttonText = "Sticky Platforms", isTogglable = true , enabled=false,toolTip="A Basic sticky platform mod" ,method=() => Movement.Platforms(false,true)},
                                new ButtonInfo { buttonText = "Invis Platforms", isTogglable = true , enabled=false,toolTip="A Basic sticky platform mod" ,method=() => Movement.Platforms(true,true)},
                new ButtonInfo { buttonText = "Trigger Platforms", isTogglable = true , enabled=false,toolTip="A Basic trigger platform mod" ,method=() => Movement.TriggerPlatforms(false,true)},
                new ButtonInfo { buttonText = "Force Tag Freeze", isTogglable = true , enabled=false,toolTip="it gives you tag freeze" ,method=() => Player.PBBVWalk(), disableMethod=() => Player.NoTagFreeze()},
                new ButtonInfo { buttonText = "No Tag Freeze", isTogglable = true , enabled=false,toolTip="it makes it so you dont freeze opon tagged" ,method=() => Player.NoTagFreeze()},
                new ButtonInfo { buttonText = "Fly [A]", isTogglable = true , enabled=false,toolTip="A Basic fly mod with speed optimization in settings" ,method=() => Movement.Fly()},
                new ButtonInfo { buttonText = "Trigger Fly", isTogglable = true , enabled=false,toolTip="A Basic trigger fly mod with speed optimization in settings" ,method=() => Movement.TriggerFly()},
                new ButtonInfo { buttonText = "Hands Fly [A]", isTogglable = true , enabled=false,toolTip="A fly based of your hands" ,method=() => Movement.HandFly()},
                new ButtonInfo { buttonText = "Up And Down", isTogglable = true , enabled=false,toolTip="A Basic Up And Down Mod" ,method=() => Movement.UpAndDown()},
                new ButtonInfo { buttonText = "Iron Monke", isTogglable = true , enabled=false,toolTip="A Basic Iron Monke Mod" ,method=() => Movement.IronMonke()},
                //new ButtonInfo { buttonText = "Checkpoint", isTogglable = true , enabled=false,toolTip="A Basic Checkpoint Mod" ,method=() => Movement.Checkpoint()},
                new ButtonInfo { buttonText = "SpiderMonk", isTogglable = true , enabled=false,toolTip="A Basic SpiderMan Mod" ,method=() => Movement.SpiderMan(), disableMethod=() => Movement.DisableSpiderMan()},
                 new ButtonInfo { buttonText = "WallWalk  [<color=magenta>COMP</color>]", isTogglable = true , enabled=false,toolTip="wall walk" ,method=() => Movement.WallWalk()},
                new ButtonInfo { buttonText = "Speed Boost  [<color=magenta>COMP</color>]", isTogglable = true , enabled=false,toolTip="very good speedboost" ,method=() => { GorillaLocomotion.Player.Instance.maxJumpSpeed = 6.4f;GorillaLocomotion.Player.Instance.jumpMultiplier = 1.4f; } },
                new ButtonInfo { buttonText = "Low Gravity", method =() => Movement.LowGravity(), toolTip = "low gravity"},
                new ButtonInfo { buttonText = "Zero Gravity", method =() => Movement.ZeroGravity(), toolTip = "fuck gravity"},
                new ButtonInfo { buttonText = "High Gravity", method =() => Movement.HighGravity(), toolTip = "higher gravity"},
                new ButtonInfo { buttonText = "Spaz Head", isTogglable = true , enabled=false,toolTip="makes your head have a seizure" ,method=() => Player.HeadSeizure(),disableMethod=() => Player.FixHead() },
                new ButtonInfo { buttonText = "GhostMonk", isTogglable = true , enabled=false,toolTip="A Working Ghost Monk" ,method=() => Player.GhostMonk()},
                new ButtonInfo { buttonText = "InvisMonk", isTogglable = true , enabled=false,toolTip="A Working Invis Monk" ,method=() => Player.InvisMonk()},
                new ButtonInfo { buttonText = "Grab Rig", isTogglable = true , enabled=false,toolTip="lets you grab your own rig" ,method=() => Player.GrabRig()},
                new ButtonInfo { buttonText = "No Clip", isTogglable = true , enabled=false,toolTip="No Clip" ,method=() => Player.NoClip()},
                new ButtonInfo { buttonText = "Long Arms", isTogglable = true , enabled=false,toolTip="it gives you longer arms" ,method=() => Player.LongArms(),disableMethod=() => Player.FixLongArms()},
            },

            new ButtonInfo[] { // Visual Buttons
                new ButtonInfo { buttonText = "Return to Home", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Opens the home for the menu."},
                new ButtonInfo { buttonText = "Casual Bone ESP", isTogglable = true , enabled=false,toolTip="Bone ESP Casual" ,method=() => Visual.BoneESP(),disableMethod=() => Visual.BoneESPOff() },
                new ButtonInfo { buttonText = "Casual Tracers", isTogglable = true , enabled=false,toolTip="Tracers Casual" ,method=() => Visual.Tracers(),disableMethod=() => Visual.TracersOff() },
            },

        };
    }
}
