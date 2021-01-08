using UnityEngine;

namespace ProjectPrikol
{
    public interface IWeaponFactory
    {
        GameObject Create(IWeaponData data);
    }
}