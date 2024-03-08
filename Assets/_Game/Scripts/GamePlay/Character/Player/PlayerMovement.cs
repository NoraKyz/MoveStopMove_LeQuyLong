﻿using _Game.Scripts.GamePlay.Input;
using _SDK.Pool.Scripts;
using _SDK.ServiceLocator.Scripts;
using _SDK.UI.Base;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.Player
{
    public class PlayerMovement : GameUnit
    {
        #region Config

        [Header("References")]
        [SerializeField] private CharacterController controller;
        
        [Header("Config")]
        [SerializeField] private float moveSpeed = 5f;
        
        private bool _moveAble;
        private bool _isStartMove;
        private Vector3 _moveDirection;
        
        private InputManager _inputManager;
        
        public bool IsMoving => _moveDirection != Vector3.zero;
        public Vector3 MoveDirection => _moveDirection;

        #endregion
        
        #region Init
        
        public void OnInit()
        {
            _moveAble = false;
            _isStartMove = false;
            _moveDirection = Vector3.zero;
            _inputManager = this.GetService<InputManager>();
            
            TF.position = Vector3.zero;
            TF.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        #endregion
        
        private void Update()
        {
            if (!_moveAble && !GameManager.IsState(GameState.GamePlay))
            {
                return;
            }
            
            GetInput();
        }
        private void GetInput()
        {
            if (_inputManager.HasInput())
            {
                GetDirectionFromInput();

                OnStartMove();
            }
            else
            {
                _moveDirection = Vector3.zero;
            }
        }
        private void GetDirectionFromInput()
        {
            _moveDirection.Set(_inputManager.HorizontalAxis, 0, _inputManager.VerticalAxis);
            _moveDirection.Normalize();
        }
        private void OnStartMove()
        {
            if(_isStartMove == false)
            {
                _isStartMove = true;
                UIManager.Instance.GetUI<_SDK.UI.GamePlay.UIGamePlay>().SetTutorial(false);
            }
        }
        public void Move()
        {
            controller.Move(_moveDirection * (Time.deltaTime * moveSpeed));
            
            if(_moveDirection != Vector3.zero)
            {
                TF.forward = _moveDirection;
            }
        }
    }
}