using UnityEngine;

namespace ProjectPrikol
{
    public interface IWeaponFactory
    {
        Transform Transform { get; }
        Transform BarrelTransform { get; }
        AudioSource AudioSource { get; }
        
        GameObject Create(IWeaponData data);
    }
}