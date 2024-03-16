using ExitGames.Client.Photon; 
using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon.Encryption;
using GorillaNetworking;
using GorillaTag;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.PUN;
using PlayFab;
using PlayFab.Internal;
using PolaroidMenu.Notifications;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using PolaroidMenu.Menu;

namespace PolaroidMenu.Mods
{
    internal class Overpowered : MonoBehaviour
    {
        private static object[] tagsoundData = new object[1];
        private static float slowallDebounce = 0;

        /*/internal static void SendEvent(in byte code, in object evData, in RaiseEventOptions reo, in SendOptions so)
        {
            RoomSystem.sendEventData[0] = PhotonNetwork.ServerTimestamp;
            RoomSystem.sendEventData[1] = code;
            RoomSystem.sendEventData[2] = evData;
            PhotonNetwork.RaiseEvent(3, RoomSystem.sendEventData, reo, so);
        }

        public static void TagSoundSpam()
        {
            if (Time.time > slowallDebounce)
            {
                slowallDebounce = Time.time + 0.2f;
                foreach(Photon.Realtime.Player p in PhotonNetwork.PlayerListOthers)
                {
                    tagsoundData[0] = sound.id;
                    tagsoundData[1] = sound.volume;
                    tagsoundData[2] = target;
                    byte b = 3;
                    object obj = tagsoundData;
                    RoomSystem.SendEvent(b, obj, RoomSystem.reoOthers, RoomSystem.soUnreliable);
                }
            }
        }/*/


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

        public static ExitGames.Client.Photon.Hashtable removeFilter = new ExitGames.Client.Photon.Hashtable();
        public static ExitGames.Client.Photon.Hashtable ServerCleanDestroyEvent = new ExitGames.Client.Photon.Hashtable();
        public static RaiseEventOptions ServerCleanOptions = new RaiseEventOptions();
        static VRRig LockedOnRig = null;
        public static GameObject pointer;
        public static PhotonView GetPhotonViewFromVRRig(VRRig p)
        {
            GameObject RigParent = GameObject.Find("Rig Parent");
            GameObject RigCache = GameObject.Find("RigCache");
            GameObject NetworkParent = GameObject.Find("Network Parent");
            if (p == null || p.isOfflineVRRig)
            {
                return null;
            }
            else if (NetworkParent)
            {
                var ChildrenComponents = NetworkParent.GetComponentsInChildren<PhotonView>();
                foreach (PhotonView PV in ChildrenComponents)
                {
                    if (PV.Owner.NickName == p.playerText.text)
                    {
                        return PV;
                    }
                }
            }
            return null;
        }



