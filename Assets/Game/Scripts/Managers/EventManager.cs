using System;
using Game.Scripts.Events;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class EventManager : MonoBehaviour
    {
        public event EventHandler<PathFinderEventArgs> OnChangePath;

        public void TriggerPathFindEvent()
        {
            OnChangePath?.Invoke(this, new PathFinderEventArgs());
        }
    }
}
