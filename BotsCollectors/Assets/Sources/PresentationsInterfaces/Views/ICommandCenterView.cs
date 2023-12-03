using System.Collections.Generic;
using Sources.InfrastructureInterfaces.Factoryes;
using Sources.InfrastructureInterfaces.Services;
using Sources.Presentations.Views;
using UnityEngine;

namespace Sources.PresentationsInterfaces.Views
{
    public interface ICommandCenterView
    {
        Vector3 OverlapStartPoint { get; }
        float Radius { get; }
        
    }
}