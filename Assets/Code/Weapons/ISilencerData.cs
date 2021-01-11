using UnityEngine;

namespace ProjectPrikol
{
    public interface ISilencerData
    {
        Mesh Mesh { get; }
        Vector3 Scale { get; }
        Material Material { get; }
        Vector3 BarrelPosition { get; }
        string Name { get; }
        string BarrelName { get; }
    }
}