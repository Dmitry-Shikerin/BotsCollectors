using System;
using JetBrains.Annotations;
using Sources.Controllers.CameraMovements;
using Sources.Domain.CameraMovements;
using Sources.PresentationsInterfaces.Vievs;

namespace Sources.Infrastructure.Factoryes.Controllers
{
    public class CameraMovementPresenterFactory
    {
        private readonly InputService _inputService;

        public CameraMovementPresenterFactory(InputService inputService)
        {
            _inputService = inputService ??
                            throw new ArgumentNullException(nameof(inputService));
        }

        public CameraMovementPresenter Create
            (ICameraMovementView cameraMovementView, CameraMovement cameraMovement)
        {
            return new CameraMovementPresenter
            (
                _inputService,
                cameraMovement,
                cameraMovementView
            );
        }
    }
}