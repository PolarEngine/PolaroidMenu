using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PolaroidMenu.Mods
{
    internal class Visual
    {

        public static void ESP()
        {
            foreach(Photon.Realtime.Player lolf in PhotonNetwork.PlayerListOthers)
            {
                VRRig riglol = GorillaGameManager.instance.FindPlayerVRRig(lolf);
                riglol.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                riglol.mainSkin.material.color = Color.magenta;
            }
        }

        public static void FixESP()
        {
            foreach (Photon.Realtime.Player lolf in PhotonNetwork.PlayerListOthers)
            {
                VRRig riglol = GorillaGameManager.instance.FindPlayerVRRig(lolf);
                riglol.mainSkin.material.shader = Shader.Find("GorillaTag/UberShader");
                riglol.mainSkin.material.color = riglol.playerColor;
            }
        }
    }
}
