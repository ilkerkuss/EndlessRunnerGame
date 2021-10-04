using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    //private CharacterController _chrController; //kaldýrdýk
    private CapsuleCollider _chrCollider;
    [SerializeField] private LayerMask _groundLayer;
    private bool _isGrounded;

    [SerializeField] private float _forwardSpeed = 5f;
    private float _playerMaxSpeed = 20f;

    private float _laneDistance = 4f;
    private int _movementLane ; //0:left 1:middle 2:right

    [SerializeField] private float _jumpForce = 5f;

    private Vector3 _playerPos;
   // public static bool _isDead;

    private Rigidbody _rb;

    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    private Animator _playerAnimator;

    // ItemController methodunu obje içine almak için
    private IEnumerator _takeStar;
    private IEnumerator _takeShoe;


    private LevelManager _levelManager;

    public delegate void PlayerEvents();
    public static event PlayerEvents PlayerDead;


    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
     

    }


    private void Start()
    {
        Init();

        _levelManager = LevelManager.Instance;
    }


    private void Update()
    {
        //IsGrounded
        _isGrounded = Physics.CheckSphere(transform.position, 1f, _groundLayer);

        Debug.Log(_movementLane);


        if (_levelManager.GameState == LevelManager.GameStates.IsGamePlaying ) // oyun baþlatýldý ve karakter ölü deðil ise  ** ve oyun durdurulmamýþ ise
        {
            Move();
            SpeedUpCharacter();

        }

    }

    #region Movement Functions

    private void Jump()
    {
        _rb.velocity = Vector3.up * _jumpForce;

        _playerAnimator.SetTrigger("IsJumped");

        AudioController.Instance.PlaySound("JumpSound");

    }

    private void Move()
    {

        var moveDir = Vector3.forward * _forwardSpeed * Time.deltaTime;
        transform.position += moveDir;



        _playerAnimator.SetBool("IsRunning", true);


        if (Input.GetMouseButtonDown(0))
        {
            //Týklama baþlangýç pozisyonu
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //Týklama bitiþ pozisyonu
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //Baþlangýç ile bitiþ vektörü farký
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);


            currentSwipe.Normalize();



            if (_isGrounded)
            {
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    Jump();

                }

                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    StartCoroutine(Slide());
                }
            }

            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {

                if (_movementLane > 0)
                {
                    transform.position += Vector3.left * _laneDistance;
                    _movementLane--;

                    AudioController.Instance.PlaySound("LaneChangeSound");
                }
                else
                {
                    _movementLane = 0;
                }

            }
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {

                if (_movementLane < 2)
                {
                    transform.position += Vector3.right * _laneDistance;
                    _movementLane++;

                    AudioController.Instance.PlaySound("LaneChangeSound");
                }
                else
                {
                    _movementLane = 2;
                }

            }

        }
    }

    private IEnumerator Slide()
    {

        yield return new WaitForSeconds(0.25f / Time.timeScale);

        _playerAnimator.SetBool("IsSlide", true);
        _chrCollider.center = new Vector3(0, -0.01f, 0);
        _chrCollider.height = 0;

        AudioController.Instance.PlaySound("SlideSound");



        yield return new WaitForSeconds((1.5f - 0.25f) / Time.timeScale);

        _chrCollider.center = Vector3.zero;
        _chrCollider.height = 0.032f;

        _playerAnimator.SetBool("IsSlide", false);
    }



    #endregion

    #region Get/Set Functions About Player 

    //Item alýndýðýnda kullanýlan player func.
    //Shoe item func.
    public float GetPlayerSpeed()
    {
        return _forwardSpeed;
    }

    public void SetPlayerSpeed(float Speed)
    {
        _forwardSpeed = Speed;
    }

    public float GetPlayerJumpForce()
    {
        return _jumpForce;
    }

    public void SetPlayerJumpForce(float ForceAmount)
    {
        _jumpForce = ForceAmount;
    }

    #endregion


    private void OnCollisionEnter(Collision hit)
    {
        if (hit.collider.CompareTag("Obstacle"))
        {

            _rb.velocity = Vector3.zero;

            PlayerDead?.Invoke();


            _playerAnimator.SetBool("IsRunning",false);

            AudioController.Instance.PlaySound("GameOver");

            LevelManager.Instance.GameOver();
        }


        if (hit.collider.CompareTag("Coin"))
        {
            PlayerManager.Instance.IncreaseNumberOfCoin();

            Destroy(hit.gameObject);

            AudioController.Instance.PlaySound("PickUpCoin");
        }


        if (hit.collider.CompareTag("Item"))
        {
            if (hit.collider.name.Equals("Shoe"))
            {
                if (_takeStar != null)
                {
                    StopCoroutine(_takeShoe);
                }

                _takeShoe = ItemController.Instance.TakeShoe();
                StartCoroutine(_takeShoe);

            }
            if (hit.collider.name.Equals("Star"))
            {

                if (_takeStar!=null)
                {
                    StopCoroutine(_takeStar);
                }

                _takeStar = ItemController.Instance.TakeStar();
                StartCoroutine(_takeStar);
            }
            Destroy(hit.gameObject);
        }
    }



    public Vector3 GetPlayerPosition()
    {
        return _playerPos;
    }


    public void CharacterDead() //ölünce position sýfýrla ve baþlangýç fonk çaðýr
    {
        transform.position = _playerPos;
        Init();

    }



    public void Init()
    {

        _forwardSpeed = 5f;
        _movementLane = 1;
        _rb = GetComponent<Rigidbody>();
        _playerPos = transform.position;

        _chrCollider = GetComponent<CapsuleCollider>();

        _playerAnimator = GetComponent<Animator>();

    }


    private void SpeedUpCharacter()
    {
        if (_forwardSpeed < _playerMaxSpeed)
        {
            _forwardSpeed += 0.1f * Time.deltaTime;
        }
        
    }


    
}
