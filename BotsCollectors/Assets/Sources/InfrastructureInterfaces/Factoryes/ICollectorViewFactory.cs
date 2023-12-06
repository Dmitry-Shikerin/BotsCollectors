using Sources.Infrastructure.Builders;
using Sources.PresentationsInterfaces.Views;
using UnityEngine;

namespace Sources.InfrastructureInterfaces.Factoryes
{
    public interface ICollectorViewFactory
    {
        ICollectorView Create(ICommandCenterView view, Vector3 spawnPosition);
    }
}