using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D controller;

    public float animatorFloat;
    public Animator animator;
    
    private Vector2 currentMovement;
    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 movement = input * (speed / 20);

        // Smoothes animation float
        if (movement.magnitude > 0)
            animatorFloat = Mathf.Lerp(animatorFloat, movement.normalized.magnitude, Time.deltaTime * 50);   
        else
            animatorFloat = Mathf.Lerp(animatorFloat, 0, Time.deltaTime * 50);   
        
        animator.SetFloat("Speed", animatorFloat);
        
        controller.MovePosition((Vector2)transform.position + movement);
    }
}
