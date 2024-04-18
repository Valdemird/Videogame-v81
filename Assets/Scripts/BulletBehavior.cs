using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float speed;
    private float weaponRange;
    private Vector3 startPosition;
    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Launch(Vector3 direction, float weaponRange, Vector3 startPosition)
    {
        this.weaponRange = weaponRange;
        this.startPosition = startPosition;
        rb.AddForce(direction.normalized * speed, ForceMode.Impulse);
        rb.useGravity = false;
    }

    public void Update()
    {
        if (Vector3.Distance(startPosition, transform.position) > weaponRange)
        {
            BulletPool.Instance.ReturnBullet(this.gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        BulletPool.Instance.ReturnBullet(this.gameObject);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyBehavior>().Hit();
        }
        Debug.Log(collision.gameObject.name);
    }
}
