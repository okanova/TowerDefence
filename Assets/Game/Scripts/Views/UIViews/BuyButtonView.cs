using System;
using Game.Scripts.Events;
using Game.Scripts.Managers;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace Game.Scripts.Views.UIViews
{
    public class BuyButtonView : ButtonView
    {
        [EnumPaging]
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
            GameManager.Instance.UIManager.GoldController.RemoveValue(Cost);
            GameManager.Instance.EventManager.TriggerPathFindEvent();
            GameManager.Instance.UIManager.TowerBuyPanelView.CloseOpenImages(false);
            GameManager.Instance.GridManager.ClickGrid.CreateTower(TowerType);
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
