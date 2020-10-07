using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Saving;
using RPG.Stats;
using RPG.Core;
using System;
using GameDevTV.Utils;
using UnityEngine.Events;

namespace RPG.Atributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70;
        [SerializeField] TakeDamageEvent takeDamage;
        [SerializeField] UnityEvent onDie;
        [System.Serializable]
        public class TakeDamageEvent: UnityEvent<float>
        {
        }

        LazyValue<float> healthPoints;
        bool isDead = false;
        private void Awake()
        {
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }
        private void Start()
        {
            healthPoints.ForceInit();
        }
        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public void Heal(float healthRestore)
        {
            healthPoints.value = Mathf.Min(healthPoints.value + healthRestore, GetMAxHealth());
        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }
        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }
        public void TakeDamage(GameObject instigator, float damage)
        {
            Debug.Log(gameObject.name + " took damage: " + damage);
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);
            takeDamage.Invoke(damage);
            if (healthPoints.value == 0)
            {
                onDie.Invoke();
                AwardExperience(instigator);
                Die();
            }
        }
        public bool IsDead()
        {
            return isDead;
        }
        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;
            experience.GainXp(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }
        public float GetHealth()
        {
            return healthPoints.value;
        }
        public float GetMAxHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }
        private void Die()
        {
            isDead = true;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionScheduler>().CancelAction();
        }
        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * regenerationPercentage;
            healthPoints.value = Mathf.Max(healthPoints.value, regenHealthPoints);
        }
        public object CaptureState()
        {
            return healthPoints.value;
        }
        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;
            if (healthPoints.value == 0) Die();
        }
    }
}
