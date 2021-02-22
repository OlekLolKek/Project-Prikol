using UnityEngine;

namespace ProjectPrikol
{
    public sealed class WeaponFactory : IWeaponFactory
    {
        public Transform BarrelTransform { get; private set; }
        public Transform ScopeRailTransform { get; private set; }
        public AudioSource AudioSource { get; private set; }

        public GameObject Create(IWeaponData data)
        {
            var gun = Object.Instantiate(data.Prefab);
            var parts = gun.GetComponentsInChildren<Transform>();

            BarrelTransform = new GameObject(data.BarrelName).transform;
            BarrelTransform.parent = gun.transform;
            BarrelTransform.localPosition = data.BarrelPosition;

            AudioSource = BarrelTransform.gameObject.AddComponent<AudioSource>();
            AudioSource.loop = false;
            AudioSource.playOnAwake = false;
            AudioSource.clip = data.ShotClip;

            ScopeRailTransform = new GameObject(data.ScopeRailName).transform;
            ScopeRailTransform.parent = gun.transform;
            ScopeRailTransform.localPosition = data.ScopePosition;
            
            foreach (var part in parts)
            {
                part.gameObject.layer = LayerStorage.GUN_LAYER;
            }

            return gun;
        }
    }
}