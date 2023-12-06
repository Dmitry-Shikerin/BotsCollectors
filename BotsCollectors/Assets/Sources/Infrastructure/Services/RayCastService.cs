using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Sources.Infrastructure.Services
{
    public class RayCastService
    {
        private readonly Camera _cameraMain;

        public RayCastService(Camera cameraMain)
        {
            _cameraMain = cameraMain ? cameraMain :
                throw new ArgumentNullException(nameof(cameraMain));
        }
        
        public bool RayCast(out RaycastHit hit)
        {
            int maxDistance = 1000;
            
            Ray ray = _cameraMain.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(_cameraMain.transform.position, ray.direction, out hit, maxDistance);
        }
    }
}