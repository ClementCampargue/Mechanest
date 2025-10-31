using UnityEngine;

public class SC_enemy_spawn : MonoBehaviour
{
    public GameObject enemy;
    public float cycle;
    void Start()
    {
        InvokeRepeating("delay", cycle, cycle);
    }

    void delay()
    {
        Instantiate(enemy, transform.position,Quaternion.identity);
    }
}
