using UnityEngine;

public class SC_random_spawn : MonoBehaviour
{

    public GameObject chest;
    public int chest_proba;

    public GameObject bone_pile;
    public int bone_proba;

    public float area_size;
    public float spawn_frequency;
    void Start()
    {
        InvokeRepeating("spawn_", spawn_frequency, spawn_frequency);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawn_()
    {
        if (GameObject.Find("GAME_MASTER").GetComponent<SC_game_master>().fighting)
        {
            int rdm_chest = Random.Range(0, 100 / chest_proba);
            int rdm_bone = Random.Range(0, 100 / bone_proba);
            float rdm1 = Random.Range(-area_size, area_size);
            float rdm2 = Random.Range(-area_size, area_size);
            if (rdm_chest == 1)
            {
                Instantiate(chest, transform.position + new Vector3(rdm1, 0, rdm2), Quaternion.identity);
            }
            if (rdm_bone == 1)
            {
                Instantiate(bone_pile, transform.position + new Vector3(rdm1, 0, rdm2), Quaternion.identity);
            }
        }

    }
}
