using UnityEngine;
using UnityEngine.InputSystem;

public class ZoneDepot : MonoBehaviour
{
    public int id = 1;
    public AudioClip successSound;
    public AudioClip errorSound;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                OnZoneClicked();
            }
        }
    }

    void OnZoneClicked()
    {
        if (ObjetCliquable.selectedAnimal != null)
        {
            ObjetCliquable animal = ObjetCliquable.selectedAnimal.GetComponent<ObjetCliquable>();

            if (animal != null && animal.id == id)
            {
                Debug.Log("Zone " + id + " cliquée: " + gameObject.name);
                Debug.Log("Animal ID " + animal.id + " disparu: " + ObjetCliquable.selectedAnimal.name);
                
                PlaySound(successSound);

                Destroy(ObjetCliquable.selectedAnimal);
                ObjetCliquable.selectedAnimal = null;

                Destroy(gameObject);
            }
            else
            {
                Debug.Log("IDs ne correspondent pas! Animal ID " + animal.id + " vs Zone ID " + id);
                PlaySound(errorSound);
            }
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
    }
}