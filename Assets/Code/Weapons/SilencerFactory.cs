using UnityEngine;

namespace ProjectPrikol
{
    public class SilencerFactory : IFactory
    {
        private ISilencerData _data;
        
        public Transform Transform { get; private set; }
        public Transform BarrelTransform { get; private set; }
        public AudioSource AudioSource { get; private set; }


        public SilencerFactory(ISilencerData data)
        {
            _data = data;
        }
        
        public GameObject Create()
        {
            var silencer = new GameObject(_data.Name);
            Transform = silencer.transform;
            
            silencer.AddComponent<MeshFilter>().mesh = _data.Mesh;
            var renderer = silencer.AddComponent<MeshRenderer>();
            renderer.material = _data.Material;
            
            Transform.localScale = _data.Scale;

            BarrelTransform = new GameObject(_data.BarrelName).transform;
            BarrelTransform.parent = Transform;
            BarrelTransform.localPosition = _data.BarrelPosition;

            AudioSource = BarrelTransform.gameObject.AddComponent<AudioSource>();
            AudioSource.loop = false;
            AudioSource.playOnAwake = false;

            return silencer;
        }
    }
}