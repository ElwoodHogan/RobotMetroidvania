using System.Linq;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    Rigidbody _rb;

    [SerializeField] float moveSpeed = 10;
    [SerializeField] float jumpForce = 60;
    [SerializeField] bool isJumping = false;
    [SerializeField] bool grounded = true;
    [SerializeField] float moveHoriz = 0;
    [SerializeField] float moveVert = 0;

    [SerializeField] Transform BoxCastCenter;
    [SerializeField] Vector3 GroundBoxcastSize = new Vector3(.45f, .01f, .5f);

    public bool isFacingRight = true;

    public float highestHeight = 0;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveHoriz = Input.GetAxisRaw("Horizontal");
        moveVert = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (highestHeight < transform.position.y) highestHeight = transform.position.y;

        //using a boxcollide if the player is on the ground
        grounded = Physics.OverlapBox(BoxCastCenter.position, GroundBoxcastSize, Quaternion.identity).Any(Collider => Collider.tag == "Ground");

        


        //set velocity movement
        if (Mathf.Abs(moveHoriz) == 1)
        {
            _rb.velocity = new Vector2(moveHoriz * moveSpeed, _rb.velocity.y);
            isFacingRight = moveHoriz == 1 ? true : false;
        }
        else
            _rb.velocity = new Vector2(0, _rb.velocity.y);

        if (moveVert > .1 && grounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            //_rb.AddForce(new Vector2(0, jumpForce), ForceMode.Impulse);
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(BoxCastCenter.position, GroundBoxcastSize * 2);
    }

}
