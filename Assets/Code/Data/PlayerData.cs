using UnityEngine;

namespace ProjectPrikol
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    public class PlayerData : ScriptableObject, IData
    {
        [SerializeField] private Mesh _playerMesh;
        [SerializeField] private float _speed;

        public Mesh PlayerMesh => _playerMesh;
        public float Speed => _speed;
    }
}