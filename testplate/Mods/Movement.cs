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
using static UnityEngine.GridBrushBase;
using System.Reflection;

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
        private static Vector3 walkPos;
        private static Vector3 walkNormal;

        public static void TeleportToStump()
        {
            foreach (MeshCollider mesh in Resources.FindObjectsOfTypeAll<MeshCollider>())
            {
                mesh.enabled = false;
            }
            GorillaLocomotion.Player.Instance.transform.position = new Vector3(-66.4848f, 11.8871f, -82.6619f);
            foreach (MeshCollider mesh in Resources.FindObjectsOfTypeAll<MeshCollider>())
            {
                mesh.enabled = false;
            }
        }

        public static void LowGravity()
        {
            GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.deltaTime * (6.66f / Time.deltaTime)), ForceMode.Acceleration);
        }

        public static void ZeroGravity()
        {
            GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.deltaTime * (9.81f / Time.deltaTime)), ForceMode.Acceleration);
        }

        public static void HighGravity()
        {
            GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.down * (Time.deltaTime * (6.66f / Time.deltaTime)), ForceMode.Acceleration);
        }
        public static void FakeOculusMenu()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaTagger.Instance.offlineVRRig.leftHandTransform.position = GorillaTagger.Instance.bodyCollider.transform.position;
                GorillaTagger.Instance.offlineVRRig.leftHandTransform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;
                GorillaTagger.Instance.offlineVRRig.rightHandTransform.position = GorillaTagger.Instance.bodyCollider.transform.position;
                GorillaTagger.Instance.offlineVRRig.rightHandTransform.rotation = GorillaTagger.Instance.bodyCollider.transform.rotation;
            }
        }
        public static void WallWalk()
        {
            // Credits to iidk since he is the cooker when it comes to comp cheats
            if ((GorillaLocomotion.Player.Instance.wasLeftHandTouching || GorillaLocomotion.Player.Instance.wasRightHandTouching) && ControllerInputPoller.instance.rightGrab || ControllerInputPoller.instance.leftGrab)
            {
                FieldInfo fieldInfo = typeof(GorillaLocomotion.Player).GetField("lastHitInfoHand", BindingFlags.NonPublic | BindingFlags.Instance);
                RaycastHit ray = (RaycastHit)fieldInfo.GetValue(GorillaLocomotion.Player.Instance);
                walkPos = ray.point;
                walkNormal = ray.normal;
            }

            if (!ControllerInputPoller.instance.rightGrab)
            {
                walkPos = Vector3.zero;
            }

            if (walkPos != Vector3.zero)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(walkNormal * -10, ForceMode.Acceleration);
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.deltaTime * (9.81f / Time.deltaTime)), ForceMode.Acceleration);
            }
        }

        public static void UpAndDown()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0f)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(new Vector3(0, 500, 0), ForceMode.Acceleration);
            }

            if (ControllerInputPoller.instance.leftControllerIndexFloat > 0f)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(new Vector3(0, -500, 0), ForceMode.Acceleration);
            }
        }

        public static void SlingshotFly()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * (13 * 2);
            }

            if (ControllerInputPoller.instance.rightControllerPrimaryButton && ControllerInputPoller.instance.rightControllerSecondaryButton)
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += GorillaLocomotion.Player.Instance.headCollider.transform.up * 3;
            }
        }

        public static void HandFly()
        {
            if (ControllerInputPoller.instance.leftControllerPrimaryButton)
            {
                GorillaLocomotion.Player.Instance.transform.position += (GorillaLocomotion.Player.Instance.leftControllerTransform.forward * Time.deltaTime) * Main.flySpeed;
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity = Vector3.zero;
            }

            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                GorillaLocomotion.Player.Instance.transform.position += (GorillaLocomotion.Player.Instance.rightControllerTransform.forward * Time.deltaTime) * Main.flySpeed;
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity = Vector3.zero;
            }
        }

        public static void IronMonke()
        {
            if (ControllerInputPoller.instance.leftGrab)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(GorillaLocomotion.Player.Instance.leftControllerTransform.right, ForceMode.Acceleration);
            }

            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(GorillaLocomotion.Player.Instance.rightControllerTransform.right, ForceMode.Acceleration);
            }
        }
        private static bool isLeftGrappling;
        private static bool isRightGrappling;
        private static SpringJoint rightjoint;
        private static Vector3 leftgrapplePoint;
        private static Vector3 rightgrapplePoint;
        public static SpringJoint leftjoint;
        public static void SpiderMan()
        {
            
            bool leftGrab = ControllerInputPoller.instance.leftGrab;
            bool rightGrab = ControllerInputPoller.instance.rightGrab;
            if (leftGrab)
            {
                if (!isLeftGrappling)
                {
                    isLeftGrappling = true;
                    RaycastHit lefthit;
                    if (Physics.Raycast(GorillaTagger.Instance.leftHandTransform.position, GorillaTagger.Instance.leftHandTransform.forward, out lefthit, 100f))
                    {
                        leftgrapplePoint = lefthit.point;

                        leftjoint = GorillaTagger.Instance.gameObject.AddComponent<SpringJoint>();
                        leftjoint.autoConfigureConnectedAnchor = false;
                        leftjoint.connectedAnchor = leftgrapplePoint;

                        float leftdistanceFromPoint = Vector3.Distance(GorillaTagger.Instance.bodyCollider.attachedRigidbody.position, leftgrapplePoint);

                        leftjoint.maxDistance = leftdistanceFromPoint * 0.8f;
                        leftjoint.minDistance = leftdistanceFromPoint * 0.25f;

                        leftjoint.spring = 20f;
                        leftjoint.damper = 50f;
                        leftjoint.massScale = 11f;
                    }
                }

                GameObject line = new GameObject("Line");
                LineRenderer liner = line.AddComponent<LineRenderer>();
                UnityEngine.Color thecolor = Color.white;
                liner.startColor = thecolor; liner.endColor = thecolor; liner.startWidth = 0.025f; liner.endWidth = 0.025f; liner.positionCount = 2; liner.useWorldSpace = true;
                liner.SetPosition(0, GorillaTagger.Instance.leftHandTransform.position);
                liner.SetPosition(1, leftgrapplePoint);
                liner.material.shader = Shader.Find("GorillaTag/UberShader");
                UnityEngine.Object.Destroy(line, Time.deltaTime);
            }
            else
            {
                isLeftGrappling = false;
                UnityEngine.Object.Destroy(leftjoint);
            }

            if (rightGrab)
            {
                if (!isRightGrappling)
                {
                    isRightGrappling = true;
                    RaycastHit righthit;
                    if (Physics.Raycast(GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.forward, out righthit, 100f))
                    {
                        rightgrapplePoint = righthit.point;

                        rightjoint = GorillaTagger.Instance.gameObject.AddComponent<SpringJoint>();
                        rightjoint.autoConfigureConnectedAnchor = false;
                        rightjoint.connectedAnchor = rightgrapplePoint;

                        float rightdistanceFromPoint = Vector3.Distance(GorillaTagger.Instance.bodyCollider.attachedRigidbody.position, rightgrapplePoint);

                        rightjoint.maxDistance = rightdistanceFromPoint * 0.8f;
                        rightjoint.minDistance = rightdistanceFromPoint * 0.25f;

                        rightjoint.spring = 20f;
                        rightjoint.damper = 50f;
                        rightjoint.massScale = 11f;
                    }
                }

                GameObject line = new GameObject("Line");
                LineRenderer liner = line.AddComponent<LineRenderer>();
                UnityEngine.Color thecolor = Color.white;
                liner.startColor = thecolor; liner.endColor = thecolor; liner.startWidth = 0.025f; liner.endWidth = 0.025f; liner.positionCount = 2; liner.useWorldSpace = true;
                liner.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
                liner.SetPosition(1, rightgrapplePoint);
                liner.material.shader = Shader.Find("GorillaTag/UberShader");
                UnityEngine.Object.Destroy(line, Time.deltaTime);
            }
            else
            {
                isRightGrappling = false;
                UnityEngine.Object.Destroy(rightjoint);
            }
        }

        public static void DisableSpiderMan()
        {
            isLeftGrappling = false;
            UnityEngine.Object.Destroy(leftjoint);
            isRightGrappling = false;
            UnityEngine.Object.Destroy(rightjoint);
        }

        public static GameObject checkpointPoint;

        public static void Checkpoint()
        {
            bool rightGrab = ControllerInputPoller.instance.rightGrab;
            if (checkpointPoint == null && rightGrab)
            {
                checkpointPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(checkpointPoint.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(checkpointPoint.GetComponent<SphereCollider>());
                checkpointPoint.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                checkpointPoint.gameObject.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                checkpointPoint.gameObject.GetComponent<Renderer>().material.color = Color.red;
            }

            if (checkpointPoint != null && rightGrab)
            {
                checkpointPoint.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
            }

            if (checkpointPoint != null && ControllerInputPoller.instance.rightControllerSecondaryButton)
            {
                foreach (MeshCollider renderer in Resources.FindObjectsOfTypeAll<MeshCollider>())
                {
                    renderer.enabled = false;
                }
                GorillaLocomotion.Player.Instance.transform.position = checkpointPoint.transform.position;
                foreach (MeshCollider renderer in Resources.FindObjectsOfTypeAll<MeshCollider>())
                {
                    renderer.enabled = true;
                }
                checkpointPoint = null;
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
