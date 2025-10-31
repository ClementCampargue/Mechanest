using UnityEngine;

public class SC_chest : MonoBehaviour
{
    [SerializeField] private bool chest_;
    [SerializeField] private GameObject item;

    [SerializeField] private bool bone_pile_;
    [SerializeField] private int bones_per_hp = 1;
    [SerializeField] private GameObject bone;

    [SerializeField] private int health;
    private int bone_count;
    [SerializeField] private Animator anim;

    public void takedamage(int damage)
    {
        bone_count = bones_per_hp;
        health--;
        if(health > 0)
        {
            anim.SetTrigger("hit");
            if (bone_pile_)
            {
                while (bone_count > 0)
                {
                    float rdm = Random.Range(-1f, 1f);
                    bone_count--;
                    Instantiate(bone, transform.position + new Vector3(rdm, 0, rdm), Quaternion.identity);

                }
            }
        }
        else
        {
            open();
        }
    }

    void open()
    {
        anim.SetTrigger("open");
        if (chest_)
        {
            Time.timeScale = 0;
            GameObject.Find("GAME_MASTER").GetComponent<SC_XP_manager>().panel_upgrade.SetActive(true);
        }
        if (bone_pile_)
        {
            while (bone_count > 0)
            {
                float rdm = Random.Range(-1f, 1f);
                bone_count--;
                Instantiate(bone, transform.position + new Vector3(rdm, 0, rdm), Quaternion.identity);

            }
        }
        Destroy(gameObject);
    }
}
