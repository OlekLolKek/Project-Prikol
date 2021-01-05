using UnityEngine;

namespace ProjectPrikol
{
    [CreateAssetMenu(fileName = "InputData", menuName = "Data/InputData")]
    public class AssaultRifleData : ScriptableObject, IData, IWeaponData
    {
        [SerializeField] private Vector3 _barrelPosition;
        [SerializeField] private Vector3 _position;
        [SerializeField] private Vector3 _scale;
        [SerializeField] private Material _material;
        [SerializeField] private Mesh _assaultRifleMesh;

        [SerializeField] private string _name;
        [SerializeField] private string _barrelName;
        [SerializeField] private float _shootCooldown;
        [SerializeField] private float _damage;

        public Vector3 BarrelPosition => _barrelPosition;
        public Vector3 Position => _position;
        public Vector3 Scale => _scale;
        public Material Material => _material;
        public Mesh Mesh => _assaultRifleMesh;

        public string Name => _name;
        public string BarrelName => _barrelName;
        public float ShootCooldown => _shootCooldown;
        public float Damage => _damage;
    }
}