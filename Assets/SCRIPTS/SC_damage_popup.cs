using UnityEngine;
using TMPro;

public class SC_damage_popup : MonoBehaviour
{
    public TextMeshPro text;

    void Start()
    {
        Invoke("Destro_y", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Destro_y()
    {
        Destroy(gameObject);
    }
}
