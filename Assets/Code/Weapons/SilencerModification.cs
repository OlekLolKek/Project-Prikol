using UnityEngine;

namespace ProjectPrikol
{
    public class SilencerModification : WeaponModification
    {
        private readonly ISilencer _silencer;
        private readonly Vector3 _silencerPosition;

        public SilencerModification(ISilencer silencer, Vector3 silencerPosition)
        {
            _silencer = silencer;
            _silencerPosition = silencerPosition;
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

            return weapon;

            //throw new System.NotImplementedException($"{nameof(RemoveModification)} is not implemented yet.");
        }
    }
}