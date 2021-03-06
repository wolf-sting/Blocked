﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class control : MonoBehaviour
{
    public float speed;
    public float jump;
    public float down;

    public Transform spawnpoint;
    
    private float moveinput;

    private Rigidbody2D rb;

    private bool facingright = true;
    private bool isgrounded;
    public Transform groundcheck;
    public float checkradius;
    public LayerMask whatisground;
    private int extrajump;
    public int extrajumpValue;
    
    void Start()
    {
        extrajump = extrajumpValue;
        rb = GetComponent<Rigidbody2D>();

        if (speed > 5)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {

        isgrounded = Physics2D.OverlapCircle(groundcheck.position, checkradius, whatisground);

        moveinput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveinput * speed, rb.velocity.y);

        if (facingright == false && moveinput > 0)
        {
            flip();
        }
        else if (facingright == true && moveinput < 0)
        {
            flip();
        }

    }
    void Update()
    {
        if (isgrounded == true)
        {
            extrajump = extrajumpValue;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && extrajump > 0)
        {
            rb.velocity = Vector2.up * jump;
            extrajump--;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow) && extrajump == 0 && isgrounded == true)
        {
            rb.velocity = Vector2.up * jump;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.velocity = Vector2.down * down;
        }


    }

    void flip()
    {
        facingright = !facingright;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }



    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {

            Respawn();

        }
    }


    public void Respawn()
    {
        this.transform.position = spawnpoint.position;
    }


   
}
