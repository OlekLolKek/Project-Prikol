using System;
using System.Collections;
using System.Dynamic;
using UniRx;
using UnityEngine;


namespace ProjectPrikol
{
    public class MoveController : IExecutable, ICleanable
    {
        //TODO: divide this controller into separate Move, Crouch, Jump etc controllers
        #region Fields

        private readonly PlayerView _playerView;
        private readonly LayerMask _groundLayer;
        private readonly Transform _transform;
        private readonly Rigidbody _rigidbody;
        private readonly Vector3 _crouchScale;
        private readonly Vector3 _playerScale;
        private readonly float _sensitivityMultiplier;
        private readonly float _sensitivity;
        private readonly float _moveSpeed;
        private readonly float _maxSpeed = 20.0f;
        private readonly float _jumpCooldown = 0.25f;
        private readonly float _jumpForce;
        private readonly float _counterMovement = 0.175f;
        private readonly float _counterMovementThreshold = 0.01f;
        private readonly float _axisThreshold = 0.05f;
        private readonly float _maxSlopeAngle = 35.0f;
        private readonly float _slideForce;
        private readonly float _crouchHeight = 0.6f;
        private readonly float _crouchBoostSpeed = 0.5f;
        private readonly float _extraGravity = 300.0f;
        private readonly float _crouchingGravity = 3000;
        private readonly float _stopGroundedDelay = 3.0f;
        private readonly float _jumpUpMultiplier = 1.5f;
        private readonly float _jumpNormalMultiplier = 0.5f;
        private float _deltaTime;

        private Vector3 _normalVector = Vector3.up;
        private Vector3 _wallNormalVector;
        
        private float _xRotation;
        private float _horizontal;
        private float _vertical;
        private float _desiredX;
        private bool _isPressingJumpButton;
        private bool _sprinting;
        private bool _isCrouching;
        private bool _isReadyToJump = true;
        private bool _isGrounded;
        private bool _isCancellingGrounded;

        private readonly IInputAxisChange _horizontalAxis;
        private readonly IInputAxisChange _verticalAxis;
        private readonly IInputKeyPress _startCrouch;
        private readonly IInputKeyRelease _stopCrouch;
        private readonly IInputKeyHold _jump;

        //TODO: eto che
        private IDisposable _stopGroundedInvoke;

        #endregion
        
        
        public MoveController(PlayerModel playerModel, PlayerData playerData, 
            InputModel inputModel)
        {
            _rigidbody = playerModel.Rigidbody;
            _transform = playerModel.Transform;
            
            _playerView = playerModel.PlayerView;
            _playerView.OnCollisionStayEvent += PlayerCollision;

            _playerScale = playerData.PlayerScale;
            _transform.localScale = _playerScale;
            _crouchScale = playerData.CrouchScale;
            _groundLayer = playerData.GroundLayerMask;
            
            _moveSpeed = playerData.Speed;
            _slideForce = playerData.SlideForce;
            _jumpForce = playerData.JumpForce;
            
            _horizontalAxis = inputModel.Horizontal;
            _verticalAxis = inputModel.Vertical;
            _startCrouch = inputModel.StartCrouch;
            _stopCrouch = inputModel.StopCrouch;
            _jump = inputModel.Jump;
            _horizontalAxis.OnAxisChanged += HorizontalAxisChanged;
            _verticalAxis.OnAxisChanged += VerticalAxisChanged;
            _startCrouch.OnKeyPressed += StartCrouch;
            _stopCrouch.OnKeyReleased += StopCrouch;
            _jump.OnKeyHeld += IsJumpButtonHeld;
        }
        
        public void Execute(float deltaTime)
        {
            _deltaTime = deltaTime;
            Move();
        }

        private void Move()
        {
            _rigidbody.AddForce(Vector3.down * (_deltaTime * _extraGravity));
            
            var magnitude = FindVelocityRelativeToLook();

            CounterMovement(_horizontal, _vertical, magnitude, _deltaTime);

            if (_isReadyToJump && _isPressingJumpButton)
            {
                //Jump();
            }

            if (_isCrouching && _isGrounded && _isReadyToJump)
            {
                _rigidbody.AddForce(Vector3.down * (_deltaTime * _crouchingGravity));
                return;
            }

            var multiplier = 1.0f;
            var multiplierForward = 1.0f;

            if (!_isGrounded)
            {
                multiplier = 0.5f;
                multiplierForward = 0.5f;
            }
            
            _rigidbody.AddForce(_transform.forward * (_vertical * _moveSpeed * _deltaTime * multiplier * multiplierForward));
            _rigidbody.AddForce(_transform.right * (_horizontal * _moveSpeed * _deltaTime * multiplier));
        }
        
