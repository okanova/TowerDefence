using System;
using UnityEngine;

namespace Game.Scripts.Settings
{
    [CreateAssetMenu(fileName = "Tower Settings", menuName = "ScriptableObject/TowerSettings")]
    public class TowerSettings : ScriptableObject
    {
        public Towers[] Towers;
    }
    
    [Serializable]
    
    public class Towers
    {
        public TowerType TowerType;
        public TowerLevels[] TowerLevels;
    }

    [Serializable]
    public class TowerLevels
    {
        public string Name;
        public Sprite Sprite;
        public int Damage;
        public int ExtraDamage;
        public float SlowValue;
        public float AttackRate;
        public int UpgradeValue;
        public int SellValue;
    }
}
