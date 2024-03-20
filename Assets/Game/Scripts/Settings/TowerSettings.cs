using System;
using Sirenix.OdinInspector;
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
        public int Level1Damage;
        public int Level1ExtraDamage;
        public float Level1SlowValue;
        public float Level1AttackRate;
        public int Level1UpgradeValue;
        public int Level1SellValue;
        
        public int Level2Damage;
        public int Level2ExtraDamage;
        public float Level2SlowValue;
        public float Level2AttackRate;
        public int Level2UpgradeValue;
        public int Level2SellValue;
        
        public int Level3Damage;
        public int Level3ExtraDamage;
        public float Level3SlowValue;
        public float Level3AttackRate;
        public int Level3UpgradeValue;
        public int Level3SellValue;
    }
}
