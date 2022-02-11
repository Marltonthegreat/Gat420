using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(StateAgent owner, string name) : base(owner, name)
    {

    }

    public override void OnEnter()
    {
        owner.movement.Stop();
        owner.animator.SetTrigger("attack");
        Debug.Log(name + " enter");
    }

    public override void OnExit()
    {
        Debug.Log(name + " exit");
    }

    public override void OnUpdate()
    {
        owner.animator.SetTrigger("attack");
        owner.timer.value = 2;
        Debug.Log(name + " update");
    }
}
