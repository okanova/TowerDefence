using System;
using Game.Scripts.Settings;
using UniRx;
using UnityEngine;

namespace Game.Scripts.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        public WaveSettings WaveSettings;
        public int WaveCount;

        private IDisposable _spawnDisp;

        public void Initialize()
        {
           CreateEnemies();
        }

        private void CreateEnemies()
        {
            int count = 0;
            
            _spawnDisp?.Dispose();
            _spawnDisp = Observable.Interval(TimeSpan.FromSeconds(WaveSettings.Waves[WaveCount].SpawnTime), Scheduler.MainThreadIgnoreTimeScale)
                .UniSubscribe(_ =>
                {
                    for (int i = 0; i < WaveSettings.Waves[WaveCount].WaveDoors.Length; i++)
                    {
                        if (count >= WaveSettings.Waves[WaveCount].WaveDoors[i].Types.Length)
                            continue;
                        
                        Enemy enemy = GameManager.Instance.ObjectPooling.GetObject(ObjectPoolType.Enemy,
                            GameManager.Instance.PathFinder.Paths[WaveSettings.Waves[WaveCount].WaveDoors[i].DoorCount].
                                Enter.transform).GetComponent<Enemy>();

                        enemy.Initialize(WaveSettings.Waves[WaveCount].WaveDoors[i].Types[count], i);
                    }
                    
                    count++;
                    int doneCount = 0;

                    for (int i = 0; i < WaveSettings.Waves[WaveCount].WaveDoors.Length; i++)
                    {
                        if (count >= WaveSettings.Waves[WaveCount].WaveDoors[i].Types.Length)
                            doneCount++;
                    }

                    if (doneCount == WaveSettings.Waves[WaveCount].WaveDoors.Length)
                    {
                        WaveCount++;
                        _spawnDisp?.Dispose();

                        if (WaveCount == WaveSettings.Waves.Length)
                        {
                            //done
                        }
                    }
                   
                });
        }
    }
}
