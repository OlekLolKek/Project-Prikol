using UnityEngine;

namespace ProjectPrikol
{
    [CreateAssetMenu(fileName = "SafetyData", menuName = "Data/Weapon/Attachment/SafetyData")]
    public class SafetyData : ScriptableObject, ISafetyData
    {
        [SerializeField] private AudioClip _safetyClick;
        public AudioClip SafetyClick => _safetyClick;
    }
}