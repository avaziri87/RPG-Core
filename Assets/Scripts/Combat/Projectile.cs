using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Atributes;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 0.0f;
        [SerializeField] ProjectileMover effectPrefab = null;
        [SerializeField] bool isHooming = false;
        [SerializeField] float maxLifeTime = 0.0f;

        Health target = null;
        GameObject instigator = null;
        float damage = 0.0f;
        // Start is called before the first frame update
        void Start()
        {
            transform.LookAt(GetAim());
            if(effectPrefab != null) speed = effectPrefab.speed;
        }

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            this.target = target;
            this.damage = damage;
            this.instigator = instigator;

            Destroy(gameObject, maxLifeTime);
        }
        // Update is called once per frame
        void Update()
        {
            if (target == null) return;
            if(isHooming && !target.IsDead()) transform.LookAt(GetAim());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        Vector3 GetAim()
        {
            CapsuleCollider targetCollider = target.GetComponent<CapsuleCollider>();
            return target.transform.position + Vector3.up * targetCollider.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;
            target.TakeDamage(instigator, damage);
            Destroy(gameObject);
        }
    }
}
