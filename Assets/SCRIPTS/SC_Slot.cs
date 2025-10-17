using UnityEngine;
using UnityEngine.UI;

public class SC_Slot : MonoBehaviour
{
    public float Invoquation_speed;
    public int Stamina_used;
    public GameObject Minion;

    public Image fill;

    private Transform player;

    void Start()
    {
        InvokeRepeating("spawn", 0, Invoquation_speed);

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
    }


    void spawn()
    {
        Instantiate(Minion, new Vector3(player.position.x + Random.Range(-1, 1), player.position.y, player.position.z+ Random.Range(-1, 1)), Quaternion.identity);
    }
}

