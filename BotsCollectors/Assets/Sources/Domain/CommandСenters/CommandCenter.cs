using System;
using System.Collections.Generic;
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

        public CommandCenter()
        {
            _extractedResources = new ObservableProperty<int>();
            _foundedResources = new ObservableProperty<int>();
            _collectorsCount = new ObservableProperty<int>();
        }

        public IObservableProperty ExtractedResources => _extractedResources;
        public IObservableProperty FoundedResources => _foundedResources;
        public IObservableProperty CollectorsCount => _collectorsCount;
        public Collider[] Colliders { get; private set; } = new Collider[32];
        public Vector3 ParkingPoint { get; private set; }

        public void SetFoundedResources(int value) => 
            _foundedResources.Value = value;

        public void AddCollector(ICollectorView collector)
        {
            _collectors.Enqueue(collector);
            
            _collectorsCount.Value = _collectors.Count;
        }

        public void SendCollector(ICrystalView targetCrystal)
        {
            if (_collectors.Count == 0)
                throw new InvalidOperationException(nameof(_collectors));

            ICollectorView collector = _collectors.Dequeue();
            
            collector.SetTarget(targetCrystal);
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