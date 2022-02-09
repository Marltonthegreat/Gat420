using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(StateAgent owner, string name) : base(owner, name) { }

    public override void OnEnter()
    {
        Debug.Log(name + " enter");
        //owner.movement.Stop();
        owner.timer.value = 2;
    }

    public override void OnUpdate()
    {
        Debug.Log(name + " update");
    }

    public override void OnExit()
    {
        Debug.Log(name + " exit");
    }
}