        private void CounterMovement(float x, float y, Vector2 magnitude, float deltaTime)
        {
            if (!_isGrounded)
                return;
            if (_isPressingJumpButton)
                return;
            
            if (Math.Abs(magnitude.x) > _counterMovementThreshold && Math.Abs(x) < _axisThreshold ||
                (magnitude.x < -_counterMovementThreshold && x > 0) ||
                magnitude.x > _counterMovementThreshold && x < 0)
            {
                _rigidbody.AddForce(_transform.right * (_moveSpeed * deltaTime * -magnitude.x * _counterMovement));
            }

            if (Math.Abs(magnitude.y) > _counterMovementThreshold && Math.Abs(y) < _axisThreshold ||
                (magnitude.y < -_counterMovementThreshold && y > 0) ||
                (magnitude.y > _counterMovementThreshold && y < 0))
            {
                _rigidbody.AddForce(_transform.forward * (_moveSpeed * deltaTime * -magnitude.y * _counterMovement));
            }

            if (Mathf.Sqrt((Mathf.Pow(_rigidbody.velocity.x, 2) + Mathf.Pow(_rigidbody.velocity.z, 2))) > _maxSpeed)
            {
                var velocity = _rigidbody.velocity;
                var fallSpeed = velocity.y;
                var newVelocity = velocity.normalized * _maxSpeed;
                velocity = new Vector3(newVelocity.x, fallSpeed, newVelocity.z);
                _rigidbody.velocity = velocity;
            }
        }

        private void Jump()
        {
            if (_isGrounded && _isReadyToJump)
            {
                _isReadyToJump = false;
                _rigidbody.AddForce(Vector2.up * (_jumpForce * _jumpUpMultiplier));
                _rigidbody.AddForce(_normalVector * (_jumpForce * _jumpNormalMultiplier));

                ResetJump().ToObservable().Subscribe();
            }
        }

        private IEnumerator ResetJump()
        {
            yield return new WaitForSeconds(_jumpCooldown);
            _isReadyToJump = true;
        }

        private Vector2 FindVelocityRelativeToLook()
        {
            var lookAngle = _transform.eulerAngles.y;
            var velocity = _rigidbody.velocity;
            var moveAngle = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;

            var deltaAngleY = Mathf.DeltaAngle(lookAngle, moveAngle);
            var deltaAngleX = 90 - deltaAngleY;
            var magnitude = _rigidbody.velocity.magnitude;
            var magnitudeY = magnitude * Mathf.Cos(deltaAngleY * Mathf.Deg2Rad);
            var magnitudeX = magnitude * Mathf.Cos(deltaAngleX * Mathf.Deg2Rad);

            return new Vector2(magnitudeX, magnitudeY);
        }

        private bool IsFloor(Vector3 normal)
        {
            var angle = Vector3.Angle(Vector3.up, normal);
            return angle < _maxSlopeAngle;
        }

        private void PlayerCollision(Collision other)
        {
            int layer = other.gameObject.layer;

            //TODO: what is this
            if (_groundLayer != (_groundLayer | (1 << layer))) return;

            for (int i = 0; i < other.contactCount; i++)
            {
                var normal = other.contacts[i].normal;
                if (IsFloor(normal))
                {
                    _isGrounded = true;
                    _isCancellingGrounded = false;
                    _normalVector = normal;
                    _stopGroundedInvoke?.Dispose();
                }
            }

            if (!_isCancellingGrounded)
            {
                _isCancellingGrounded = true;
                _stopGroundedInvoke = StopGrounded(_deltaTime * _stopGroundedDelay).ToObservable().Subscribe();
            }
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
            _isCrouching = true;
            _transform.localScale = _crouchScale;
            var position = _transform.position;
            position = new Vector3(position.x, position.y - _crouchHeight, position.z);
            _transform.position = position;
            if (_isGrounded)
            {
                if (_rigidbody.velocity.magnitude > _crouchBoostSpeed)
                {
                    _rigidbody.AddForce(_transform.forward * _slideForce);
                }
            }
        }

        private IEnumerator StopGrounded(float delay)
        {
            yield return new WaitForSeconds(delay);
            _isGrounded = false;
        }

        private void StopCrouch()
        {
            _isCrouching = false;
            _transform.localScale = _playerScale;
            var position = _transform.position;
            position = new Vector3(position.x, position.y + _crouchHeight, position.z);
            _transform.position = position;
        }

        private void IsJumpButtonHeld(bool isButtonPressed)
        {
            if (isButtonPressed)
            {
                _isPressingJumpButton = true;
            }
            else
            {
                _isPressingJumpButton = false;
            }
        }

        public void Cleanup()
        {
            _horizontalAxis.OnAxisChanged -= HorizontalAxisChanged;
            _verticalAxis.OnAxisChanged -= VerticalAxisChanged;
            _startCrouch.OnKeyPressed -= StartCrouch;
            _stopCrouch.OnKeyReleased -= StopCrouch;
            _jump.OnKeyHeld -= IsJumpButtonHeld;
            _playerView.OnCollisionStayEvent -= PlayerCollision;
        }
    }
}