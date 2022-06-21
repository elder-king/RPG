using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    enum facing { up, down, right, left }
    bool sideU;
    bool sideR;

    public GameObject Light;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        rb.velocity = move * speed * Time.deltaTime;

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0f);
        }
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            transform.rotation = Quaternion.Euler(0, 0, 180f);
        }
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            transform.rotation = Quaternion.Euler(0, 0, 90f);
        }
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            SceneManager.LoadScene(0);
    }
    bool On = false;
    public void FlashLight()
    {
        if (On == false)
        {
            On = !On;
            Light.SetActive(true);
        }
        else
        {
            On = !On;
            Light.SetActive(false);
        }
    }
}
