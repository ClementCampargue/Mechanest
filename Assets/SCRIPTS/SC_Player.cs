using UnityEngine;

public class SC_Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float Move_speed;
    [SerializeField] private Animator anim;
    [SerializeField] private bool fly;
    [SerializeField] private float health;
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
      
        if (fly)
        {
            if (Input.GetAxis("Vertical") != 0 && !Input.GetKey(KeyCode.Space) && Input.GetAxis("Horizontal") != 0 && !Input.GetKey(KeyCode.Space))
            {
                anim.SetBool("walk", true);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * Move_speed);
                rb.AddForce(Vector3.up * Move_speed * 2);
            }
            else
            {
                rb.AddForce(new Vector3(Input.GetAxis("Horizontal"), -1, Input.GetAxis("Vertical")).normalized * Move_speed);
            }
        }
        else
        {
            if (rb.linearVelocity.sqrMagnitude > 0.0001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(rb.linearVelocity, Vector3.up);
                transform.rotation = targetRotation;
            }
            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0 )
            {
                rb.linearVelocity = (new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * Move_speed);

                anim.SetBool("walk", true);
            }
            else
            {
                anim.SetBool("walk", false);

                rb.linearVelocity = Vector3.zero;
            }

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
