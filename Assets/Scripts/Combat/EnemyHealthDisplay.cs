using RPG.Atributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        private void Awake()
        {
            fighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
        }
        void Update()
        {
            if(fighter.GetTargetHealth() == null)
            {
                GetComponent<Text>().text = " ";
                return;
            }
            Health health = fighter.GetTargetHealth();
                GetComponent<Text>().text = string.Format("{0:0}/{1:0}", health.GetHealth(), health.GetMAxHealth());
        }
    }
}
