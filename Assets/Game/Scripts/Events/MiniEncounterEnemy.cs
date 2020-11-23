using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Atributes;
using RPG.Control;
using GameDevTV.Saving;

namespace RPG.GameEvents
{
    public class MiniEncounterEnemy : MonoBehaviour, ISaveable
    {
        [SerializeField] Event gameEvent01 = null;
        [SerializeField] Event gameEvent02 = null;
        [SerializeField] GameObject effect = null;
        [SerializeField] GameObject prefab = null;
        [SerializeField] GameObject enemy = null;
        [SerializeField] SkinnedMeshRenderer enemyVisual = null;
        [SerializeField] Animator animator = null;
        bool isDead = false;

        private void Start()
        {
            prefab.SetActive(true);
        }
        private void Update()
        {
            if (enemy.GetComponent<Health>().IsDead() && !isDead)
            {
                isDead = true;
                gameEvent02.Ocurred(this.gameObject);
            }
        }
        public void TriggerSpawn(GameObject gameObject)
        {
            gameEvent01.Ocurred(this.gameObject);
            effect.SetActive(true);
            animator.SetTrigger("spawn");
            StartCoroutine(EnemySpawn());
        }

        IEnumerator EnemySpawn()
        {
            yield return new WaitForSeconds(1);
            effect.SetActive(false);
            prefab.SetActive(false);
            enemyVisual.enabled = true;
            enemy.GetComponent<AIControler>().enabled = true;
        }
        public object CaptureState()
        {
            return isDead;
        }

        public void RestoreState(object state)
        {
            isDead = (bool)state;
            if(isDead)
            {
                prefab.SetActive(false);
                enemyVisual.enabled = true;
                enemy.GetComponent<AIControler>().enabled = true;
            }
        }
    }
}
