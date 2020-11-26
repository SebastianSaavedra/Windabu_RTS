using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetClipPitch : StateMachineBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] audioContaainer clip;
    [SerializeField] float timer;
    float time;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        source = animator.GetComponentInParent<AudioSource>();
        clip = animator.GetComponentInParent<audioContaainer>();
        timer = stateInfo.length;
        source.clip = clip.audios[Random.Range(0,clip.audios.Length)];
        source.pitch = Random.Range(0.8f, 1.2f);
        source.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;
        if (timer <= 0) 
        {
            source.clip = clip.audios[Random.Range(0, clip.audios.Length)];
            source.pitch = Random.Range(0.8f, 1.2f);
            source.Play();
            timer = stateInfo.length;
        }
      
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        source.Stop();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
