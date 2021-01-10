using UnityEngine;

namespace ProjectPrikol
{
    public interface IWeaponData : IData
    {
        Material TracerMaterial { get; }
        LayerMask HitLayerMask { get; }
        Vector3 BarrelPosition { get; }
        Vector3 Position { get; }
        Vector3 Scale { get; }
        Material Material { get; }
        Mesh Mesh { get; }
        string Name { get; }
        string BarrelName { get; }
        string TracerName { get; }
        float ShootCooldown { get; }
        float Damage { get; }
        float TracerWidth { get; }
        float TracerFadeMultiplier { get; }
        float MaxShotDistance { get; }
    }
}