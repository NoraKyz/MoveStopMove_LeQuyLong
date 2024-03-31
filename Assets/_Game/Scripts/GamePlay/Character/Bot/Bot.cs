using System;
using _Game.Scripts.Other.Utils;
using _Game.Scripts.Setting.Sound;
using _SDK.Pool.Scripts;
using _SDK.ServiceLocator.Scripts;
using _SDK.StateMachine;
using _SDK.StateMachine.BotState;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.Bot
{
    public class Bot : GamePlay.Character.Base.Character
    {
        #region Config
        
        [Header("References")]
        [SerializeField] private BotMovement botMovement;
        
        private StateMachine<Bot> _stateMachine;
        
        public bool IsDestination => botMovement.IsDestination;
        
        #endregion

        private void Awake()
        {
            _stateMachine = new StateMachine<Bot>(this);
        }
        
        public override void OnInit()
        {
            base.OnInit();
            
            SetName(NameUtilities.GetRandomName());
            
            characterSkin.OnInit(this);
            botMovement.OnInit();
            _stateMachine.ChangeState(new BotIdleState());
        }

        private void Update()
        {
            _stateMachine.UpdateState(this);
        }

        public override void OnHit(Action hitAction, Base.Character killer)
        {
            base.OnHit(hitAction, killer);
            
            ChangeState(new BotDieState());
        }
        
        public override void OnDespawn()
        {
            base.OnDespawn();
            
            SimplePool.Despawn(this);
        }
        
        public void MoveToPosition(Vector3 position) => botMovement.MoveToPosition(position);
        
        public void StopMove() => botMovement.StopMove();
        
        public void ChangeState(IState<Bot> state) => _stateMachine.ChangeState(state);
    }
} 
