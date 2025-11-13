using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour		
{
    public float walkSpeed = 1f;
	public float runSpeed = 8f;
	Vector2 moveInput;
	
	public float CurrentMoveSpeed{ 
		get{
            if (IsMoving)
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
	
	
	
	
	Rigidbody2D rb;
	Animator animator;
	Animator anima;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>(); // ← add this
		anima = GetComponent<Animator>(); // add this line
		Debug.Log($"PlayerController initialized on {gameObject.name}");
		Debug.Log("PlayerController Awake from: " + gameObject.GetInstanceID());
	}
	
	private void FixedUpdate()
	{
		//rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
		
		//Debug.Log($"Velocity before: {rb.velocity}, IsMoving={IsMoving}, IsRunning={IsRunning}, CurrentSpeed={CurrentMoveSpeed}");
		
		

		rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

		anima.SetFloat("yVelocity", rb.velocity.y);
	}
}
