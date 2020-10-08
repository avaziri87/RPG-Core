using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "RPG/Stats/New Progresion", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookuptable = null;
        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookup();
            float[] levels = lookuptable[characterClass][stat];
            if(levels.Length < level)
            {
                return 0;
            }

            return levels[level-1];
        }

        public int GetLevels(Stat stat, CharacterClass characterClass)
        {
            BuildLookup();
            float[] levels = lookuptable[characterClass][stat];
            return levels.Length;
        }

        private void BuildLookup()
        {
            if (lookuptable != null) return;

            lookuptable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach (ProgressionCharacterClass progresionClass in characterClasses)
            {
                var statLookupTable = new Dictionary<Stat, float[]>();
                foreach (ProgresionStat progresionStat in progresionClass.stats)
                {
                    statLookupTable[progresionStat.stat] = progresionStat.levels;
                }
                lookuptable[progresionClass.characterClass] = statLookupTable;
            }
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            //public float[] health;
            public ProgresionStat[] stats;
        }

        [System.Serializable]
        class ProgresionStat
        {
            public Stat stat;
            public float[] levels;
        }
    }
}
