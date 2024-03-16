using BepInEx;
using GorillaNetworking;
using Photon.Pun;
using PlayFab;
using PolaroidMenu.Classes;
using PolaroidMenu.Menu;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

namespace PolaroidMenu.Patches
{
    [Description(PolaroidMenu.PluginInfo.Description)]
    [BepInPlugin(PolaroidMenu.PluginInfo.GUID, PolaroidMenu.PluginInfo.Name, PolaroidMenu.PluginInfo.Version)]
    public class HarmonyPatches : BaseUnityPlugin
    {
        public bool isInit = false;
        public void Update()
        {
            if (GorillaLocomotion.Player.Instance  != null && !isInit)
            {
                GorillaLocomotion.Player.Instance.AddComponent<Main>();
                GorillaLocomotion.Player.Instance.AddComponent<ShaderFix>();
                isInit = true;
            }

            
                foreach (ButtonInfo[] buttonlist in Buttons.buttons)
                {
                    foreach (ButtonInfo v in buttonlist)
                    {
                        if (v.enabled)
                        {
                            if (v.method != null)
                            {
                                v.method.Invoke();


                            }
                        }
                    }
                }
            
        }

        public void OnGUI()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach(Photon.Realtime.Player plr in PhotonNetwork.PlayerListOthers)
                {
                    GUILayout.Label(plr.NickName + " | " + plr.UserId);
                }
            }
        }
        private void OnEnable()
        {
            Menu.ApplyHarmonyPatches();
        }

        

        private void OnDisable()
        {
            Menu.RemoveHarmonyPatches();
        }

       


    }
}
