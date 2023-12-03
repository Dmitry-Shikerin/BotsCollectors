using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sources.Infrastructure.Services
{
    public class OverlapService
    {
        private readonly LinecastService _linecastService;

        public OverlapService(LinecastService linecastService)
        {
            _linecastService = linecastService ?? throw new ArgumentNullException(nameof(linecastService));
        }
        
        public IReadOnlyList<T> OverlapSphere<T>(Vector3 position, float radius,
            int searchLayerMask, int obstacleLayerMask, Collider[] colliders)
        {
            int collidersCount = Overlap(position, radius, colliders, searchLayerMask);

            if (collidersCount == 0)
                return new List<T>();

            return Filter<T>(position, obstacleLayerMask, collidersCount, colliders);
        }

        private IReadOnlyList<T> Filter<T>(Vector3 position, int obstacleLayerMask,
            int collidersCount, Collider [] colliders)
        {
            List<T> components = new List<T>();

            for (int i = 0; i < collidersCount; i++)
            {
                Collider collider = colliders[i];

                if (collider.TryGetComponent(out T component) == false)
                {
                    continue;
                }

                Vector3 colliderPosition = collider.transform.position;

                if (_linecastService.Linecast(position, colliderPosition, obstacleLayerMask))
                {
                    continue;
                }

                components.Add(component);
            }

            return components;
        }
        
        private int Overlap(Vector3 position, float radius, Collider[] results, int layerMask)
        {
            return Physics.OverlapSphereNonAlloc(position, radius, results, layerMask);
        }
    }
}