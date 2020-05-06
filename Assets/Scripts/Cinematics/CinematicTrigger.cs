﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool hasPlayed = false;
        private void OnTriggerEnter(Collider other)
        {
            if(other.transform.CompareTag("Player") && !hasPlayed)
            {
                hasPlayed = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}
