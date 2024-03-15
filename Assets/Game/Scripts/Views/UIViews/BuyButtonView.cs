using System;
using Game.Scripts.Events;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.Views.UIViews
{
    public class BuyButtonView : ButtonView
    {
        public TowerType TowerType;
        public int Cost;

        [SerializeField] private GameObject _panel;


        public override void Initialize()
        {
            base.Initialize();
            OnEnable();
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
            if (GameManager.Instance.UIManager.GoldController.CanBuy(Cost))
            {
                GameManager.Instance.EventManager.TriggerPathFindEvent();
                _panel.SetActive(false);
            }
        }

        private void CheckMoney(object sender, EventArgs args)
        {
            GoldEventArgs goldArgs = args as GoldEventArgs;

            _button.interactable = goldArgs.Amount >= Cost;
            Debug.Log(_button.interactable);
        }
    }
}
