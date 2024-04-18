using System;
using UnityEngine;

[RequireComponent(typeof(BotAnimation))]
public class BotMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _turnSpeed = 2f;

    private Vector3 _targetPosition;
    private Vector3 _startPosition;
    private BotAnimation _botAnimation;

    private bool _isBusy;

    public Vector3 StartPosition => _startPosition;

    private void Awake()
    {
        _botAnimation = GetComponent<BotAnimation>();
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (_isBusy)
        {
            Move();
        }
    }

    public void SetTarget(Vector3 target)
    {
        if (target == null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        _targetPosition = target;
        _isBusy = true;
    }

    private void Move()
    {
        Quaternion rotation = Quaternion.LookRotation(_targetPosition);
        transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime),
                                         Quaternion.Lerp(transform.rotation, rotation, _turnSpeed * Time.deltaTime));

        _botAnimation.StartAnimation(_isBusy);
    }
}
