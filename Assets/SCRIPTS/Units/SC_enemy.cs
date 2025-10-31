using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SC_enemy : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Animator anim;
    [SerializeField] private MeshRenderer renderer;
    private float timer;
    private Transform player;

    [Header("Movement")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float max_distance_from_aim;
    private Transform aim;

    [Header("Combat")]
    private bool fighting;

    [SerializeField] private int health;
    private float maxhealth;
    [SerializeField] private int attack;
    [SerializeField] private float attack_speed;
    [SerializeField] private AudioSource damage_audio;
    [SerializeField] private GameObject death_fx;
    [SerializeField] private GameObject damage_popup;






    void Start()
    {

        maxhealth = health;
        player = GameObject.Find("Player").transform;

        InvokeRepeating("update_", 0, 1f);


    }
    void update_()
    {
        aim = player;

        Collider[] hits = Physics.OverlapSphere(transform.position, 25f, LayerMask.GetMask("Minion") + LayerMask.GetMask("Player"));
        float minDistSq = float.MaxValue;

        foreach (var hit in hits)
        {
            float distSq = (hit.transform.position - transform.position).sqrMagnitude;
            if (distSq < minDistSq)
            {
                minDistSq = distSq;
                aim = hit.transform;
            }
        }

        Vector3 direction = aim.position - transform.position;
        direction.y = 0f;
        if (direction.sqrMagnitude < 0.0001f) return;

        transform.rotation = Quaternion.LookRotation(direction);

    }
    void FixedUpdate()
    {
        Vector3 toAim = aim.position - transform.position;
        float sqrDistance = toAim.sqrMagnitude;

        if (sqrDistance > max_distance_from_aim * max_distance_from_aim)
        {

            if (!anim.GetBool("moving"))
            {
                anim.SetBool("moving", true);
            }
            Vector3 direction = (aim.position - rb.position).normalized;
            Vector3 newPosition = rb.position + direction * speed * Time.fixedDeltaTime;
            rb.linearVelocity += (direction * speed) * Time.fixedDeltaTime;

        }
        else
        {
            if (anim.GetBool("moving"))
            {
                anim.SetBool("moving", false);
            }
            timer += Time.fixedDeltaTime;

            if (timer >= attack_speed)
            {
                Attack();
                timer = 0f;
            }
        }
    }


    void Attack()
    {
        anim.SetTrigger("attack");
    }
    public void takedamage(int dmg)
    {
        damage_audio.pitch = Random.Range(0.9f, 1.1f);

        damage_audio.Play();
        float rdm = Random.Range(-0.1f, 0.1f);
        SC_damage_popup tmp = Instantiate(damage_popup, transform.position + new Vector3(rdm, rdm, rdm) , Quaternion.identity).GetComponent<SC_damage_popup>(); 
        tmp.Setup(dmg);
        health = health - dmg;
        if (health > 0)
        {
            Invoke("damage_delay", 0.1f);
            renderer.material.SetFloat("_Damage_amount", maxhealth / health);
            renderer.material.SetFloat("_Damage_glow", 1);

        }
        else
        {
            die();
        }
    }

    void damage_delay()
    {
        renderer.material.SetFloat("_Damage_glow", 0);
    }

    void die()
    {
        Instantiate(death_fx, transform.position, Quaternion.identity);

        Collider[] hits = Physics.OverlapSphere(transform.position, 5f, LayerMask.GetMask("Minion"));

        foreach (var hit in hits)
        {
            hit.GetComponent<SC_minion>().go_to_player();
        }



        Destroy(gameObject);
    }
}
