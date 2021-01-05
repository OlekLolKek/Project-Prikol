using UnityEngine;

namespace ProjectPrikol
{
    public interface IWeaponData
    {
        Vector3 BarrelPosition { get; }
        Vector3 Position { get; }
        Vector3 Scale { get; }
        Material Material { get; }
        Mesh Mesh { get; }
        string Name { get; }
        string BarrelName { get; }
        float ShootCooldown { get; }
        float Damage { get; }
    }
}