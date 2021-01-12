using UnityEngine;

namespace ProjectPrikol
{
    public sealed class ScopeFactory : IFactory
    {
        private IScopeData _data;
        
        public Transform Transform { get; private set; }
        

        public ScopeFactory(IScopeData data)
        {
            _data = data;
        }
        
        public GameObject Create()
        {
            var scope = Object.Instantiate(_data.Prefab);
            Transform = scope.transform;

            var parts = scope.GetComponentsInChildren<Transform>();
            foreach (var part in parts)
            {
                part.gameObject.layer = LayerStorage.GUN_LAYER;
            }

            return scope;
        }
    }
}