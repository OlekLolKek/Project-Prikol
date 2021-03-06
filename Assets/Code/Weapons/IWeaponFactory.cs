﻿using UnityEngine;

namespace ProjectPrikol
{
    public interface IWeaponFactory
    {
        Transform BarrelTransform { get; }
        Transform ScopeRailTransform { get; }
        AudioSource AudioSource { get; }
        
        GameObject Create(IWeaponData data);
    }
}