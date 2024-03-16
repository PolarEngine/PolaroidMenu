using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PolaroidMenu.Mods
{
    internal class Visual
    {
        public static int[] bones = new int[] {
            4, 3, 5, 4, 19, 18, 20, 19, 3, 18, 21, 20, 22, 21, 25, 21, 29, 21, 31, 29, 27, 25, 24, 22, 6, 5, 7, 6, 10, 6, 14, 6, 16, 14, 12, 10, 9, 7
        };
        public static void BoneESP()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                UnityEngine.Color thecolor = vrrig.playerColor;
                if (vrrig != GorillaTagger.Instance.offlineVRRig)
                {

                    if (vrrig.head.rigTarget.gameObject.GetComponent<LineRenderer>())
                    {
                        LineRenderer liner = vrrig.head.rigTarget.gameObject.GetComponent<LineRenderer>();
                        liner.SetPosition(0, vrrig.head.rigTarget.transform.position + new Vector3(0f, 0.16f, 0f));
                        liner.SetPosition(1, vrrig.head.rigTarget.transform.position - new Vector3(0f, 0.4f, 0f));
                    }
                    else
                    {
                        LineRenderer liner = vrrig.head.rigTarget.gameObject.AddComponent<LineRenderer>();
                        liner.startWidth = 0.025f;
                        liner.endWidth = 0.025f;

                        liner.startColor = thecolor;
                        liner.endColor = thecolor;

                        liner.material.shader = Shader.Find("GUI/Text Shader");
                    }

                    for (int i = 0; i < bones.Count<int>(); i += 2)
                    {
                        if (vrrig.mainSkin.bones[bones[i]].gameObject.GetComponent<LineRenderer>())
                        {
                            LineRenderer liner = vrrig.mainSkin.bones[bones[i]].gameObject.GetComponent<LineRenderer>();

                            liner.startWidth = 0.025f;
                            liner.endWidth = 0.025f;
                            thecolor.a = 0.5f;
                            liner.startColor = thecolor;
                            liner.endColor = thecolor;

                            liner.material.shader = Shader.Find("GUI/Text Shader");

                            liner.SetPosition(0, vrrig.mainSkin.bones[bones[i]].position);
                            liner.SetPosition(1, vrrig.mainSkin.bones[bones[i + 1]].position);
                        }
                        else
                        {
                            LineRenderer liner = vrrig.mainSkin.bones[bones[i]].gameObject.AddComponent<LineRenderer>();

                            liner.startWidth = 0.025f;
                            liner.endWidth = 0.025f;
                            thecolor.a = 0.5f;
                            liner.startColor = thecolor;
                            liner.endColor = thecolor;

                            liner.material.shader = Shader.Find("GUI/Text Shader");

                            liner.SetPosition(0, vrrig.mainSkin.bones[bones[i]].position);
                            liner.SetPosition(1, vrrig.mainSkin.bones[bones[i + 1]].position);
                        }



                    }
                }
            }
        }

        public static void BoneESPOff()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                UnityEngine.Color thecolor = vrrig.playerColor;
                if (vrrig != GorillaTagger.Instance.offlineVRRig)
                {
                    UnityEngine.Object.Destroy(vrrig.head.rigTarget.gameObject.GetComponent<LineRenderer>());


                    for (int i = 0; i < bones.Count<int>(); i += 2)
                    {
                        if (vrrig.mainSkin.bones[bones[i]].gameObject.GetComponent<LineRenderer>())
                        {
                            UnityEngine.Object.Destroy(vrrig.mainSkin.bones[bones[i]].gameObject.GetComponent<LineRenderer>());
                        }
                    }
                }
            }
        }
        public static void Tracers()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                
                if (vrrig != GorillaTagger.Instance.offlineVRRig)
                {
                    if (vrrig.head.rigTarget.gameObject.GetComponent<LineRenderer>())
                    {
                        LineRenderer fre = vrrig.head.rigTarget.gameObject.GetComponent<LineRenderer>();
                        fre.SetPosition(0, GorillaLocomotion.Player.Instance.rightControllerTransform.position);
                        fre.SetPosition(1, vrrig.head.rigTarget.transform.position - new Vector3(0f, 0.4f, 0f));
                    }
                    else
                    {
                        UnityEngine.Color thecolor = vrrig.playerColor;
                        LineRenderer liner = vrrig.head.rigTarget.gameObject.AddComponent<LineRenderer>();
                        liner.startWidth = 0.025f;
                        liner.endWidth = 0.025f;
                        thecolor.a = 0.5f;
                        liner.startColor = thecolor;
                        liner.endColor = thecolor;

                        liner.material.shader = Shader.Find("GUI/Text Shader");

                        liner.SetPosition(0, GorillaLocomotion.Player.Instance.rightControllerTransform.position);
                        liner.SetPosition(1, vrrig.head.rigTarget.transform.position - new Vector3(0f, 0.4f, 0f));
                    }
                    

                }
            }
        }

        public static void TracersOff()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                UnityEngine.Color thecolor = Color.magenta;
                if (vrrig != GorillaTagger.Instance.offlineVRRig)
                {
                    UnityEngine.Object.Destroy(vrrig.GetComponent<LineRenderer>());

                }
            }
        }
    }
}
