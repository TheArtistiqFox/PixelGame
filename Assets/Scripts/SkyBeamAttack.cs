using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBeamAttack : StateMachineBehaviour
{
    [SerializeField] private float _timeTilIdle = 2f;
    private float _timer = 0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<SkyBeam>().damageActive = true;
        _timer = 0f;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<SkyBeam>().damageActive = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeTilIdle)
        {
            animator.ResetTrigger("IsAttacking");
            animator.SetTrigger("IsIdle");
        }
    }
}
