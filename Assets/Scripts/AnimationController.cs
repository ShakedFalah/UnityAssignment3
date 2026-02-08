using Unity.VisualScripting;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    string walking = "Walking";
    string hit = "Hit";
    string punch = "Punch";
    string kick = "Kick";
    string dead = "Die";
    Animator animator;
    CharacterManager characterManager;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterManager = GetComponent<CharacterManager>();
    }
    public void SetState(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Idle:
                animator.SetBool(walking, false);
                break;
            case CharacterState.Walking:
                animator.SetBool(walking, true);
                break;
            case CharacterState.Hit:
                animator.SetTrigger(hit);
                break;
            case CharacterState.Punch:
                animator.SetTrigger(punch);
                break;
            case CharacterState.Kick:
                animator.SetTrigger(kick);
                break;
            case CharacterState.Dead:
                animator.SetTrigger(dead);
                break;
        }
    }

    public void AnimationEnded()
    {
        characterManager.ReturnToIdle();
    }
}
