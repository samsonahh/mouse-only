using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigidBody;

    [SerializeField] float mouseFollowSpeed = 5f;

    Vector2 targetPosition;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetTargetPosition();
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(Vector2.MoveTowards(transform.position, targetPosition, mouseFollowSpeed * Time.fixedDeltaTime));
    }

    void GetTargetPosition()
    {
        Vector2 mousePosition = Input.mousePosition;
        targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
