using UnityEngine;

namespace ProjectPrikol
{
    [CreateAssetMenu(fileName = "AssaultRifleSilencerData", menuName = "Data/Weapon/Attachment/AssaultRifleSilencer")]
    public class BarrelAttachmentData : ScriptableObject, IBarrelAttachmentData
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Vector3 _barrelPosition;
        
        [SerializeField] private string _barrelName;

        public GameObject Prefab => _prefab;
        public Vector3 BarrelPosition => _barrelPosition;
        public string BarrelName => _barrelName;
    }
}