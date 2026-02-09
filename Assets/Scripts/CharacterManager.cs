using System.Collections;
using UnityEngine;

public enum CharacterState
{
    Idle,
    Walking,
    Punch,
    Kick,
    Hit,
    Dead,
}

public class CharacterManager : MonoBehaviour
{
    CharacterState currentState;
    AnimationController animationController;
    CharacterMovement characterMovement;
    CharacterAttack characterAttack;
    CharacterHealth characterHealth;
    CharacterInterface characterInterface;
    bool isInStoppingAnimation = false;
    float invincibilityTime = 0.3f;
    bool isInvincible = false;
    private void Awake()
    {
        animationController = GetComponent<AnimationController>();
        characterMovement = GetComponent<CharacterMovement>();
        characterAttack = GetComponent<CharacterAttack>();
        characterHealth = GetComponent<CharacterHealth>();
        characterInterface = GetComponent<CharacterInterface>();
    }

    public void UpdateCharacterState(CharacterState newState)
    {
        currentState = newState;
        SetIsStoppingAnimation(IsStoppingState(newState));
        animationController.SetState(currentState);
    }

    public void walk(Vector3 direction)
    {
        if (isInStoppingAnimation)
        {
            return;
        }

        characterMovement.walk(direction);
    }

    bool IsStoppingState(CharacterState state)
    {
        CharacterState[] stoppingStates = { CharacterState.Punch, CharacterState.Kick, CharacterState.Hit, CharacterState.Dead };

        for (int i = 0; i < stoppingStates.Length; i++)
        {
            if (stoppingStates[i] == state)
            {
                return true;
            }
        }

        return false;
    }

    public bool Attack(CharacterState attackState)
    {
        if (isInStoppingAnimation || !characterAttack.attack())
        {
            return false;
        }

        UpdateCharacterState(attackState);
        return true;

    }

    void SetIsStoppingAnimation(bool isStopping)
    {
        isInStoppingAnimation = isStopping;
    }

    public void ReturnToIdle()
    {
        UpdateCharacterState(CharacterState.Idle);
    }

    public void GetHit(float damage, CharacterInterface attacker)
    {
        if (isInvincible)
        {
            return;
        }
        

        StartCoroutine(invincibilityRoutine());
        float healthPercentage = characterHealth.TakeDamage(damage);
        characterInterface.UpdateHealth(healthPercentage);

        if (healthPercentage <= 0)
        {
            attacker.GetKill(characterInterface);
            UpdateCharacterState(CharacterState.Dead);
        } else
        {
            UpdateCharacterState(CharacterState.Hit);
        }

        return;
    }

    public void HealToFull()
    {
        characterHealth.HealToFull();
        characterInterface.UpdateHealth(characterHealth.HealthPercentage());
    }


    private IEnumerator invincibilityRoutine()
    {
        isInvincible = true;
        
        yield return new WaitForSeconds(invincibilityTime);
        
        isInvincible = false;
    }

    public bool IsTurnedRight()
    {
        return characterMovement.IsTurnedRight();
    }

    public void Turn(bool turnRight)
    {
        characterMovement.Turn(turnRight);
    }
}
