﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Inventories;
using UnityEngine.AI;
using RPG.Stats;

namespace RPG.Inventories
{
    public class RandomDropper : ItemDropper
    {
        [Tooltip("how far can they items scatter from the dropper")]
        [SerializeField] float scatterDistance = 1;
        [SerializeField] DropLibrary dropLibrary;
        [Tooltip("how many items are going to be dropped")]
        [SerializeField] int numberOfDrops = 2;

        const int attempts = 30;

        public void RandomDrop()
        {
            var baseStats = GetComponent<BaseStats>();
            var drops = dropLibrary.GetRandomDrops(baseStats.GetLevel());
            //Debug.Log("drops: " + drops);
            foreach(var drop in drops)
            {
                Debug.Log("Item Dropped: " + drop.item + "Amount Dropped: " + drop.number);
                DropItem(drop.item, drop.number);                
            }

        }
        protected override Vector3 GetDropLocation()
        {
            for(int i=0; i < attempts; i++)
            {
                Vector3 randomPoint = transform.position + Random.insideUnitSphere * scatterDistance;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 0.1f, NavMesh.AllAreas))
                {
                    return hit.position;
                }
            }
            return transform.position;
        }
    }
}
