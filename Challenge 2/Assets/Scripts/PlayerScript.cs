using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    Animator anim;

    public Text score;

    public Text lives;

    public Text winText;

    private int livesValue = 3;

    private int scoreValue = 0;

    private int teleportValue = 0;

    public AudioSource musicSource;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;


    private bool facingRight = true;
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        score.text = "Score = " + scoreValue.ToString();
        lives.text = "Lives = " + livesValue.ToString();
        winText.text = "";
        

    }

    //physics
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
        
    }
    
    //coin and enemy collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score = " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            teleportValue = scoreValue;
        }
        //teleport to 2nd stage
        if (teleportValue == 4)
        {
            transform.position = new Vector2(54, 0);
            teleportValue = 5;
            livesValue = 3;
            lives.text = "Lives = " + livesValue.ToString();
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives = " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
    }

    //jump
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
           
        }
    }

   


    void Update()
    {
        if (scoreValue <= 7)
        {
            musicSource.clip = musicClipOne;
            musicSource.Play();
        }
        else {
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
        if (scoreValue >= 8)
        {
            winText.text = "You win! Game Created by Nathaniel Green";
            anim.SetInteger("State", 0);
            Destroy(this);
        }
        
        if (livesValue == 0)
        {
            winText.text = "You lose... Try again?";
            Destroy(this);
            anim.SetInteger("State", 3);
        }


        //animation for Walking
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 0);
        }

    }
}
        

