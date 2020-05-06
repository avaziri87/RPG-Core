using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Atributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        void Awake()
        {
            health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            GetComponent<Text>().text =string.Format("{0:0}/{1:0}", health.GetHealth(), health.GetMAxHealth());
        }
    }
}
