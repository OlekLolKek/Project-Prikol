using System.IO;
using UnityEngine;

namespace ProjectPrikol
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Data/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private string _weaponDataRootPath;
        [SerializeField] private string _assaultRifleDataPath;

        private AssaultRifleData _assaultRifleData;
        

        public AssaultRifleData AssaultRifleData
        {
            get
            {
                if (_assaultRifleData == null)
                {
                    _assaultRifleData = Load<AssaultRifleData>(_weaponDataRootPath + _assaultRifleDataPath);
                }

                return _assaultRifleData;
            }
        }


        private T Load<T>(string resourcesPath) where T : Object
        {
            return Resources.Load<T>(Path.ChangeExtension(resourcesPath, null));
        }
    }
}