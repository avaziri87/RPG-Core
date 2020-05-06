using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using System;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints;

        public event Action OnExperienceGained;

        public object CaptureState()
        {
            return experiencePoints;
        }

        public void GainXp( float experience)
        {
            experiencePoints += experience;
            OnExperienceGained();
        }
        public float GetXp()
        {
            return experiencePoints;
        }
        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }
    }
}
