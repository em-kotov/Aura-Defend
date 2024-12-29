using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    public Color AuraColor { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Border border))
        {
            Deactivate();
        }
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void AddForce(Vector3 direction)
    {
        _rigidbody.AddForce(direction, ForceMode.Impulse);
    }

    public void SetAuraColor(Color color)
    {
        AuraColor = color;
        GetComponent<SpriteRenderer>().color = color;
    }
}
