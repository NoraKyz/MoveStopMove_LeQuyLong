﻿using System;
using _Game.Scripts.Data;
using _Game.Scripts.GamePlay.Character.Base;
using _Game.Scripts.Other.Utils;
using _SDK.Observer.Scripts;
using _SDK.UI.Shop;

namespace _Game.Scripts.GamePlay.Character.Player
{
    public class PlayerSkin : CharacterSkin
    {
        private Action<object> _onSelectSkinItem;
        private Action<object> _onCloseSkinShop;

        #region Init

        private void OnEnable()
        {
            _onSelectSkinItem = (param) => TrySkin((ItemShop) param);
            this.RegisterListener(EventID.OnSelectItem, _onSelectSkinItem);

            _onCloseSkinShop = (_) => OnInit();
            this.RegisterListener(EventID.OnCloseShop, _onCloseSkinShop);
        }
        
        private void OnDisable()
        {
            this.RemoveListener(EventID.OnSelectItem, _onSelectSkinItem);
            this.RemoveListener(EventID.OnCloseShop, _onCloseSkinShop);
        }
        
        public override void OnInit()
        {
            base.OnInit();
            
            ChangeWeapon(UserData.Ins.PlayerWeapon);
            ChangeHair(UserData.Ins.PlayerHair);
            ChangeShield(UserData.Ins.PlayerShield);
            ChangePant(UserData.Ins.PlayerPant);
        }

        #endregion
        
        private void TrySkin(ItemShop item)
        {
            switch (item.ShopType)
            {
                case ShopType.Hair:
                    DespawnHair();
                    ChangeHair((HairType) item.ItemType);
                    break;
                case ShopType.Pant:
                    DespawnHair();
                    ChangePant((PantType) item.ItemType);
                    break;
                case ShopType.Shield:
                    DespawnShield();
                    ChangeShield((ShieldType) item.ItemType);
                    break;
                case ShopType.Set:
                    break;
                case ShopType.Weapon:
                    DespawnWeapon();
                    ChangeWeapon((WeaponType) item.ItemType);
                    break;
            }
        }
    }
}