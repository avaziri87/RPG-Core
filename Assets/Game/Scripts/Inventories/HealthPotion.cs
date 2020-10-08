using GameDevTV.Inventories;
using RPG.Atributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventories
{
    [CreateAssetMenu(menuName = ("RPG/Inventory/Action Item"))]
    public class HealthPotion : ActionItem
    {
        [SerializeField] float healAmount = 20f;

        public override void Use(GameObject user)
        {
            Health health = user.GetComponent<Health>();
            if (!health) return;
            health.Heal(healAmount);
        }
    }
}
