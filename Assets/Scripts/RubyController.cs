using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class RubyController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public InputAction _playerControls;
    private Rigidbody2D rb;

    private void OnEnable() {
        _playerControls.Enable();    
    }

    private void OnDisable() {
        _playerControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position + _playerControls.ReadValue<Vector2>() * moveSpeed * Time.deltaTime);
        //Debug.Log(horizontal)
    }
}