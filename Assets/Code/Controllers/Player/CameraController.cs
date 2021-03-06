﻿using UnityEngine;


namespace ProjectPrikol
{
    public class CameraController : IExecutable, ICleanable
    {
        private readonly Camera _camera;
        private readonly Transform _cameraTransform;
        private readonly Transform _playerTransform;
        private readonly Transform _headTransform;
        private readonly Rigidbody _rigidbody;

        private readonly float _FOVChangeMultiplier;
        private readonly float _maxXRotation = 90.0f;
        private readonly float _minXRotation = -90.0f;
        private readonly float _sensitivity;
        private readonly float _sensitivityMultiplier;
        private readonly float _baseFOV;
        
        private float _xRotation;
        private float _mouseX;
        private float _mouseY;

        public CameraController(CameraModel cameraModel, CameraData cameraData,
            PlayerModel playerModel, InputModel inputModel)
        {
            _camera = cameraModel.Camera;
            _cameraTransform = cameraModel.CameraTransform;
            _playerTransform = playerModel.Transform;
            _headTransform = playerModel.Head;
            _rigidbody = playerModel.Rigidbody;

            _FOVChangeMultiplier = cameraData.FOVChangeMultiplier;
            _sensitivity = cameraData.Sensitivity;
            _sensitivityMultiplier = cameraData.SensitivityMultiplier;
            _baseFOV = cameraData.FOV;
            
            var mouseX = inputModel.MouseX;
            var mouseY = inputModel.MouseY;
            mouseX.OnAxisChanged += ChangeMouseX;
            mouseY.OnAxisChanged += ChangeMouseY;
        }

        #region Methods

        public void Execute(float deltaTime)
        {
            MoveCamera();
            RotateCamera(deltaTime);
            ChangeFOV();
        }

        private void MoveCamera()
        {
            _cameraTransform.position = _headTransform.position;
        }

        private void ChangeFOV()
        {
            var velocity = _rigidbody.velocity.magnitude;
            var newFOV = _baseFOV + velocity * _FOVChangeMultiplier;
            _camera.fieldOfView = newFOV;
        }

        private void RotateCamera(float deltaTime)
        {
            var mouseX = _mouseX * _sensitivity * _sensitivityMultiplier * deltaTime;
            var mouseY = _mouseY * _sensitivity * _sensitivityMultiplier * deltaTime;

            var rotation = _cameraTransform.localRotation.eulerAngles;
            var desiredX = rotation.y + mouseX;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, _minXRotation, _maxXRotation);
            
            _cameraTransform.localRotation = Quaternion.Euler(_xRotation, desiredX, 0.0f);
            _playerTransform.localRotation = Quaternion.Euler(0.0f, desiredX, 0.0f);
        }

        private void ChangeMouseX(float value)
        {
            _mouseX = value;
        }
        
        private void ChangeMouseY(float value)
        {
            _mouseY = value;
        }

        public void Cleanup()
        {
        }

        #endregion
    }
}