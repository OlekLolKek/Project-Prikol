using System.IO;
using UnityEngine;

namespace ProjectPrikol
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Data/Weapon/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private string _weaponDataRootPath;
        [SerializeField] private string _assaultRifleSilencerDataPath;
        [SerializeField] private string _assaultRifleDataPath;

        private AssaultRifleSilencerData _assaultRifleSilencerData;
        private AssaultRifleData _assaultRifleData;


        public AssaultRifleSilencerData AssaultRifleSilencerData
        {
            get
            {
                if (_assaultRifleSilencerData == null)
                {
                    _assaultRifleSilencerData =
                        Load<AssaultRifleSilencerData>(_weaponDataRootPath + _assaultRifleSilencerDataPath);
                }

                return _assaultRifleSilencerData;
            }
        }
        
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