using System.Collections;
using UniRx;
using UnityEngine;


namespace ProjectPrikol
{
    public class Weapon : IExecutable, IFire
    {
        #region Fields

        private AudioClip _audioClip;
        private readonly AudioSource _audioSource;
        private readonly TracerFactory _tracerFactory;
        private readonly LayerMask _hitLayerMask;
        private readonly GameObject _instance;
        private readonly Transform _cameraTransform;
        private readonly Transform _baseBarrel;
        private readonly Camera _camera;
        private readonly Vector3 _position;

        private float _mouseX;
        private float _mouseY;
        private float _deltaTime;
        private bool _isSilenced = false;

        private readonly float _tracerFadeMultiplier;
        private readonly float _colorFadeMultiplier = 15;
        private readonly float _maxShotDistance;
        private readonly int _linePositionCount = 2;

        #endregion


        #region Properties

        public Transform Barrel { get; private set; }
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
            _tracerFadeMultiplier = data.TracerFadeMultiplier;
            _maxShotDistance = data.MaxShotDistance;

            _instance = factory.Create(data);
            Barrel = factory.BarrelTransform;
            _baseBarrel = Barrel;
            _audioSource = factory.AudioSource;

            _cameraTransform = cameraModel.CameraTransform;
            _camera = cameraModel.Camera;

            _instance.transform.parent = _cameraTransform;
            _instance.transform.localPosition = _position;

            _tracerFactory = new TracerFactory(data);
            
            Deactivate();
        }

        #region Methods

        public void Execute(float deltaTime)
        {
            _deltaTime = deltaTime;
        }

        public void Fire()
        {
            if (!IsActive) return;

            _tracerFactory.Create();
            var line = _tracerFactory.LineRenderer;

            line.SetPosition(0, Barrel.position);

            var ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            if (Physics.Raycast(ray, out var hit, _maxShotDistance, _hitLayerMask))
            {
                line.SetPosition(1, hit.point);
            }
            else
            {
                line.SetPosition(1, _cameraTransform.localPosition + _cameraTransform.forward * _maxShotDistance);
            }

            _audioSource.Play();

            TweenLineWidth(line).ToObservable().Subscribe();
        }

        private IEnumerator TweenLineWidth(LineRenderer line)
        {
            while (line.endWidth > 0)
            {
                line.endWidth -= _deltaTime * _tracerFadeMultiplier;
                line.startWidth -= _deltaTime * _tracerFadeMultiplier;

                var color = line.material.color;
                color.a -= _deltaTime * _tracerFadeMultiplier * _colorFadeMultiplier;
                line.material.color = color;

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
            Barrel = barrel;
        }

        public void ResetBarrelPosition()
        {
            Barrel = _baseBarrel;
        }

        public void SetAudioClip(AudioClip clip)
        {
            _audioSource.clip = clip;
        }

        public void Activate()
        {
            IsActive = true;
            _instance.SetActive(true);
            //.transform.localRotation = Quaternion.identity;
        }

        public void Deactivate()
        {
            IsActive = false;
            _instance.SetActive(false);
        }

        #endregion
    }
}