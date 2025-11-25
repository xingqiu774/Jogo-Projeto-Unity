using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TouchingDirection: MonoBehaviour
{
    Rigidbody2D rb;
    CapsuleCollider2D touchingCol;
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    Animator anima;
    [SerializeField]
    private bool _IsGrounded;
    public bool IsGrounded
    {
        get
        {
            return _IsGrounded;
        }
        private set
        {
            _IsGrounded = value;
            anima.SetBool("isGrounded", value);
        }
    }
    [SerializeField]
    private bool _isOnWall;
    public bool IsOnWall
    {
        get{
            return _isOnWall;
            }
        
        private set
        {
            _isOnWall = value;
            anima.SetBool("isOnWall", value);
        }
    }
    [SerializeField]
    private bool _isOnCeiling;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        
        private set
        {
            _isOnCeiling = value;
            anima.SetBool("isOnCeiling", value);
        }
    }
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingCol = GetComponent<CapsuleCollider2D>();
        anima = GetComponent<Animator>();
    }
    //Vamos usar mais o FixedUpdate() pois serpa um c�digo mais focado em
    //identificar a f� sica do que est� acontecendo
    private void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}
