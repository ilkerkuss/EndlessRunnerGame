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
    private float _gravityForce = -9.81f;

    private Vector3 _playerPos;
    public static bool _isDead;

    private Rigidbody _rb;

    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    private Animator _playerAnimator;

    // ItemController methodunu obje içine almak için
    private IEnumerator _takeStar;

    //player altýn sayýsý ve skoru
    private int _scoreOfPlayer;
    private int _coinsNumberOfPlayer;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Init();
    }


    private void Update()
    {
        //IsGrounded
        _isGrounded = Physics.CheckSphere(transform.position, 1f, _groundLayer);
        
        Debug.Log("Yerde mi:" + _isGrounded);

     
        //Debug.Log("öldü mü"+_isDead);

    }

    private void LateUpdate()
    {
        if (!_isDead && LevelManager.IsGameStarted) // oyun baþlatýldý ve karakter ölü deðil ise
        {
            Move();
            SpeedUpCharacter();
            IncreaseScore();

        }
    }


    private void Jump()
    {
        _rb.velocity = Vector3.up * _jumpForce;
        //_direction.y = _jumpForce;
        Debug.Log("Zýpla");

        _playerAnimator.SetTrigger("IsJumped");

        AudioController.Instance.PlaySound("JumpSound");

    }



    private IEnumerator Slide()
    {
        Debug.Log("kay kay");
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


    private void OnCollisionEnter(Collision hit)
    {
        if (hit.collider.CompareTag("Obstacle"))
        {
            //_direction.z = 0;
            _rb.velocity = Vector3.zero;
            _isDead = true;
            _playerAnimator.SetBool("IsRunning",false);

            AudioController.Instance.PlaySound("GameOver");

            LevelManager.Instance.ShowGameOver();
        }


        if (hit.collider.CompareTag("Coin"))
        {
            _coinsNumberOfPlayer += 1;
            _scoreOfPlayer += 50;
            //Debug.Log("alýnan altýn"+GetNumberOfCoin());
            //Debug.Log("alýnan skor"+GetScoreOfPlayer());
            Destroy(hit.gameObject);

            AudioController.Instance.PlaySound("PickUpCoin");
        }


        if (hit.collider.CompareTag("Item"))
        {
            //Debug.Log(hit.collider.name);
            if (hit.collider.name.Equals("Shoe"))
            {
                ItemController.Instance.TakeShoe();

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
        // Debug.Log(hit.gameObject.name);
    }



    public Vector3 GetPlayerPosition()
    {
        return _playerPos;
    }


    public void CharacterDead() //ölünce position sýfýrla ve baþlangýç fonk çaðýr
    {

        transform.position = _playerPos;
        LevelManager.IsGameOver = true;
        LevelManager.IsGameStarted = false;
        Init();

        // _direction.z = _forwardSpeed;
    }


    

    public void Init()
    {
        //Debug.Log("PlayerController INIT");
        _forwardSpeed = 5f;
        _movementLane = 1;
        _rb = GetComponent<Rigidbody>();
        _isDead = false;
        _playerPos = transform.position;

        _chrCollider = GetComponent<CapsuleCollider>();

        _playerAnimator = GetComponent<Animator>();

        _scoreOfPlayer = 0;
        _coinsNumberOfPlayer = 0;

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
            }/*
            else
            {
                //transform.position += Vector3.up * _gravityForce * Time.deltaTime;
                _rb.velocity = Vector3.down * (_gravityForce * Time.deltaTime);
            } */

            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                Debug.Log("Sola kay");
               
             
                if (_movementLane > 0)
                {
                    _movementLane--;
                    transform.position += Vector3.left * _laneDistance;

                    AudioController.Instance.PlaySound("LaneChangeSound");
                }
                if (_movementLane <= 0)
                {
                    _movementLane = 0;
                }

            }
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                Debug.Log("Saða kay");
               
                if (_movementLane < 2)
                {
                    _movementLane++;
                    transform.position += Vector3.right * _laneDistance;
                    

                    AudioController.Instance.PlaySound("LaneChangeSound");
                }
                else if (_movementLane >= 2)
                {
                    _movementLane = 2;
                }

            }


            //Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

            /*
            if (_movementLane == 0)
            {
                //targetPosition += Vector3.left * _laneDistance;
                transform.position += Vector3.left * _laneDistance;
            }
            else if (_movementLane == 2)
            {
                //targetPosition += Vector3.right * _laneDistance;
                transform.position += Vector3.left * _laneDistance;
            }
            */

            //transform.position = Vector3.Lerp(transform.position,targetPosition,80* Time.deltaTime);


        }
    }


    private void SpeedUpCharacter()
    {
        if (_forwardSpeed < _playerMaxSpeed)
        {
            _forwardSpeed += 0.1f * Time.deltaTime;
        }
        
    }


    public int GetNumberOfCoin()
    {
        return _coinsNumberOfPlayer;

    }


    public int GetScoreOfPlayer()
    {
        return _scoreOfPlayer;

    }

    private void IncreaseScore() //hýz deðerine göre puan arttýrma
    {
        float increaseAmount = 0;
        increaseAmount = transform.position.z + 2.2f;
        Debug.Log("increase amount : "+increaseAmount);
    
        _scoreOfPlayer += (int)Mathf.Round(increaseAmount); 
               
    }

    
}
