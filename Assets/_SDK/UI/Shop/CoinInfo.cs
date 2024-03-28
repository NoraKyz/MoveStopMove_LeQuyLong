﻿using System;
using _Game.Scripts.Data;
using _SDK.Observer.Scripts;
using _SDK.ServiceLocator.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _SDK.UI.Shop
{
    public class CoinInfo : MonoBehaviour
    {
        #region Config

        [SerializeField] private Text coinText;
        
        private PlayerData PlayerData => this.GetService<DataManager>().PlayerData;
        
        private Action<object> _onChangeCoin;

        #endregion

        private void OnEnable()
        {
            _onChangeCoin = _ => OnInit();
            this.RegisterListener(EventID.OnChangeCoin, _onChangeCoin);
            
            OnInit();
        }
        
        private void OnDisable()
        {
            this.RemoveListener(EventID.OnChangeCoin, _onChangeCoin);
        }

        private void OnInit()
        {
            coinText.text = PlayerData.Coin.ToString();
        }
    }
}