using UnityEngine;

namespace ProjectPrikol
{
    public class WeaponFactory : IWeaponFactory
    {
        public Transform Transform { get; private set; }
        public Transform BarrelTransform { get; private set; }
        public AudioSource AudioSource { get; private set; }
        

        public GameObject Create(IWeaponData data)
        {
            var gun = new GameObject(data.Name);
            
            Transform = gun.transform;
            Transform.localScale = data.Scale;

            gun.AddComponent<MeshFilter>().mesh = data.Mesh;
            var renderer = gun.AddComponent<MeshRenderer>();
            renderer.material = data.Material;

            BarrelTransform = new GameObject(data.BarrelName).transform;
            BarrelTransform.parent = Transform;
            BarrelTransform.localPosition = data.BarrelPosition;
            
            AudioSource = BarrelTransform.gameObject.AddComponent<AudioSource>();
            AudioSource.loop = false;
            AudioSource.playOnAwake = false;
            AudioSource.clip = data.ShotClip;

            return gun;
        }
    }
}