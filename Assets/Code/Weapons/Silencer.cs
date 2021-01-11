using UnityEngine;

namespace ProjectPrikol
{
    public class Silencer : ISilencer
    {
        private readonly Transform _weaponBarrel;
        
        
        public Transform SilencerBarrel { get; }
        public GameObject Instance { get; }
        public bool IsActive { get; set; }
        

        public Silencer(SilencerFactory factory, Weapon weapon)
        {
            Instance = factory.Create();
            SilencerBarrel = factory.BarrelTransform;
            _weaponBarrel = weapon.Barrel;
            Deactivate();
        }

        public void Activate()
        {
            IsActive = true;
            Instance.SetActive(true);
            Instance.transform.parent = _weaponBarrel;
            Instance.transform.localPosition = new Vector3(0.0f, 0.0f, Instance.transform.localScale.z / 2);
            Instance.transform.localRotation = _weaponBarrel.localRotation;
        }

        public void Deactivate()
        {
            IsActive = false;
            Instance.SetActive(false);
        }
    }
}