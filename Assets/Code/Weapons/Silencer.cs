using UnityEngine;

namespace ProjectPrikol
{
    public class Silencer : ISilencer
    {
        public AudioClip SilencedClip { get; }
        public float SilencedShotVolume { get; }
        public Transform SilencerBarrelPosition { get; }
        public GameObject Instance { get; }

        public Silencer(AudioClip silencedClip, float silencedShotVolume,
            Transform silencerBarrelPosition, GameObject instance)
        {
            SilencedClip = silencedClip;
            SilencedShotVolume = silencedShotVolume;
            SilencerBarrelPosition = silencerBarrelPosition;
            Instance = instance;
        }
    }
}