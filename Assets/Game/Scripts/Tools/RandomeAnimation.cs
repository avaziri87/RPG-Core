using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomeAnimation : MonoBehaviour
{
    [SerializeField] string[] animationTriggers;

    public string RandomeAnimationTrigger()
    {
        return animationTriggers[Random.Range(0, animationTriggers.Length)];
    }
}
