using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 1.0f;
    private GameObject navAreaObject;
    private Collider2D navAreaCollider;
    private Animator animator;
    private Vector3 originalScale;
    private void Awake()
    {
        navAreaObject = GameObject.FindGameObjectWithTag("NavArea");
        navAreaCollider = navAreaObject.GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;
    }

    public void walk(Vector3 direction)
    {
        Vector3 position = transform.position;
        Vector3 normalizedDirection = Vector3.Normalize(direction) * speed * Time.deltaTime;
        Vector3 deltaX = normalizedDirection.x *  Vector3.right;
        Vector3 deltaY = normalizedDirection.y * Vector3.up;

        if (navAreaCollider.OverlapPoint(position + deltaX))
        {
            transform.position += deltaX;
        }

        if (navAreaCollider.OverlapPoint(position + deltaY))
        {
            transform.position += deltaY;
        }
        if (direction != Vector3.zero)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        if (deltaX.x != 0)
        {

            float sign = deltaX.x < 0 ? -1 : 1;

            transform.localScale = new Vector3(
                sign * originalScale.x,
                originalScale.y,
                originalScale.z);
        }
    }
}
