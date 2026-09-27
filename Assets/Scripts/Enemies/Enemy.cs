using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private GameObject[] _skins;

    [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }

    public Transform PlayerTransform { get; private set; }
    public EnemyStateMachine StateMachine { get; private set; }

    public void TakeDamage(int damage)
    {

    }

    public void SetPlayerTransform(Transform transform)
    {
        PlayerTransform = transform;
    }

    internal void ChooseSkin()
    {
        _skins[UnityEngine.Random.Range(0, _skins.Length)].SetActive(true);
    }
}
