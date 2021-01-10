using UnityEngine;

namespace ProjectPrikol
{
    public class SilencerModification : WeaponModification
    {
        private readonly AudioSource _audioSource;
        private readonly ISilencer _silencer;
        private readonly Vector3 _silencerPosition;

        public SilencerModification(AudioSource audioSource, ISilencer silencer,
            Vector3 silencerPosition)
        {
            _audioSource = audioSource;
            _silencer = silencer;
            _silencerPosition = silencerPosition;
        }

        protected override Weapon AddModification(Weapon weapon)
        {
            var silencer = Object.Instantiate(_silencer.Instance, _silencerPosition,
                Quaternion.identity);
            _audioSource.volume = _silencer.SilencedShotVolume;
            weapon.SetAudioClip(_silencer.SilencedClip);
            weapon.SetBarrelPosition(_silencer.SilencerBarrelPosition);
            return weapon;
        }

        protected override Weapon RemoveModification(Weapon weapon)
        {
            throw new System.NotImplementedException($"{nameof(RemoveModification)} is not implemented yet.");
        }
    }
}