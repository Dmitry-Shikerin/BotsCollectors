﻿using System;
using JetBrains.Annotations;
using Sources.Controllers.CameraMovements;
using Sources.Domain.CameraMovements;
using Sources.Infrastructure.Factoryes.Controllers;
using Sources.PresentationsInterfaces.Vievs;

namespace Sources.Infrastructure.Factoryes.Presentations.Views
{
    public class CameraMovementViewFactory
    {
        private readonly CameraMovementPresenterFactory _cameraMovementPresenterFactory;

        public CameraMovementViewFactory(CameraMovementPresenterFactory cameraMovementPresenterFactory)
        {
            _cameraMovementPresenterFactory = cameraMovementPresenterFactory ?? 
                                              throw new ArgumentNullException(nameof(cameraMovementPresenterFactory));
        }

        public ICameraMovementView Create(CameraMovementView cameraMovementView, CameraMovement cameraMovement)
        {
            CameraMovementPresenter cameraMovementPresenter =
                _cameraMovementPresenterFactory.Create(cameraMovementView, cameraMovement);
            
            cameraMovementView.Construct(cameraMovementPresenter);

            return cameraMovementView;
        }
    }
}