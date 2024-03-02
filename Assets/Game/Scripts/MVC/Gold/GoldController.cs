using System;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.MVC.Gold
{
    public class GoldController : MonoBehaviour
    {
        private GoldView _goldView;
        
        public void Initialize()
        {
            _goldView = GetComponent<GoldView>();
            _goldView.SetText(GoldModel.GoldValue);
        }

        public void AddValue(int value)
        {
            GoldModel.GoldValue += value;
            _goldView.SetText(GoldModel.GoldValue);
            GameManager.Instance.EventManager.GoldEvent(GoldModel.GoldValue);
        }

        public void RemoveValue(int value)
        {
            if (value > GoldModel.GoldValue)
                return;
            
            GoldModel.GoldValue -= value;
            _goldView.SetText(GoldModel.GoldValue);
            GameManager.Instance.EventManager.GoldEvent(GoldModel.GoldValue);
        }
    }
}
