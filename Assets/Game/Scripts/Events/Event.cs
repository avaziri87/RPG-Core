using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameEvents
{
    [CreateAssetMenu(fileName ="New Game Event", menuName ="RPG/Game Event")]
    public class Event : ScriptableObject
    {
        List<EventListener> eventListeners = new List<EventListener>();

        public void Register(EventListener listener)
        {
            eventListeners.Add(listener);
        }

        public void Unregister(EventListener listener)
        {
            eventListeners.Remove(listener);
        }

        public void Ocurred(GameObject gameObject)
        {
            for (int i = 0; i < eventListeners.Count; i++)
            {
                eventListeners[i].OnEventOcurred(gameObject);
            }
        }
    }
}
