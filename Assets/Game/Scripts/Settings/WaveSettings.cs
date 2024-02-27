using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Settings
{
    [CreateAssetMenu(fileName = "Wave Settings", menuName = "ScriptableObject/WaveSettings")]
    public class WaveSettings : ScriptableObject
    {
        public WaveClass[] Waves;
    }

    [Serializable]
    public class WaveClass
    {
        public float SpawnTime;
        public WaveDoor[] WaveDoors;

    }
    
    [Serializable]
    public class WaveDoor
    {
        public EnemyType[] Types;
        public int DoorCount;
    }
    
}
