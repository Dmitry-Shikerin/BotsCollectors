using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Sources.Domain.CommandSenters;
using Sources.Infrastructure.Services;
using Sources.Presentations.Views;
using Sources.PresentationsInterfaces.Vievs;
using Sources.Utills;
using Sources.Utills.Constants;
using Unity.VisualScripting;
using UnityEngine;

namespace Sources.Controllers.CommandCenters
{
    public class CommandCenterPresenter
    {
        private readonly ICommandCenterView _commandCenterView;
        private readonly CommandCenter _commandCenter;
        private readonly OverlapService _overlapService;

        private IReadOnlyList<CrystalView> _newCrystals;
        private Queue<CrystalView> _crystalViewsQueue;
        private Queue<CollectorView> _collectorViewsQueue;
        
        public CommandCenterPresenter
        (
            //TODO переделать на интерфейс
            ICommandCenterView commandCenterView,
            CommandCenter commandCenter,
            OverlapService overlapService
        )
        {
            _commandCenterView = commandCenterView ?? 
                                 throw new ArgumentNullException(nameof(commandCenterView));
            _commandCenter = commandCenter ?? 
                             throw new ArgumentNullException(nameof(commandCenter));
            _overlapService = overlapService ?? throw new ArgumentNullException(nameof(overlapService));

        }

        public void Update()
        {
            _collectorViewsQueue = new Queue<CollectorView>(_commandCenterView.Collectors);
            // try
            // {
                if (Input.GetKeyDown(KeyCode.R))
                    Scan();
            // }
            // catch(ArgumentOutOfRangeException)
            // {
            //     Debug.Log("Недопустимо отрицательное знание");
            // }
        }
        
        private void Scan()
        {
            //todo TryCatch сделать в презентере

             _newCrystals
                = _overlapService.OverlapSphere<CrystalView>
                (_commandCenterView.OverlapStartPoint, _commandCenterView.Radius,
                    LayerMaskConstants.Touchable, LayerMaskConstants.Obstacle,
                    _commandCenter.Colliders);

             //TODO как сделать другое добавление очереди без сосдания новой?
             _crystalViewsQueue = new Queue<CrystalView>(_newCrystals);
             
             //TODO через методы передавать значения в модель
            _commandCenter.SetFoundedResources(_newCrystals.Count);
            
            //TODO это из модели
            //TODO вылетают ошибки
            // if (_crystalViewsQueue.Count == 0)
            //     throw new ArgumentOutOfRangeException(nameof(_crystalViewsQueue),
            //         "Очередь кристаллов пуста");
            
            CrystalView crystal = _crystalViewsQueue.Dequeue();

            // if (_collectorViewsQueue.Count == 0)
            //     throw new ArgumentOutOfRangeException(nameof(_collectorViewsQueue),
            //         "Очередь свободных Сборщиков пуста");
            
            CollectorView collector = _collectorViewsQueue.Dequeue();
            collector.SetTarget(crystal);
            //TODO если нету ресурсов или коллекторов кидаем эксепшены
        }
    }
}