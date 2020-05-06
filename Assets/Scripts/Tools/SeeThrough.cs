using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Tools
{
    public class SeeThrough : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}
