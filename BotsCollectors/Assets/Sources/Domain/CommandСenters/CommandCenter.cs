using System;
using System.Collections.Generic;
using Sources.Presentations.Views;
using Sources.PresentationsInterfaces.Views;
using Sources.Utils;
using UnityEngine;

namespace Sources.Domain.CommandСenters
{
    public class CommandCenter
    {
        private readonly ObservableProperty<int> _extractedResources;
        private readonly ObservableProperty<int> _foundedResources;
        private readonly ObservableProperty<int> _collectorsCount;
        
        private Queue<ICollectorView> _collectors = new Queue<ICollectorView>();
        private List<ICrystalView> _crystals = new List<ICrystalView>();

        private const int _minCountCollectors = 0;
        private const int _minValueExtractedResources = 0;
        
        public CommandCenter()
        {
            _extractedResources = new ObservableProperty<int>();
            _foundedResources = new ObservableProperty<int>();
            _collectorsCount = new ObservableProperty<int>();

            _extractedResources.Value = PricePerCommandCenter;
        }

        public IObservableProperty ExtractedResources => _extractedResources;
        public IObservableProperty FoundedResources => _foundedResources;
        public IObservableProperty CollectorsCount => _collectorsCount;
        public Collider[] Colliders { get; private set; } = new Collider[32];
        public Vector3 ParkingPoint { get; private set; }
        public FlagView FlagView { get; private set; }
        public bool CanBuild { get; private set; }
        public bool IsCollectorOnVay { get; private set; }
        public bool CanBuildState => FlagView != null;
        public int PricePerCommandCenter => 5;
        public int PricePerCollector => 1;

        public void SetCanBuild(bool canBuild) => 
            CanBuild = canBuild;

        public bool TryBuildCommandCenter()
        {
            return CanBuildState && _collectorsCount.Value > _minCountCollectors && 
                   _extractedResources.Value >= PricePerCommandCenter;
        }

        public void SendCollectorBuild()
        {
            if (_collectors.Count == _minCountCollectors)
                throw new InvalidOperationException(nameof(_collectors));
            
            if(FlagView == null)
                return;
            
            if(IsCollectorOnVay)
                return;

            ICollectorView collector = _collectors.Dequeue();
            SetIsCollectorOnWay(true);
            
            collector.SetFlag(FlagView);
            collector.SetCommandCenter(this);
            
            _collectorsCount.Value = _collectors.Count;
        }

        public void SetIsCollectorOnWay(bool isCollectorOnVay) => 
            IsCollectorOnVay = isCollectorOnVay;

        public void RemoveExtractedResources(int amount)
        {
            if(_extractedResources.Value <= _minValueExtractedResources)
                return;
            
            _extractedResources.Value -= amount;
        }
        
        public void SetFlag(FlagView flagView) => 
            FlagView = flagView;

        public void SetFoundedResources(int value) => 
            _foundedResources.Value = value;

        public void AddCollector(ICollectorView collector)
        {
            _collectors.Enqueue(collector);
            
            _collectorsCount.Value = _collectors.Count;
        }

        public void SendCollector(ICrystalView targetCrystal)
        {
            if (_collectors.Count == _minCountCollectors)
                throw new InvalidOperationException(nameof(_collectors));

            ICollectorView collector = _collectors.Dequeue();
            
            collector.SetCrystal(targetCrystal);
            collector.SetCommandCenter(this);
            
            _collectorsCount.Value = _collectors.Count;
        }
        
        public void AddExtractedResources(ICrystalView crystal)
        {
            if (crystal == null)
                throw new ArgumentNullException(nameof(crystal));
            
            crystal.RemoveParent();
            crystal.Destroy();

            _extractedResources.Value ++;
        }

        public void SetParkingPoint(Vector3 parkingPoint) => 
            ParkingPoint = parkingPoint;
    }
}