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

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        public DestinationNode destinationNode = null;
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
        [SerializeField] float maxPathLenght = 40f;
        private void Awake()
        {
            health = GetComponent<Health>();
        }
        void Update()
        {
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
        private bool InteractWithUI()
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool InteractWithComponent()
        {
            RaycastHit[] hits = SortedRaycast();
            foreach (RaycastHit hit in hits)
            {
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
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
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

            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path);
            if (!hasPath) return false;
            if (path.status != NavMeshPathStatus.PathComplete) return false;
            if (GetPathLenght(path) > maxPathLenght) return false;
            
            return true;
        }

        private float GetPathLenght(NavMeshPath path)
        {
            float total = 0;
            if (path.corners.Length < 2) return total;
            for(int i =0; i<path.corners.Length-1; i++)
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }
            return total;
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
    }
}