        public static bool IsAntiban()
        {
            if (PhotonNetwork.InRoom)
            {
                if (PhotonNetwork.CurrentRoom.CustomProperties.ToString().Contains("MODDED"))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsAntibanMaster()
        {
            if (PhotonNetwork.InRoom)
            {
                if (PhotonNetwork.CurrentRoom.CustomProperties.ToString().Contains("MODDED") && PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    return true;
                }
            }
            return false;
        }
        public static void LagAll()
        {
            if (IsAntiban())
            {
                Hashtable lofl = new Hashtable();
                lofl[0] = -1;
                PhotonNetwork.CurrentRoom.LoadBalancingClient.OpRaiseEvent(207, lofl, null, SendOptions.SendReliable);
            }
        }
        public static void DaisyTroll()
        {
            if (IsAntiban())
            {
                    Photon.Realtime.Player plr = PhotonNetwork.PlayerListOthers[UnityEngine.Random.Range(0,PhotonNetwork.PlayerListOthers.Length + 1)];
                    plr.NickName = "DAISY09";
                    System.Type targ = typeof(Photon.Realtime.Player);
                    MethodInfo StartEruptionMethod = targ.GetMethod("SetPlayerNameProperty", BindingFlags.NonPublic | BindingFlags.Instance);
                    StartEruptionMethod?.Invoke(plr, new object[] { });
            }
        }

        public static void PBBVTroll()
        {
            if (IsAntiban())
            {
                Photon.Realtime.Player plr = PhotonNetwork.PlayerListOthers[UnityEngine.Random.Range(0, PhotonNetwork.PlayerListOthers.Length + 1)];
                plr.NickName = "PBBV";
                System.Type targ = typeof(Photon.Realtime.Player);
                MethodInfo StartEruptionMethod = targ.GetMethod("SetPlayerNameProperty", BindingFlags.NonPublic | BindingFlags.Instance);
                StartEruptionMethod?.Invoke(plr, new object[] { });
            }
        }

        public static void EchoTroll()
        {
            if (IsAntiban())
            {
                Photon.Realtime.Player plr = PhotonNetwork.PlayerListOthers[UnityEngine.Random.Range(0, PhotonNetwork.PlayerListOthers.Length + 1)];
                plr.NickName = "Echo";
                System.Type targ = typeof(Photon.Realtime.Player);
                MethodInfo StartEruptionMethod = targ.GetMethod("SetPlayerNameProperty", BindingFlags.NonPublic | BindingFlags.Instance);
                StartEruptionMethod?.Invoke(plr, new object[] { });
            }
        }

        public static void BanSheeTroll()
        {
            if (IsAntiban())
            {
                Photon.Realtime.Player plr = PhotonNetwork.PlayerListOthers[UnityEngine.Random.Range(0, PhotonNetwork.PlayerListOthers.Length + 1)];
                plr.NickName = "Banshee";
                System.Type targ = typeof(Photon.Realtime.Player);
                MethodInfo StartEruptionMethod = targ.GetMethod("SetPlayerNameProperty", BindingFlags.NonPublic | BindingFlags.Instance);
                StartEruptionMethod?.Invoke(plr, new object[] { });
            }
        }

        public static void NameChangeAll()
        {
            if (IsAntiban())
            {
                Photon.Realtime.Player plr = PhotonNetwork.PlayerListOthers[UnityEngine.Random.Range(0, PhotonNetwork.PlayerListOthers.Length + 1)];
                plr.NickName = PhotonNetwork.LocalPlayer.NickName;
                System.Type targ = typeof(Photon.Realtime.Player);
                MethodInfo StartEruptionMethod = targ.GetMethod("SetPlayerNameProperty", BindingFlags.NonPublic | BindingFlags.Instance);
                StartEruptionMethod?.Invoke(plr, new object[] { });
            }
        }
        public static void MatSpam()
        {
            if (IsAntibanMaster())
            {
                GorillaTagManager tagManager = GameObject.Find("Gorilla Tag Manager").GetComponent<GorillaTagManager>();
                Photon.Realtime.Player plr = PhotonNetwork.PlayerListOthers[UnityEngine.Random.Range(0, PhotonNetwork.PlayerListOthers.Length + 1)];
                if (tagManager.currentIt != plr & tagManager.currentInfected.Contains(plr))
                {
                   // tagManager.currentIt = plr;
                    tagManager.currentInfected.Remove(plr);
                }

                if (tagManager.currentIt != plr & !tagManager.currentInfected.Contains(plr))
                {
                    tagManager.AddInfectedPlayer(plr);
                }
            }
        }



        public static void GlitchLeaderboard()
        {
            if (IsAntiban())
            {
                Photon.Realtime.Player plr = PhotonNetwork.PlayerListOthers[UnityEngine.Random.Range(0, PhotonNetwork.PlayerListOthers.Length + 1)];
                plr.NickName = PhotonNetwork.PlayerListOthers[UnityEngine.Random.Range(0, PhotonNetwork.PlayerListOthers.Length + 1)].NickName;
                System.Type targ = typeof(Photon.Realtime.Player);
                MethodInfo StartEruptionMethod = targ.GetMethod("SetPlayerNameProperty", BindingFlags.NonPublic | BindingFlags.Instance);
                StartEruptionMethod?.Invoke(plr, new object[] { });
            }
        }


        public static void FreezeAll()
        {
            if (IsAntiban())
            {
                RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                {
                    CachingOption = EventCaching.RemoveFromRoomCache,
                    Receivers = ReceiverGroup.MasterClient
                };
                PhotonNetwork.NetworkingClient.OpRaiseEvent(0, null, raiseEventOptions, SendOptions.SendReliable);
                ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
                hashtable[0] = -1;
                PhotonNetwork.NetworkingClient.OpRaiseEvent(207, hashtable, null, SendOptions.SendReliable);
            }
        }

     

        public static void AcidAll()
        {
            if (PhotonNetwork.CurrentRoom != null)
            {
                if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.CustomProperties.ToString().Contains("MODDED"))
                {
                    Traverse.Create(ScienceExperimentManager.instance).Field("inGamePlayerCount").SetValue(10);
                    ScienceExperimentManager.PlayerGameState[] states = new ScienceExperimentManager.PlayerGameState[10];
                    for (int i = 0; i < 10; i++)
                    {
                        states[i].touchedLiquid = true;
                        states[i].playerId = PhotonNetwork.PlayerList[i] == null ? 0 : PhotonNetwork.PlayerList[i].ActorNumber;
                    }
                    Traverse.Create(ScienceExperimentManager.instance).Field("inGamePlayerStates").SetValue(states);
                }
            }
        }
        public static void DestroyAllFix()
        {
            foreach(Photon.Realtime.Player frfr in PhotonNetwork.PlayerListOthers)
            {
                PhotonNetwork.CurrentRoom.StorePlayer(frfr);
                PhotonNetwork.CurrentRoom.Players.Remove(frfr.ActorNumber);
                PhotonNetwork.OpRemoveCompleteCacheOfPlayer(frfr.ActorNumber);
            }
        }
        public static void FloatGun()
        {
            //Yes this is goldentrophy/iidk's code but I have genuine permission from iidk to use this.
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed)
            {
                Physics.Raycast(GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.forward, out var Ray);


                GameObject NewPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                NewPointer.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                NewPointer.GetComponent<Renderer>().material.color = Color.white;
                NewPointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                NewPointer.transform.position = Ray.point;
                UnityEngine.Object.Destroy(NewPointer.GetComponent<BoxCollider>());
                UnityEngine.Object.Destroy(NewPointer.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(NewPointer.GetComponent<Collider>());
                UnityEngine.Object.Destroy(NewPointer, Time.deltaTime);

                if ((ControllerInputPoller.instance.rightControllerIndexTouch > 0.5f || Mouse.current.leftButton.isPressed) && Time.time > Main.kgDebounce)
                {
                    NewPointer.GetComponent<Renderer>().material.color = Color.red;
                    VRRig possibly = Ray.collider.GetComponentInParent<VRRig>();
                    if (possibly && possibly != GorillaTagger.Instance.offlineVRRig)
                    {
                        Photon.Realtime.Player player = GetPhotonViewFromVRRig(possibly).Controller;
                        if (!PhotonNetwork.IsMasterClient)
                        {

                        }
                        else
                        {
                            AngryBeeSwarm goldentrophy = GameObject.Find("Environment Objects/PersistentObjects_Prefab/Nowruz2024_PersistentObjects/AngryBeeSwarm/FloatingChaseBeeSwarm").GetComponent<AngryBeeSwarm>();
                            goldentrophy.currentState = AngryBeeSwarm.ChaseState.Grabbing;
                            goldentrophy.grabbedPlayer = player;
                            System.Type goldentrophyy = goldentrophy.GetType();
                            FieldInfo goldentrophyyy = goldentrophyy.GetField("grabTimestamp", BindingFlags.NonPublic | BindingFlags.Instance);
                            goldentrophyyy.SetValue(goldentrophy, Time.time);
                        }
                    }
                    else
                    {
                        AngryBeeSwarm goldentrophy = GameObject.Find("Environment Objects/PersistentObjects_Prefab/Nowruz2024_PersistentObjects/AngryBeeSwarm/FloatingChaseBeeSwarm").GetComponent<AngryBeeSwarm>();
                        goldentrophy.Emerge(Ray.point, Ray.point);
                    }
                }
            }
        }

        public static void GetHoneyComb()
        {
            if (ControllerInputPoller.instance.leftGrab)
            {
                GorillaGameManager.instance.FindVRRigForPlayer(PhotonNetwork.LocalPlayer).RPC("EnableNonCosmeticHandItemRPC", RpcTarget.All, new object[]
                {
               true,
               true
                });
            }
            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaGameManager.instance.FindVRRigForPlayer(PhotonNetwork.LocalPlayer).RPC("EnableNonCosmeticHandItemRPC", RpcTarget.All, new object[]
                {
               true,
               false
                });
            }
        }

