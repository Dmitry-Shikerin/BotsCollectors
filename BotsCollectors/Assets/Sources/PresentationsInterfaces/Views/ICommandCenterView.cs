using Sources.Domain.CommandСenters;
using Sources.Presentations.Views;
using UnityEngine;

namespace Sources.PresentationsInterfaces.Views
{
    public interface ICommandCenterView
    {
        Vector3 OverlapStartPoint { get; }
        float Radius { get; }

        public CommandCenter BuildCommandCenter();
        FlagView InstantiateFlag(FlagView gameObject, Vector3 position);
        void DestroyFlag();
    }
}