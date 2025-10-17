using UnityEngine;

public class SC_Player : MonoBehaviour
{
    private Rigidbody rb;
    public float Move_speed;
    public Animator anim;

    public float health;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Application.targetFrameRate = 240;
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (rb.linearVelocity.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rb.linearVelocity, Vector3.up);
            transform.rotation = targetRotation;
        }

        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            anim.SetBool("walk", true);
            rb.linearVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * Move_speed;
        }
        else
        {
            anim.SetBool("walk",false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player_attack")
        {
            TakeDamage();
        }

    }
    public void TakeDamage()
    {

    }

    void Die()
    {
        Destroy(gameObject);
    }
}
