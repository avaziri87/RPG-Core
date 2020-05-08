using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;
using RPG.Atributes;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] WeaponConfig weapon;
        [SerializeField] float respawnTime = 0.0f;
        [SerializeField] float healthRestore = 0.0f;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                PickUp(other.gameObject);
            }
        }

        private void PickUp(GameObject subject)
        {
            if(weapon != null)
            {
                subject.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(Hide(respawnTime));
            }
            if(healthRestore >0)
            {
                subject.GetComponent<Health>().Heal(healthRestore);
            }
        }
        private IEnumerator Hide(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }
        private void ShowPickup(bool show)
        {
            GetComponent<BoxCollider>().enabled = show;
            foreach(Transform child in transform)
            {
                child.gameObject.GetComponent<MeshRenderer>().enabled = show;
            }
        }
        public bool HandleRaycast(PlayerController callingControler)
        {
            if(Input.GetMouseButtonDown(0))
            {
                PickUp(callingControler.gameObject);
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }
}
