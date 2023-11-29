using System;
using JetBrains.Annotations;
using Sources.Domain.CollectorCharacteristics;
using Sources.PresentationsInterfaces.Vievs;
using UnityEngine;

namespace Sources.Domain
{
    public class Collector
    {
        private readonly CollectorCharacteristic _characteristics;

        public Collector()
        {
        }

        public bool IsIdle => TargetCrystalView == null;
        public CommandCenterView CurrentCommandCenterView { get; private set; }
        // public Transform CrystalTrunkPoint => _characteristics.CrystalTrunkPoint;
        public ICrystalView TargetCrystalView { get; private set; }
        public Vector3 TargetPosition => TargetCrystalView.Position;
        public Vector3 ParkingPoint => CurrentCommandCenterView.ParkingPoint.transform.position;
        
        public void SetTarget(ICrystalView targetCrystal)
        {
            if (IsIdle == false)
            {
                Debug.Log("Коллектор занят");
                return;
            }

            TargetCrystalView = targetCrystal;
        
            if(targetCrystal == null)
                return;
        
            TargetCrystalView.Hide();
        }

        public void SetCommandCenter(CommandCenterView commandCenter)
        {
            CurrentCommandCenterView = commandCenter;
        }
    }
}