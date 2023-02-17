using System.Collections;
using DefaultNamespace;
using MoreMountains.Feedbacks;
using UnityEngine;

public class IceShard : MonoBehaviour
{
    [SerializeField] private float _minDropTimer;
    [SerializeField] private float _maxDropTimer;

    private Rigidbody2D _rigidbody2D;
    private GravityController _gravityController;

    private Vector3 _initialPosition;

    private Quaternion _initialRotation;

    [SerializeField] private MMFeedbacks _onIceBeforeDrop;

    [SerializeField] private MMFeedbacks _onIceBlasted;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _gravityController = GetComponent<GravityController>();

        _initialPosition = transform.position;
        _initialRotation = transform.rotation;

        Drop();
    }

    public void Drop()
    {
        StartCoroutine(DropIE());
    }

    private IEnumerator  DropIE()
    {
        _onIceBeforeDrop?.PlayFeedbacks();
        
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
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _onIceBlasted.PlayFeedbacks();
        
        gameObject.SetActive(false);
    }
}