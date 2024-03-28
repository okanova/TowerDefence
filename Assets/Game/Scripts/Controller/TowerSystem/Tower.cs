using Game.Scripts.Settings;
using UnityEngine;

namespace Game.Scripts.Controller.TowerSystem
{
    public class Tower : MonoBehaviour
    {
        private TowerType _towerType;
        public TowerType TowerType => _towerType;
        
        [SerializeField] private GameObject[] _models;
        [SerializeField] private TowerSettings _towerSettings;
        
        private int _towerTypeCount;

        private int _towerLevel;
        public int TowerLevel => _towerLevel;

        private string _name;
        public string Name => _name;

        private Sprite _sprite;
        public Sprite Sprite => _sprite;

        private int _damage;
        public int Damage => _damage;
        
        private int _extraDamage;
        public int ExtraDamage => _extraDamage;
        
        private float _slowValue;
        public float SlowValue => _slowValue;
        
        private float _attackRate;
        public float AttackRate => _attackRate;
        
        private int _upgradeValue;
        public int UpgradeValue => _upgradeValue;
        
        private int _sellValue;
        public int SellValue => _sellValue;


        public void Initialize(TowerType towerType)
        {
            _towerType = towerType;
            _towerLevel = 0;

            for (int i = 0; i < _models.Length; i++)
            {
                if (towerType.ToString() == _models[i].name)
                    _models[i].SetActive(true);
                else
                    _models[i].SetActive(false);
            }
            
            for (int i = 0; i < _towerSettings.Towers.Length; i++)
            {
                if (_towerSettings.Towers[i].TowerType == towerType)
                {
                    _towerTypeCount = i;
                }
            }
            
            SetValues();
        }

        private void SetValues()
        {
            _name = _towerSettings.Towers[_towerTypeCount].TowerLevels[_towerLevel].Name;
            _sprite = _towerSettings.Towers[_towerTypeCount].TowerLevels[_towerLevel].Sprite;
            _damage = _towerSettings.Towers[_towerTypeCount].TowerLevels[_towerLevel].Damage;
            _extraDamage = _towerSettings.Towers[_towerTypeCount].TowerLevels[_towerLevel].ExtraDamage;
            _slowValue = _towerSettings.Towers[_towerTypeCount].TowerLevels[_towerLevel].SlowValue;
            _attackRate = _towerSettings.Towers[_towerTypeCount].TowerLevels[_towerLevel].AttackRate;
            _upgradeValue = _towerSettings.Towers[_towerTypeCount].TowerLevels[_towerLevel].UpgradeValue;
            _sellValue = _towerSettings.Towers[_towerTypeCount].TowerLevels[_towerLevel].SellValue;
        }
        
        
        public void LevelUp()
        {
            _towerLevel++;
            SetValues();
        }
    }
}
