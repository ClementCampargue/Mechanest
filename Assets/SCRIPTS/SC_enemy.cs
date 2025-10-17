using Unity.Burst.Intrinsics;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class SC_enemy : MonoBehaviour
{
    public int health;
    public int attack;
    public float attack_speed;
    public float speed;

    public GameObject[] enemies_positions;
    private SC_minion enemy;
    private Rigidbody rb;
    public GameObject damage_popup;

    private Transform aim;
    public float max_distance_from_aim;
    private float timer;

    public GameObject death_fx;

    public List<MeshRenderer> renderers;
    public Material glow_damage;
    private List<Material> mats = new List<Material>();

    void Start() 
    {
        foreach (Renderer rend in renderers)
        {
            if (rend != null)
            {
                mats.AddRange(rend.sharedMaterials);
            }
        }


        rb = GetComponent<Rigidbody>();
        enemies_positions = GameObject.FindGameObjectsWithTag("Minion");
        InvokeRepeating("checkenemies", 0, 1);
        checkenemies();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Vector3.Distance(aim.position, transform.position) > max_distance_from_aim)
        {
            Vector3 direction = (enemy.transform.position - rb.position).normalized;
            rb.linearVelocity = direction * speed;
        }
        else
        {
            timer += Time.deltaTime;

            if (timer >= attack_speed)
            {
                Attack();
                timer = 0f;
            }
        }
        transform.LookAt(aim.transform);

    }


    void checkenemies()
    {
        enemies_positions = enemies_positions.Where(obj => obj).ToArray();

        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = Vector3.zero;

        foreach (GameObject obj in enemies_positions)
        {
            if (obj == null) continue; // Sécurité

            Vector3 directionToTarget = obj.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude; // plus rapide que Vector3.Distance

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = obj.transform;
            }
        }

        aim = bestTarget;
        enemy = aim.GetComponent<SC_minion>();

    }

    void Attack()
    {
        enemy.takedamage(attack);
    }

    public void takedamage(int dmg)
    {
        SC_damage_popup dmg_ = Instantiate(damage_popup, transform.position, Quaternion.identity).GetComponent<SC_damage_popup>();
        dmg_.text.text = dmg.ToString();
        health = health - dmg;
        if (health > 0)
        {
            Invoke("damage_delay", 0.1f);
            for (int i = 0; i < renderers.Count; i++)
            {
                renderers[i].material = glow_damage;
            }

        }
        else
        {
            die();
        }
    }

    void damage_delay()
    {
        for (int i = 0; i < renderers.Count; i++)
        {
           renderers[i].material = mats[i];
        }
    }

    void die()
    {
        Instantiate(death_fx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
