using System;
using Game.Scripts.Events;
using Game.Scripts.Managers;
using UnityEngine.UI;

namespace Game.Scripts.Views.UIViews
{
    public class UpgradeButtonView : ButtonView
    {
        private int _cost;
        public int Cost => _cost;

        public void SetCost(int value)
        {
            _cost = value;
        }
        
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
            GameManager.Instance.GridManager.ClickGrid.Tower.LevelUp();
            GameManager.Instance.UIManager.TowerUpgradePanelView.ClosePanel();
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
