using UnityEngine;

namespace ProjectPrikol
{
    [CreateAssetMenu(fileName = "AssaultRifleData", menuName = "Data/Weapon/AssaultRifleData")]
    public class AssaultRifleData : ScriptableObject, IData, IWeaponData
    {
        [SerializeField] private AudioClip _shotClip;
        [SerializeField] private Material _tracerMaterial;
        [SerializeField] private LayerMask _hitLayerMask;
        [SerializeField] private Vector3 _barrelPosition;
        [SerializeField] private Vector3 _position;
        [SerializeField] private Vector3 _scale;
        [SerializeField] private Material _material;
        [SerializeField] private Mesh _mesh;

        [SerializeField] private string _name;
        [SerializeField] private string _barrelName;
        [SerializeField] private string _tracerName;
        [SerializeField] private float _tracerFadeMultiplier;
        [SerializeField] private float _maxShotDistance;
        [SerializeField] private float _tracerWidth;
        [SerializeField] private float _shootCooldown;
        [SerializeField] private float _damage;


        public AudioClip ShotClip => _shotClip;
        public Material TracerMaterial => _tracerMaterial;
        public LayerMask HitLayerMask => _hitLayerMask;
        public Vector3 BarrelPosition => _barrelPosition;
        public Vector3 Position => _position;
        public Vector3 Scale => _scale;
        public Material Material => _material;
        public Mesh Mesh => _mesh;

        public string Name => _name;
        public string BarrelName => _barrelName;
        public string TracerName => _tracerName;
        public float ShootCooldown => _shootCooldown;
        public float Damage => _damage;
        public float TracerWidth => _tracerWidth;
        public float TracerFadeMultiplier => _tracerFadeMultiplier;
        public float MaxShotDistance => _maxShotDistance;
    }
}