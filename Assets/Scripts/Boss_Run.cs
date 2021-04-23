using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{

    public float speed = 2.5f;
    public float jumpSpeed = 6;
    public float attackRange = 3f;
    private float spreadshotTimer = 0f;
    public float spreadshotFrequency = 3f;

    Transform player;
    Rigidbody2D rb;
    Boss boss;
    CapsuleCollider2D bossCollider;
    public LayerMask ground;

    private float _timeTilNextAttack = 0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timeTilNextAttack = GetNextAttackTime();
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        bossCollider = animator.GetComponent<CapsuleCollider2D>();
    }

    private float GetNextAttackTime()
    {
        return Random.Range(1f, 2.5f);
    }
    
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timeTilNextAttack -= Time.deltaTime;
        if (_timeTilNextAttack <= 0f)
        {
            // DO MY ATTACK
            _timeTilNextAttack = GetNextAttackTime();
        }
        
        boss.LookAtPlayer();

        spreadshotTimer += Time.deltaTime;
        if (spreadshotTimer >= spreadshotFrequency)
        {
            boss.GetComponent<Boss_Weapon>().SpreadShot();
            spreadshotTimer = 0f;
        }

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Physics2D.IsTouching(boss.GetComponent<Collider2D>(), player.GetComponent<Collider2D>()))
        {
            animator.SetTrigger("Attack");
        }
        else if (ground != null) 
        {
            if (Physics2D.Raycast(bossCollider.transform.position, Vector2.right, bossCollider.size.x + 1f, ground))
            {
                boss.Jump();
            }
            else if (Physics2D.Raycast(bossCollider.transform.position, Vector2.left, bossCollider.size.x + 1f, ground))
            {
                boss.Jump();
            }
        }

        //if (Vector2.Distance(player.position, rb.position) <= attackRange)
        //{
        //    //attack
        //    animator.SetTrigger("Attack");
        //}
    }
    
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

}