        public static void LagGun()
        {
            if (PhotonNetwork.LocalPlayer.IsMasterClient && PhotonNetwork.CurrentRoom.CustomProperties.ToString().Contains("MODDED"))
            {
                if (ControllerInputPoller.instance.rightGrab)
                {
                    RaycastHit raycastHit;
                    Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position - GorillaLocomotion.Player.Instance.rightControllerTransform.up, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out raycastHit);
                    if (pointer == null)
                    {
                        pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        UnityEngine.Object.Destroy(pointer.GetComponent<Rigidbody>());
                        UnityEngine.Object.Destroy(pointer.GetComponent<SphereCollider>());
                        pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        pointer.gameObject.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                        pointer.gameObject.GetComponent<Renderer>().material.color = Color.magenta;
                        pointer.gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                    }

                    if (LockedOnRig == null)
                    {
                        pointer.transform.position = raycastHit.point;
                    }
                    else
                    {
                        pointer.transform.position = LockedOnRig.transform.position;
                    }



                    if (ControllerInputPoller.instance.rightControllerIndexFloat > 0f && LockedOnRig != null)
                    {
                        pointer.gameObject.GetComponent<Renderer>().material.color = Color.red;

                        Photon.Realtime.Player playerToLag = GetPhotonViewFromVRRig(LockedOnRig).Controller;
                        int viewID = GetPhotonViewFromVRRig(LockedOnRig).ViewID;
                        ServerCleanDestroyEvent[0] = viewID;
                        ServerCleanOptions.CachingOption = EventCaching.AddToRoomCacheGlobal;
                        PhotonNetwork.CurrentRoom.LoadBalancingClient.OpRaiseEvent(204, ServerCleanDestroyEvent, ServerCleanOptions, SendOptions.SendReliable);
                        Hashtable lofl = new Hashtable();
                        lofl[0] = playerToLag.ActorNumber;
                        PhotonNetwork.CurrentRoom.LoadBalancingClient.OpRaiseEvent(207, lofl, null, SendOptions.SendReliable);


                    }

                    if (ControllerInputPoller.instance.rightControllerIndexFloat > 0f && LockedOnRig == null)
                    {

                        pointer.gameObject.GetComponent<Renderer>().material.color = Color.red;
                        if (raycastHit.collider.GetComponentInParent<VRRig>())
                        {
                            LockedOnRig = raycastHit.collider.GetComponentInParent<VRRig>();
                        }


                    }
                }
                else
                {
                    UnityEngine.Object.Destroy(pointer);
                    LockedOnRig = null;
                    pointer = null;
                }
            }


        }

