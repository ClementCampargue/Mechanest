using UnityEngine;

public class SC_destroy : MonoBehaviour
{
    public float delay = 1;
    public bool disable;
    void OnEnable()
    {
        Invoke("Destroy", delay);
    }

    void Destroy()
    {
        if (disable)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
