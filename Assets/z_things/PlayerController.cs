using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
	Vector2 moveInput;
	
	private bool _isMoving = false;

	public bool IsMoving { 
		get
		{
			return _isMoving;
		}
		private set
		{
			_isMoving = value;
			animator.SetBool("isMoving", value);
		}
	}

	private bool _isRunning = false;

	public bool IsRunning { 
		get
		{
			return _isRunning;
		}
		private set
		{
			_isRunning = value;
			animator.SetBool("isRunning", value);
		}
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		moveInput = context.ReadValue<Vector2>();
		
		IsMoving = moveInput != Vector2.zero;
	}
	
	public void OnRun(InputAction.CallbackContext context)
	{
		if (context.started){
			IsRunning = true;
		}else if (context.canceled){
			IsRunning = false;
		}
	}

	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	
	
	
	Rigidbody2D rb;
	Animator animator;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
	private void FixedUpdate()
	{
		rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
	}
}
