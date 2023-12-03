using System;
using Sources.Domain.CameraMovements;
using Sources.Infrastructure.Services;
using Sources.PresentationsInterfaces.Views;
using UnityEngine;

namespace Sources.Controllers.CameraMovements
{
    public class CameraMovementPresenter : PresenterBase
    {
        private readonly InputService _inputService;
        private readonly CameraMovement _cameraMovement;
        private readonly ICameraMovementView _cameraMovementView;

        private float _scrollWheel;
        private float _multiplier;
        private bool _isLeftRotation;
        private bool _isRightRotation;
        private Vector2 _movementDirection;

        public CameraMovementPresenter
        (
            InputService inputService,
            CameraMovement cameraMovement,
            ICameraMovementView cameraMovementView
        )
        {
            _inputService = inputService ? inputService : 
                throw new ArgumentNullException(nameof(inputService));
            _cameraMovement = cameraMovement ?? throw new ArgumentNullException(nameof(cameraMovement));
            _cameraMovementView = cameraMovementView ??
                                  throw new ArgumentNullException(nameof(cameraMovementView));
        }

        public override void Enable()
        {
            _inputService.MovementAxis += OnMovementAxis;
            _inputService.RotationAxis += OnRotationAxis;
            _inputService.MultiplayerAxis += OnMultiplayerAxis;
            _inputService.ScrollWheelAxis += OnScrollWheelAxis;
        }

        public override void Disable()
        {
            _inputService.MovementAxis -= OnMovementAxis;
            _inputService.RotationAxis -= OnRotationAxis;
            _inputService.MultiplayerAxis -= OnMultiplayerAxis;
            _inputService.ScrollWheelAxis -= OnScrollWheelAxis;
        }

        public void Update()
        {
            if (_isRightRotation)
                _cameraMovement.SetRightRotation();

            if (_isLeftRotation)
                _cameraMovement.SetLeftRotation();

            float movementSpeed = _cameraMovement.GetSpeed(_multiplier ,_cameraMovement.MovementSpeed);
            float zoomSpeed = _cameraMovement.GetSpeed(_multiplier, _cameraMovement.ZoomSpeed);
            
            Vector3 zoomDirection = _cameraMovement.GetZoomDirection(_scrollWheel, zoomSpeed);
            Vector3 movementDirection = _cameraMovement.GetMovementDirection(_movementDirection, movementSpeed);

            _cameraMovementView.Rotate(_cameraMovement.AngleY);
            _cameraMovementView.Zoom(zoomDirection);
            _cameraMovementView.Move(movementDirection);
        }

        private void OnScrollWheelAxis(float scrollWheelAxis) => 
            _scrollWheel = scrollWheelAxis;

        private void OnMultiplayerAxis(float multiplierAxis) => 
            _multiplier = multiplierAxis;

        private void OnMovementAxis(Vector2 movementAxis) => 
            _movementDirection = movementAxis;

        private void OnRotationAxis(bool isLeftRotation, bool isRightRotation)
        {
            _isLeftRotation = isLeftRotation;
            _isRightRotation = isRightRotation;
        }
    }
}