using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class PlayerMover : MonoBehaviour
{
    Rigidbody _rb;

    [SerializeField] float moveSpeed = 10;
    [SerializeField] float jumpForce = 60;
    [SerializeField] bool isJumping = false;
    [SerializeField] bool grounded = true;
    bool groundedLastFrame = false;  //groundcheck is done for two frames to preven physics issues
    [SerializeField] bool nextToWall = true;
    [SerializeField] float moveHoriz = 0;
    [SerializeField] float moveVert = 0;

    [SerializeField] Transform BoxCastCenter;
    [SerializeField] Transform left;

    [SerializeField] float LevelWidth;
    [SerializeField] Vector3 GroundBoxcastSize = new Vector3(.45f, .01f, .5f);

    [SerializeField] float WallcheckDistanceFromCenter = .2f;
    [SerializeField] Vector3 WallcheckSize = new Vector3(.1f, .7f, .5f);
    [SerializeField] bool WallTouchingLeft = false;
    [SerializeField] bool WallTouchingRight = false;
    [SerializeField] bool canWallJump = true;
    [SerializeField] bool wallJumpReady = true;
    [SerializeField] bool tryingToWalljump = false;
    [SerializeField, Range(0, 2)] float walljumpExtraForcePercent;
    [SerializeField] float airControl;
    [SerializeField, Range(0, 2)] float airBrakeModifier;
    public AnimationCurve AirAccelerationCurve;
    [SerializeField] float airSpeed = 0;

    public bool isFacingRight = true;

    public float testSpeed = 3;

    public float TestPushForce = 50;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnValidate()
    {
        GroundBoxcastSize.z = LevelWidth / 2;
        WallcheckSize.z = LevelWidth / 2;
    }

    private void Update()
    {
        moveHoriz = Input.GetAxisRaw("Horizontal");
        moveVert = Input.GetAxisRaw("Vertical");
        tryingToWalljump = (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && Input.GetKey(KeyCode.W);

        if (Input.GetKeyDown(KeyCode.R)) _rb.AddForce(new Vector2(TestPushForce, 0), ForceMode.Impulse);
    }

    private void FixedUpdate()
    {

        //using a boxcollide if the player is on the ground
        if (groundedLastFrame)
        {
            grounded = Physics.OverlapBox(BoxCastCenter.position, GroundBoxcastSize, Quaternion.identity).Any(Collider => Collider.tag == "Ground");
            groundedLastFrame = grounded;
        }
        else groundedLastFrame = Physics.OverlapBox(BoxCastCenter.position, GroundBoxcastSize, Quaternion.identity).Any(Collider => Collider.tag == "Ground");


        if (grounded) wallJumpReady = true;

        WallTouchingLeft = Physics.OverlapBox(transform.position.Change(-WallcheckDistanceFromCenter,0,0), WallcheckSize, Quaternion.identity).Any(Collider => Collider.tag == "Ground");
        WallTouchingRight = Physics.OverlapBox(transform.position.Change(WallcheckDistanceFromCenter, 0, 0), WallcheckSize, Quaternion.identity).Any(Collider => Collider.tag == "Ground");
        nextToWall = WallTouchingLeft || WallTouchingRight;


        TwoDMover();
        //TestMover();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(BoxCastCenter.position, GroundBoxcastSize * 2);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(BoxCastCenter.position, BoxCastCenter.position + (Vector3.down * .1f));

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position.Change(-WallcheckDistanceFromCenter, 0, 0), WallcheckSize * 2);
        Gizmos.DrawWireCube(transform.position.Change(WallcheckDistanceFromCenter, 0, 0), WallcheckSize * 2);
    }

    void TestMover()
    {
        if (Mathf.Abs(moveHoriz) == 1)  //Destects if there is howizontal input
        {
            if (Mathf.Abs(_rb.velocity.x) > moveSpeed)
                _rb.MovePosition(transform.position.Change(testSpeed * moveHoriz, 0, 0));
            else
                _rb.velocity = new Vector2(moveHoriz * moveSpeed, _rb.velocity.y);
            isFacingRight = moveHoriz == 1 ? true : false;
        }
        else if (grounded) _rb.velocity = Vector2.zero;

        if (moveVert > .1 && grounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        }

        //THIS ALLOWS THE PLAYER TO MOVE THROUGH A ONEWAY PLATFORM
        if (moveVert < -.1)
        {
            RaycastHit hit;
            OneWayPlatform platform = null;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 11f)) hit.collider.TryGetComponent<OneWayPlatform>(out platform);
            if (platform) Physics.IgnoreCollision(platform.PlatformCollider, GetComponent<Collider>(), true);
        }

        //WALL JUMP IMPLIMENTATION
        if (wallJumpReady && nextToWall && tryingToWalljump && !grounded && canWallJump)
        {
            _rb.velocity = new Vector2(_rb.velocity.x + ((moveHoriz * moveSpeed) * (walljumpExtraForcePercent)), jumpForce);
            wallJumpReady = false;
        }
    }

    void TwoDMover()
    {
        //WALL JUMP IMPLIMENTATION
        if (wallJumpReady && nextToWall && tryingToWalljump && !grounded && canWallJump)
        {
            if ((WallTouchingLeft && moveHoriz > 0) || (WallTouchingRight && moveHoriz < 0))
            {
                _rb.velocity = new Vector2(((moveHoriz * moveSpeed) * (walljumpExtraForcePercent)), jumpForce / 1.2f);
                wallJumpReady = false;
            }
        }

        airSpeed = AirAccelerationCurve.Evaluate(Mathf.Abs(_rb.velocity.x) / moveSpeed);


        //set velocity movement
        if (Mathf.Abs(moveHoriz) == 1)  //Destects if there is horizontal input
        {
            if(grounded)
                _rb.velocity = new Vector2(moveHoriz * moveSpeed, _rb.velocity.y);
            else
            {
                if(_rb.velocity.x == 0) _rb.AddForce(new Vector2(moveHoriz * moveSpeed * airSpeed * airControl * 3, 0), ForceMode.Acceleration);

                //Check if the player is trying to move against or with their current velocity
                else if ((_rb.velocity.x < 0) == (moveHoriz < 0))
                {
                    if (Mathf.Abs(_rb.velocity.x) <= moveSpeed)  //if the player is less then their threshhold, accelerate
                    {
                        _rb.AddForce(new Vector2(moveHoriz * moveSpeed * airSpeed * airControl, 0), ForceMode.Acceleration);
                    }
                }
                else
                {
                    //if the player is over their threshhold, increase aircontrol by airBrake percent
                    if(Mathf.Abs(_rb.velocity.x) >= moveSpeed)
                        _rb.AddForce(new Vector2(moveHoriz * moveSpeed * airSpeed * airControl * airBrakeModifier, 0), ForceMode.Acceleration);
                    else
                        _rb.AddForce(new Vector2(moveHoriz * moveSpeed * airSpeed * airControl, 0), ForceMode.Acceleration);
                }

            }
                

            isFacingRight = moveHoriz == 1 ? true : false;
        }
        else if(grounded) _rb.velocity = Vector2.zero;
        //else _rb.velocity = new Vector2(0, _rb.velocity.y);

        //grounded = Physics.OverlapBox(BoxCastCenter.position, GroundBoxcastSize, Quaternion.identity).Any(Collider => Collider.tag == "Ground");
        if (moveVert > .1 && grounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        }

        //THIS ALLOWS THE PLAYER TO MOVE THROUGH A ONEWAY PLATFORM
        if (moveVert < -.1)
        {
            RaycastHit hit;
            OneWayPlatform platform = null;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 11f)) hit.collider.TryGetComponent<OneWayPlatform>(out platform);
            if (platform) Physics.IgnoreCollision(platform.PlatformCollider, GetComponent<Collider>(), true);
        }
    }
}
