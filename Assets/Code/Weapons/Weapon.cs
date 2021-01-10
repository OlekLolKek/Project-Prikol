using System.Collections;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering;

namespace ProjectPrikol
{
    public class Weapon : IExecutable, IFire
    {
        #region Fields
        
        private Transform _barrel;
        private AudioClip _audioClip;

        private readonly TracerFactory _factory;
        private readonly Material _tracerMaterial;
        private readonly LayerMask _hitLayerMask;
        private readonly GameObject _instance;
        private readonly Transform _cameraTransform;
        private readonly Camera _camera;
        private readonly Vector3 _position;

        private float _mouseX;
        private float _mouseY;
        private float _deltaTime;

        private readonly float _tracerWidth;
        private readonly float _tracerFadeMultiplier;
        private readonly float _maxShotDistance;
        private readonly int _linePositionCount = 2;

        #endregion


        #region Properties

        public bool IsActive { get; private set; }
        public float Damage { get; set; }
        public float ShootCooldown { get; set; }
        public float ReloadTime { get; set; }
        public bool IsFullAuto { get; set; }

        #endregion


        public Weapon(IWeaponFactory factory, IWeaponData data,
            CameraModel cameraModel)
        {
            _position = data.Position;
            _hitLayerMask = data.HitLayerMask;
            Damage = data.Damage;
            ShootCooldown = data.ShootCooldown;
            _tracerWidth = data.TracerWidth;
            _tracerFadeMultiplier = data.TracerFadeMultiplier;
            _maxShotDistance = data.MaxShotDistance;
            _tracerMaterial = data.TracerMaterial;

            _instance = factory.Create(data);
            _barrel = factory.BarrelTransform;

            _cameraTransform = cameraModel.CameraTransform;
            _camera = cameraModel.Camera;

            _factory = new TracerFactory(data);
        }

        #region Methods

        public void Execute(float deltaTime)
        {
            _deltaTime = deltaTime;
        }

        public void Fire()
        {
            if (!IsActive) return;

            _factory.Create();
            var line = _factory.LineRenderer;
            
            line.SetPosition(0, _barrel.position);
            
            var ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            if (Physics.Raycast(ray, out var hit, _maxShotDistance, _hitLayerMask))
            {
                line.SetPosition(1, hit.point);
            }
            else
            {
                line.SetPosition(1, _cameraTransform.localPosition + _cameraTransform.forward * _maxShotDistance);
            }

            TweenLineWidth(line).ToObservable().Subscribe();
        }

        private IEnumerator TweenLineWidth(LineRenderer line)
        {
            while (line.endWidth > 0)
            {
                line.endWidth -= _deltaTime * _tracerFadeMultiplier;
                line.startWidth -= _deltaTime * _tracerFadeMultiplier;

                var color = line.endColor;
                color.a -= _deltaTime * _tracerFadeMultiplier;
                line.endColor = color;
                line.startColor = color;

                yield return 0;
            }

            Object.Destroy(line.gameObject);
        }

        public void Rotate(float mouseX, float mouseY)
        {
            if (!IsActive) return;
            var rotation = Vector3.zero;
            rotation.y += mouseX;
            rotation.x -= mouseY;
            _instance.transform.localEulerAngles = rotation;
        }

        public void SetBarrelPosition(Transform barrel)
        {
            _barrel = barrel;
        }

        public void SetAudioClip(AudioClip clip)
        {
            _audioClip = clip;
        }

        public void Activate()
        {
            IsActive = true;
            _instance.SetActive(true);
            _instance.transform.parent = _cameraTransform;
            _instance.transform.localPosition = _position;
            _instance.transform.localRotation = Quaternion.identity;
        }

        public void Deactivate()
        {
            IsActive = false;
            _instance.SetActive(false);
        }

        #endregion
    }
}