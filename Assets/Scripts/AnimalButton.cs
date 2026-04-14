using UnityEngine;
using UnityEngine.InputSystem;

public class ObjetCliquable : MonoBehaviour
{
    public int id = 1;
    public static GameObject selectedAnimal;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                OnClicked();
            }
        }
    }

    void OnClicked()
    {
        if (selectedAnimal != null && selectedAnimal != gameObject)
        {
            ObjetCliquable previousAnimal = selectedAnimal.GetComponent<ObjetCliquable>();
            if (previousAnimal != null)
            {
                previousAnimal.RemoveGlow();
            }
        }

        selectedAnimal = gameObject;
        AddGlow();
        Debug.Log("Animal sélectionné (ID " + id + "): " + gameObject.name);
    }

    void AddGlow()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(1f, 1f, 0.5f);
        }
    }

    void RemoveGlow()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }
}