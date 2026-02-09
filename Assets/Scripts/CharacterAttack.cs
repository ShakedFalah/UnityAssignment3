using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public float attackRate = 1f;
    public float damage = 10f;
    public string[] attackableTags = { "Enemy" };
    public Collider2D damageCollider;
    bool isOnCooldown = false;

    public bool attack()
    {
        if (isOnCooldown)
        {
            return false;
        }
        
        StartCoroutine(attackCooldownRoutine());
        return true;
    }

    private bool DealDamage(CharacterManager enemy)
    {
        if (!attackableTags.Contains(enemy.tag)) 
        {
            return false;
        }

        enemy.GetHit(damage, GetComponent<CharacterInterface>());

        return true;
    }

    IEnumerator attackCooldownRoutine()
    {
        isOnCooldown = true;

        yield return new WaitForSeconds(attackRate);
        
        isOnCooldown = false;
    }

    private void Update()
    {
        List<Collider2D> overlapping = new List<Collider2D>();
        damageCollider.Overlap(overlapping);

        foreach (Collider2D collision in overlapping)
        {
            if (collision.CompareTag("HitBox"))
            {
                GameObject enemy = collision.transform.parent.gameObject;
                DealDamage(enemy.GetComponent<CharacterManager>());
            }
        }
    }
}
