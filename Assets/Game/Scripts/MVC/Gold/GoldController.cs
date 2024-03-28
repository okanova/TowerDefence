using System;
using Game.Scripts.Consts;
using Game.Scripts.Interfaces;
using Game.Scripts.Managers;
using UnityEngine;

namespace Game.Scripts.MVC.Gold
{
    public class GoldController : MonoBehaviour, ISaveable
    {
        private GoldView _goldView;

        [SerializeField] private int _firstGold;
        
        public void Initialize()
        {
            LoadData();
            _goldView = GetComponent<GoldView>();
            _goldView.SetText(GoldModel.GoldValue);
            GameManager.Instance.EventManager.GoldEvent(GoldModel.GoldValue);
        }
        
        public void AddValue(int value)
        {
            GoldModel.GoldValue += value;
            _goldView.SetText(GoldModel.GoldValue);
            GameManager.Instance.EventManager.GoldEvent(GoldModel.GoldValue);
            SaveData();
        }

        public void RemoveValue(int value)
        {
            if (value > GoldModel.GoldValue)
                return;
            
            GoldModel.GoldValue -= value;
            _goldView.SetText(GoldModel.GoldValue);
            GameManager.Instance.EventManager.GoldEvent(GoldModel.GoldValue);
            SaveData();
        }

        public void SaveData()
        {
           PlayerPrefs.SetInt(CurrencyDataConsts.Gold, GoldModel.GoldValue);
        }

        public void LoadData()
        {
            if (GameManager.Instance.IsFirstTimeStart)
                ResetData();
            
            GoldModel.GoldValue = PlayerPrefs.GetInt(CurrencyDataConsts.Gold);
        }

        public void ResetData()
        {
            PlayerPrefs.SetInt(CurrencyDataConsts.Gold, _firstGold);
        }
    }
}
