using Sources.App.Core;
using Sources.Domain.CommandSenters;
using UnityEngine;

namespace Sources.Infrastructure.Factoryes.AppCores
{
    public class AppCoreFactory
    {
        public AppCore Create()
        {
            AppCore appCore = new GameObject(nameof(AppCore)).AddComponent<AppCore>();
            
            return appCore;
        }
    }
}