using UnityEngine;

namespace ProjectPrikol
{
    public class SilencerModification : WeaponModification
    {
        private readonly ISilencer _silencer;

        public SilencerModification(ISilencer silencer)
        {
            _silencer = silencer;
        }

        protected override Weapon AddModification(Weapon weapon)
        {
            _silencer.Activate();
            weapon.SetBarrelPosition(_silencer.SilencerBarrel);
            
            return weapon;
        }

        protected override Weapon RemoveModification(Weapon weapon)
        {
            _silencer.Deactivate();
            weapon.ResetBarrelPosition();

            return weapon;
        }
    }
}