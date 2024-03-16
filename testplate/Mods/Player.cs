using GorillaNetworking;
using GorillaTag;
using Photon.Pun;
using PolaroidMenu.Classes;
using PolaroidMenu.Menu;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using PolaroidMenu.Menu;
namespace PolaroidMenu.Mods
{
    internal class Player : MonoBehaviour
    {
        public static void GhostMonk()
        {
            bool primaryright = ControllerInputPoller.instance.rightControllerPrimaryButton;
            if (primaryright)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }

        public static VRRig LockedOnRig;
        public static GameObject pointer;
        public static float rgbDebounce = 0;

    

        

        public static void NoClip()
        {
            MeshCollider[] meshColliders = Resources.FindObjectsOfTypeAll<MeshCollider>();
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0f) 
            {
                foreach (MeshCollider fork in meshColliders)
                {
                    fork.enabled = false;
                }
            }
            else
            {
                foreach (MeshCollider fork in meshColliders)
                {
                    fork.enabled = true;
                }
            }
        }

        

        public static void Maniac()
        {
            GorillaTagger.Instance.leftHandTransform.position = new Vector3(UnityEngine.Random.Range(0f, 999999f), UnityEngine.Random.Range(0f, 999999f), UnityEngine.Random.Range(0f, 999999f));
            GorillaTagger.Instance.rightHandTransform.position = new Vector3(UnityEngine.Random.Range(0f, 999999f), UnityEngine.Random.Range(0f, 999999f), UnityEngine.Random.Range(0f, 999999f));
        }

        public static void GrabRig()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }

        public static void PBBVWalk()
        {
            GorillaLocomotion.Player.Instance.disableMovement = true;
        }

        public static void FixHead()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.x = 0f;
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y = 0f;
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z = 0f;
        }
        public static float nameDebounce = 0;
        
        public static void BecomePBBV()
        {
            if (Time.time > nameDebounce)
            {
                Task.Delay(1000).ContinueWith(t => ChangeName("PBBV"));
                Task.Delay(1000).ContinueWith(t => ChangeName("IS"));
                Task.Delay(1000).ContinueWith(t => ChangeName("HERE"));
                nameDebounce = Time.time + 0.1f;
            }
        }
        public static void HeadSeizure()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.x += 69f;
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y += 69f;
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z += 69f;
        }
        public static void NoTagFreeze()
        {
            GorillaLocomotion.Player.Instance.disableMovement = false;
        }

        public static void LongArms()
        {
            GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }

        public static void ChangeName(string PlayerName)
        {
            try
            {
                if (PhotonNetwork.InRoom)
                {
                    if (GorillaComputer.instance.friendJoinCollider.playerIDsCurrentlyTouching.Contains(PhotonNetwork.LocalPlayer.UserId))
                    {
                        GorillaComputer.instance.currentName = PlayerName;
                        PhotonNetwork.LocalPlayer.NickName = PlayerName;
                        GorillaComputer.instance.offlineVRRigNametagText.text = PlayerName;
                        GorillaComputer.instance.savedName = PlayerName;
                        PlayerPrefs.SetString("playerName", PlayerName);
                        PlayerPrefs.Save();

                        ChangeColor(GorillaTagger.Instance.offlineVRRig.playerColor);
                    }
                }
                else
                {
                    GorillaComputer.instance.currentName = PlayerName;
                    PhotonNetwork.LocalPlayer.NickName = PlayerName;
                    GorillaComputer.instance.offlineVRRigNametagText.text = PlayerName;
                    GorillaComputer.instance.savedName = PlayerName;
                    PlayerPrefs.SetString("playerName", PlayerName);
                    PlayerPrefs.Save();

                    ChangeColor(GorillaTagger.Instance.offlineVRRig.playerColor);
                }
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogError(string.Format("iiMenu <b>NAME ERROR</b> {1} - {0}", exception.Message, exception.StackTrace));
            }
        }

        public static void ChangeColor(Color color)
        {
            if (PhotonNetwork.InRoom)
            {
                if (GorillaComputer.instance.friendJoinCollider.playerIDsCurrentlyTouching.Contains(PhotonNetwork.LocalPlayer.UserId))
                {
                    PlayerPrefs.SetFloat("redValue", Mathf.Clamp(color.r, 0f, 1f));
                    PlayerPrefs.SetFloat("greenValue", Mathf.Clamp(color.g, 0f, 1f));
                    PlayerPrefs.SetFloat("blueValue", Mathf.Clamp(color.b, 0f, 1f));

                    //GorillaTagger.Instance.offlineVRRig.mainSkin.material.color = color;
                    GorillaTagger.Instance.UpdateColor(color.r, color.g, color.b);
                    PlayerPrefs.Save();

                    GorillaTagger.Instance.myVRRig.RPC("InitializeNoobMaterial", RpcTarget.All, new object[] { color.r, color.g, color.b, false });
                }
            }
            else
            {
                PlayerPrefs.SetFloat("redValue", Mathf.Clamp(color.r, 0f, 1f));
                PlayerPrefs.SetFloat("greenValue", Mathf.Clamp(color.g, 0f, 1f));
                PlayerPrefs.SetFloat("blueValue", Mathf.Clamp(color.b, 0f, 1f));
                GorillaTagger.Instance.UpdateColor(color.r, color.g, color.b);
                PlayerPrefs.Save();
                GorillaTagger.Instance.myVRRig.RPC("InitializeNoobMaterial", RpcTarget.All, new object[] { color.r, color.g, color.b, false });
            }
        }
        public static void FixLongArms()
        {
            GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        public static void CopyPlayerGun()
        {
            if (PhotonNetwork.InRoom)
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
                        GorillaTagger.Instance.offlineVRRig.enabled = false;
                    }
                    else
                    {
                        pointer.transform.position = LockedOnRig.transform.position;
                        GorillaTagger.Instance.offlineVRRig.enabled = false;
                        GorillaTagger.Instance.offlineVRRig.leftHandTransform.position = LockedOnRig.leftHandTransform.position;
                        GorillaTagger.Instance.offlineVRRig.leftHandTransform.rotation = LockedOnRig.leftHandTransform.rotation;
                        GorillaTagger.Instance.offlineVRRig.rightHandTransform.position = LockedOnRig.rightHandTransform.position;
                        GorillaTagger.Instance.offlineVRRig.rightHandTransform.rotation = LockedOnRig.rightHandTransform.rotation;
                    }


                    if (ControllerInputPoller.instance.rightControllerIndexFloat > 0f && LockedOnRig != null)
                    {
                        pointer.gameObject.GetComponent<Renderer>().material.color = Color.red;

                    }
                    else
                    {
                        GorillaTagger.Instance.offlineVRRig.enabled = true;
                    }

                    if (ControllerInputPoller.instance.rightControllerIndexFloat > 0f && LockedOnRig == null)
                    {

                        pointer.gameObject.GetComponent<Renderer>().material.color = Color.red;
                        GorillaTagManager gorillaManager = GameObject.Find("Gorilla Tag Manager").GetComponent<GorillaTagManager>();
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

        public static void InvisMonk()
        {
            bool primaryright = ControllerInputPoller.instance.rightControllerPrimaryButton;
            if (primaryright)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = new UnityEngine.Vector3(0f, -9999f, 0f);
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
    }
}
