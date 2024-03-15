using System;
using Game.Scripts.Events;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class EventManager : MonoBehaviour
    {
        public event EventHandler<PathFinderEventArgs> OnChangePath;
        public event EventHandler<GoldEventArgs> GoldChanger;
        public event EventHandler<HPEventArgs> TakeDamage;
        public event EventHandler<GameLoseEventArgs> GameLose;

        public void TriggerPathFindEvent()
        {
            OnChangePath?.Invoke(this, new PathFinderEventArgs());
        }
        
        
        public void GoldEvent(int amount)
        {
            GoldChanger?.Invoke(this, new GoldEventArgs(amount));
        }

        public void HPEvent()
        {
            TakeDamage?.Invoke(this, new HPEventArgs());
        }
        
        public void GameLoseEvent()
        {
            GameLose?.Invoke(this, new GameLoseEventArgs());
        }
       
    }
}
