using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] AudioClip soundToPlay = null;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<SoundMixer>().FadeOut(soundToPlay);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<SoundMixer>().FadeIn();
        }
    }
}
