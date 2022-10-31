using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

 public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    public Text lives;

    public GameObject winTextObject;


    public GameObject loseTextObject;
    public AudioSource musicSource;
    public Animator animator;

    private bool facingRight = true;
    private int scoreValue = 0;
    private int livesValue = 3;
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    public Text hozText;
    public Text jumpText;
    

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        rd2d = GetComponent<Rigidbody2D>();
        livesValue = 3;
        animator = GetComponent<Animator>();
        SetCountText();

        
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        
    }

    void SetCountText()
    {
        score.text = " " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }


        else if (facingRight == true && hozMovement < 0)

        {
            Flip();
        }


        if (hozMovement > 0 && facingRight == true)
        {
            Debug.Log ("Facing Right");
            
        }

        if (hozMovement < 0 && facingRight == false)
        {
            Debug.Log ("Facing Left");
            
        }

        if (vertMovement > 0 && isOnGround == false)
        {
            Debug.Log("Jumping");
            
        
        }

        else if (vertMovement == 0 && isOnGround == true)
        {
            Debug.Log ("Not Jumping");
        
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
       if (scoreValue >= 8)
        {
            winTextObject.SetActive(true);
        }
       if (scoreValue >= 8)
       {

          musicSource.Play();

       }

       else if (collision.collider.tag == "Enemy")
        {
            livesValue = livesValue - 1; 
            lives.text = "Lives: " + livesValue.ToString(); 
            Destroy(collision.collider.gameObject);

        }

    
        if (livesValue <= 0)
        { 
            livesValue = 0;
            lives.text = "Lives: " + livesValue.ToString();
            loseTextObject.SetActive(true); 
        }

        if (livesValue <= 0)
        {
            speed = 0;
        }

        if (scoreValue == 4) 
            {
                transform.position = new Vector3(28.0f, 12.5f, 0.0f); 
            }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 5), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
                
                animator.SetInteger("State", 2);

            }

            if (Input.GetKey(KeyCode.A))
            {
                rd2d.AddForce(new Vector2(-1,0), ForceMode2D.Impulse);
            
                animator.SetInteger("State", 1);

            }

            if (Input.GetKey(KeyCode.D))
            {
                rd2d.AddForce(new Vector2(1,0), ForceMode2D.Impulse);

                animator.SetInteger("State", 1);
            }
        }
    }

    void Flip ()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
    



}