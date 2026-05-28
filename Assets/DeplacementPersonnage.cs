using UnityEngine;
using UnityEngine.InputSystem;

public class DeplacementPersonnage : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    BoxCollider2D boxCollider;

    [Header("Actions du personnage")]
    public InputAction actionMarche;
    public InputAction actionSaut;

    [Header("Déplacement horizontal")]
    public float vitesse = 6f;

    [Header("Saut")]
    public float forceSaut = 10f;
    public LayerMask masqueSol;
    public float groundCheckDistance = 0.15f;
    public float jumpCooldown = 1f;

    bool inputSaut;
    bool estAuSol;
    float jumpTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnEnable()
    {
        if (actionMarche != null)
            actionMarche.Enable();
        if (actionSaut != null)
            actionSaut.Enable();
    }

    void OnDisable()
    {
        if (actionMarche != null)
            actionMarche.Disable();
        if (actionSaut != null)
            actionSaut.Disable();
    }

    void Update()
    {
        float moveInput = ReadMoveInput();
        bool jumpPressed = ReadJumpInput();

        if (jumpTimer > 0f)
            jumpTimer -= Time.deltaTime;

        estAuSol = IsGrounded();

        if (jumpPressed && estAuSol && jumpTimer <= 0f)
        {
            inputSaut = true;
            jumpTimer = jumpCooldown;
        }

        UpdateOrientation(moveInput);
        UpdateAnimations(moveInput);
    }

    void FixedUpdate()
    {
        float moveInput = ReadMoveInput();
        rb.linearVelocity = new Vector2(moveInput * vitesse, rb.linearVelocity.y);

        if (inputSaut)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * forceSaut, ForceMode2D.Impulse);
            inputSaut = false;
        }
    }

    float ReadMoveInput()
    {
        if (actionMarche != null)
            return actionMarche.ReadValue<float>();

        if (Keyboard.current != null)
        {
            float value = 0f;
            if (Keyboard.current.aKey.isPressed) value -= 1f;
            if (Keyboard.current.dKey.isPressed) value += 1f;
            return value;
        }

        return 0f;
    }

    bool ReadJumpInput()
    {
        if (actionSaut != null && actionSaut.WasPressedThisFrame())
            return true;

        if (Keyboard.current != null)
            return Keyboard.current.spaceKey.wasPressedThisFrame;

        return false;
    }

    bool IsGrounded()
    {
        if (boxCollider == null)
            return false;

        Vector2 feetCenter = (Vector2)boxCollider.bounds.center + Vector2.down * (boxCollider.bounds.extents.y + groundCheckDistance * 0.5f);
        Vector2 feetSize = new Vector2(boxCollider.bounds.size.x * 0.9f, groundCheckDistance);
        Collider2D hit = Physics2D.OverlapBox(feetCenter, feetSize, 0f, masqueSol);
        return hit != null;
    }

    void UpdateOrientation(float moveInput)
    {
        if (sr == null)
            return;

        if (moveInput < 0)
            sr.flipX = true;
        else if (moveInput > 0)
            sr.flipX = false;
    }

    void UpdateAnimations(float moveInput)
    {
        if (anim == null)
            return;

        anim.SetFloat("vitesse", Mathf.Abs(moveInput));
        anim.SetBool("estDansLesAirs", !estAuSol);
    }

    void OnDrawGizmosSelected()
    {
        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider2D>();

        if (boxCollider == null)
            return;

        Vector2 feetCenter = (Vector2)boxCollider.bounds.center + Vector2.down * (boxCollider.bounds.extents.y + groundCheckDistance * 0.5f);
        Vector2 feetSize = new Vector2(boxCollider.bounds.size.x * 0.9f, groundCheckDistance);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(feetCenter, feetSize);
    }
}
