using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class log : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition; //enemy back to sleep when player is out of chase radius
    // Start is called before the first frame update
    public Animator anim;
    void Start()
    {
        currentState = EnemyState.idle;
        anim = GetComponent <Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance(){
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius){
            if(currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger){
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeState(EnemyState.walk);
            }
        }
    }

    private void ChangeState(EnemyState newState){
        if(currentState != newState){
            currentState = newState;
        }
    }
}
