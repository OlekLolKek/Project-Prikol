using UnityEngine;

namespace ProjectPrikol
{
    [CreateAssetMenu(fileName = "AssaultRifleSilencerData", menuName = "Data/Weapon/Attachment/AssaultRifleSilencer")]
    public class AssaultRifleSilencerData : ScriptableObject, ISilencerData
    {
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Vector3 _scale;
        [SerializeField] private Material _material;
        [SerializeField] private Vector3 _barrelPosition;
        
        [SerializeField] private string _name;
        [SerializeField] private string _barrelName;

        public Mesh Mesh => _mesh;
        public Vector3 Scale => _scale;
        public Material Material => _material;
        public Vector3 BarrelPosition => _barrelPosition;
        public string Name => _name;
        public string BarrelName => _barrelName;
    }
}