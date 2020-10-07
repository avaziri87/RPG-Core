using RPG.Control;
using RPG.Movement;
using GameDevTV.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.DialogueSystem
{
    public class NPCCharacter : MonoBehaviour, IRaycastable, ISaveable
    {
        [SerializeField] Dialogue dialogue01;
        [SerializeField] Dialogue dialogue02;
        [SerializeField] Transform interactPoint = null;
        bool dialoguePlayed = false;

        public object CaptureState()
        {
            return dialoguePlayed;
        }

        public CursorType GetCursorType()
        {
            return CursorType.NPCdialogue;
        }

        public bool HandleRaycast(PlayerController callingControler)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (callingControler.GetComponent<Mover>().CanMoveto(interactPoint.position))
                {
                    callingControler.GetComponent<Mover>().StartMoveAction(interactPoint.position, 1f);
                }
            }
            return true;
        }

        public void RestoreState(object state)
        {
            bool hasPlayed = (bool)state;
            dialoguePlayed = hasPlayed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                other.transform.LookAt(transform.position);
                if (!dialoguePlayed)
                {
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue01);
                    dialoguePlayed = true;
                }
                else
                {
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue02);
                }
            }
        }
    }
}
