﻿using UnityEngine;

namespace _Game.Scripts.UI.Base
{
    public class UICanvas : MonoBehaviour
    {
        //public bool IsAvoidBackKey = false;
        public bool IsDestroyOnClose = false;

        protected RectTransform m_RectTransform;
        private Animator m_Animator;
        private float m_OffsetY = 0f;

        private void Start()
        {
            OnInit();
        }

        //Init default Canvas
        //khoi tao gia tri canvas
        protected void OnInit()
        {
            m_RectTransform = GetComponent<RectTransform>();
            m_Animator = GetComponent<Animator>();

            // xu ly tai tho
            float ratio = (float)Screen.height / (float)Screen.width;
            if (ratio > 2.1f)
            {
                Vector2 leftBottom = m_RectTransform.offsetMin;
                Vector2 rightTop = m_RectTransform.offsetMax;
                rightTop.y = -100f;
                m_RectTransform.offsetMax = rightTop;
                leftBottom.y = 0f;
                m_RectTransform.offsetMin = leftBottom;
                m_OffsetY = 100f;
            }
        }

        //Setup canvas to avoid flash UI
        //set up mac dinh cho UI de tranh truong hop bi nhay' hinh
        public virtual void Setup()
        {
            UIManager.Instance.AddBackUI(this);
            UIManager.Instance.PushBackAction(this, BackKey);
        }


        //back key in android device
        //back key danh cho android
        public virtual void BackKey()
        {

        }

        //Open canvas
        //mo canvas
        public virtual void Open()
        {
            gameObject.SetActive(true);
            RegisterEvents();
        }

        //close canvas directly
        //dong truc tiep, ngay lap tuc
        public virtual void CloseDirectly()
        {
            UIManager.Instance.RemoveBackUI(this);
            gameObject.SetActive(false);
            RemoveEvents();
        
            if (IsDestroyOnClose)
            {
                Destroy(gameObject);
            }
        }

        //close canvas with delay time, used to anim UI action
        //dong canvas sau mot khoang thoi gian delay
        public virtual void Close(float delayTime)
        {
            Invoke(nameof(CloseDirectly), delayTime);
        }
    
        protected virtual void RegisterEvents() { }
    
        protected virtual void RemoveEvents() { }
    }
}