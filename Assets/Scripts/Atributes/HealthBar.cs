using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Atributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health health = null;
        [SerializeField] RectTransform Foreground = null;
        [SerializeField] Canvas canvas = null;
        void Update()
        {
            float value = health.GetHealth() / health.GetMAxHealth();
            if(Mathf.Approximately(value, 1) || Mathf.Approximately(value, 0))
            {
                canvas.enabled = false;
                return;
            }
            canvas.enabled = true;
            Foreground.localScale = new Vector3(value, 1,1);
        }
    }
}
