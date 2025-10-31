using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SC_minion : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private Animator anim;
    private SC_game_master gm;
    private float timer;
    private Transform player;
    private Camera cam;
    private Vector3 mousePos;

    [Header("Movement")]   
    [SerializeField] private float speed;
    [SerializeField] private float speed_aim;
    [SerializeField] private float distance_aim;
    [SerializeField] float Distance_detection = 5;
    [SerializeField] float noiseAmplitude = 0.3f;
    [SerializeField] float noiseFrequency = 0.8f;
    private float noiseAmplitude2 = 0.3f;
    private float noiseFrequency2 = 0.8f;
    public Transform aim;
    public bool searching;
    public Transform trs_aim;

    [Header("Combat")]
    public bool fighting;
    [SerializeField] private int health;
    [SerializeField] private int attack;
    [SerializeField] private float attack_speed;
    [SerializeField] private float attack_knockback = 5f;
    [SerializeField] private AudioSource damage_audio;
    [SerializeField] private GameObject damage_popup;
    [SerializeField] private float max_distance_from_aim;
    [SerializeField] private GameObject death_fx;


    void Start()
    {

        noiseAmplitude2 = noiseAmplitude + Random.Range(-noiseAmplitude / 2, noiseAmplitude / 2);
        noiseFrequency2 = noiseFrequency + Random.Range(-noiseFrequency / 2, noiseFrequency / 2);
        cam = Camera.main;
        gm = GameObject.Find("GAME_MASTER").GetComponent<SC_game_master>();
        gm.minions.Add(gameObject);

        player = GameObject.Find("Player").transform;
        trs_aim = GameObject.Find("Player_aim").transform;

            int rdm = Random.Range(0, 3);
            if (rdm == 2)
            {
                player = GameObject.Find("Player_up1").transform;
            }
            else if (rdm == 1)
            {
                player = GameObject.Find("Player_up2").transform;
            }
            else
            {
                player = GameObject.Find("Player_up").transform;
            }
        InvokeRepeating("update_", 0, 1f);
        InvokeRepeating("search_player", 0, 2);

    }


    void search_player()
    {
        int rdm = Random.Range(0, 3);
        if (rdm == 2)
        {
            player = GameObject.Find("Player_up1").transform;
        }
        else if (rdm == 1)
        {
            player = GameObject.Find("Player_up2").transform;
        }
        else
        {
            player = GameObject.Find("Player_up").transform;
        }
    }
    private void update_()
    {
        if (!fighting && !searching)
        {
            aim = player;
        }

        Vector3 direction = aim.position - transform.position;
        if (direction.sqrMagnitude < 0.0001f) return;

        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void go_to_mouse()
    {
        searching = true;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, LayerMask.GetMask("Default") + LayerMask.GetMask("Enemy")))
        {
            mousePos = hit.point;
        }
        float dist = Vector3.Distance(transform.position, mousePos);


        if (dist < distance_aim)
        {
           trs_aim.position = mousePos;
           aim = trs_aim;
        }
    }
    public void go_to_player()
    {
        fighting = false;
        searching = false; 
        aim = player;

        Vector3 toAim = aim.position - transform.position;
        float sqrDistance = toAim.sqrMagnitude;

        if (sqrDistance < Distance_detection && searching)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 15f, LayerMask.GetMask("Enemy") + LayerMask.GetMask("Loot"));
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
        }
    }

    void FixedUpdate()
    {
       
        Vector3 toAim = aim.position - transform.position;
        float sqrDistance = toAim.sqrMagnitude;

        if(sqrDistance < Distance_detection && searching)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 15f, LayerMask.GetMask("Enemy") + LayerMask.GetMask("Loot"));
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
        }

        if (sqrDistance > max_distance_from_aim * max_distance_from_aim)
        {
            if (!anim.GetBool("moving"))
            {
                anim.SetBool("moving", true);
            }
            Vector3 direction = (aim.position - rb.position).normalized;
            Vector3 newPosition = rb.position + direction * speed * Time.fixedDeltaTime;
            float noiseX = (Mathf.PerlinNoise(Time.time * noiseFrequency2, 0f) - 0.5f) * 2f;
            float noiseY = (Mathf.PerlinNoise(0f, Time.time * noiseFrequency2) - 0.5f) * 2f;

            Vector3 noise = new Vector3(noiseX, noiseY, 0f) * noiseAmplitude2;
            Vector3 noisyDirection = (direction + noise).normalized;

            rb.linearVelocity += noisyDirection * speed * Time.fixedDeltaTime;

        }
        else
        {

            searching = false;
            if (anim.GetBool("moving"))
            {
                anim.SetBool("moving", false);
            }
            timer += Time.fixedDeltaTime;
            if (timer >= attack_speed && aim != player)
            {
                fighting = true;

                Attack();
                timer = 0f;
            }
        }
    }


    void Attack()
    {
        anim.SetTrigger("attack");

        Collider[] hits = Physics.OverlapSphere(transform.position, 5f, LayerMask.GetMask("Enemy"));
        Transform nearest = null;
        float minDistSq = float.MaxValue;

        foreach (var hit in hits)
        {
            float distSq = (hit.transform.position - transform.position).sqrMagnitude;
            if (distSq < minDistSq)
            {
                minDistSq = distSq;
                nearest = hit.transform;
            }
        }
        nearest.GetComponent<SC_enemy>().takedamage(attack);


        rb.AddForce((nearest.position - transform.position).normalized * attack_knockback, ForceMode.Impulse);
    }

    public void takedamage(int dmg)
    {
        damage_audio.pitch = Random.Range(0.9f, 1.1f);
        damage_audio.Play();
        float rdm = Random.Range(-0.1f, 0.1f);
        SC_damage_popup tmp = Instantiate(damage_popup, transform.position + new Vector3(rdm, rdm, rdm), Quaternion.identity).GetComponent<SC_damage_popup>();
        health = health - dmg;
        if (health > 0)
        {
            Invoke("damage_delay", 0.1f);
        }
        else
        {
            die();
        }
    }

    public void damage_delay()
    {
        renderer.material.SetFloat("_Damage_glow", 0);
    }

    void die()
    {
        gm.minions.Remove(gameObject);
        Instantiate(death_fx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}