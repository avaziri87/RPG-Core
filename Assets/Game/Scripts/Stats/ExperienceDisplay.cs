using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        [SerializeField] Text xpText;
        [SerializeField] Text lvlText;
        Experience experience;
        BaseStats baseStats;
        private void Awake()
        {
            experience = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();
            baseStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
        }
        void Update()
        {
            xpText.text = " " + experience.GetXp();
            lvlText.text = "" + baseStats.GetLevel();
        }
    }
}
