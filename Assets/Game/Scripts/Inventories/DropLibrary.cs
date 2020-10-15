using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Inventories;

namespace RPG.Inventories
{
    [CreateAssetMenu(menuName = ("RPG/Inventory/Drop Library"))]
    public class DropLibrary : ScriptableObject
    {
        [Tooltip("")]
        [SerializeField] DropConfig[] potentialDrops;
        [Tooltip("")]
        [SerializeField] float[] dropChancePercentage;
        [Tooltip("minimun items dropped")]
        [SerializeField] int[] minDrops;
        [Tooltip("maximun items dropped")]
        [SerializeField] int[] maxDrops;

        [System.Serializable]
        class DropConfig
        {
            [Tooltip("Item to be dropped")]
            public InventoryItem item;
            [Tooltip("")]
            public float[] relativeChance;
            [Tooltip("")]
            public int[] minNumber;
            [Tooltip("")]
            public int[] maxNumber;
            public int GetRandomNumber(int level)
            {
                if(!item.IsStackable())
                {
                    return 1;
                }
                int min = GetByLevel(minNumber, level);
                int max = GetByLevel(maxNumber, level);
                return Random.Range(min, max + 1);
            }
        }
        public struct Dropped
        {
            public InventoryItem item;
            public int number;
        }
        public IEnumerable<Dropped> GetRandomDrops(int level)
        {
            if(!shouldDropRandom(level))
            {
                yield break;
            }
            for(int i = 0; i <GetRandomNumberOfDrops(level); i++)
            {
                yield return GetRandomDrop(level);
            }
        }
        bool shouldDropRandom(int level)
        {
           return Random.Range(0,100) < GetByLevel(dropChancePercentage, level);
        }
        int GetRandomNumberOfDrops(int level)
        {
            int min = GetByLevel(minDrops, level);
            int max = GetByLevel(maxDrops, level);
            return Random.Range(min, max + 1);
        }
        Dropped GetRandomDrop(int level)
        {
            var drop = SelectRandomItem(level);
            var result = new Dropped();
            result.item = drop.item;
            result.number = drop.GetRandomNumber(level);
            return result;
        }
        DropConfig SelectRandomItem(int level)
        {
            float totalChance = GetTotalChance(level);
            float randomRoll = UnityEngine.Random.Range(0, totalChance);
            float chanceTotal = 0;
            foreach(var drop in potentialDrops)
            {
                chanceTotal += GetByLevel(drop.relativeChance, level);
                if (chanceTotal > randomRoll)
                {
                    return drop;
                }
            }
            return null;
        }

        private float GetTotalChance(int level)
        {
            float total = 0;
            foreach(var drop in potentialDrops)
            {
                total += GetByLevel(drop.relativeChance, level);
            }
            return total;
        }
        static T GetByLevel<T>(T[] values, int level)
        {
            if(values.Length == 0)
            {
                return default;
            }
            if(level > values.Length)
            {
                return values[values.Length - 1];
            }
            if(level <= 0)
            {
                return default;
            }
            return values[level - 1];
        }
    }

}
