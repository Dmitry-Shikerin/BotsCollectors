using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sources.Domain.CommandSenters
{
    public class CommandCenter
    {
        // private IEnumerable<CrystalView> _newCrystals = new CrystalView[] { };
        // private IEnumerable<CrystalView> _oldCrystals = new CrystalView[] { };

        private Queue<CollectorViewOld> _collectorsQueue;

        private int _extractedResources;
        private int _foundedResources;
        private int _collectorsFreeCount;

        public event Action<int> CollectorsFreeCountChanged;
        public event Action<int> FoundedResourcesChanged;
        public event Action<int> ExtractedResourcesChanged;

        public Collider[] Colliders { get; private set; } = new Collider[32];
        
        public int CollectorsFreeCount
        {
            get => _collectorsFreeCount;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));

                if (_collectorsFreeCount != value)
                {
                    _collectorsFreeCount = value;
                    CollectorsFreeCountChanged?.Invoke(value);
                }
            }
        }

        public int FoundedResources
        {
            get => _foundedResources;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));

                if (_foundedResources != value)
                {
                    _foundedResources = value;
                    FoundedResourcesChanged?.Invoke(value);
                }
            }
        }

        public int ExtractedResources
        {
            get => _extractedResources;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));

                if (_extractedResources != value)
                {
                    _extractedResources = value;
                    ExtractedResourcesChanged?.Invoke(value);
                }
            }
        }
        
        public void AddExtractedResources(int resourcesCount)
        {
            if (resourcesCount < 0)
                throw new ArgumentOutOfRangeException(nameof(resourcesCount));
        
            ExtractedResources += resourcesCount;
        }

        //TODO не знаю как записывать сюда значения из презентера
        public void SetFoundedResources(int count)
        {
            FoundedResources = count;
        }
    }
}