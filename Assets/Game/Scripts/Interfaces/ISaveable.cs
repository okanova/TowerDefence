using UnityEngine;

namespace Game.Scripts.Interfaces
{
    public interface ISaveable
    {
        void SaveData();
        void LoadData();
        void ResetData();
    }
}
