using System.Linq;
using UnityEngine;

public class PlayerMoverAddForce : MonoBehaviour
{
    Rigidbody _rb;

    [SerializeField] float moveSpeed = 10;
    [SerializeField] float jumpForce = 60;
    [SerializeField] bool isJumping = false;
    [SerializeField] bool grounded = true;
    [SerializeField] float gravityScale = 1.0f;

    [SerializeField] Transform BoxCastCenter;

    public static float globalGravity = -9.81f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        //using a boxcollide if the player is on the ground
        grounded = Physics.OverlapBox(BoxCastCenter.position, new Vector3(.45f, .02f, .5f), Quaternion.identity).Any(Collider => Collider.tag == "Ground");

        //Custom gravity
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        _rb.AddForce(gravity, ForceMode.Acceleration);
        float moveHoriz = Input.GetAxisRaw("Horizontal");
        float moveVert = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(moveHoriz) == 1)
            _rb.AddForce(new Vector2(moveHoriz * moveSpeed, 0), ForceMode.Impulse);
        else
            _rb.velocity = new Vector2(0, _rb.velocity.y);

        if ((moveVert > .1) && grounded)
        {
            _rb.AddForce(new Vector2(_rb.velocity.x, jumpForce), ForceMode.Impulse);
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(BoxCastCenter.position, new Vector3(.45f, .02f, .5f) * 2);
    }

}
