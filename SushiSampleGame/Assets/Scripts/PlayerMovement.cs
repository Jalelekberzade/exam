using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float verticalSpeed;
    [SerializeField] public float speedMultiplier;
    [SerializeField] GameObject Simple;
    [SerializeField] GameObject Spray;
    [SerializeField] GameObject Graffiti;
    [SerializeField] GameObject Neon;
    [SerializeField] GameObject Fly;
    [SerializeField] Animator _anim;
    private Touch touch;
    private Rigidbody _rb;
    private Sequence _sequence;
    private Vector3 _direction;
    private Transform _transform;
    private bool _isMoving = true;
    private float count = 0;
    public float SmoothSpeed = 0.01f;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = transform.GetComponentInChildren<Animator>();
        _transform = transform;
        IceCreamHolder.Instance.icecreamList.Add(transform.GetChild(0));
        Simple = this.gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
        Spray = this.gameObject.transform.GetChild(0).transform.GetChild(1).gameObject;
        Graffiti = this.gameObject.transform.GetChild(0).transform.GetChild(2).gameObject;
        Neon = this.gameObject.transform.GetChild(0).transform.GetChild(3).gameObject;
        Fly = this.gameObject.transform.GetChild(0).transform.GetChild(4).gameObject;
    }
    void Update()
    {
        Mathf.Clamp(transform.rotation.z, -25, 25);
    }
    //     transform.position += Vector3.forward * verticalSpeed * Time.deltaTime;
    //     if (Input.touchCount > 0)
    //     {
    //         touch = Input.GetTouch(0);
    //         if (touch.phase == TouchPhase.Moved)
    //         {
    //             transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * SmoothSpeed * Time.deltaTime, transform.position.y, transform.position.z);
    //             transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.6f, 2.6f), transform.position.y, transform.position.z);
    //         }
    //     }
    // 
    void FixedUpdate()
    {
        if (_isMoving) Movement();

    }

    private void Movement()
    {
        _direction = new Vector3(Input.GetAxis("Horizontal") * speedMultiplier, 0, verticalSpeed) * Time.fixedDeltaTime;
        _transform.Translate(_direction.x, 0, _direction.z);
        _anim.SetFloat("TurnFloat", Input.GetAxis("Horizontal"));
        var localPosition = _transform.localPosition;
        localPosition = new Vector3(Mathf.Clamp(localPosition.x, -2.6f, 2.6f), localPosition.y, localPosition.z);
        Mathf.Clamp(transform.rotation.z, -15, 15);
        _transform.localPosition = localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            _isMoving = false;
            EventHolder.Instance.FinishCollider();
            //SoundManager.instance.Stop("Skate");
        }
        //Change to Spray
        if (other.transform.CompareTag("Spray"))
        {
            DamageNum.Instance.ShowNumber(5, transform);
            Simple.gameObject.SetActive(false);
            Spray.gameObject.SetActive(true);
            Graffiti.gameObject.SetActive(false);
            Neon.gameObject.SetActive(false);
            Fly.gameObject.SetActive(false);
            _sequence = DOTween.Sequence();
            _sequence.Join(transform.DOScale(1.3f, 0.1f));
            _sequence.AppendInterval(0.05f);
            _sequence.Join(transform.DOScale(1f, 0.1f));
        }

        //Change to Graffiti
        if (other.transform.CompareTag("Graffiti"))
        {
            DamageNum.Instance.ShowNumber(5, transform);
            Simple.gameObject.SetActive(false);
            Spray.gameObject.SetActive(false);
            Graffiti.gameObject.SetActive(true);
            Neon.gameObject.SetActive(false);
            Fly.gameObject.SetActive(false);
            _sequence = DOTween.Sequence();
            _sequence.Join(transform.DOScale(1.3f, 0.1f));
            _sequence.AppendInterval(0.05f);
            _sequence.Join(transform.DOScale(1f, 0.1f));
        }

        //Change to Neon
        if (other.transform.CompareTag("Neon"))
        {
            DamageNum.Instance.ShowNumber(5, transform);
            Simple.gameObject.SetActive(false);
            Spray.gameObject.SetActive(false);
            Graffiti.gameObject.SetActive(false);
            Neon.gameObject.SetActive(true);
            Fly.gameObject.SetActive(false);
            _sequence = DOTween.Sequence();
            _sequence.Join(transform.DOScale(1.3f, 0.1f));
            _sequence.AppendInterval(0.05f);
            _sequence.Join(transform.DOScale(1f, 0.1f));
        }

        //Change to Fly
        if (other.transform.CompareTag("Fly"))
        {
            DamageNum.Instance.ShowNumber(5, transform);
            Simple.gameObject.SetActive(false);
            Spray.gameObject.SetActive(false);
            Graffiti.gameObject.SetActive(false);
            Neon.gameObject.SetActive(false);
            Fly.gameObject.SetActive(true);
            _sequence = DOTween.Sequence();
            _sequence.Join(transform.DOScale(1.3f, 0.1f));
            _sequence.AppendInterval(0.05f);
            _sequence.Join(transform.DOScale(1f, 0.1f));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Collectable"))
        {
            EventHolder.Instance.IceCreamCollided(other.transform);
            DamageNum.Instance.ShowNumber(other.transform.GetComponent<PriceModel>().Price, transform);
        }
    }
}
