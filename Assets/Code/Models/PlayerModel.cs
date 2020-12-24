using UnityEngine;


namespace ProjectPrikol
{
    public class PlayerModel
    {
        private readonly Rigidbody _rigidbody;
        private readonly Transform _transform;

        public PlayerModel(PlayerFactory factory)
        {
            factory.Create();
            _rigidbody = factory.Rigidbody;
            _transform = factory.Transform;
        }
    }
}