using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistantObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject presistenObjectPrefab;
        static bool hasSpawn;

        private void Awake()
        {
            if (hasSpawn) return;
            SpawnPersistantObject();
            hasSpawn = true;
        }

        private void SpawnPersistantObject()
        {
            GameObject persistantObject = Instantiate(presistenObjectPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            DontDestroyOnLoad(persistantObject);
        }
    }
}
