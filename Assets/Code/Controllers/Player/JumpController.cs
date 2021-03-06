﻿using System.Collections;
using UniRx;
using UnityEngine;


namespace ProjectPrikol
{
    public class JumpController : IExecutable, ICleanable
    {
        #region Fields

        private readonly PlayerJumpModel _playerJumpModel;
        private readonly LayerMask _groundLayer;
        private readonly Rigidbody _rigidbody;
        private readonly IInputKeyHold _jump;

        private readonly float _jumpNormalMultiplier = 0.5f;
        private readonly float _jumpUpMultiplier = 1.5f;
        private readonly float _jumpCooldown = 0.25f;
        private readonly float _jumpForce;
        
        private Vector3 _normalVector = Vector3.up;
        
        private bool _isReadyToJump = true;
        private bool _isGrounded;

        #endregion

        public JumpController(PlayerModel playerModel, PlayerData playerData,
            InputModel inputModel, PlayerJumpModel jumpModel)
        {
            _rigidbody = playerModel.Rigidbody;

            _jumpForce = playerData.JumpForce;

            _playerJumpModel = jumpModel;
            
            _jump = inputModel.Jump;
            _jump.OnKeyHeld += IsJumpButtonHeld;
        }
        
        public void Execute(float deltaTime)
        {
            GetValues();
            Jump();
        }

        private void Jump()
        {
            if (!_isReadyToJump || !_playerJumpModel.IsPressingJumpButton || !_isGrounded) 
                return;
            
            _isReadyToJump = false;
            _rigidbody.AddForce(Vector2.up * (_jumpForce * _jumpUpMultiplier));
            _rigidbody.AddForce(_normalVector * (_jumpForce * _jumpNormalMultiplier));

            ResetJump().ToObservable().Subscribe();
        }

        private void GetValues()
        {
            _isGrounded = _playerJumpModel.IsGrounded;
            _normalVector = _playerJumpModel.NormalVector;
        }

        private IEnumerator ResetJump()
        {
            yield return new WaitForSeconds(_jumpCooldown);
            _isReadyToJump = true;
        }
        
        private void IsJumpButtonHeld(bool isButtonPressed)
        {
            if (isButtonPressed)
            {
                _playerJumpModel.IsPressingJumpButton = true;
            }
            else
            {
                _playerJumpModel.IsPressingJumpButton = false;
            }
        }

        public void Cleanup()
        {
            _jump.OnKeyHeld -= IsJumpButtonHeld;
        }
    }
}