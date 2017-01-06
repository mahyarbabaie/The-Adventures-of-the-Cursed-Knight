using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkeleState
{
    void Execute();
    void Enter(Skele skele);
    void Exit();
    void OnTriggerEnter(Collider2D collider);
}
