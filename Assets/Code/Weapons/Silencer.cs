using UnityEngine;

namespace ProjectPrikol
{
    public class Silencer : ISilencer
    {
        private Transform _weaponBarrel;
        
        
        public Transform SilencerBarrel { get; }
        public GameObject Instance { get; }
        public bool IsActive { get; set; }
        

        public Silencer(SilencerFactory factory, Weapon weapon)
        {
            Instance = factory.Create();
            SilencerBarrel = factory.BarrelTransform;
            _weaponBarrel = weapon.Barrel;
        }

        public void Activate()
        {
            IsActive = true;
            Instance.SetActive(true);
            Instance.transform.parent = _weaponBarrel;
            Instance.transform.position = new Vector3(_weaponBarrel.position.x, _weaponBarrel.position.y,
                _weaponBarrel.position.z + Instance.transform.localScale.z / 2);
        }

        public void Deactivate()
        {
            IsActive = false;
            Instance.SetActive(false);
        }
    }
}