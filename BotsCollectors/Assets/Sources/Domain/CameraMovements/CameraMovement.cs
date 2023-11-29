using System;
using JetBrains.Annotations;
using Sources.Domain.CameraMovements.CameraMovementCharacteristics;
using UnityEngine;

namespace Sources.Domain.CameraMovements
{
    public class CameraMovement
    {
        private const float MultiplierZoomSpeed = 150;
        
        private readonly CameraMovementCharacteristic _characteristic;

        public CameraMovement(CameraMovementCharacteristic cameraMovementCharacteristic)
        {
            _characteristic = cameraMovementCharacteristic ??
                              throw new ArgumentNullException(nameof(cameraMovementCharacteristic));
        }
        
        public float AngleY { get; private set; }
        public float MovementSpeed => _characteristic.MovementSpeed;
        public float ZoomSpeed => _characteristic.ZoomSpeed;
        public float AngularSpeed => _characteristic.AngularSpeed;

        public void SetLeftRotation() => 
            AngleY -= AngularSpeed;

        public void SetRightRotation() => 
            AngleY += AngularSpeed;

        public float GetSpeed(float multiplier, float speedValue)
        {
            return multiplier * _characteristic.MultiplierSpeed + speedValue;
        }
        
        public Vector3 GetMovementDirection(Vector2 movementDirection, float speed)
        {
            return new Vector3(movementDirection.x, 0, movementDirection.y) * 
                   speed * Time.deltaTime;
        }

        //TODO как заклемпить значение зума
        public Vector3 GetZoomDirection(float scrollWheelAxis, float speed)
        {
            return new Vector3(0, 0, scrollWheelAxis) * 
                   speed * MultiplierZoomSpeed * Time.deltaTime;
        }
    }
}