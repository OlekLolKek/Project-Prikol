using UnityEngine;

namespace ProjectPrikol
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    public class PlayerData : ScriptableObject, IData
    {
        [SerializeField] private Vector3 _playerScale;
        [SerializeField] private Vector3 _headPosition;
        [SerializeField] private Mesh _playerMesh;
        [SerializeField] private string _orientationName;
        [SerializeField] private string _headName;
        [SerializeField] private float _speed;
        [SerializeField] private float _mass;

        public Vector3 PlayerScale => _playerScale;
        public Vector3 HeadPosition => _headPosition;

        public Mesh PlayerMesh => _playerMesh;
        public string OrientationName => _orientationName;
        public string HeadName => _headName;
        public float Speed => _speed;
        public float Mass => _mass;
    }
}