﻿using System;
using UnityEngine;


namespace ProjectPrikol
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Data _data;
        private Controllers _controllers;

        private void Start()
        {
            var playerFactory = new PlayerFactory(_data.PlayerData);
            var cameraFactory = new CameraFactory(_data.CameraData);

            var inputModel = new InputModel(_data.InputData);
            var playerModel = new PlayerModel(playerFactory);
            var cameraModel = new CameraModel(cameraFactory);

            var inputController = new InputController(
                inputModel.Horizontal, inputModel.Vertical,
                inputModel.MouseX, inputModel.MouseY, 
                inputModel.StartCrouch, inputModel.StopCrouch,
                inputModel.Jump);
            var moveController = new MoveController(playerModel, _data.PlayerData);
            var cameraController = new CameraController(cameraModel, playerModel);

            _controllers.Add(inputController);
            _controllers.Add(moveController);
            _controllers.Add(cameraController);
            
            _controllers.Initialize();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _controllers.Execute(deltaTime);
        }

        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;
            _controllers.LateExecute(deltaTime);
        }

        private void OnDestroy()
        {
            _controllers.Cleanup();
        }
    }
}