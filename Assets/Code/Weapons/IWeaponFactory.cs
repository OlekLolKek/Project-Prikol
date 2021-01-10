using UnityEngine;

namespace ProjectPrikol
{
    public interface IWeaponFactory
    {
        Transform Transform { get; }
        Transform BarrelTransform { get; }
        GameObject Create(IWeaponData data);
    }
}