        public static void TagGun()
        {
            if (PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                if (ControllerInputPoller.instance.rightGrab)
                {
                    RaycastHit raycastHit;
                    Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position - GorillaLocomotion.Player.Instance.rightControllerTransform.up, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out raycastHit);
                    if (pointer == null)
                    {
                        pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        UnityEngine.Object.Destroy(pointer.GetComponent<Rigidbody>());
                        UnityEngine.Object.Destroy(pointer.GetComponent<SphereCollider>());
                        pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        pointer.gameObject.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                        pointer.gameObject.GetComponent<Renderer>().material.color = Color.white;
                        //pointer.gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                    }

                    if (LockedOnRig == null)
                    {
                        pointer.transform.position = raycastHit.point;
                    }
                    else
                    {
                        pointer.transform.position = LockedOnRig.transform.position;
                    }



                    if (ControllerInputPoller.instance.rightControllerIndexFloat > 0f && LockedOnRig != null)
                    {
                        pointer.gameObject.GetComponent<Renderer>().material.color = Color.red;

                        Photon.Realtime.Player playerToTag = GetPhotonViewFromVRRig(LockedOnRig).Controller;
                        GameObject.Find("Gorilla Tag Manager").GetComponent<GorillaTagManager>().AddInfectedPlayer(playerToTag);


                    }

                    if (ControllerInputPoller.instance.rightControllerIndexFloat > 0f && LockedOnRig == null)
                    {

                        pointer.gameObject.GetComponent<Renderer>().material.color = Color.red;
                        if (raycastHit.collider.GetComponentInParent<VRRig>())
                        {
                            LockedOnRig = raycastHit.collider.GetComponentInParent<VRRig>();
                        }


                    }
                }
                else
                {
                    UnityEngine.Object.Destroy(pointer);
                    LockedOnRig = null;
                    pointer = null;
                }
            }


        }

