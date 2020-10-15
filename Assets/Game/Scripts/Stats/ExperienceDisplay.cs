using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        [SerializeField] Slider xpSlider;
        [SerializeField] TextMeshProUGUI text;

        Experience experience;
        BaseStats baseStats;
        private void Awake()
        {
            experience = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();
            baseStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
        }
        void Update()
        {
            text.text = baseStats.GetLevel().ToString();
            xpSlider.value = experience.GetXp() / baseStats.GetBaseStat(Stat.ExperienceToLevelUp);
        }
    }
}
