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


    private int scoreValue = 0;
    private int livesValue = 3;
    

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        rd2d = GetComponent<Rigidbody2D>();
        livesValue = 3;
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
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 5), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }

            if (Input.GetKey(KeyCode.A))
            {
                rd2d.AddForce(new Vector2(-1,0), ForceMode2D.Impulse);
            }

            if (Input.GetKey(KeyCode.D))
            {
                rd2d.AddForce(new Vector2(1,0), ForceMode2D.Impulse);
            }
        }
    }



}