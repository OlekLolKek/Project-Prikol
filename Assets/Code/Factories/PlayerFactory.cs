﻿using System;
using UnityEngine;


namespace ProjectPrikol
{
    public class PlayerFactory : IFactory
    {
        private PlayerData _playerData;

        public Rigidbody Rigidbody { get; private set; }
        public Transform Transform { get; private set; }
        public Transform Orientation { get; private set; }
        public Transform Head { get; private set; }

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
            player.AddComponent<CapsuleCollider>();
            Transform.localScale = _playerData.PlayerScale;
            Rigidbody = player.AddComponent<Rigidbody>();
            Rigidbody.mass = _playerData.Mass;
            Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

            Orientation = new GameObject(_playerData.OrientationName).transform;
            Orientation.parent = player.transform;
            Orientation.localPosition = Vector3.zero;

            Head = new GameObject(_playerData.HeadName).transform;
            Head.parent = player.transform;
            Head.localPosition = _playerData.HeadPosition;

            return player;
        }
    }
}