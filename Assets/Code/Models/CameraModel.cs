using UnityEngine;

namespace ProjectPrikol
{
    public class CameraModel
    {
        private Transform _cameraTransform;
        private Camera _camera;

        public CameraModel(CameraFactory factory)
        {
            factory.Create();
            _cameraTransform = factory.CameraTransform;
            _camera = factory.Camera;
        }
    }
}