using Unity.Burst.Intrinsics;
using UnityEngine;

public class SC_loot : MonoBehaviour
{
    private Transform player;
    public float distance;
    public float speed;
    public GameObject effect_get;

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.position, transform.position) < distance)
        {
            if (Vector3.Distance(player.position, transform.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                Instantiate(effect_get, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

    }
}
