using System;
using Game.Scripts.Events;
using Game.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Views.UIViews
{
    public class BuyButtonView : ButtonView
    {
        public TowerType TowerType;
        public int Cost;
        
       
        private void OnEnable()
        {
            GameManager.Instance.EventManager.OnGoldChanger += CheckMoney;
        }

        private void OnDisable()
        {
            GameManager.Instance.EventManager.OnGoldChanger -= CheckMoney;
        }

        protected override void ButtonClick()
        {
            if (GameManager.Instance.UIManager.GoldController.CanBuy(Cost))
            {
                GameManager.Instance.EventManager.TriggerPathFindEvent();
                GameManager.Instance.UIManager.TowerBuyPanelView.CloseOpenImages(false);
            }
        }

        private void CheckMoney(object sender, EventArgs args)
        {
            GoldEventArgs goldArgs = args as GoldEventArgs;

            if (_button == null)
                _button = GetComponent<Button>();
            
            _button.interactable = goldArgs.Amount >= Cost;
        }
    }
}
