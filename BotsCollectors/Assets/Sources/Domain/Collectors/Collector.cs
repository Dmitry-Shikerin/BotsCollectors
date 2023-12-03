using Sources.Domain.CommandСenters;
using Sources.PresentationsInterfaces.Views;
using UnityEngine;

namespace Sources.Domain.Collectors
{
    public class Collector
    {
        private bool _isIdle  = true;
        public CommandCenter CommandCenter { get; private set; }
        public ICrystalView TargetCrystalView { get; private set; }
        public Vector3 TargetPosition => TargetCrystalView.Position;
        public Vector3 ParkingPoint => CommandCenter.ParkingPoint;

        public void SetIdle(bool isIdle) => 
            _isIdle = isIdle;

        public void SetTarget(ICrystalView targetCrystal)
        {
            if (_isIdle == false)
            {
                return;
            }

            TargetCrystalView = targetCrystal;
        
            if(targetCrystal == null)
                return;
        
            TargetCrystalView.ChangeLayerMask();
        }

        public void SetCommandCenter(CommandCenter commandCenter) => 
            CommandCenter = commandCenter;
    }
}