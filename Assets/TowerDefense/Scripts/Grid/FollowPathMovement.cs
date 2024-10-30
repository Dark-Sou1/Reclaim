using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Giacomo
{
    public class FollowPathMovement : MonoBehaviour
    {
        [ReadOnly] public Tile startTile;
        [ReadOnly] public Tile destinationTile;
        [DisableInEditorMode] public Vector3 positionOffset;

        public event Action OnArrive;

        protected List<Tile> path;
        protected int currentTileIndex;
        protected Vector3 nextTargetPosition;

        Tile CurrentTile => GridManager.Instance.Get(transform.position);


        public void SetDestination(Tile destination)
        {
            destinationTile = destination;

            if (destination == null)
            {
                path = null;
                return;
            }

            UpdatePath();
        }

        protected void UpdatePath()
        {
            if (destinationTile == null)
                return;

            path = Pathfinder.FindPath(GridManager.Instance, CurrentTile, destinationTile);
        
            if (path == null) 
                return;

            currentTileIndex = 0;
            nextTargetPosition = path[0].transform.position + positionOffset;
        }
    

        public void Move(float amount)
        {
            if (path == null)
            {
                UpdatePath();

                if (path == null)
                    return;
            }

            if (currentTileIndex >= path.Count)
                return;

            float distance = Vector3.Distance(transform.position, nextTargetPosition);
            if (amount >= distance)
            {
                transform.position = nextTargetPosition;
                amount -= distance;

                currentTileIndex++;

                if (currentTileIndex == path.Count)
                {
                    OnArrive?.Invoke();
                    SetDestination(null);
                    return;
                }

                nextTargetPosition = path[currentTileIndex].transform.position + positionOffset;

                if (path.Count <= 1 || path[currentTileIndex] == null || !path[currentTileIndex].IsWalkable)
                {
                    UpdatePath();
                    if (path == null)
                        return;
                }
            }

            Vector3 dir = (nextTargetPosition - transform.position).normalized;
            transform.position += dir * amount;
        }



        public float DistanceFromTarget()
        {
            if (path == null) return float.MaxValue;
            return path.Count - currentTileIndex - 1 + Vector3.Distance(transform.position, nextTargetPosition);
        }
    }

}