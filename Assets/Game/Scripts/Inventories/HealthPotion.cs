using GameDevTV.Inventories;
using RPG.Atributes;
using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventories
{
    public enum Type { Health, Experience}
    [CreateAssetMenu(menuName = ("RPG/Inventory/Action Item"))]
    public class HealthPotion : ActionItem
    {
        [SerializeField] float value = 20f;
        [SerializeField] Type type;

        public override void Use(GameObject user)
        {
            if (type == Type.Health) Heal(user);
            if (type == Type.Experience) GaionXp(user);
        }

        private void Heal(GameObject user)
        {
            Health health = user.GetComponent<Health>();
            if (!health) return;
            health.Heal(value);
        }

        private void GaionXp(GameObject user)
        {
            Experience experience = user.GetComponent<Experience>();
            if (!experience) return;
            experience.GainXp(value);
        }
    }
}
