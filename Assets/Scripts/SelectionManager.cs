using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameObject objetSelectionne;
    private bool estSelectionne = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectionnerObjet(GameObject obj)
    {
        objetSelectionne = obj;
        estSelectionne = true;
        Debug.Log("Objet sélectionné : " + obj.name);
    }

    public void PlacerObjet(GameObject zone)
    {
        if (!estSelectionne) return;

        if (objetSelectionne.CompareTag(zone.tag))
        {
            objetSelectionne.transform.position = zone.transform.position;
            Debug.Log("Bon placement");
        }
        else
        {
            Debug.Log("Mauvais placement");
        }

        objetSelectionne = null;
        estSelectionne = false;
    }
}