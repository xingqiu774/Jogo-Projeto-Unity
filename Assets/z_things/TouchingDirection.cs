using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
    Rigidbody2D rb;
    CapsuleCollider2D touchingCol;

    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];

    [SerializeField]
    private bool _isGrounded;

    Animator anima;

    public bool IsGrounded
    {
        get { return _isGrounded; }
        private set
        {
            _isGrounded = value;
            anima.SetBool("isGrounded", value);
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingCol = GetComponent<CapsuleCollider2D>();
        anima = GetComponent<Animator>();
    }

    // Vamos usar mais o FixedUpdate() pois será um código mais
    // focado em identificar a física do que está acontecendo
    private void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
    }
}
