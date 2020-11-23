using GameDevTV.Inventories;
using RPG.Inventories;
using GameDevTV.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    [RequireComponent(typeof(Pickup))]
    public class ChestPickup : MonoBehaviour, IRaycastable, ISaveable
    {
        Pickup pickup;
        bool hasBeenPickedup = false;
        [SerializeField] RandomDropper randomDropper = null;

        private void Awake()
        {
            pickup = GetComponent<Pickup>();
            if (randomDropper != null) Clickable();
        }

        public CursorType GetCursorType()
        {
            if (pickup.CanBePickedUp() && !hasBeenPickedup)
            {
                return CursorType.Pickup;
            }
            else
            {
                return CursorType.FullPickup;
            }
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(randomDropper != null)
                {
                    randomDropper.RandomDrop();
                    GetComponent<BoxCollider>().enabled = false;
                }
                else
                {
                    pickup.PickupItem();
                }
                hasBeenPickedup = true;
                GetComponent<Animator>().SetTrigger("Open");
            }
            return true;
        }

        public void Clickable()
        {
            GetComponent<BoxCollider>().enabled = true;
        }

        public object CaptureState()
        {
            return hasBeenPickedup;
        }

        public void RestoreState(object state)
        {
            hasBeenPickedup = (bool)state;

            if(hasBeenPickedup)
            {
                GetComponent<Animator>().SetTrigger("Set Opened");
                GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
