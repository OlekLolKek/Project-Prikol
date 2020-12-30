using UnityEngine;


namespace ProjectPrikol
{
    public class PlayerModel
    {
        public Rigidbody Rigidbody { get; }
        public Transform Transform { get; }
        public Transform Orientation { get; }
        public Transform Head { get; }
        public PlayerView PlayerView { get; }

        public PlayerModel(PlayerFactory factory)
        {
            factory.Create();
            Rigidbody = factory.Rigidbody;
            Transform = factory.Transform;
            Orientation = factory.Orientation;
            Head = factory.Head;
            PlayerView = factory.PlayerView;
        }
    }
}