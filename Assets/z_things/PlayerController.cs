using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection))]

public class PlayerController : MonoBehaviour		
{



	Rigidbody2D rb;
	Animator anima;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		anima = GetComponent<Animator>(); 
		touchingDirections = GetComponent<TouchingDirection>();

		Debug.Log($"PlayerController initialized on {gameObject.name}");
		Debug.Log("PlayerController Awake from: " + gameObject.GetInstanceID());
	}



    public float walkSpeed = 5f;
	public float runSpeed = 8f;
	Vector2 moveInput;

	TouchingDirection touchingDirections;
	public float jumpImpulse = 10f;
	
	public float CurrentMoveSpeed{ 
		get{

			if (!CanMove)
            {
                return 0;
            }

            if (IsMoving && !touchingDirections.IsOnWall)
            {
                if (IsRunning)
                {
                    return runSpeed;
                }
                else
                {
                    return walkSpeed;
                }
            }else
            {
                return 0;
            }
		}
    }

	private bool _isMoving = false;

	public bool IsMoving { 
		get
		{
			return _isMoving;
		}
		private set
		{
			_isMoving = value;
			anima.SetBool("isMoving", value);
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
			anima.SetBool("isRunning", value);
		}
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		moveInput = context.ReadValue<Vector2>();
		
		IsMoving = moveInput != Vector2.zero;

		SetFacingDirection(moveInput);
	}

	public void OnRun(InputAction.CallbackContext context)
	{
		Debug.Log($"OnRun: phase={context.phase}");
		if (context.started){
			IsRunning = true;
		}
		else if (context.performed){
			IsRunning = true;
		}
		else if (context.canceled){
			IsRunning = false;
		}
	}

	public void OnJump(InputAction.CallbackContext context){


		if (touchingDirections == null){
        	Debug.LogError("[OnJump] touchingDirections IS NULL!");
    	}
		if (anima == null){
        	Debug.LogError("[OnJump] anima IS NULL!");
    	} else {
        	Debug.Log("[OnJump] anima OK");
    	}

		if (context.started && touchingDirections.IsGrounded && CanMove){
			anima.SetTrigger("jump");
			rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);

			

		}
	}


	public void OnAttack(InputAction.CallbackContext context){
    	// Log everything useful immediately
    	Debug.Log($"[OnAttack] called. phase={context.phase}, started={context.started}, performed={context.performed}, canceled={context.canceled}");

    	// Check references BEFORE accessing any property that can throw
    	if (touchingDirections == null){
        	Debug.LogError("[OnAttack] touchingDirections IS NULL!");
		} else {
        	// safely inspect IsGrounded only after touchingDirections != null
        	bool groundedSafe = false;
        	try {
            	groundedSafe = touchingDirections.IsGrounded;
        	} catch (System.Exception e) {
            	Debug.LogError($"[OnAttack] touchingDirections.IsGrounded threw: {e}");
        	}
        	Debug.Log($"[OnAttack] touchingDirections != null, IsGrounded (safe) = {groundedSafe}");
    	}

    	if (anima == null){
        	Debug.LogError("[OnAttack] anima IS NULL!");
    	} else {
        	Debug.Log("[OnAttack] anima OK");
    	}

    	// Now use safe-guards
    	if (!context.started) return;
    	if (touchingDirections == null) return;
    	if (!touchingDirections.IsGrounded) return;
    	if (anima == null) return;

    	anima.SetTrigger("Attack");
    	Debug.Log($"Attack Button Pressed");
	}

	public bool CanMove
    {
        get
        {
            return anima.GetBool("canMove");
        }
    }

	private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight){
            IsFacingRight = true;
        } else if (moveInput.x < 0 && IsFacingRight){
            IsFacingRight = false;
        }
    }

	public bool _isFacingRight = true;

	public bool IsFacingRight {get {return _isFacingRight;} private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1,1);
            }
			_isFacingRight = value;
        } }
	
	

	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	//  git config --local user.email "youmukonpakubr@gmail.com"
  //git config --local user.name "Xingqiu774"
	
	
	
	
	private void FixedUpdate()
	{
		//rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
		
		//Debug.Log($"Velocity before: {rb.velocity}, IsMoving={IsMoving}, IsRunning={IsRunning}, CurrentSpeed={CurrentMoveSpeed}");
		
		

		rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

		anima.SetFloat("yVelocity", rb.velocity.y);
	}
}
