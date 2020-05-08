using RPG.Core;
using RPG.Atributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] Weapon equipedPrefab = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;
        [SerializeField] float weaponRange = 2.0f;
        [SerializeField] float attackSpeed = 1.0f;
        [SerializeField] float weaponDamage = 1.0f;
        [SerializeField] float weaponPercentageBuff = 0.0f;

        const string weaponName = "weapon";

        public Weapon Spawn(Transform rightHandTransform,Transform leftHandTransform, Animator animator)
        {
            DestroyOldWeapon(rightHandTransform, leftHandTransform);
            Weapon weapon = null;
            if(equipedPrefab != null)
            {
                Transform handTransform = GetHand(rightHandTransform, leftHandTransform);
                weapon =  Instantiate(equipedPrefab, handTransform);
                weapon.gameObject.name = weaponName;
            }
            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if(overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }

            return weapon;
        }

        private void DestroyOldWeapon(Transform rightHandTransform, Transform leftHandTransform)
        {
            Transform oldWeapon = rightHandTransform.Find(weaponName);
            if(oldWeapon == null)
            {
                oldWeapon = leftHandTransform.Find(weaponName);
            }
            if (oldWeapon == null) return;

            oldWeapon.name = "destroy";
            Destroy(oldWeapon.gameObject);
        }

        private Transform GetHand(Transform rightHandTransform, Transform leftHandTransform)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHandTransform;
            else handTransform = leftHandTransform;
            return handTransform;
        }

        public bool HasPrjectile()
        {
            return projectile != null;
        }
        public void InstantiateProjectile(Transform rightHandTransform, Transform leftHandTransform, Health target, GameObject instigator, float damage)
        {
            Projectile projectileInstance = Instantiate(projectile, GetHand(rightHandTransform, leftHandTransform).position, Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, damage);
        }
        public float GetWeaponDamage()
        {
            return weaponDamage;
        }
        public float GetWeaponRange()
        {
            return weaponRange;
        }        
        public float GetAttackSpeed()
        {
            return attackSpeed;
        }
        public float GetPercentageBuff()
        {
            return weaponPercentageBuff;
        }
    }
}
