using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


namespace ProjectPrikol
{
    public class CameraFactory : IFactory
    {
        private CameraData _cameraData;
        public Camera Camera { get; set; }
        public Transform CameraTransform { get; set; }

        public CameraFactory(CameraData cameraData)
        {
            _cameraData = cameraData;
        }
        
        public GameObject Create()
        {
            var camera = new GameObject(_cameraData.CameraName);
            
            Camera = camera.AddComponent<Camera>();
            Camera.farClipPlane = _cameraData.ClippingPlaneFar;
            Camera.nearClipPlane = _cameraData.ClippingPlaneNear;
            Camera.fieldOfView = _cameraData.FOV;

            CameraTransform = Camera.transform;
            
            camera.AddComponent<AudioListener>();

            var postProcessing = camera.AddComponent<PostProcessLayer>();
            postProcessing.Init(_cameraData.PostProcessResources);
            postProcessing.volumeTrigger = camera.transform;
            postProcessing.volumeLayer = _cameraData.PostProcessingLayer;
            postProcessing.antialiasingMode = PostProcessLayer.Antialiasing.TemporalAntialiasing;

            Camera.cullingMask = _cameraData.CullingLayerMask.value;

            return Camera.gameObject;
        }
    }
}