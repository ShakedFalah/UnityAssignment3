using System.Collections.Generic;
using UnityEngine;

public enum ShootyEnemyState
{
    Patrol,
    ReturnToPatrol,
    FirstShot,
    DoubleShot,
}

public class ShootyEnemyController : MonoBehaviour ,CharacterInterface
{
    public CircleCollider2D detectionArea;
    public float idleDistance = 0.001f;
    public float scoreValue = 20f;
    private bool isPatrollingToEnd;
    HUD hud;
    GameObject player;
    CharacterManager characterManager;
    ShootyEnemyState enemyState;
    Vector3 enemyPosition;
    Vector3 playerPosition;

    [SerializeField]
    private Vector3 patrolStart;
    
    [SerializeField]
    private Vector3 patrolEnd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        hud = GameObject.FindWithTag("Hud").GetComponent<HUD>();
        player = GameObject.FindWithTag("Player");
        characterManager = GetComponent<CharacterManager>();
        enemyState = ShootyEnemyState.Patrol;
        isPatrollingToEnd = true;
    }

    // Update is called once per frame
    void Update()
    {
        enemyPosition = transform.position;
        if (enemyState == ShootyEnemyState.Patrol)
        {
            if (DetectPlayer())
            {
                characterManager.Turn(IsPlayerToTheRight());
                enemyState = ShootyEnemyState.FirstShot;
            }
            else
            {
                if (DidReachPatrolPoint())
                {
                    isPatrollingToEnd = !isPatrollingToEnd;
                }
            }
        }

        Act();
    }

    public void Die()
    {
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        Animator animator = GetComponent<Animator>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }

        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }

        animator.enabled = false;
    }

    public void UpdateHealth(float healthPercentage)
    {
        hud.UpdateEnemyHealthBar(healthPercentage);
    }

    bool DetectPlayer()
    {
        List<Collider2D> overlapResults = new List<Collider2D>();

        detectionArea.Overlap(overlapResults);

        foreach (Collider2D overlapping in overlapResults)
        {
            if (overlapping.CompareTag("PlayerShadow"))
            {
                return true;
            }
        }

        return false;
    }

    void Act()
    {
        switch (enemyState)
        {
            case ShootyEnemyState.FirstShot:
                characterManager.Attack(CharacterState.Punch);
                break;
            case ShootyEnemyState.Patrol:
                Vector3 patrolPosition = isPatrollingToEnd ? patrolEnd : patrolStart;
                characterManager.walk(patrolPosition - enemyPosition);
                break;
            case ShootyEnemyState.ReturnToPatrol:
                ReturnToPosition();
                break;
        }
    }

    void ReturnToPosition()
    {
        if (Vector3.Distance(enemyPosition, patrolStart) < idleDistance)
        {
            transform.position = patrolStart;
            ReturnToIdle();
        } else
        {
            characterManager.walk(patrolStart - enemyPosition);
        }
    }

    void ReturnToIdle()
    {
        characterManager.ReturnToIdle();
        enemyState = ShootyEnemyState.Patrol;
    }

    public void GetKill(CharacterInterface character)
    {
        return;
    }

    public float ScoreValue()
    {
        return scoreValue;
    }
    
    private bool IsPlayerToTheRight()
    {
        return transform.position.x < player.transform.position.x;
    }

    private bool DidReachPatrolPoint()
    {
        if (isPatrollingToEnd)
        {
            if (Vector3.Distance(enemyPosition, patrolStart) < idleDistance)
            {
                return true;
            }
        } else if (Vector3.Distance(enemyPosition, patrolEnd) < idleDistance)
        {
            return true;
        }
        
        return false;
    }
}
