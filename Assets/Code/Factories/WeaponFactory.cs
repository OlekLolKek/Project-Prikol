using UnityEngine;

namespace ProjectPrikol
{
    public class WeaponFactory : IFactory
    {
        private IWeaponData _data;

        public Transform Transform { get; private set; }
        public Transform BarrelTransform { get; private set; }
        
        public WeaponFactory(IWeaponData data)
        {
            _data = data;
        }
        
        public GameObject Create()
        {
            var gun = new GameObject(_data.Name);
            
            Transform = gun.transform;
            Transform.localScale = _data.Scale;
            
            gun.AddComponent<MeshFilter>().mesh = _data.Mesh;
            var renderer = gun.AddComponent<MeshRenderer>();
            renderer.material = _data.Material;

            BarrelTransform = new GameObject(_data.BarrelName).transform;
            BarrelTransform.parent = Transform;
            BarrelTransform.localPosition = _data.BarrelPosition;

            return gun;
        }
    }
}