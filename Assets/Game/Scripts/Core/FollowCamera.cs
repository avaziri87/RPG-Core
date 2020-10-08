using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform Target = null;
        void Update()
        {
            transform.position = Target.position;
        }
    }
}
