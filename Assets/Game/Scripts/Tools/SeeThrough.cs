using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Tools
{
    public class SeeThrough : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            
            if (other.tag == "SeeThrough")
            {
                other.GetComponent<MeshRenderer>().enabled = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "SeeThrough")
            {
                other.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}
