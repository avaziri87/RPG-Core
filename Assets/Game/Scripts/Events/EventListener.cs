using UnityEngine;
using UnityEngine.Events;

namespace RPG.GameEvents
{
    [System.Serializable]
    public class UnityGameObjectEvent : UnityEvent<GameObject>
    {

    }
    public class EventListener : MonoBehaviour
    {
        public Event gEvent;
        public UnityGameObjectEvent response = new UnityGameObjectEvent();

        private void OnEnable()
        {
            gEvent.Register(this);
        }
        private void OnDisable()
        {
            gEvent.Unregister(this);
        }
        public void OnEventOcurred(GameObject gameObject)
        {
            response.Invoke(gameObject);
        }
    }
}
