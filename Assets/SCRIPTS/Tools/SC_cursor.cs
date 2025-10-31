using TMPro;
using UnityEngine;

public class SC_cursor : MonoBehaviour
{
    private Camera _camera;
    public float speed = 5f;   // Vitesse du déplacement
    public LayerMask groundLayer; // Le calque du sol
    public GameObject effect;
    private Vector3 targetPosition;
    public GameObject collider;
    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            targetPosition = hit.point;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
        }

        if (Input.GetMouseButton(1))
        {
            if (!collider.activeInHierarchy)
            {
                collider.SetActive(true);
            }
        }
        else if(collider.activeInHierarchy)
        {
            collider.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Minion")
        {
            other.GetComponent<SC_minion>().go_to_player();
        }
    }
}
