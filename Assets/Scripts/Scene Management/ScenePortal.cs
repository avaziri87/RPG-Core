using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Saving;
using System;
using RPG.Control;

namespace RPG.SceneManagment
{
    public class ScenePortal : MonoBehaviour
    {
        enum destinationID
        {
            A,B,C,D
        }
        [SerializeField] string sceneToLoad = " ";
        [SerializeField] Transform spawnPoint;
        [SerializeField] destinationID destination;
        [SerializeField] float fadeOutTime = 0.0f;
        [SerializeField] float fadeInTime = 0.0f;
        [SerializeField] float fadeWaitTime = 0.0f;
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if(sceneToLoad == " ")
            {
                Debug.LogError("scene to load not set");
                yield break;
            }
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerController.enabled = false;

            yield return fader.FadeOut(fadeOutTime);

            wrapper.Save();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            PlayerController newPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            newPlayerController.enabled = false;
            wrapper.Load();

            ScenePortal otherScenePortal = GetOtherPortal();
            UpdatePlayer(otherScenePortal);
            wrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            fader.FadeIn(fadeInTime);
            newPlayerController.enabled = true;

            Destroy(gameObject);
        }

        private void PlayerControl(bool active)
        {

        }

        private void UpdatePlayer(ScenePortal otherScenePortal)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherScenePortal.spawnPoint.position);
            player.transform.rotation = otherScenePortal.spawnPoint.rotation;
        }

        private ScenePortal GetOtherPortal()
        {
            foreach(ScenePortal portals in FindObjectsOfType<ScenePortal>())
            {
                if (portals == this) continue;
                if (portals.destination != destination) continue;

                return portals;
            }
            return null;
        }
    }
}
