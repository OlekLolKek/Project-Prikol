using UnityEngine;

namespace ProjectPrikol
{
    public class Weapon
    {
        private GameObject _instance;
        private Transform _camera;
        private Vector3 _position;

        private float _mouseX;
        private float _mouseY;
        
        public float Damage { get; set; }
        public float ShootCooldown { get; set; }
        public float ReloadTime { get; set; }
        public bool IsFullAuto { get; set; }
        
        public Weapon(IWeaponFactory factory, IWeaponData data, 
            CameraModel cameraModel)
        {
            _position = data.Position;
            _instance = factory.Create(data);
            _camera = cameraModel.CameraTransform;

            Damage = data.Damage;
            ShootCooldown = data.ShootCooldown;
        }

        public void Rotate(float mouseX, float mouseY)
        {
            var rotation = Vector3.zero;
            rotation.y += mouseX * 1;
            rotation.x -= mouseY * 1;
            _instance.transform.localEulerAngles = rotation;
        }

        public void Activate()
        {
            _instance.SetActive(true);
            _instance.transform.parent = _camera;
            _instance.transform.localPosition = _position;
            _instance.transform.localRotation = Quaternion.identity;
        }
        
        public void Deactivate()
        {
            _instance.SetActive(false);
        }
    }
}