using UnityEngine;
using UnityEngine.Rendering;


namespace ProjectPrikol
{
    public class TracerFactory : IFactory
    {
        public GameObject GameObject { get; private set; }
        public LineRenderer LineRenderer { get; private set; }

        private IWeaponData _data;
        
        public TracerFactory(IWeaponData data)
        {
            _data = data;
        }
        
        public GameObject Create()
        {
            GameObject = new GameObject(_data.TracerName);
            LineRenderer = GameObject.AddComponent<LineRenderer>();
            
            LineRenderer.positionCount = 2;
            LineRenderer.startWidth = _data.TracerWidth;
            LineRenderer.endWidth = _data.TracerWidth;
            LineRenderer.generateLightingData = true;
            LineRenderer.shadowCastingMode = ShadowCastingMode.Off;
            LineRenderer.receiveShadows = false;
            LineRenderer.material = _data.TracerMaterial;

            return GameObject;
        }
    }
}