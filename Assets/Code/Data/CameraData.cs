using UnityEngine;

namespace ProjectPrikol
{
    [CreateAssetMenu(fileName = "CameraData", menuName = "Data/CameraData")]
    public class CameraData : ScriptableObject, IData
    {
        [SerializeField] private string _cameraName;
        [SerializeField, Range(1, 179)] private float _fov;
        [SerializeField, Range(0.01f, 5000)] private float _clippingPlaneFar;
        [SerializeField, Range(0.01f, 5000)] private float _clippingPlaneNear;

        public string CameraName => _cameraName;
        public float FOV => _fov;
        public float ClippingPlaneFar => _clippingPlaneFar;
        public float ClippingPlaneNear => _clippingPlaneNear;
    }
}