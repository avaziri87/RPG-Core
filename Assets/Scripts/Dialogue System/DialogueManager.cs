using RPG.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        private Queue<string> sentences;
        [SerializeField] Text nameText;
        [SerializeField] Text dialogueText;
        [SerializeField] Animator animator;
        [SerializeField] PlayerController player = null;

        void Start()
        {
            sentences = new Queue<string>();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            DisableControl();
            animator.SetTrigger("Open");
            nameText.text = dialogue.name;

            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if(sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(Type(sentence));
        }

        public void EndDialogue()
        {
            animator.SetTrigger("Close");
            EnableControl();
        }

        IEnumerator Type(string sentence)
        {
            dialogueText.text = "";
            foreach(char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return (null);
            }
        }

        void DisableControl()
        {
            player.enabled = false;

        }
        void EnableControl()
        {
            player.enabled = true;
        }
    }

}