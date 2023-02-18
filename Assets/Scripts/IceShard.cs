using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class IceShard : MonoBehaviour
{
    [SerializeField] private float _minDropTimer;
    [SerializeField] private float _maxDropTimer;

    private Rigidbody2D _rigidbody2D;
    private GravityController _gravityController;

    private Vector3 _initialPosition;

    private Quaternion _initialRotation;

    [SerializeField] private UnityEvent _onIceBeforeDrop;

    [SerializeField] private UnityEvent _onIceBlasted;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _gravityController = GetComponent<GravityController>();

        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

    public void Drop()
    {
        StartCoroutine(DropIE());
    }

    private IEnumerator  DropIE()
    {
        _onIceBeforeDrop?.Invoke();
        
        yield return new WaitForSeconds(Random.Range(_minDropTimer, _maxDropTimer));

        _rigidbody2D.isKinematic = false;
        _gravityController.enabled = true;
    }

    public void ReCreateIceShard()
    {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;

        _rigidbody2D.isKinematic = true;
        _gravityController.enabled = false;
        
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _onIceBlasted?.Invoke();

        gameObject.SetActive(false);
    }
}