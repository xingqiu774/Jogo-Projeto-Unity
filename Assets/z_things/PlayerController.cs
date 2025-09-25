using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
	Vector2 moveInput;
	
	public bool IsMoving { get; private set; }
	
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	
	public void OnMove(InputAction.CallbackContext context)
	{
		moveInput = context.ReadValue<Vector2>();
		
		IsMoving = moveInput != Vector2.zero;
	}
	
	Rigidbody2D rb;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
	private void FixedUpdate()
	{
		rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
	}
}
