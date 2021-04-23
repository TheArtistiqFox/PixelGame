using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBeamIdle : StateMachineBehaviour
{
    [SerializeField] private float _minTimeTilActivation = 2f;
    [SerializeField] private float _maxTimeTilActivation = 10f;
    private float _timer = 0f;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<SkyBeam>().Hide();

        _timer = Random.Range(_minTimeTilActivation, _maxTimeTilActivation);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            animator.ResetTrigger("IsIdle");
            animator.SetTrigger("IsWarning");
        }
    }
}
