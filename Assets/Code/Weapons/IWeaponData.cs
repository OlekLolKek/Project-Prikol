using UnityEngine;

namespace ProjectPrikol
{
    public interface IWeaponData : IData
    {
        GameObject Prefab { get; }
        AudioClip ShotClip { get; }
        Material TracerMaterial { get; }
        LayerMask HitLayerMask { get; }
        Vector3 BarrelPosition { get; }
        Vector3 ScopePosition { get; }
        Vector3 Position { get; }
        string Name { get; }
        string BarrelName { get; }
        string ScopeRailName { get; }
        string TracerName { get; }
        float ShootCooldown { get; }
        float Damage { get; }
        float TracerWidth { get; }
        float TracerFadeMultiplier { get; }
        float MaxShotDistance { get; }
    }
}