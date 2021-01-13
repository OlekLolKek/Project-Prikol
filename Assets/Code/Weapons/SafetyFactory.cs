using UnityEngine;


namespace ProjectPrikol
{
    public sealed class SafetyFactory : IFactory
    {
        private readonly ISafetyData _data;
        
        public AudioSource AudioSource { get; private set; }

        public SafetyFactory(ISafetyData data)
        {
            _data = data;
        }
        
        public GameObject Create()
        {
            var safety = new GameObject();

            AudioSource = safety.AddComponent<AudioSource>();
            AudioSource.playOnAwake = false;
            AudioSource.loop = false;
            AudioSource.clip = _data.SafetyClick;

            return safety;
        }
    }
}