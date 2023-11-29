using System.Collections.Generic;
using Sources.Presentations.Views;
using UnityEngine;

namespace Sources.PresentationsInterfaces.Vievs
{
    public interface ICommandCenterView
    {
        IReadOnlyList<CollectorView> Collectors { get; }
        Vector3 OverlapStartPoint { get; }
        float Radius { get; }
    }
}