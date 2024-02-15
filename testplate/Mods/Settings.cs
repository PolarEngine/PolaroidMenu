using Photon.Pun;
using UnityEngine;
using static PolaroidMenu.Menu.Main;
using static PolaroidMenu.Settings;

namespace PolaroidMenu.Mods
{
    internal class SettingsMods
    {
        public static void EnterSettings()
        {
            buttonsType = 1;
        }

        public static void MenuButtonTypeAdd()
        {
            string[] Themes = new string[]
            {
        "Rainbow","Purple","B&W"
            };

            themeType++;
            if (themeType > Themes.Length - 1)
            {
                themeType = 0;
            }

            if (themeType == 0)
            {
                Settings.backgroundColor = rainbowColor;
            }
            if (themeType == 1)
            {
                Settings.backgroundColor = Settings.magentaColor;
            }

            if (themeType == 2)
            {
                Settings.backgroundColor = Settings.redFadeGradient;
            }
            GetIndex("Theme").overlapText = "Theme [<color=green>" + Themes[themeType] + "</color>]";
        }

        public static void AddFlySpeed()
        {
            string[] flytypes = new string[]
            {
        "Normal","Fast","Very Fast"
            };

            flyType++;
            if (flyType > flytypes.Length - 1)
            {
                flyType = 0;
            }

            if (flyType == 0)
            {
                flySpeed = 15f;
            }
            if (flyType == 1)
            {
                flySpeed = 25f;
            }

            if (flyType == 2)
            {
                flySpeed = 45f;
            }
            GetIndex("Fly Speed").overlapText = "Fly Speed [<color=green>" + flytypes[flyType] + "</color>]";
        }

        public static void AntiReport()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerListOthers)
            {
                VRRig rig = GorillaGameManager.instance.FindPlayerVRRig(player).gameObject.GetComponentInParent<VRRig>();
                GameObject reportButtonstuff = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Terrain/campgroundstructure/scoreboard").gameObject;
                if (Vector3.Distance(reportButtonstuff.gameObject.transform.position, rig.rightHandTransform.gameObject.transform.position) < 2f)
                {
                    PhotonNetwork.Disconnect();
                }
                if (Vector3.Distance(reportButtonstuff.gameObject.transform.position, rig.leftHandTransform.gameObject.transform.position) < 2f)
                {
                    PhotonNetwork.Disconnect();
                }
            }
        }

        public static void ChangeNavigationType()
        {
            string[] navigationtypes = new string[]
            {
        "Buttons","Triggers"//,"Very Fast"
            };

            navType++;
            if (navType > navigationtypes.Length - 1)
            {
                navType = 0;
            }
            GetIndex("Page Type").overlapText = "Page Type [<color=green>" + navigationtypes[navType] + "</color>]";
        }


        public static void MenuSettings()
        {
            buttonsType = 2;
        }

        public static void MovementSettings()
        {
            buttonsType = 3;
        }

        public static void ProjectileSettings()
        {
            buttonsType = 4;
        }

        public static void RightHand()
        {
            rightHanded = true;
        }

        public static void LeftHand()
        {
            rightHanded = false;
        }

        public static void EnableFPSCounter()
        {
            fpsCounter = true;
        }

        public static void DisableFPSCounter()
        {
            fpsCounter = false;
        }

        public static void EnableNotifications()
        {
            disableNotifications = false;
        }

        public static void DisableNotifications()
        {
            disableNotifications = true;
        }

        public static void EnableDisconnectButton()
        {
            disconnectButton = true;
        }

        public static void DisableDisconnectButton()
        {
            disconnectButton = false;
        }
    }
}
