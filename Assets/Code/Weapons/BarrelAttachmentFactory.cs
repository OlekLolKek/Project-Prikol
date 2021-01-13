using UnityEngine;

namespace ProjectPrikol
{
    public sealed class BarrelAttachmentFactory : IFactory
    {
        private IBarrelAttachmentData _data;
        
        public Transform Transform { get; private set; }
        public Transform BarrelTransform { get; private set; }
        public AudioSource AudioSource { get; private set; }


        public BarrelAttachmentFactory(IBarrelAttachmentData data)
        {
            _data = data;
        }
        
        public GameObject Create()
        {
            var attachment = Object.Instantiate(_data.Prefab);
            Transform = attachment.transform;
            attachment.layer = LayerStorage.GUN_LAYER;
            
            BarrelTransform = new GameObject(_data.BarrelName).transform;
            BarrelTransform.parent = Transform;
            BarrelTransform.localPosition = _data.BarrelPosition;

            AudioSource = BarrelTransform.gameObject.AddComponent<AudioSource>();
            AudioSource.loop = false;
            AudioSource.playOnAwake = false;
            AudioSource.clip = _data.AttachmentShotClip;

            return attachment;
        }
    }
}