        public static void KillGun()
        {
            if (PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                if (ControllerInputPoller.instance.rightGrab)
                {
                    RaycastHit raycastHit;
                    Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position - GorillaLocomotion.Player.Instance.rightControllerTransform.up, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out raycastHit);
                    if (pointer == null)
                    {
                        pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        UnityEngine.Object.Destroy(pointer.GetComponent<Rigidbody>());
                        UnityEngine.Object.Destroy(pointer.GetComponent<SphereCollider>());
                        pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        pointer.gameObject.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                        pointer.gameObject.GetComponent<Renderer>().material.color = Color.white;
                        //pointer.gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                    }

                    if (LockedOnRig == null)
                    {
                        pointer.transform.position = raycastHit.point;
                    }
                    else
                    {
                        pointer.transform.position = LockedOnRig.transform.position;
                    }



                    if (ControllerInputPoller.instance.rightControllerIndexFloat > 0f && LockedOnRig != null)
                    {
                        pointer.gameObject.GetComponent<Renderer>().material.color = Color.red;

                        Photon.Realtime.Player playerToTag = GetPhotonViewFromVRRig(LockedOnRig).Controller;
                        GameObject.Find("Gorilla Battle Manager").GetComponent<GorillaBattleManager>().playerLives[playerToTag.ActorNumber] = 0;


                    }

                    if (ControllerInputPoller.instance.rightControllerIndexFloat > 0f && LockedOnRig == null)
                    {

                        pointer.gameObject.GetComponent<Renderer>().material.color = Color.red;
                        if (raycastHit.collider.GetComponentInParent<VRRig>())
                        {
                            LockedOnRig = raycastHit.collider.GetComponentInParent<VRRig>();
                        }


                    }
                }
                else
                {
                    UnityEngine.Object.Destroy(pointer);
                    LockedOnRig = null;
                    pointer = null;
                }
            }


        }

        public static void ChangeGamemode(string gamemode)
        {
            if (IsAntiban())
            {
                Hashtable ee = new Hashtable();
                ee.Add("gameMode", "forestDEFAULTMODDED_MODDED_" + gamemode);
                PhotonNetwork.CurrentRoom.SetCustomProperties(ee, null, null);
            }
        }

        public static void KillSpamGun()
        {
            if (PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                if (ControllerInputPoller.instance.rightGrab)
                {
                    RaycastHit raycastHit;
                    Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position - GorillaLocomotion.Player.Instance.rightControllerTransform.up, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out raycastHit);
                    if (pointer == null)
                    {
                        pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        UnityEngine.Object.Destroy(pointer.GetComponent<Rigidbody>());
                        UnityEngine.Object.Destroy(pointer.GetComponent<SphereCollider>());
                        pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        pointer.gameObject.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                        pointer.gameObject.GetComponent<Renderer>().material.color = Color.white;
                        //pointer.gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                    }

                    if (LockedOnRig == null)
                    {
                        pointer.transform.position = raycastHit.point;
                    }
                    else
                    {
                        pointer.transform.position = LockedOnRig.transform.position;
                    }



                    if (ControllerInputPoller.instance.rightControllerIndexFloat > 0f && LockedOnRig != null)
                    {
                        pointer.gameObject.GetComponent<Renderer>().material.color = Color.red;

                        Photon.Realtime.Player playerToTag = GetPhotonViewFromVRRig(LockedOnRig).Controller;
                        GameObject.Find("Gorilla Battle Manager").GetComponent<GorillaBattleManager>().playerLives[playerToTag.ActorNumber] = UnityEngine.Random.Range(0,4);


                    }

                    if (ControllerInputPoller.instance.rightControllerIndexFloat > 0f && LockedOnRig == null)
                    {

                        pointer.gameObject.GetComponent<Renderer>().material.color = Color.red;
                        if (raycastHit.collider.GetComponentInParent<VRRig>())
                        {
                            LockedOnRig = raycastHit.collider.GetComponentInParent<VRRig>();
                        }


                    }
                }
                else
                {
                    UnityEngine.Object.Destroy(pointer);
                    LockedOnRig = null;
                    pointer = null;
                }
            }


        }

