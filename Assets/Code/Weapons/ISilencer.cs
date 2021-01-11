using UnityEngine;

namespace ProjectPrikol
{
    public interface ISilencer
    {
        Transform SilencerBarrel { get; }
        GameObject Instance { get; }
        bool IsActive { get; set; }

        void Activate();
        void Deactivate();
    }
}