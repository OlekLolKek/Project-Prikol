using System.IO;
using UnityEngine;

namespace ProjectPrikol
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/Data")]
    public class Data : ScriptableObject
    {
        [SerializeField] private string _dataRootPath;
        [SerializeField] private string _playerDataPath;

        private PlayerData _playerData;

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

        private T Load<T>(string resourcesPath) where T : Object
        {
            return Resources.Load<T>(Path.ChangeExtension(resourcesPath, null));
        }
    }
}