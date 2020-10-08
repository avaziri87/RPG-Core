using RPG.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.DialogueSystem
{
    public class DialogueTrigger : MonoBehaviour, IRaycastable
    {
        [SerializeField] Dialogue dialogue01;
        [SerializeField] GameObject Player;
        [SerializeField] GameObject NPC;
        bool continueDialogue = false;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                NextDialogue();
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                NextDialogue();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                FindObjectOfType<DialogueManager>().EndDialogue();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {

            }
        }

        private void NextDialogue()
        {
            //if (dialogue01 != null && dialogue01.HasPlayed == false)
            //{
            //    NPC.transform.LookAt(Player.transform);
            //    FindObjectOfType<DialogueManager>().StartDialogue(dialogue01);
            //    return;
            //}
            //if (dialogue02 != null && dialogue02.HasPlayed == false)
            //{
            //    NPC.transform.LookAt(Player.transform);
            //    FindObjectOfType<DialogueManager>().StartDialogue(dialogue02);
            //    return;
            //}
            //if (dialogue03 != null && dialogue03.HasPlayed == false)
            //{
            //    NPC.transform.LookAt(Player.transform);
            //    FindObjectOfType<DialogueManager>().StartDialogue(dialogue03);
            //    return;
            //}
        }

        public CursorType GetCursorType()
        {
            return CursorType.NPCdialogue;
        }

        public bool HandleRaycast(PlayerController callingControler)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("dialogue active");
            }
            return true;
        }
    }
}
