using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PolaroidMenu.Mods
{
    internal class Water
    {
        private static GameObject ff = null;
        public static void SwimEverywhere()
        {
            if (ff == null)
            {
                ff = GameObject.Instantiate(GameObject.Find("OceanWater"));
                ff.transform.localScale = new Vector3(5f, 5f, 5f);
                ff.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                ff.transform.position = GorillaTagger.Instance.headCollider.transform.position;
            }
        }

        public static void FixSwimEverywhere()
        {
            GameObject.Destroy(ff);
        }
        public static void FastSwim()
        {
            if (GorillaLocomotion.Player.Instance.InWater)
            {
                GorillaLocomotion.Player.Instance.gameObject.GetComponent<Rigidbody>().velocity *= 1.000f;
            }
        }
    }
}
