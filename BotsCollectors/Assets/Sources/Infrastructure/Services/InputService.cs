using System;
using UnityEngine;

namespace Sources.Infrastructure.Services
{
    public class InputService : MonoBehaviour
    {
        private const string Vertical = "Vertical";
        private const string Horizontal = "Horizontal";
        private const string Multiplier = "Multiplier";
        private const string MouseScrollWheel = "Mouse ScrollWheel";

        public event Action<bool, bool> RotationAxis;
        public event Action<Vector2> MovementAxis;
        public event Action<float> MultiplayerAxis;
        public event Action<float> ScrollWheelAxis;
        public event Action Scanning;
        public event Action SendCollector;
        public event Action CreatureCollector;
        public event Action ChooseCommandCenter;
        public event Action SettingFlagPosition;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        void Update()
        {
            UpdateRotation();
            UpdateMovementAxis();
            UpdateMultiplayerAxis();
            UpdateScrollWheelAxis();
            UpdateScanning();
            UpdateSendCollector();
            UpdateCreatureCollector();
            UpdateChooseCommandCenter();
            UpdateSettingFlagPosition();
        }

        private void UpdateRotation()
        {
            bool isLeftRotation = Input.GetKey(KeyCode.Q);
            bool isRightRotation = Input.GetKey(KeyCode.E);
            
            RotationAxis?.Invoke(isLeftRotation, isRightRotation);
        }

        private void UpdateMovementAxis()
        {
            float horizontalInput = Input.GetAxis(Horizontal);
            float verticalInput = Input.GetAxis(Vertical);
            Vector2 movementDirection = new Vector2(horizontalInput, verticalInput);
            
            MovementAxis?.Invoke(movementDirection);
        }
    
        private void UpdateMultiplayerAxis()
        {
            float multiplier = Input.GetAxis(Multiplier);
            
            MultiplayerAxis?.Invoke(multiplier);
        }

        private void UpdateScrollWheelAxis()
        {
            float scrollWheelInput = Input.GetAxis(MouseScrollWheel);
        
            ScrollWheelAxis?.Invoke(scrollWheelInput);
        }

        private void UpdateScanning()
        {
            if(Input.GetKeyDown(KeyCode.R))
                Scanning?.Invoke();
        }

        private void UpdateSendCollector()
        {
            if(Input.GetKeyDown(KeyCode.F))
                SendCollector?.Invoke();
        }

        private void UpdateCreatureCollector()
        {
            if(Input.GetKeyDown(KeyCode.T))
                CreatureCollector?.Invoke();
        }

        private void UpdateChooseCommandCenter()
        {
            if(Input.GetMouseButtonDown(1))
                ChooseCommandCenter?.Invoke();
        }

        private void UpdateSettingFlagPosition()
        {
            if(Input.GetMouseButtonDown(0))
                SettingFlagPosition?.Invoke();
        }
    }
}
