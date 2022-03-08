using System.Linq;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    Rigidbody _rb;
    Rigidbody2D _rb2D;

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


        try
        {
            if (FrontMan.FM.twoD3D)
                TwoDMover();
            else
                ThreeDMover();
        }
        catch (System.Exception)
        {
            if (FrontMan.FM.twoD3D)
                _rb2D = GetComponent<Rigidbody2D>();
            else
                _rb = GetComponent<Rigidbody>();
        }
        



        if (moveVert < -.1)
        {
            RaycastHit hit;
            OneWayPlatform platform = null;
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 11f)) hit.collider.TryGetComponent<OneWayPlatform>(out platform);
            if(platform) Physics.IgnoreCollision(platform.PlatformCollider, GetComponent<Collider>(), true);
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(BoxCastCenter.position, GroundBoxcastSize * 2);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(BoxCastCenter.position, BoxCastCenter.position + (Vector3.down * .1f));
    }

    void ThreeDMover()
    {
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
    void TwoDMover()
    {
        //set velocity movement
        if (Mathf.Abs(moveHoriz) == 1)
        {
            _rb2D.velocity = new Vector2(moveHoriz * moveSpeed, _rb2D.velocity.y);
            isFacingRight = moveHoriz == 1 ? true : false;
        }
        else
            _rb2D.velocity = new Vector2(0, _rb2D.velocity.y);

        if (moveVert > .1 && grounded)
        {
            _rb2D.velocity = new Vector2(_rb2D.velocity.x, jumpForce);
            //_rb.AddForce(new Vector2(0, jumpForce), ForceMode.Impulse);
        }
    }

}
