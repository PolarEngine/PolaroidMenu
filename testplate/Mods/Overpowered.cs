using ExitGames.Client.Photon.Encryption;
using GorillaNetworking;
using GorillaTag;
using Photon.Pun;
using PlayFab;
using PolaroidMenu.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace PolaroidMenu.Mods
{
    internal class Overpowered
    {

        public static void FlushRpcs()
        {
            if (PhotonNetwork.InRoom)
            {
                GorillaNot.instance.rpcCallLimit = int.MaxValue;
                PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
                PhotonNetwork.OpCleanActorRpcBuffer(PhotonNetwork.LocalPlayer.ActorNumber);
                PhotonNetwork.OpCleanRpcBuffer(GorillaTagger.Instance.myVRRig);
                PhotonNetwork.RemoveBufferedRPCs(GorillaTagger.Instance.myVRRig.ViewID, null, null);
            }
        }

        public static void UnacidAll()
        {
            PhotonView sciencephotonview = ScienceExperimentManager.instance.photonView;
            foreach (Photon.Realtime.Player pl in PhotonNetwork.PlayerList)
            {
                sciencephotonview.RPC("PlayerHitByWaterBalloonRPC", RpcTarget.MasterClient, pl.ActorNumber);
                FlushRpcs();
            }
        }

        public static void AntiBan()
        {
            if (!PhotonNetwork.CurrentRoom.CustomProperties.ToString().Contains("MODDED"))
            {
                PlayFabClientAPI.ExecuteCloudScript(new PlayFab.ClientModels.ExecuteCloudScriptRequest
                {
                    FunctionName = "RoomClosed",
                    FunctionParameter = new
                    {
                        GameId = PhotonNetwork.CurrentRoom.Name,
                        Region = Regex.Replace(PhotonNetwork.CloudRegion, "[^a-zA-Z0-9]", "").ToUpper(),
                        UserId = PhotonNetwork.PlayerList[UnityEngine.Random.Range(0, PhotonNetwork.PlayerList.Length + 1)].UserId,
                        ActorNr = PhotonNetwork.PlayerList[UnityEngine.Random.Range(0, PhotonNetwork.PlayerList.Length + 1)],
                        ActorCount = PhotonNetwork.ViewCount,
                        AppVersion = PhotonNetwork.AppVersion
                    },
                }, result =>
                {
                    NotifiLib.SendNotification("<color=grey>[</color><color=purple>ANTIBAN</color><color=grey>]</color> <color=white>ANTI-BAN HAS BEEN EXECUTED</color>");
                }, null);
                string gamemode = PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString().Replace(GorillaComputer.instance.currentQueue, GorillaComputer.instance.currentQueue + "MODDEDMODDED");
                ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable
                {
                    { "gameMode", gamemode }
                };
                PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
            }
        }

        public static void SetMaster()
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties.ToString().Contains("MODDED"))
            {
                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
            }
        }

        public static void TagAll()
        {
            if (PhotonNetwork.InRoom)
            {
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    foreach(Photon.Realtime.Player lf in PhotonNetwork.PlayerListOthers)
                    {
                        GameObject.Find("Gorilla Tag Manager").GetComponent<GorillaTagManager>().AddInfectedPlayer(lf);
                    }
                }
            }
        }

        public static void UntagAll()
        {
            if (PhotonNetwork.InRoom)
            {
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    foreach (Photon.Realtime.Player lf in PhotonNetwork.PlayerListOthers)
                    {
                        GameObject.Find("Gorilla Tag Manager").GetComponent<GorillaTagManager>().currentInfected.Remove(lf);
                    }
                }
            }
        }

        public static void AntiTag()
        {
            if (PhotonNetwork.InRoom)
            {
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                  GameObject.Find("Gorilla Tag Manager").GetComponent<GorillaTagManager>().currentInfected.Remove(PhotonNetwork.LocalPlayer);
                }
            }
        }

        public static void UntagSelf()
        {
            if (PhotonNetwork.InRoom)
            {
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    GameObject.Find("Gorilla Tag Manager").GetComponent<GorillaTagManager>().currentInfected.Remove(PhotonNetwork.LocalPlayer);
                }
            }
        }

        public static void TagSelf()
        {
            if (PhotonNetwork.InRoom)
            {
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    GameObject.Find("Gorilla Tag Manager").GetComponent<GorillaTagManager>().currentInfected.Add(PhotonNetwork.LocalPlayer);
                }
            }
        }

        public static void AntiKill()
        {
            if (PhotonNetwork.InRoom)
            {
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                        GameObject.Find("Gorilla Battle Manager").GetComponent<GorillaBattleManager>().playerLives[PhotonNetwork.LocalPlayer.ActorNumber] = 3;
                }
            }

        }

        public static void ReviveSelf()
        {
            if (PhotonNetwork.InRoom)
            {
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    GameObject.Find("Gorilla Battle Manager").GetComponent<GorillaBattleManager>().playerLives[PhotonNetwork.LocalPlayer.ActorNumber] = 3;

                }
            }

        }

        public static void ReviveAll()
        {
            if (PhotonNetwork.InRoom)
            {
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    foreach (Photon.Realtime.Player lf in PhotonNetwork.PlayerList)
                    {
                        GameObject.Find("Gorilla Battle Manager").GetComponent<GorillaBattleManager>().playerLives[lf.ActorNumber] = 3;
                    }
                }
            }

        }
        public static void KillAll()
        {
            if (PhotonNetwork.InRoom)
            {
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    foreach (Photon.Realtime.Player lf in PhotonNetwork.PlayerList)
                    {
                        GameObject.Find("Gorilla Battle Manager").GetComponent<GorillaBattleManager>().playerLives[lf.ActorNumber] = 0;
                    }
                }
            }
        }

        public static void PaintbrawlSpam()
        {
            if (PhotonNetwork.InRoom)
            {
                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    foreach (Photon.Realtime.Player lf in PhotonNetwork.PlayerListOthers)
                    {
                        GameObject.Find("Gorilla Battle Manager").GetComponent<GorillaBattleManager>().playerLives[lf.ActorNumber] = UnityEngine.Random.Range(0, 4);
                    }
                }
            }
        }
    }
}
