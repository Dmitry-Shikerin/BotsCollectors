using UnityEngine;
using UnityEngine.AI;

namespace Sources.PresentationsInterfaces.Vievs
{
    public interface ICollectorView
    {
        Transform CrystalTrunkPoint { get; }
        NavMeshAgent NavMeshAgent { get; }
        Vector3 Position { get; }
        void SetDestination(Vector3 destination);
        CrystalView GetCrystal();
    }
}