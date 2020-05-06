using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Atributes;
using RPG.Control;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }

        public bool HandleRaycast(PlayerController callingControler)
        {
            if (Input.GetMouseButtonDown(0))
            {
                callingControler.destinationNode.TurnOff();
                callingControler.GetComponent<Fighter>().Attack(gameObject);
            }
            return true;
        }
    }
}
