using UnityEngine;


namespace ProjectPrikol
{
    public class CrouchController : IExecutable, ICleanable
    {
        private readonly IInputKeyPress _startCrouch;
        private readonly IInputKeyRelease _stopCrouch;
        
        private readonly PlayerJumpModel _playerJumpModel;
        private readonly PlayerCrouchModel _playerCrouchModel;
        private readonly PlayerModel _playerModel;
        private readonly Transform _transform;
        private readonly Rigidbody _rigidbody;
        private readonly Vector3 _crouchScale;
        private readonly Vector3 _playerScale;

        private readonly float _crouchBoostSpeed = 0.5f;
        private readonly float _crouchHeight = 0.6f;
        private readonly float _slideForce;


        public CrouchController(InputModel inputModel, PlayerCrouchModel crouchModel,
            PlayerJumpModel jumpModel, PlayerModel playerModel,
            PlayerData playerData)
        {
            _startCrouch = inputModel.StartCrouch;
            _stopCrouch = inputModel.StopCrouch;
            _startCrouch.OnKeyPressed += StartCrouch;
            _stopCrouch.OnKeyReleased += StopCrouch;

            _playerCrouchModel = crouchModel;

            _playerJumpModel = jumpModel;

            _playerModel = playerModel;
            _transform = _playerModel.Transform;
            _rigidbody = _playerModel.Rigidbody;
            
            _crouchScale = playerData.CrouchScale;
            _playerScale = playerData.PlayerScale;
        }
        
        public void Execute(float deltaTime)
        {
        }
        
        private void StartCrouch()
        {
            _playerCrouchModel.IsCrouching = true;
            _transform.localScale = _crouchScale;
            var position = _transform.position;
            position = new Vector3(position.x, position.y - _crouchHeight, position.z);
            _transform.position = position;
            if (_playerJumpModel.IsGrounded)
            {
                if (_rigidbody.velocity.magnitude > _crouchBoostSpeed)
                {
                    _rigidbody.AddForce(_transform.forward * _slideForce);
                }
            }
        }

        private void StopCrouch()
        {
            _playerCrouchModel.IsCrouching = false;
            _transform.localScale = _playerScale;
            var position = _transform.position;
            position = new Vector3(position.x, position.y + _crouchHeight, position.z);
            _transform.position = position;
        }

        public void Cleanup()
        {
            _startCrouch.OnKeyPressed -= StartCrouch;
            _stopCrouch.OnKeyReleased -= StopCrouch;
        }
    }
}