using UnityEngine;

namespace Sources.Domain.CameraMovements.CameraMovementCharacteristics
{
    [CreateAssetMenu(fileName = "CameraMovementCharacteristics", 
        menuName = "Characteristics/CameraMovementCharacteristic", order = 51)]
    public class CameraMovementCharacteristic : ScriptableObject
    {
        [field : SerializeField] public float RotateSpeed { get; private set; } = 10f;
        [field : SerializeField] public float MovementSpeed { get; private set; } = 10f;
        [field : SerializeField] public float ZoomSpeed { get; private set; } = 10f;
        [field : SerializeField] public float MultiplierSpeed { get; private set; } = 2;
        [field: SerializeField] public float AngularSpeed { get; private set; } = 1;
    }
}