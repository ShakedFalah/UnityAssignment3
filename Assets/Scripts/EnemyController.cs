using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;
public enum EnemyState
{
    Idle,
    Attack,
    ReturningToPosition,
    Chase,
}

public class EnemyController : MonoBehaviour ,CharacterInterface
{
    public CircleCollider2D detectionArea;
    public float attackDistance = 0.5f;
    public float idleDistance = 0.001f;
    public float scoreValue = 10f;
    HUD hud;
    GameObject player;
    CharacterManager characterManager;
    Vector3 startingPosition;
    EnemyState enemyState;
    Vector3 enemyPosition;
    Vector3 playerPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        hud = GameObject.FindWithTag("Hud").GetComponent<HUD>();
        player = GameObject.FindWithTag("Player");
        characterManager = GetComponent<CharacterManager>();
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        enemyPosition = transform.position;

        playerPosition = player.transform.position;

        if (Vector3.Distance(enemyPosition, playerPosition) < attackDistance)
        {
            enemyState = EnemyState.Attack;
        }
        else if (DetectPlayer())
        {
            enemyState = EnemyState.Chase;
        }
        else if (enemyState != EnemyState.Idle)
        {
            enemyState = EnemyState.ReturningToPosition;
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
            case EnemyState.Idle:
                break;
            case EnemyState.Attack:
                characterManager.Attack(CharacterState.Punch);
                break;
            case EnemyState.Chase:
                characterManager.walk(playerPosition - enemyPosition);
                break;
            case EnemyState.ReturningToPosition:
                ReturnToPosition();
                break;
        }
    }

    void ReturnToPosition()
    {
        if (Vector3.Distance(enemyPosition, startingPosition) < idleDistance)
        {
            transform.position = startingPosition;
            ReturnToIdle();
        } else
        {
            characterManager.walk(startingPosition - enemyPosition);
        }

        
    }

    void ReturnToIdle()
    {
        characterManager.ReturnToIdle();
        enemyState = EnemyState.Idle;
    }

    public void GetKill(CharacterInterface character)
    {
        return;
    }

    public float ScoreValue()
    {
        return scoreValue;
    }
}
