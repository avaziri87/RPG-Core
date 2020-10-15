using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Atributes
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] Slider healthSlider;
        Health health;
        void Awake()
        {
            health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            healthSlider.value = health.GetHealth() / health.GetMAxHealth();
        }
    }
}
