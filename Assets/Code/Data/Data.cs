﻿using System.IO;
using UnityEngine;


namespace ProjectPrikol
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/Data")]
    public class Data : ScriptableObject, IData
    {
        #region Fields

        [SerializeField] private string _dataRootPath;
        [SerializeField] private string _playerDataPath;
        [SerializeField] private string _cameraDataPath;
        [SerializeField] private string _inputDataPath;

        private CameraData _cameraData;
        private PlayerData _playerData;
        private InputData _inputData;

        #endregion


        #region Properties
        public PlayerData PlayerData
        {
            get
            {
                if (_playerData == null)
                {
                    _playerData = Load<PlayerData>(_dataRootPath + _playerDataPath);
                }

                return _playerData;
            }
        }

        public CameraData CameraData
        {
            get
            {
                if (_cameraData == null)
                {
                    _cameraData = Load<CameraData>(_dataRootPath + _cameraDataPath);
                }

                return _cameraData;
            }
        }

        public InputData InputData
        {
            get
            {
                if (_inputData == null)
                {
                    _inputData = Load<InputData>(_dataRootPath + _inputDataPath);
                }

                return _inputData;
            }
        }

        #endregion

        private T Load<T>(string resourcesPath) where T : Object
        {
            return Resources.Load<T>(Path.ChangeExtension(resourcesPath, null));
        }
    }
}