using Sources.Domain.CommandСenters;
using Sources.Presentations.Views;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.PresentationsInterfaces.Views
{
    public interface ICollectorView
    {
        Transform CrystalTrunkPoint { get; }
        NavMeshAgent NavMeshAgent { get; }
        Vector3 Position { get; }
        
        void SetDestination(Vector3 destination);
        CrystalView GetCrystal();
        void SetCrystal(ICrystalView destination);
        void SetCommandCenter(CommandCenter commandCenter);
        void SetFlag(FlagView flagView);
    }
}