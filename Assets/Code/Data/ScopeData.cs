using UnityEngine;

namespace ProjectPrikol
{
    [CreateAssetMenu(fileName = "AssaultRifleScopeData", menuName = "Data/Weapon/Attachment/AssaultRifleScope")]

    public class ScopeData : ScriptableObject, IScopeData
    {
        [SerializeField] private GameObject _prefab;
        public GameObject Prefab => _prefab;
    }
}