using UnityEngine;

namespace ProjectPrikol
{
    public interface IBarrelAttachmentData
    {
        GameObject Prefab { get; }
        Vector3 BarrelPosition { get; }
        AudioClip AttachmentShotClip { get; }
        string BarrelName { get; }
    }
}