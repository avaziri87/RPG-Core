using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using RPG.Atributes;
using System;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using GameDevTV.Inventories;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        public DestinationNode destinationNode = null;
        public Door door;
        Health health;

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }
        [SerializeField] CursorMapping[] curorMappings = null;
        [SerializeField] float maxNavMeshProjection = 1f;
        [SerializeField] float raycastRadius = 1f;

        bool isDragginUI = false;
        private void Awake()
        {
            health = GetComponent<Health>();
        }
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            CheckSpecialAbilityKeys();
            if (InteractWithUI()) return;
            if (health.IsDead())
            {
                SetCursor(CursorType.None);            
                return;
            }
            if (InteractWithComponent()) return;
            if (InteractWithMovement()) return;
            SetCursor(CursorType.None);
        }
        private void CheckSpecialAbilityKeys()
        {
            var actionStore = GetComponent<ActionStore>();
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                actionStore.Use(0, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                actionStore.Use(1, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                actionStore.Use(2, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                actionStore.Use(3, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                actionStore.Use(4, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                actionStore.Use(5, gameObject);
            }
        }
        private bool InteractWithUI()
        {
            if (Input.GetMouseButtonUp(0)) isDragginUI = false;
            if(EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0)) isDragginUI = true;
                SetCursor(CursorType.UI);
                return true;
            }
            if(isDragginUI)
            {
                return true;
            }
            return false;
        }
        private bool InteractWithComponent()
        {
            RaycastHit[] hits = SortedRaycast();
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.tag == "Player") return false;
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach(IRaycastable raycastable in raycastables)
                {
                    if(raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }
        RaycastHit[] SortedRaycast()
        {
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), raycastRadius);
            float[] distance = new float[hits.Length];
            for(int i = 0; i< hits.Length; i++)
            {
                distance[i] = hits[i].distance;
            }
            Array.Sort(distance, hits);
            return hits;
        }
        private bool InteractWithMovement()
        {

            Vector3 target;
            bool hashit = RaycastNavmesh(out target);
            if (hashit)
            {
                if (!GetComponent<Mover>().CanMoveto(target)) return false;
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Mover>().StartMoveAction(target, 1f);
                    destinationNode.TurnOn(target);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        private bool RaycastNavmesh(out Vector3 target)
        {
            target = new Vector3();
            RaycastHit hit;
            bool hashit = Physics.Raycast(GetMouseRay(), out hit);
            if (!hashit) return false;

            NavMeshHit navMeshHit;
            bool hasCastTo  = NavMesh.SamplePosition(hit.point, out navMeshHit, maxNavMeshProjection, NavMesh.AllAreas);
            if (!hasCastTo) return false;
            target = navMeshHit.position;
            
            return true;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMappig(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }
        private CursorMapping GetCursorMappig(CursorType type)
        {
            foreach(CursorMapping mapping in curorMappings)
            {
                if(mapping.type == type)
                {
                    return mapping;
                }
            }
            return curorMappings[0];
        }
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private void OnTriggerStay(Collider other)
        {
            door = other.GetComponent<Door>();
        }
    }
}
