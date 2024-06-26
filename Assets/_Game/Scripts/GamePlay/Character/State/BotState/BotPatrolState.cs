﻿using _Game.Scripts.Level;
using _Game.Scripts.Other.Utils;
using _SDK.StateMachine;
using _SDK.UI.Base;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.State.BotState
{
    public class BotPatrolState : IState<Bot.Bot>
    {
        // Ty le tan cong ke thu tren duong di
        private int _chanceAttack = Random.Range(0, 100);
        private bool _attackIfEnemyInRange;
        private Vector3 _nextDestination;
        
        public void OnEnter(Bot.Bot bot)
        {
            // Random de xem co tan cong ke thu tren duong di hay khong
            _attackIfEnemyInRange = Utilities.Chance(_chanceAttack);
            
            // Lay 1 diem ngau nhien trong map
            Map.Map currentMap = LevelManager.Ins.CurrentMap;
            _nextDestination = currentMap.GetRandomPos();
            
            bot.ChangeAnim(AnimName.RUN);
            bot.MoveToPosition(_nextDestination);
        }

        public void OnExecute(Bot.Bot bot)
        {
            if (bot.IsDestination)
            {
                bot.ChangeState(new BotIdleState());
            }
            
            if (GameManager.IsState(GameState.GamePlay) == false)
            {
                return;
            }
            
            if (bot.HasEnemyInRange && _attackIfEnemyInRange)
            {
                bot.ChangeState(new BotIdleState());
            }
        }

        public void OnExit(Bot.Bot bot)
        {
            
        }
    }
}