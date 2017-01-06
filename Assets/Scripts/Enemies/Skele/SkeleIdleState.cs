using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleIdleState : ISkeleState
{
    private Skele skele;
    private float idleTimer;
    private float idleDuration;

    public void Enter(Skele skele)
    {
        // using Random Range so not every SINGLE ENEMY patrols at the same time
        idleDuration = UnityEngine.Random.Range(1, 10);
        this.skele = skele;
    }

    public void Execute()
    {
        Idle();
        if (skele.Target != null)
        {
            skele.ChangeState(new SkelePatrolState());
        }
    }

    public void Exit()
    {
        return;
    }

    public void OnTriggerEnter(Collider2D collider)
    {
        return;
    }

    private void Idle()
    {
        skele.MyAnimator.SetFloat("speed", 0);
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            skele.ChangeState(new SkelePatrolState());
        }
    }
}
