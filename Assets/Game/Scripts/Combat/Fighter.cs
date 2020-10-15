using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using GameDevTV.Saving;
using RPG.Atributes;
using RPG.Stats;
using GameDevTV.Utils;
using GameDevTV.Inventories;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] WeaponConfig defaultWeapon = null;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;

        Health target;
        Equipment equipment;
        float timeSinceLastAttack = Mathf.Infinity;
        WeaponConfig currentWeaponConfig;
        LazyValue<Weapon> currentWeapon;

        private void Awake()
        {
            currentWeaponConfig = defaultWeapon;
            currentWeapon = new LazyValue<Weapon>(SetUpDefaultWeapon);
            equipment = GetComponent<Equipment>();
            if(equipment)
            {
                equipment.equipmentUpdated += UpdateWeapon;
            }

        }

        private Weapon SetUpDefaultWeapon()
        {
             return AttachWeapon(defaultWeapon);
        }

        private void Start()
        {
            currentWeapon.ForceInit();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead())
            {
                GetComponent<Animator>().ResetTrigger("Attack");
                GetComponent<Animator>().SetTrigger("StopAttack");
                return;
            }

            if (!GetIsInRange(target.transform))
            {
                GetComponent<Animator>().ResetTrigger("Attack");
                GetComponent<Animator>().SetTrigger("StopAttack");
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }

        }
        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > currentWeaponConfig.GetAttackSpeed())
            {
                GetComponent<Animator>().ResetTrigger("StopAttack");
                GetComponent<Animator>().SetTrigger("Attack");
                timeSinceLastAttack = 0;
            }
        }
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }
        public void EquipWeapon(WeaponConfig weapon)
        {
            currentWeaponConfig = weapon;
            currentWeapon.value = AttachWeapon(weapon);
        }
        private void UpdateWeapon()
        {
            var weapon = equipment.GetItemInSlot(EquipLocation.Weapon) as WeaponConfig;
            if (weapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
            else
            {
                EquipWeapon(weapon);
            }
        }
        private Weapon AttachWeapon(WeaponConfig weapon)
        {
            Animator animator = GetComponent<Animator>();
            return weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public Health GetTargetHealth()
        {
            return target;
        }
        //Animation Event
        void Hit()
        {
            if (target == null) return;
            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            if (gameObject.tag == "Player") Debug.Log("damage is: " + damage);

            if (currentWeapon.value != null)
            {
                currentWeapon.value.OnHit();
            }

            if (currentWeaponConfig.HasPrjectile())
            {
                currentWeaponConfig.InstantiateProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage);
            }
            else
            {
                target.TakeDamage(gameObject, damage);
            }
        }
        //Animation Event
        void Shoot()
        {
            Hit();
        }
        void FootR()
        {

        }
        void FootL()
        {

        }
        private bool GetIsInRange(Transform target)
        {
            return Vector3.Distance(transform.position, target.position) < currentWeaponConfig.GetWeaponRange();
        }
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            if (!GetComponent<Mover>().CanMoveto(combatTarget.transform.position) && !GetIsInRange(combatTarget.transform)) 
            { 
                return false; 
            }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }
        public void Cancel()
        {
            if (target == null) return;
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("StopAttack");
            target = null;
            GetComponent<Mover>().Cancel();
        }

        public object CaptureState()
        {
            return currentWeapon.value.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            WeaponConfig weapon = UnityEngine.Resources.Load<WeaponConfig>(weaponName);
            EquipWeapon(weapon);
        }
    }
}
