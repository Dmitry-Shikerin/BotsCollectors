using UnityEngine;

namespace Sources.Domain.CollectorCharacteristics
{
    [CreateAssetMenu(fileName = "CollectorCharacteristics", 
        menuName = "Characteristics/CollectorCharacteristic", order = 51)]
    public class CollectorCharacteristic : ScriptableObject
    {
        [field : SerializeField] public Transform CrystalTrunkPoint { get; private set; }
    }
}