using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float jumpForce;
    //public Text countText;
    //public Text winText;
    public Slider healthBarSlider;

    private Rigidbody rb;
    //private int count;

    public int maxHp;
    public int hp;

    bool isDead;
    public float distToGround;
    bool isGrounded;
    public LayerMask groundLayerMask;

    public GameObject explosionPrefab;

    public int maxJumpNum = 2;
    int jumpCounter = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isDead = false;
        SetHealthBarSliderValue();
    }

    void FixedUpdate() {
        if (GameManager.instance.gameState != GameManager.GameState.STARTED)
            return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.001f, groundLayerMask);

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical) * moveSpeed; 
    
        rb.AddForce(movement);

        if (isGrounded == true)
        {
            jumpCounter = 0;
        }

        if (jumpCounter<maxJumpNum && Input.GetButtonDown("Jump"))
        {
          
            jumpCounter += 1;
           
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    public void DecreaseHp(int value)
    {
        //if (isDead)
        //    return;
        hp -= value;
        if (hp < 0)
            hp = 0;

        SetHealthBarSliderValue();

        if (hp == 0)
        {
            Die();
        }
        
    }

    void SetHealthBarSliderValue()
    {

        healthBarSlider.value = hp / (float)maxHp;
    }

    public void Die()
    {
        isDead = true;
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, 2.0f);

        GameManager.instance.ShowLoseGameUI();

        gameObject.SetActive(false);
    }
}
