using Game.Scripts.Consts;
using Game.Scripts.Interfaces;
using Game.Scripts.Managers;
using Game.Scripts.MVC.Gold;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.MVC.HP
{
    public class HealthController : MonoBehaviour, ISaveable
    {
        private HealthView _healthView;
        
        [SerializeField] private int _firstHP;
        
        public void Initialize()
        {
            LoadData();
            _healthView = GetComponent<HealthView>();
            _healthView.SetText(HealthModel.HPValue);
        }

        public void AddValue(int value = 1)
        {
            HealthModel.HPValue += value;
            _healthView.SetText(HealthModel.HPValue);
            GameManager.Instance.EventManager.HPEvent();
            SaveData();
        }

        public void RemoveValue(int value = 1)
        {
            HealthModel.HPValue -= value;
            HealthModel.HPValue = Mathf.Max(0, HealthModel.HPValue);
            _healthView.SetText(HealthModel.HPValue);
            SaveData();

            if (HealthModel.HPValue == 0)
            {
                GameManager.Instance.EventManager.GameLoseEvent();
            }
            else
            {
                GameManager.Instance.EventManager.HPEvent();
            }
        }

        public void SaveData()
        {
            PlayerPrefs.SetInt(CurrencyDataConsts.Health, HealthModel.HPValue);
        }

        public void LoadData()
        {
            if (GameManager.Instance.IsFirstTimeStart)
                ResetData();
            
            HealthModel.HPValue = PlayerPrefs.GetInt(CurrencyDataConsts.Health);
        }

        public void ResetData()
        {
            PlayerPrefs.SetInt(CurrencyDataConsts.Health, _firstHP);
        }

        [Button]
        public void ResetSave()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
