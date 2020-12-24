using System;
using UnityEngine;


namespace ProjectPrikol
{
    public class PlayerFactory : IFactory
    {
        private PlayerData _playerData;

        public Rigidbody Rigidbody { get; set; }
        public Transform Transform { get; set; }

        public PlayerFactory(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public GameObject Create()
        {
            var player = new GameObject();
            Transform = player.transform;
            player.AddComponent<MeshFilter>().mesh = _playerData.PlayerMesh;
            player.AddComponent<MeshRenderer>();
            Rigidbody = player.AddComponent<Rigidbody>();

            return player;
        }
    }
}