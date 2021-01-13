using UnityEngine;

namespace ProjectPrikol
{
    public class WeaponProxy : IFire
    {
        private readonly IFire _weapon;
        private readonly WeaponSafety _unlockWeapon;

        public WeaponProxy(IFire weapon, WeaponSafety unlockWeapon)
        {
            _weapon = weapon;
            _unlockWeapon = unlockWeapon;
        }

        public void Fire()
        {
            if (_unlockWeapon.IsSafetyOn)
            {
                Debug.Log("Weapon is on safety.");
            }
            else
            {
                _weapon.Fire();
            }
        }
    }
}