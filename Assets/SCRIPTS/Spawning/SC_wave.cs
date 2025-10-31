using System.Collections.Generic;
using UnityEngine;

public class SC_wave : MonoBehaviour
{
    public List<GameObject> waves = new List<GameObject>();
    public float frequency;
    private int index;
    void Start()
    {
        InvokeRepeating("spawn_wave", 0, frequency);
        GameObject.Find("GAME_MASTER").GetComponent<SC_game_master>().begin_wave();

    }


    void spawn_wave()
    {
        if (index >= waves.Count)
        {
            GameObject.Find("GAME_MASTER").GetComponent<SC_game_master>().end_wave();
            Destroy(gameObject);
        }

        waves[index].SetActive(true);
        index++;

    }

    void Update()
    {
        
    }
}
