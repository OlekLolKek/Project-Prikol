using UnityEngine;


namespace ProjectPrikol
{
    public class MoveController : IExecutable, ICleanable
    {
        //TODO: divide this controller into separate Move, Crouch, Jump etc controllers
        #region Fields

        private readonly LayerMask _groundLayer;
        private readonly Transform _transform;
        private readonly Rigidbody _rigidbody;
        private readonly Vector3 _crouchScale;
        private readonly Vector3 _playerScale;
        private readonly float _sensitivityMultiplier;
        private readonly float _sensitivity;
        private readonly float _speed;
        private readonly float _maxSpeed;
        private readonly float _jumpCooldown;
        private readonly float _jumpForce;
        private readonly float _counterMovement;
        private readonly float _threshold;
        private readonly float _maxSlopeAngle;
        private readonly float _slideForce;
        private readonly float _slideCounterMovement;
        private readonly float _crouchHeight = 0.5f;
        private readonly float _crouchBoostSpeed = 0.5f;
        private readonly float _extraGravity = 10.0f;

        private Vector3 _normalVector = Vector3.up;
        private Vector3 _wallNormalVector;
        
        private float _xRotation;
        private float _horizontal;
        private float _vertical;

        private bool _jumping;
        private bool _sprinting;
        private bool _crouching;
        private bool _isReadyToJump = true;
        private bool _isGrounded;

        private readonly IInputAxisChange _horizontalAxis;
        private readonly IInputAxisChange _verticalAxis;
        private readonly IInputKeyPress _startCrouch;
        private readonly IInputKeyRelease _stopCrouch;
        private readonly IInputKeyPress _jump;

        #endregion
        
        
        public MoveController(PlayerModel playerModel, PlayerData playerData, 
            InputModel inputModel)
        {
            _rigidbody = playerModel.Rigidbody;
            _transform = playerModel.Transform;
            _playerScale = _transform.localScale;
            _speed = playerData.Speed;

            _horizontalAxis = inputModel.Horizontal;
            _verticalAxis = inputModel.Vertical;
            _startCrouch = inputModel.StartCrouch;
            _stopCrouch = inputModel.StopCrouch;
            _jump = inputModel.Jump;
            _horizontalAxis.OnAxisChanged += HorizontalAxisChanged;
            _verticalAxis.OnAxisChanged += VerticalAxisChanged;
            _startCrouch.OnKeyPressed += StartCrouch;
            _stopCrouch.OnKeyReleased += StopCrouch;
            

            //TODO: move to CursorController
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        public void Execute(float deltaTime)
        {
            MyInput();
            Move(deltaTime);
            Look();
        }

        private void Move(float deltaTime)
        {
            _rigidbody.AddForce(Vector3.down * (deltaTime * _extraGravity));
            var magnitude = FindVelocityRelativeToLook();
        }

        private void MyInput()
        {
            
        }

        private void Look()
        {
            
        }

        private Vector2 FindVelocityRelativeToLook()
        {
            var lookAngle = _transform.eulerAngles.y;
            var velocity = _rigidbody.velocity;
            var moveAngle = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;

            var deltaAngle = Mathf.DeltaAngle(lookAngle, moveAngle);
            var deltaAngleY = 90 - deltaAngle;
            var magnitude = _rigidbody.velocity.magnitude;
            var magnitudeY = magnitude * Mathf.Cos(deltaAngle * Mathf.Deg2Rad);
            var magnitudeX = magnitude * Mathf.Cos(deltaAngleY * Mathf.Deg2Rad);

            return new Vector2(magnitudeX, magnitudeY);
        }

        private void HorizontalAxisChanged(float value)
        {
            _horizontal = value;
        }
        
        private void VerticalAxisChanged(float value)
        {
            _vertical = value;
        }

        private void StartCrouch()
        {
            _crouching = true;
            _transform.localScale = _crouchScale;
            var position = _transform.position;
            position = new Vector3(position.x, position.y - _crouchHeight, position.z);
            _transform.position = position;
            if (_isGrounded)
            {
                if (_rigidbody.velocity.magnitude > _crouchBoostSpeed)
                {
                    //TODO: check if I need to replace _transform with _orientation
                    _rigidbody.AddForce(_transform.forward * _slideForce);
                }
            }
        }

        private void StopCrouch()
        {
            _crouching = false;
            _transform.localScale = _playerScale;
            var position = _transform.position;
            position = new Vector3(position.x, position.y + _crouchHeight, position.z);
            _transform.position = position;
        }

        public void Cleanup()
        {
            _horizontalAxis.OnAxisChanged -= HorizontalAxisChanged;
            _verticalAxis.OnAxisChanged -= VerticalAxisChanged;
            _startCrouch.OnKeyPressed -= StartCrouch;
            _stopCrouch.OnKeyReleased -= StopCrouch;
        }
    }
}