using BepInEx;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using PolaroidMenu.Menu;
using UnityEngine.InputSystem;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;

namespace PolaroidMenu.Mods
{
    internal class Movement
    {
        private static bool once_left;

        private static bool once_right;

        private static bool once_left_false;

        private static bool once_right_false;

        private static bool once_networking;

        private static GameObject[] jump_left_network = new GameObject[9999];

        private static GameObject[] jump_right_network = new GameObject[9999];

        private static GameObject jump_left_local = null;

        private static GameObject jump_right_local = null;

        private static Vector3 scale = new Vector3(0.0125f, 0.28f, 0.3825f);

        public static void UpAndDown()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0f)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(new Vector3(0, 1000, 0), ForceMode.Acceleration);
            }

            if (ControllerInputPoller.instance.leftControllerIndexFloat > 0f)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(new Vector3(0, -1000, 0), ForceMode.Acceleration);
            }
        }

        public static void IronMonke()
        {
            if (ControllerInputPoller.instance.leftGrab)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(GorillaTagger.Instance.offlineVRRig.leftHandTransform.right, ForceMode.Acceleration);
            }

            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(GorillaTagger.Instance.offlineVRRig.rightHandTransform.right, ForceMode.Acceleration);
            }
        }

        public static void TriggerFly()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0f)
            {
                GorillaLocomotion.Player.Instance.transform.position += (GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime) * Main.flySpeed;
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity = Vector3.zero;
            }
        }

        public static void Platforms(bool invis, bool sticky)
        {

            bool inputr;
            bool inputl;
            inputr = ControllerInputPoller.instance.rightGrab;
            inputl = ControllerInputPoller.instance.leftGrab;
            if (inputr)
            {
                if (!once_right && jump_right_local == null)
                {
                    if (sticky)
                    {
                        jump_right_local = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    }
                    else
                    {
                        jump_right_local = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    }
                    jump_right_local.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                    jump_right_local.GetComponent<Renderer>().material.color = Color.black;
                    if (invis)
                    {
                        UnityEngine.Object.Destroy(jump_right_local.GetComponent<Renderer>());
                    }
                    jump_right_local.transform.localScale = scale;
                    jump_right_local.transform.position = new Vector3(0f, -0.0100f, 0f) + GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                    jump_right_local.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                    object[] eventContent = new object[2]
                    {
                    new Vector3(0f, -0.0100f, 0f) + GorillaLocomotion.Player.Instance.rightControllerTransform.position,
                    GorillaLocomotion.Player.Instance.rightControllerTransform.rotation
                    };
                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                    {
                        Receivers = ReceiverGroup.Others
                    };
                    PhotonNetwork.RaiseEvent(70, eventContent, raiseEventOptions, SendOptions.SendReliable);
                    once_right = true;
                    once_right_false = false;
                }
            }
            else if (!once_right_false && jump_right_local != null)
            {
                UnityEngine.Object.Destroy(jump_right_local);
                jump_right_local = null;
                once_right = false;
                once_right_false = true;
                RaiseEventOptions raiseEventOptions2 = new RaiseEventOptions
                {
                    Receivers = ReceiverGroup.Others
                };
                PhotonNetwork.RaiseEvent(72, null, raiseEventOptions2, SendOptions.SendReliable);
            }
            if (inputl)
            {
                if (!once_left && jump_left_local == null)
                {
                    if (sticky)
                    {
                        jump_left_local = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    }
                    else
                    {
                        jump_left_local = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    }
                    jump_left_local.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                    if (invis)
                    {
                        UnityEngine.Object.Destroy(jump_left_local.GetComponent<Renderer>());
                    }
                    jump_left_local.transform.localScale = scale;
                    jump_left_local.transform.position = new Vector3(0f, -0.0100f, 0f) + GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                    jump_left_local.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                    jump_left_local.GetComponent<Renderer>().material.color = Color.black;
                    object[] eventContent2 = new object[2]
                    {
                    new Vector3(0f, -0.0100f, 0f) + GorillaLocomotion.Player.Instance.leftControllerTransform.position,
                    GorillaLocomotion.Player.Instance.leftControllerTransform.rotation
                    };
                    RaiseEventOptions raiseEventOptions3 = new RaiseEventOptions
                    {
                        Receivers = ReceiverGroup.Others
                    };
                    PhotonNetwork.RaiseEvent(69, eventContent2, raiseEventOptions3, SendOptions.SendReliable);
                    once_left = true;
                    once_left_false = false;
                }
            }
            else if (!once_left_false && jump_left_local != null)
            {
                UnityEngine.Object.Destroy(jump_left_local);
                jump_left_local = null;
                once_left = false;
                once_left_false = true;
                RaiseEventOptions raiseEventOptions4 = new RaiseEventOptions
                {
                    Receivers = ReceiverGroup.Others
                };
                PhotonNetwork.RaiseEvent(71, null, raiseEventOptions4, SendOptions.SendReliable);
            }
            if (!PhotonNetwork.InRoom)
            {
                for (int i = 0; i < jump_right_network.Length; i++)
                {
                    UnityEngine.Object.Destroy(jump_right_network[i]);
                }
                for (int j = 0; j < jump_left_network.Length; j++)
                {
                    UnityEngine.Object.Destroy(jump_left_network[j]);
                }
            }
        }

        public static void TriggerPlatforms(bool invis, bool sticky)
        {

            bool inputr;
            bool inputl;
            inputr = ControllerInputPoller.instance.rightControllerIndexFloat > 0f;
            inputl = ControllerInputPoller.instance.leftControllerIndexFloat > 0f;
            if (inputr)
            {
                if (!once_right && jump_right_local == null)
                {
                    if (sticky)
                    {
                        jump_right_local = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    }
                    else
                    {
                        jump_right_local = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    }
                    jump_right_local.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                    jump_right_local.GetComponent<Renderer>().material.color = Color.black;
                    if (invis)
                    {
                        UnityEngine.Object.Destroy(jump_right_local.GetComponent<Renderer>());
                    }
                    jump_right_local.transform.localScale = scale;
                    jump_right_local.transform.position = new Vector3(0f, -0.0100f, 0f) + GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                    jump_right_local.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                    object[] eventContent = new object[2]
                    {
                    new Vector3(0f, -0.0100f, 0f) + GorillaLocomotion.Player.Instance.rightControllerTransform.position,
                    GorillaLocomotion.Player.Instance.rightControllerTransform.rotation
                    };
                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                    {
                        Receivers = ReceiverGroup.Others
                    };
                    PhotonNetwork.RaiseEvent(70, eventContent, raiseEventOptions, SendOptions.SendReliable);
                    once_right = true;
                    once_right_false = false;
                }
            }
            else if (!once_right_false && jump_right_local != null)
            {
                UnityEngine.Object.Destroy(jump_right_local);
                jump_right_local = null;
                once_right = false;
                once_right_false = true;
                RaiseEventOptions raiseEventOptions2 = new RaiseEventOptions
                {
                    Receivers = ReceiverGroup.Others
                };
                PhotonNetwork.RaiseEvent(72, null, raiseEventOptions2, SendOptions.SendReliable);
            }
            if (inputl)
            {
                if (!once_left && jump_left_local == null)
                {
                    if (sticky)
                    {
                        jump_left_local = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    }
                    else
                    {
                        jump_left_local = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    }
                    jump_left_local.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                    if (invis)
                    {
                        UnityEngine.Object.Destroy(jump_left_local.GetComponent<Renderer>());
                    }
                    jump_left_local.transform.localScale = scale;
                    jump_left_local.transform.position = new Vector3(0f, -0.0100f, 0f) + GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                    jump_left_local.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                    jump_left_local.GetComponent<Renderer>().material.color = Color.black;
                    object[] eventContent2 = new object[2]
                    {
                    new Vector3(0f, -0.0100f, 0f) + GorillaLocomotion.Player.Instance.leftControllerTransform.position,
                    GorillaLocomotion.Player.Instance.leftControllerTransform.rotation
                    };
                    RaiseEventOptions raiseEventOptions3 = new RaiseEventOptions
                    {
                        Receivers = ReceiverGroup.Others
                    };
                    PhotonNetwork.RaiseEvent(69, eventContent2, raiseEventOptions3, SendOptions.SendReliable);
                    once_left = true;
                    once_left_false = false;
                }
            }
            else if (!once_left_false && jump_left_local != null)
            {
                UnityEngine.Object.Destroy(jump_left_local);
                jump_left_local = null;
                once_left = false;
                once_left_false = true;
                RaiseEventOptions raiseEventOptions4 = new RaiseEventOptions
                {
                    Receivers = ReceiverGroup.Others
                };
                PhotonNetwork.RaiseEvent(71, null, raiseEventOptions4, SendOptions.SendReliable);
            }
            if (!PhotonNetwork.InRoom)
            {
                for (int i = 0; i < jump_right_network.Length; i++)
                {
                    UnityEngine.Object.Destroy(jump_right_network[i]);
                }
                for (int j = 0; j < jump_left_network.Length; j++)
                {
                    UnityEngine.Object.Destroy(jump_left_network[j]);
                }
            }
        }

        public static void Fly()
        {

            if (ControllerInputPoller.instance.rightControllerPrimaryButton || UnityInput.Current.GetKey(KeyCode.F))
            {
                GorillaLocomotion.Player.Instance.transform.position += (GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime) * Main.flySpeed;
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity = Vector3.zero;
            }
        }
    }
}
