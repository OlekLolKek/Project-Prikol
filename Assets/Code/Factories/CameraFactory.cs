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
            
            //TODO: Fix NullReferenceException
            
            // var postProcessing = camera.AddComponent<PostProcessLayer>();
            // postProcessing = _cameraData.PostProcessLayerComponent;
            // postProcessing.antialiasingMode = PostProcessLayer.Antialiasing.TemporalAntialiasing;
            // postProcessing.volumeLayer = _cameraData.PostProcessingLayer;
            // postProcessing.volumeTrigger = camera.transform;

            return Camera.gameObject;
        }
    }
}