using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    private int scoreValue = 0;
    public Text winText;
    public Text loseText;
    public Text lives;
    private int livesValue = 3;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    Animator anim;
    private bool facingRight = true;
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
        loseText.text = "";
        lives.text = livesValue.ToString();        
        musicSource.clip = musicClipOne;
        musicSource.Play();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (Input.GetKeyDown(KeyCode.D))

        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))

        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.A))

        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.A))

        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.W))

        {
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.W))

        {
            anim.SetInteger("State", 0);
        }

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if (scoreValue == 4)
        {
            transform.position = new Vector3(65.0f, 0.0f, 0.0f);
            livesValue = 3;
            lives.text = "3";
        }


        if (scoreValue == 8)
        {
            winText.text = "You Win! Game created by Jonathan Platon";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }

        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        lives.text = "Lives: " + livesValue.ToString();

        if (livesValue <= 0)
        {
            winText.text = "You Lost!";
            Destroy(gameObject);
        }
       
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
if (collision.collider.tag == "Ground" && isOnGround)
{
if (Input.GetKey(KeyCode.W))
{
rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
}
}
}
}
