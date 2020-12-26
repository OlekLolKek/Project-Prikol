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

        private readonly LayerMask _groundLayer;
        private readonly Transform _transform;
        private readonly Rigidbody _rigidbody;
        private readonly Vector3 _crouchScale;
        private readonly Vector3 _playerScale;
        private readonly float _sensitivityMultiplier;
        private readonly float _sensitivity;
        private readonly float _moveSpeed;
        private readonly float _maxSpeed;
        private readonly float _jumpCooldown;
        private readonly float _jumpForce;
        private readonly float _counterMovement;
        private readonly float _counterMovementThreshold;
        private readonly float _axisThreshold;
        private readonly float _maxSlopeAngle;
        private readonly float _slideForce;
        private readonly float _slideCounterMovement;
        private readonly float _crouchHeight = 0.5f;
        private readonly float _crouchBoostSpeed = 0.5f;
        private readonly float _extraGravity = 10.0f;
        private readonly float _staticGravity = 3000;

        private Vector3 _normalVector = Vector3.up;
        private Vector3 _wallNormalVector;
        
        private float _xRotation;
        private float _horizontal;
        private float _vertical;
        private float _desiredX;
        private bool _isJumping;
        private bool _sprinting;
        private bool _isCrouching;
        private bool _isReadyToJump = true;
        private bool _isGrounded;

        private readonly IInputAxisChange _horizontalAxis;
        private readonly IInputAxisChange _verticalAxis;
        private readonly IInputKeyPress _startCrouch;
        private readonly IInputKeyRelease _stopCrouch;
        private readonly IInputKeyHold _jump;

        #endregion
        
        
        public MoveController(PlayerModel playerModel, PlayerData playerData, 
            InputModel inputModel)
        {
            _rigidbody = playerModel.Rigidbody;
            _transform = playerModel.Transform;
            _playerScale = _transform.localScale;
            _moveSpeed = playerData.Speed;

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
            
            //TODO: move to CursorController
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        public void Execute(float deltaTime)
        {
            Move(deltaTime);
            //Look();
        }

        private void Move(float deltaTime)
        {
            _rigidbody.AddForce(Vector3.down * (deltaTime * _extraGravity));
            
            var magnitude = FindVelocityRelativeToLook();
            var magnitudeX = magnitude.x;
            var magnitudeY = magnitude.y;

            CounterMovement(_horizontal, _vertical, magnitude, deltaTime);

            if (_isReadyToJump && _isJumping)
            {
                Jump();
            }

            var maxSpeed = _maxSpeed;

            if (_isCrouching && _isGrounded && _isReadyToJump)
            {
                _rigidbody.AddForce(Vector3.down * (deltaTime * _staticGravity));
                return;
            }

            
            //TODO: replace 2nd and 4th `if`s with `else if`
            if (_horizontal > 0.0f && magnitudeX > maxSpeed)
                _horizontal = 0.0f;
            if (_horizontal < 0.0f && magnitudeX < -maxSpeed)
                _horizontal = 0.0f;
            
            if (_vertical > 0.0f && magnitudeY > maxSpeed)
                _vertical = 0.0f;
            if (_vertical < 0.0f && magnitudeY < -maxSpeed)
                _vertical = 0.0f;

            //TODO: replace with fields
            var multiplier = 1.0f;
            var multiplierForward = 1.0f;

            if (!_isGrounded)
            {
                multiplier = 0.5f;
                multiplierForward = 0.5f;
            }

            if (_isGrounded && _isCrouching) 
                multiplierForward = 0.0f;
            
            _rigidbody.AddForce(_transform.forward * (_vertical * _moveSpeed * deltaTime * multiplier * multiplierForward));
            _rigidbody.AddForce(_transform.right * (_horizontal * _moveSpeed * deltaTime * multiplier));
        }

        public void CounterMovement(float x, float y, Vector2 magnitude, float deltaTime)
        {
            if (!_isGrounded)
                return;
            if (_isJumping)
                return;

            if (_isCrouching)
            {
                _rigidbody.AddForce(-_rigidbody.velocity.normalized * 
                                    (_moveSpeed * deltaTime * _slideCounterMovement));
                return;
            }

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
                
                //TODO: create fields instead of the numbers (all the numbers below)
                _rigidbody.AddForce(Vector2.up * (_jumpForce * 1.5f));
                _rigidbody.AddForce(_normalVector * (_jumpForce * 0.5f));

                var velocity = _rigidbody.velocity;
                if (_rigidbody.velocity.y < 0.5f)
                {
                    _rigidbody.velocity = new Vector3(velocity.x, 0.0f, velocity.z);
                }
                else if (_rigidbody.velocity.y > 0)
                {
                    _rigidbody.velocity = new Vector3(velocity.x, velocity.y / 2.0f, velocity.z);
                }

                ResetJump().ToObservable().Subscribe();
            }
        }

        private IEnumerator ResetJump()
        {
            yield return new WaitForSeconds(_jumpCooldown);
            _isReadyToJump = true;
        }

        // private void Look()
        // {
        //     var mouseX = _mouseX
        // }

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
                    //TODO: check if I need to replace _transform with _orientation
                    _rigidbody.AddForce(_transform.forward * _slideForce);
                }
            }
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
                _isJumping = true;
            }
            else
            {
                _isJumping = false;
            }
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