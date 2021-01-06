﻿using UnityEngine;

namespace ProjectPrikol
{
    [CreateAssetMenu(fileName = "AssaultRifleData", menuName = "Data/Weapon/AssaultRifleData")]
    public class AssaultRifleData : ScriptableObject, IData, IWeaponData
    {
        [SerializeField] private Vector3 _barrelPosition;
        [SerializeField] private Vector3 _position;
        [SerializeField] private Vector3 _scale;
        [SerializeField] private Material _material;
        [SerializeField] private Mesh _mesh;

        [SerializeField] private string _name;
        [SerializeField] private string _barrelName;
        [SerializeField] private float _shootCooldown;
        [SerializeField] private float _damage;

        public Vector3 BarrelPosition => _barrelPosition;
        public Vector3 Position => _position;
        public Vector3 Scale => _scale;
        public Material Material => _material;
        public Mesh Mesh => _mesh;

        public string Name => _name;
        public string BarrelName => _barrelName;
        public float ShootCooldown => _shootCooldown;
        public float Damage => _damage;
    }
}