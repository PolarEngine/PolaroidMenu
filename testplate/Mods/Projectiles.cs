using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine.UIElements;

namespace PolaroidMenu.Mods
{
    internal class Projectiles
    {
        public static float projectileDeboucne = 0;
        public static GameObject projectileBlock = null;
        public static void SendProjEvent(in byte code, in object evData, in RaiseEventOptions reo, in SendOptions so)
        {
            object[] sendEventData = new object[3];
            sendEventData[0] = PhotonNetwork.ServerTimestamp;
            sendEventData[1] = code;
            sendEventData[2] = evData;
            PhotonNetwork.RaiseEvent(3, sendEventData, reo, so);
        }
        internal enum ProjectileSource
        {
            // Token: 0x04002228 RID: 8744
            Slingshot,
            // Token: 0x04002229 RID: 8745
            LeftHand,
            // Token: 0x0400222A RID: 8746
            RightHand
        }
        public static void ProjectileSpammer()
        {
            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.CustomProperties.ToString().Contains("MODDED"))
            {
                /*/RoomSystem.projectileSendData[0] = position;
                RoomSystem.projectileSendData[1] = velocity;
                RoomSystem.projectileSendData[2] = projectileSource;
                RoomSystem.projectileSendData[3] = projectileCount;
                RoomSystem.projectileSendData[4] = randomColour;
                RoomSystem.projectileSendData[5] = r;
                RoomSystem.projectileSendData[6] = g;
                RoomSystem.projectileSendData[7] = b;
                RoomSystem.projectileSendData[8] = a;
                byte b2 = 0;
                object obj = RoomSystem.projectileSendData;/*/
                object[] projectileSendData = new object[11];
                projectileSendData[0] = GorillaTagger.Instance.offlineVRRig.gameObject.transform.position;
                projectileSendData[1] = GorillaTagger.Instance.offlineVRRig.gameObject.transform.up;
                projectileSendData[2] = ProjectileSource.Slingshot;
                projectileSendData[3] = 1;
                projectileSendData[4] = true;
                projectileSendData[5] = 255f;
                projectileSendData[6] = 255f;
                projectileSendData[7] = 255f;
                projectileSendData[8] = 255f;
                byte code = 0;
                object evData = projectileSendData;
                RaiseEventOptions reoOthers = new RaiseEventOptions
                {
                    Receivers = ReceiverGroup.All
                };
                SendProjEvent(in code, in evData, in reoOthers, in SendOptions.SendUnreliable);
                if (Time.time > projectileDeboucne)
                {
                    projectileDeboucne = Time.time + 0.1f;
                }
            }
        }
    }
}
