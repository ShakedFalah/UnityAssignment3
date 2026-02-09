using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public float lifeTime = 2f;
    public CharacterInterface creator { private get; set; }
    public List<string> hittableTags;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LifeTime());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 test = transform.rotation * Vector3.right;
        transform.position += transform.rotation * Vector3.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            GameObject enemy = collision.transform.parent.gameObject;
            if (hittableTags.Contains(enemy.tag))
            {
                enemy.GetComponent<CharacterManager>().GetHit(damage, creator);
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
