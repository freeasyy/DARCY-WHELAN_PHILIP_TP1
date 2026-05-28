using UnityEngine;

public class rotatePotion : MonoBehaviour
{
    public GameObject cible;
    public float angleFin;
    public float angleDepart;
    float distanceDepart;
    float distanceFin;

    void Start()
    {
        distanceDepart = Vector2.Distance(transform.position, cible.transform.position);
    }


    void Update()
    {

        float distanceActuelle = Vector2.Distance(transform.position, cible.transform.position);

        float pourcentage = (distanceActuelle - distanceDepart) / (distanceFin - distanceDepart);
        pourcentage = Mathf.Clamp(pourcentage, 0, 1); //Viens s'assurer que l'écart est entre 0 et 1;

        float angle = angleDepart + (angleFin - angleDepart) * pourcentage;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
