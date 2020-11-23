using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameEvents
{
    public class Enemy
    {
        public GameObject owner;
    }
    public class EncounterManager : MonoBehaviour
    {
        [SerializeField] List<Enemy> enemies = new List<Enemy>();
        [SerializeField] Event gameEvent01 = null;
        [SerializeField] Event gameEvent02 = null;
        [SerializeField] AudioClip soundToPlay = null;

        bool encounterFinished = false;

        private void OnEnable()
        {
            if (encounterFinished)
            {
                gameEvent02.Ocurred(this.gameObject);
                GetComponent<BoxCollider>().enabled = false;
            }
        }
        public void UnregisterEnemy(GameObject enemy)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].owner == enemy)
                {
                    enemies.Remove(enemies[i]);
                }
                if (enemies.Count < 1)
                {
                    encounterFinished = true;
                    gameEvent02.Ocurred(this.gameObject);
                    FindObjectOfType<SoundMixer>().FadeIn();
                }
            }
        }
        public void RegisterEnemy(GameObject enemy)
        {
            enemies.Add(new Enemy() { owner = enemy });
        }
        public void EnemySpawn(GameObject enemy)
        {
            RegisterEnemy(enemy);
        }
        public void EnemyDie(GameObject enemy)
        {
            UnregisterEnemy(enemy);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                gameEvent01.Ocurred(this.gameObject);
                GetComponent<BoxCollider>().enabled = false;
                FindObjectOfType<SoundMixer>().FadeOut(soundToPlay);
            }
        }
    }
}
