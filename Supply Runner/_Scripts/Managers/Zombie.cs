using System.Collections;
using System.Collections.Generic;


public class Zombie : PoolableObject
{
    public ZombieMovement Movement;
    public UnityEngine.AI.NavMeshAgent Agent;
    public ZombieDrop Drop;

    public override void OnDisable(){
        base.OnDisable();
        // Movement.ResetZombie();
        Agent.enabled = false;
    }
}
