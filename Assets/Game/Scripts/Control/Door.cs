using GameDevTV.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class Door : MonoBehaviour, ISaveable
    {
        [SerializeField] string doorName = " ";
        [SerializeField] Animator animator = null;
        [SerializeField] GameObject portal;
        bool hasOpened = false;

        public void OpenDoor(string name)
        {
            if(doorName == name)
            {
                hasOpened = true;
                animator.SetTrigger("Open");
                StartCoroutine(ActivatePortal());
            }
        }
        IEnumerator ActivatePortal()
        {
            yield return new WaitForSeconds(1.5f);
            portal.GetComponent<BoxCollider>().enabled = true;
        }
        public object CaptureState()
        {
            return hasOpened;
        }

        public void RestoreState(object state)
        {
            hasOpened = (bool)state;
            if(hasOpened)
            {
                animator.SetTrigger("Has Opened");
                StartCoroutine(ActivatePortal());
            }
        }
    }
}
