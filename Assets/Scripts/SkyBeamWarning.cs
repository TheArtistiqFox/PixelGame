using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SkyBeamWarning : StateMachineBehaviour
{
    [SerializeField] private float _timeTilAttack = 2f;
    private float _timer = 0f;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<SkyBeam>().Show();
        _timer = 0f;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeTilAttack)
        {
            animator.ResetTrigger("IsWarning");
            animator.SetTrigger("IsAttacking");
        }
    }
}
