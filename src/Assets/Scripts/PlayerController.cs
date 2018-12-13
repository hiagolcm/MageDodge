using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int enemyPlayerIndex;
    public ScoreController scoreManager;

    public string axisName;
    public KeyCode jumpKey;

    public float speed;
    public float jumpForce;
    public int extraJumps;
    public float wallSlideSpeedMax;

    public float castSetUpTimeValue;
    public float castDurationValue;
    public float castCoolDownTimeValue;

    public ShieldCotroller shield;

    private bool casting;
    private float castSetupTimeRemaining;
    private float castDurationTimeRemaining;
    private float castCoolDownTimeRemaining;

    public Animator animator;

    private float moveInput;
    private bool facingRight = true;
    private Rigidbody2D rb;
    private int remainingJumps;

    private bool isGrounded;
    public Transform groundCheck;
    public Transform leftCheck;
    public Transform rightCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public GameObject dustCloud;
    public TimeController timeController;

    private bool onTheWall;

    private bool dead = false;
    

    void Start () {
        Physics2D.IgnoreCollision(shield.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        rb = GetComponent<Rigidbody2D>();
        remainingJumps = extraJumps + 1;
        castSetupTimeRemaining = castSetUpTimeValue;
        castDurationTimeRemaining = castDurationValue;
        castCoolDownTimeRemaining = castCoolDownTimeValue;
	}

    private void FixedUpdate() {
        if (!dead && !PauseMenuController.GameIsPaused)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
            onTheWall = isOnTheWall();
            ResetJumps();
        }

        SetCurrentAnimationProps();
    }

    private void Update()
    {
        if (!dead && !PauseMenuController.GameIsPaused)
        {
            CastListenner();
            ShieldPositionListenner();
            JumpListenner();
            WalkListenner();
            WallJumpListenner();
        }
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void ResetJumps()
    {
        if ( (isGrounded && rb.velocity.y <= 0) || onTheWall)
        {
            remainingJumps = extraJumps + 1;
        }
    }

    private void SetCurrentAnimationProps()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    private void WalkListenner()
    {
        moveInput = Input.GetAxis(axisName);
        rb.velocity = new Vector2(speed * moveInput, rb.velocity.y);

        if (Mathf.Abs(moveInput) > 0 && Mathf.Abs(moveInput) < 0.5 && isGrounded)
        {
            InstantiateCloud();
        }

        if ((!facingRight && moveInput > 0) || (facingRight && moveInput < 0))
        {
            Flip();
        }
    }

    private void JumpListenner()
    {
        if (Input.GetKeyDown(jumpKey) && remainingJumps > 0)
        {
            InstantiateCloud();
            SoundManagerController.PlaySound("Jump");
            rb.velocity = Vector2.up * jumpForce;
            remainingJumps--;
        }
    }

    private void CastListenner()
    {
        if (Input.GetMouseButtonDown(0))
        {
            casting = true;
        }

        if (casting)
        {
            if(castSetupTimeRemaining > 0)
            {
                animator.SetBool("SettingUpCast", true);
                castSetupTimeRemaining -= Time.deltaTime;
                shield.Activate();
            } else if(castDurationTimeRemaining > 0)
            {
                animator.SetBool("SettingUpCast", false);
                animator.SetBool("Casting", true);
                castDurationTimeRemaining -= Time.deltaTime;
            }
            else
            {
                animator.SetBool("Casting", false);
                shield.Deactivate();
                castCoolDownTimeRemaining -= Time.deltaTime;
            }
        }

        if (castCoolDownTimeRemaining <=0)
        {
            casting = false;
            castSetupTimeRemaining = castSetUpTimeValue;
            castDurationTimeRemaining = castDurationValue;
            castCoolDownTimeRemaining = castCoolDownTimeValue;
        }   
    }

    private void ShieldPositionListenner()
    {
        shield.UpdatePosition(transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball" && !dead)
        {
            timeController.SlowTrigger();
            //rb.velocity = new Vector2(0, 0);
            SoundManagerController.PlaySound("Dead");
            animator.SetTrigger("Dead");
            dead = true;
            scoreManager.TriggerWin(enemyPlayerIndex);
        }

        if (collision.gameObject.name == "Tilemap" && !isOnTheWall())
        {
            InstantiateCloud();
            SoundManagerController.PlaySound("HitTheGround");
        }
    }

    private bool isOnTheWall()
    {
        return (Physics2D.OverlapCircle(leftCheck.position, checkRadius, whatIsGround) || 
            Physics2D.OverlapCircle(rightCheck.position, checkRadius, whatIsGround));
    }

    private void WallJumpListenner()
    {
        if (onTheWall)
        {
            if (rb.velocity.y < -wallSlideSpeedMax)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeedMax);
            }
        }
    }

    private void InstantiateCloud()
    {
        Vector2 cloudPosition = new Vector2(transform.position.x, transform.position.y - 0.3f);
        Instantiate(dustCloud, cloudPosition, dustCloud.transform.rotation);
    }

    public void Revive()
    {
        dead = false;
        animator.SetTrigger("Revive");
    }
}
