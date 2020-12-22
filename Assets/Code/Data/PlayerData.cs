using UnityEngine;

namespace ProjectPrikol
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private Mesh _playerMesh;

        public Mesh PlayerMesh => _playerMesh;
    }
}