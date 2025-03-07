using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Collider2D circleCollider;

    [SerializeField] float mouseFollowSpeed = 5f;

    Vector2 targetPosition;

    [SerializeField] float phaseDuration = 1f;
    public int phaseThroughTokens;
    bool isPhasing;
    float phaseTimer;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        GetMouseInput();
        GetTargetPosition();
        HandlePhasing();
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

    void GetMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryConsumePhaseToken();
        }
    }

    void TryConsumePhaseToken()
    {
        if (isPhasing) return;
        if (phaseThroughTokens <= 0) return;
        phaseThroughTokens--;
        isPhasing = true;
    }

    void HandlePhasing()
    {
        if (!isPhasing)
        {
            phaseTimer = 0;
            SetTransparency(1f);
            circleCollider.isTrigger = false;
            return;
        }

        phaseTimer += Time.deltaTime;
        SetTransparency(0.5f);
        circleCollider.isTrigger = true;

        if (phaseTimer > phaseDuration)
        {
            isPhasing = false;
        }
    }

    public void AddPhaseToken(int count)
    {
        phaseThroughTokens += count;
    }

    void SetTransparency(float transparency)
    {
        Color currentColor = spriteRenderer.material.color;
        currentColor.a = transparency;
        spriteRenderer.material.color = currentColor;
    }
}
