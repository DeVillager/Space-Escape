using UnityEngine;
using UnityEngine.Events;

public class Grabbable : Interactable
{
    public UnityEvent OnThrow;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private float throwSpeed = 500f;

    private float minSpeed = 0f;
    private float maxSpeed = 600f;
    private float maxDistance = 3f;
    private float rotationSpeed = 6f;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (grabbed)
        {
            float distance = Vector3.Distance(Player.Instance.grabPoint.position, transform.position);

            float currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, distance / maxDistance);
            currentSpeed *= Time.fixedDeltaTime;

            Vector3 direction = Player.Instance.grabPoint.position - transform.position;
            rb.velocity = direction.normalized * currentSpeed;

            Quaternion lookRot = Quaternion.LookRotation(Player.Instance.CameraPosition - transform.position);
            lookRot = Quaternion.Slerp(transform.rotation, lookRot, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(lookRot);

            if (distance > maxDistance)
                DetachFromPlayer();
        }
    }

    public void AttachToPlayer()
    {
        grabbed = true;
    }

    public void DetachFromPlayer()
    {
        grabbed = false;
    }

    public void Throw()
    {
        DetachFromPlayer();
        Vector2 pos = new Vector2(Screen.width / 2 , Screen.height / 2);
        var ray = Camera.main.ScreenPointToRay(pos);
        rb.AddForce(ray.direction.normalized * throwSpeed);
    }

}