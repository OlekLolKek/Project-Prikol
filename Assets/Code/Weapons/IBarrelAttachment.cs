using UnityEngine;

namespace ProjectPrikol
{
    public interface IBarrelAttachment
    {
        Transform AttachmentBarrel { get; }
        GameObject Instance { get; }
        bool IsActive { get; set; }

        void Activate();
        void Deactivate();
    }
}