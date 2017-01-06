using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelePatrolState : ISkeleState
{
    private Skele skele;
    private float patrolTimer;
    private float patrolDuration;

    public void Enter(Skele skele)
    {
        patrolDuration = UnityEngine.Random.Range(1, 10);
        this.skele = skele;
    }

    public void Execute()
    {
        Patrol();
        skele.Move();
        if (skele.Target != null && skele.InMeleeRange) { skele.ChangeState(new SkeleMeleeState()); }
    }

    public void Exit()
    {
        return;
    }

    public void OnTriggerEnter(Collider2D collider)
    {
        if (collider.tag == "Player") { skele.Target = Player.Instance.gameObject; }
        if (collider.tag == "Edge") { skele.ChangeDirection(); }
    }

    private void Patrol()
    {
        patrolTimer += Time.deltaTime;
        if (patrolTimer >= patrolDuration) { skele.ChangeState(new SkelePatrolState()); }
    }

  
}
