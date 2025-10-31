using UnityEngine;
using UnityEngine.UI;

public class SC_Spawn_units : MonoBehaviour
{
    public float Invoquation_speed;
    public int Spawn_amount;
    public GameObject Minion;
    public int max_units;

    private SC_game_master gm;
    private int spw_count;

    private float timer = 0f;
    void Start()
    {
        gm = GameObject.Find("GAME_MASTER").GetComponent<SC_game_master>();

    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= Invoquation_speed)
        {
            timer -= Invoquation_speed;
            while (spw_count <Spawn_amount)
            {
                spw_count++;
                spawn();
            }
            spw_count = 0;

        }
    }


    void spawn()
    {
        if (gm.minions.Count < max_units && gm.fighting)
        {
            Instantiate(Minion, new Vector3(transform.position.x + Random.Range(-1f, 1f), transform.position.y, transform.position.z + Random.Range(-1f, 1f)), Quaternion.identity);
        }
    }
}

