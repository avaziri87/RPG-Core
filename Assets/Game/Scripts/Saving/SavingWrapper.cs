using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Saving;

namespace RPG.SceneManagment
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField] float fadeInTime = 0.0f;
        const string saveFile = "save";
        void Awake()
        {
            StartCoroutine(LoadLastScene());
        }
        IEnumerator LoadLastScene()
        {
            yield return GetComponent<SavingSystem>().LoadLastScene(saveFile);
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return fader.FadeIn(fadeInTime);
        }
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log("Loading");
                Load();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("saving");
                Save();
            }

            if(Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
                Debug.Log("file deleted");
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(saveFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(saveFile);
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(saveFile);
        }
    }
}
