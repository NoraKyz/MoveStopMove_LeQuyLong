﻿using _Game.Scripts.Character.Player;
using _Game.Utils;

namespace _Pattern.StateMachine.PlayerState
{
    public class PlayerRunState : IState<Player>
    {
        public void OnEnter(Player player)
        {
            player.ChangeAnim(AnimName.Run);
        }

        public void OnExecute(Player player)
        {
            if (player.IsMoving == false)
            {
                player.ChangeState(new PlayerIdleState());
            }
            
            player.Move();
        }

        public void OnExit(Player player)
        {
            
        }
    }
}