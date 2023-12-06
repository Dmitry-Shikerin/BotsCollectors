using Sources.Domain.CommandСenters;
using Sources.Presentations.Views;
using Sources.PresentationsInterfaces.Views;
using UnityEngine;

namespace Sources.Domain.Collectors
{
    public class Collector
    {
        private bool _isIdle  = true;
        public CommandCenter CommandCenter { get; private set; }
        public ICommandCenterView CommandCenterView { get; private set; }
        public ICrystalView TargetCrystalView { get; private set; }
        public Vector3 TargetPosition => TargetCrystalView.Position;
        public Vector3 ParkingPoint => CommandCenter.ParkingPoint;
        public FlagView FlagView { get; private set; }

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
        
        public void SetCommandCenterView(ICommandCenterView commandCenterView) => 
            CommandCenterView = commandCenterView;

        public void SetFlag(FlagView flagView) => 
            FlagView = flagView;
    }
}