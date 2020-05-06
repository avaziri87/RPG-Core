using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.Core
{
    public class DestinationNode : MonoBehaviour
    {
        PlayerController Player = null;
        float turnOffDistance = 2;

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
        private void Update()
        {
            if (Vector3.Distance(transform.position, Player.transform.position) < turnOffDistance)
            {
                TurnOff();
            }
        }
        public void TurnOn(Vector3 Pos)
        {
            GetComponent<MeshRenderer>().enabled = true;
            transform.position = Pos;
        }
        public void TurnOff()
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
