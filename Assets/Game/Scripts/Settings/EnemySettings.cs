using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Settings
{
    [CreateAssetMenu(fileName = "Enemy Settings", menuName = "ScriptableObject/EnemySettings")]
    public class EnemySettings : ScriptableObject
    {
        [TabGroup("GOBLIN")] public int GoblinHP;
        [TabGroup("GOBLIN")] public int GoblinDamage;
        [TabGroup("GOBLIN")] public int GoblinGold;
        [TabGroup("GOBLIN")] public float GoblinSpeed;
    }
}
