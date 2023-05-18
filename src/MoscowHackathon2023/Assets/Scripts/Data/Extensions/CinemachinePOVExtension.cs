using System;
using Cinemachine;
using Services.Input;
using UnityEngine;
using Zenject;

namespace Data.Extensions
{
    public class CinemachinePOVExtension : CinemachineExtension
    {
        [SerializeField] private float _clampAngle = 80;
        [SerializeField] private float _horizontalSpeed = 10;
        [SerializeField] private float _verticalSpeed = 10;
        
        private PlayerInputActionReader _playerInputActionReader;
        private Vector3 _startRotation;

        private Vector2 _mouseInput;

        [Inject]
        public void Construct(PlayerInputActionReader playerInputActionReader)
        {
            _playerInputActionReader = playerInputActionReader;

            _playerInputActionReader.OnMousePositionInput += OnMousePlayerInput;
        }

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (!vcam.Follow)
            {
                return;
            }

            if (stage != CinemachineCore.Stage.Aim)
            {
                return;
            }

            _startRotation = transform.localRotation.eulerAngles;

            _startRotation.x += _mouseInput.x * Time.deltaTime;
            _startRotation.y += _mouseInput.y * Time.deltaTime;
        }

        private void OnMousePlayerInput(Vector2 mouseInput)
        {
            _mouseInput = mouseInput;
        }

        private void OnDisable()
        {
            _playerInputActionReader.OnMousePositionInput -= OnMousePlayerInput;
        }
    }
}