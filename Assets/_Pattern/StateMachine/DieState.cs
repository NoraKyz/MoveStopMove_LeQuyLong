﻿using _Game.Scripts.Character;
using _Game.Scripts.Utils;
using UnityEngine;

namespace _Pattern.StateMachine
{
    public class DieState<T> : IState<T> where T : Character
    {
        private const float DespawnTime = 1.5f;
        
        private float _timer;
        public void OnEnter(T character)
        {
            _timer = 0;
            character.ChangeAnim(AnimName.Die);
        }

        public void OnExecute(T character)
        {
            _timer += Time.deltaTime;
            if (_timer >= DespawnTime)
            {
                character.OnDeath();
            }
        }

        public void OnExit(T character)
        {
            
        }
    }
}