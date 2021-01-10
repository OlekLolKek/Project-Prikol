using UnityEngine;

namespace ProjectPrikol
{
    public interface ISilencer
    {
        AudioClip SilencedClip { get; }
        float SilencedShotVolume { get; }
        Transform SilencerBarrelPosition { get; }
        GameObject Instance { get; }
    }
}