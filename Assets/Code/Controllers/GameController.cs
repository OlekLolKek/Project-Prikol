using System;
using UnityEngine;


namespace ProjectPrikol
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Data _data;
        private Controllers _controllers;

        private void Start()
        {
            var moveController = new MoveController();

            _controllers.Add(moveController);
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