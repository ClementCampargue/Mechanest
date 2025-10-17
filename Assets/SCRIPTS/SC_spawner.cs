using UnityEngine;

public class SC_spawner : MonoBehaviour
{
    public float interval = 1;
    public GameObject enemy;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            DoAction();
            timer = 0f;
        }
    }

    void DoAction()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
