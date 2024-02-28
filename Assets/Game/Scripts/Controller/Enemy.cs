using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Scripts.Managers;
using Game.Scripts.Settings;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _health; 
    public int Health => _health;
    
    private int _damage;
    public int Damage => _damage;
    
    private int _gold;
    public int Gold => _gold;
    
    private float _speed;
    public float Speed => _speed;
    
    private EnemyType _enemyType;
    public EnemyType EnemyType => _enemyType;

    private EnemySituation _enemySituation;
    public EnemySituation EnemySituation => _enemySituation;
    
    [SerializeField] private GameObject[] _models;
    [SerializeField] private EnemySettings _enemySettings;
    
    private Animator _animator;
    private float _crossFade;

    private int _pathCount;
      
     
      public void Initialize(EnemyType type, int pathCount)
      {
          transform.SetParent(null);
          _pathCount = pathCount;
          SetEnemyType(type);
          ChooseModel();
          FindValues();
          SetEnemySituation(EnemySituation.Walk);
          _animator.speed = _enemySettings.GoblinAnimationSpeed;
          Movement();
      }
      
      private void OnEnable()
      {
          GameManager.Instance.EventManager.OnChangePath += FindPosition;
      }

      private void OnDisable()
      {
          GameManager.Instance.EventManager.OnChangePath -= FindPosition;
      }

      public void SetEnemyType(EnemyType type)
      {
          _enemyType = type;
      }

      public void SetEnemySituation(EnemySituation situation)
      {
          _enemySituation = situation;
          _animator.CrossFade(_enemySituation.ToString(), _crossFade);
      }

      private void ChooseModel()
      {
          for (int i = 0; i < _models.Length; i++)
          {
              if (_enemyType.ToString() == _models[i].name)
              {
                  _models[i].SetActive(true);
                  _animator = _models[i].GetComponent<Animator>();
              }
              else
              {
                  _models[i].SetActive(false);
              }
          }
      }

      private void FindValues()
      {
          if (_enemyType == EnemyType.Goblin)
          {
              SetValues(_enemySettings.GoblinHP, _enemySettings.GoblinDamage, _enemySettings.GoblinGold, _enemySettings.GoblinSpeed);
          }
      }

      private void SetValues(int health, int damage, int gold, float speed)
      {
          _health = health;
          _damage = damage;
          _gold = gold;
          _speed = speed;
      }


      private void FindPosition(object sender, EventArgs args)
      {
          float distance = Int32.MaxValue;
          int count = 0;

          for (int i = 0; i < GameManager.Instance.PathFinder.Paths[_pathCount].Path.Length; i++)
          {
              if (Vector3.Distance(transform.position,
                      GameManager.Instance.PathFinder.Paths[_pathCount].Path[i].transform.position) < distance)
              {
                  distance = Vector3.Distance(transform.position,
                      GameManager.Instance.PathFinder.Paths[_pathCount].Path[i].transform.position);
                  count = i;
              }
          }
          
          Movement(count);
      }
      
      private void Movement(int value = 0)
      {
          Vector3[] pathPoints = new Vector3[GameManager.Instance.PathFinder.Paths[_pathCount].Path.Length - value];

          for (int i = 0; i < pathPoints.Length; i++)
          {
              pathPoints[i] = GameManager.Instance.PathFinder.Paths[_pathCount].Path[i + value].transform.position;
          }

          transform.DOPath(pathPoints, _speed, PathType.CatmullRom).SetSpeedBased().SetLookAt(0.1f).SetEase(Ease.Linear).OnComplete(Attack);
      }
      
      public void Attack()
      {
        
      }

      public void TakeDamage(int damage, int count = 0, int extraDamage = 0)
      {
          _health -= damage;
          
          if (count == 0)
              return;
      }
}
