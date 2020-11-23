using GameDevTV.Inventories;
using RPG.Atributes;
using RPG.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;
using UnityEngine.Events;

namespace RPG.Inventories
{
    public enum Type { Health, Experience, Key, Chest}
    [CreateAssetMenu(menuName = ("RPG/Inventory/Action Item"))]
    public class ConsumableItem : ActionItem
    {
        [SerializeField] float value = 20f;
        [SerializeField] Type type;
        [SerializeField] string doorName;

        public override void Use(GameObject user)
        {
            if (type == Type.Health) Heal(user);
            if (type == Type.Experience) GainXp(user);
            if (type == Type.Key) UseKey(user);
        }

        private void UseKey(GameObject user)
        {
            user.GetComponent<PlayerController>().door.OpenDoor(doorName);
        }

        private void Heal(GameObject user)
        {
            Health health = user.GetComponent<Health>();
            if (!health) return;
            health.Heal(value);
        }

        private void GainXp(GameObject user)
        {
            Experience experience = user.GetComponent<Experience>();
            if (!experience) return;
            experience.GainXp(value);
        }
    }
}
