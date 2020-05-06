using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] Weapon weapon;
        [SerializeField] float respawnTime = 0.0f;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                PickUp(other.GetComponent<Fighter>());
            }
        }

        private void PickUp(Fighter fighter)
        {
            fighter.EquipWeapon(weapon);
            StartCoroutine(Hide(respawnTime));
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
                PickUp(callingControler.GetComponent<Fighter>());
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }
}
