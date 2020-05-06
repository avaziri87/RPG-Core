using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    [SerializeField] GameObject target = null;
    public void DestroyObject()
    {
        Destroy(target);
    }
}
