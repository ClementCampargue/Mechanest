using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SC_loot : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    [SerializeField] private ParticleSystem effect_get;
    private SC_XP_manager game_master;
    void Start()
    {
        player = GameObject.Find("Player").transform;
        game_master = GameObject.Find("GAME_MASTER").GetComponent<SC_XP_manager>();
    }

    void Update()
    {
        Vector3 toAim = player.position - transform.position;
        float sqrDistance = toAim.sqrMagnitude;

        if (sqrDistance < distance)
        {
            if (sqrDistance > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                game_master.current_xp++;
                effect_get.Play();
                effect_get.transform.parent = null;
                Destroy(gameObject);
            }
        }

    }


}
