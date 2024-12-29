using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _deadzone = 0.1f;

    private HeroInput _heroInput;
    private Vector2 _moveDirection;
    private bool _isMoving;

    private void Awake()
    {
        _heroInput = new HeroInput();

        _heroInput.Hero.Move.started += _ => _isMoving = true;
        _heroInput.Hero.Move.canceled += _ => _isMoving = false;
    }

    private void OnEnable()
    {
        _heroInput.Enable();
    }

    private void OnDisable()
    {
        _heroInput.Disable();
    }

    private void OnDestroy()
    {
        // Clean up event subscriptions
        // _heroInput.Hero.Move.canceled -= OnMovementCanceled;
        // _heroInput.Hero.Move.started -= OnMovementStarted;
        _heroInput.Dispose();
    }

    public void Move()
    {
        // Only read input if we're actually moving
        if (_isMoving)
        {
            _moveDirection = _heroInput.Hero.Move.ReadValue<Vector2>();
        }
        else
        {
            _moveDirection = Vector2.zero;
        }

        if (_moveDirection.sqrMagnitude < _deadzone)
        {
            return;
        }

        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;
        Vector3 offset = new Vector3(_moveDirection.x, _moveDirection.y, 0f) * scaledMoveSpeed;
        transform.Translate(offset);
    }
}