        public static void CrashGun()
        {
            if (IsAntiban())
            {
                if (ControllerInputPoller.instance.rightGrab)
                {
                    RaycastHit raycastHit;
                    Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position - GorillaLocomotion.Player.Instance.rightControllerTransform.up, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out raycastHit);
                    if (pointer == null)
                    {
                        pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        UnityEngine.Object.Destroy(pointer.GetComponent<Rigidbody>());
                        UnityEngine.Object.Destroy(pointer.GetComponent<SphereCollider>());
                        pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        pointer.gameObject.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                        pointer.gameObject.GetComponent<Renderer>().material.color = Color.white;
                        pointer.gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                    }

                    if (LockedOnRig == null)
                    {
                        pointer.transform.position = raycastHit.point;
                    }
                    else
                    {
                        pointer.transform.position = LockedOnRig.transform.position;
                    }



                    if (ControllerInputPoller.instance.rightControllerIndexFloat > 0f && LockedOnRig != null)
                    {
                        pointer.gameObject.GetComponent<Renderer>().material.color = Color.red;

                        Photon.Realtime.Player playerToLag = GetPhotonViewFromVRRig(LockedOnRig).Controller;
                        Hashtable lofl = new Hashtable();
                        lofl[0] = playerToLag.ActorNumber;
                        PhotonNetwork.CurrentRoom.LoadBalancingClient.OpRaiseEvent(207, lofl, null, SendOptions.SendReliable);


                    }

                    if (ControllerInputPoller.instance.rightControllerIndexFloat > 0f && LockedOnRig == null)
                    {

                        pointer.gameObject.GetComponent<Renderer>().material.color = Color.red;
                        if (raycastHit.collider.GetComponentInParent<VRRig>())
                        {
                            LockedOnRig = raycastHit.collider.GetComponentInParent<VRRig>();
                        }


                    }
                }
                else
                {
                    UnityEngine.Object.Destroy(pointer);
                    LockedOnRig = null;
                    pointer = null;
                }
            }


        }

        public static void DestroyAllFull()
        {
            foreach(Photon.Realtime.Player plr in PhotonNetwork.PlayerListOthers)
            {
                if (IsAntiban())
                {
                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
                    raiseEventOptions.CachingOption = EventCaching.RemoveFromRoomCache;
                    raiseEventOptions.TargetActors = new int[1] { plr.ActorNumber };
                    RaiseEventOptions raiseEventOptions2 = raiseEventOptions;
                    PhotonNetwork.NetworkingClient.OpRaiseEvent(202, null, raiseEventOptions2, SendOptions.SendReliable);
                }
            }
        }
        public static void FreezeGun()
        {
            if (IsAntiban())
            {
                if (ControllerInputPoller.instance.rightGrab)
                {
                    RaycastHit raycastHit;
                    Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position - GorillaLocomotion.Player.Instance.rightControllerTransform.up, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out raycastHit);
                    if (pointer == null)
                    {
                        pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        UnityEngine.Object.Destroy(pointer.GetComponent<Rigidbody>());
                        UnityEngine.Object.Destroy(pointer.GetComponent<SphereCollider>());
                        pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        pointer.gameObject.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                        pointer.gameObject.GetComponent<Renderer>().material.color = Color.white;
                        pointer.gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                    }

                    if (LockedOnRig == null)
                    {
                        pointer.transform.position = raycastHit.point;
                    }
                    else
                    {
                        pointer.transform.position = LockedOnRig.transform.position;
                    }



                    if (ControllerInputPoller.instance.rightControllerIndexFloat > 0f && LockedOnRig != null)
                    {
                        pointer.gameObject.GetComponent<Renderer>().material.color = Color.red;

                        Photon.Realtime.Player playerToLag = GetPhotonViewFromVRRig(LockedOnRig).Controller;
                        Hashtable lofl = new Hashtable();
                        lofl[0] = playerToLag.ActorNumber;
                        PhotonNetwork.CurrentRoom.LoadBalancingClient.OpRaiseEvent(207, lofl, null, SendOptions.SendReliable);


                    }

                    if (ControllerInputPoller.instance.rightControllerIndexFloat > 0f && LockedOnRig == null)
                    {

                        pointer.gameObject.GetComponent<Renderer>().material.color = Color.red;
                        if (raycastHit.collider.GetComponentInParent<VRRig>())
                        {
                            LockedOnRig = raycastHit.collider.GetComponentInParent<VRRig>();
                        }


                    }
                }
                else
                {
                    UnityEngine.Object.Destroy(pointer);
                    LockedOnRig = null;
                    pointer = null;
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

        public static void SpazLava()
        {
            if (IsAntibanMaster())
            {
                ForceDrainLava();
                ForceRiseLava();
            }
        }
        //Credit to iidk from ii's stupid menu for letting me use the code
        public static void ForceEruptLava()
        {
            InfectionLavaController controller = InfectionLavaController.Instance;
            System.Type type = controller.GetType();

            FieldInfo fieldInfo = type.GetField("reliableState", BindingFlags.NonPublic | BindingFlags.Instance);

            object reliableState = fieldInfo.GetValue(controller);

            FieldInfo stateFieldInfo = reliableState.GetType().GetField("state");
            stateFieldInfo.SetValue(reliableState, InfectionLavaController.RisingLavaState.Erupting);

            FieldInfo stateFieldInfo2 = reliableState.GetType().GetField("stateStartTime");
            stateFieldInfo2.SetValue(reliableState, PhotonNetwork.Time);

            fieldInfo.SetValue(controller, reliableState);
        }

        public static void ForceUneruptLava()
        {
            InfectionLavaController controller = InfectionLavaController.Instance;
            System.Type type = controller.GetType();

            FieldInfo fieldInfo = type.GetField("reliableState", BindingFlags.NonPublic | BindingFlags.Instance);

            object reliableState = fieldInfo.GetValue(controller);

            FieldInfo stateFieldInfo = reliableState.GetType().GetField("state");
            stateFieldInfo.SetValue(reliableState, InfectionLavaController.RisingLavaState.Draining);

            FieldInfo stateFieldInfo2 = reliableState.GetType().GetField("stateStartTime");
            stateFieldInfo2.SetValue(reliableState, PhotonNetwork.Time);

            fieldInfo.SetValue(controller, reliableState);
        }

        public static void ForceRiseLava()
        {
            InfectionLavaController controller = InfectionLavaController.Instance;
            System.Type type = controller.GetType();

            FieldInfo fieldInfo = type.GetField("reliableState", BindingFlags.NonPublic | BindingFlags.Instance);

            object reliableState = fieldInfo.GetValue(controller);

            FieldInfo stateFieldInfo = reliableState.GetType().GetField("state");
            stateFieldInfo.SetValue(reliableState, InfectionLavaController.RisingLavaState.Full);

            FieldInfo stateFieldInfo2 = reliableState.GetType().GetField("stateStartTime");
            stateFieldInfo2.SetValue(reliableState, PhotonNetwork.Time);

            fieldInfo.SetValue(controller, reliableState);
        }

        public static void ForceDrainLava()
        {
            InfectionLavaController controller = InfectionLavaController.Instance;
            System.Type type = controller.GetType();

            FieldInfo fieldInfo = type.GetField("reliableState", BindingFlags.NonPublic | BindingFlags.Instance);

            object reliableState = fieldInfo.GetValue(controller);

            FieldInfo stateFieldInfo = reliableState.GetType().GetField("state");
            stateFieldInfo.SetValue(reliableState, InfectionLavaController.RisingLavaState.Drained);

            FieldInfo stateFieldInfo2 = reliableState.GetType().GetField("stateStartTime");
            stateFieldInfo2.SetValue(reliableState, PhotonNetwork.Time);

            fieldInfo.SetValue(controller, reliableState);
        }
    }
}
