using System;
using Game.Scripts.Events;
using Game.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Views.UIViews
{
    public class SellButtonView : ButtonView
    {
        private int _cost;
        public int Cost => _cost;

        public void SetCost(int value)
        {
            _cost = value;
        }
        
        protected override void ButtonClick()
        {
            GameManager.Instance.UIManager.GoldController.AddValue(Cost);
            GameManager.Instance.GridManager.ClickGrid.ClearGrid();
            GameManager.Instance.UIManager.TowerUpgradePanelView.ClosePanel();
        }
    }
}
