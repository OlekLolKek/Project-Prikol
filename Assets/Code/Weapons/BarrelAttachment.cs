using UnityEngine;

namespace ProjectPrikol
{
    public sealed class BarrelAttachment : IBarrelAttachment
    {
        private readonly Transform _weaponBarrel;

        public Transform AttachmentBarrel { get; }
        public GameObject Instance { get; }
        public bool IsActive { get; set; }
        

        public BarrelAttachment(BarrelAttachmentFactory factory, Weapon weapon)
        {
            Instance = factory.Create();
            AttachmentBarrel = factory.BarrelTransform;
            _weaponBarrel = weapon.Barrel;
            Instance.transform.parent = _weaponBarrel;
            Instance.transform.localPosition = Vector3.zero;
            Instance.transform.localRotation = _weaponBarrel.localRotation;
            Deactivate();
        }

        public void Activate()
        {
            IsActive = true;
            Instance.SetActive(true);
        }

        public void Deactivate()
        {
            IsActive = false;
            Instance.SetActive(false);
        }
    }
}