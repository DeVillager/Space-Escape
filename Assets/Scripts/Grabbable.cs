using UnityEngine;
using UnityEngine.Events;

public class Grabbable : Interactable
{
    public UnityEvent OnThrow;

    [SerializeField] private Rigidbody rb;
    // public bool grabbed;

    [SerializeField] private float dropPower = 10f;
    [SerializeField] private float throwSpeed = 500f;

    // public PlayerController playerController;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    // public void HandleGrabbing(Transform t)
    // {
    //     if (!grabbed)
    //     {
    //         AttachToPlayer(t);
    //     }
    //     else
    //     {
    //         DetachFromPlayer();
    //     }
    // }
    
    public void AttachToPlayer()
    {
        rb.useGravity = false;
        rb.isKinematic = true;
        transform.position = Player.Instance.grabPoint.position;
        
        // Vector3 scale = transform.localScale;
        transform.parent = Player.Instance.grabPoint;
        // transform.localScale = scale;
        
        // Vector3 v = Player.Instance.body.localScale;
        // v = new Vector3(1/v.x, 1/v.y, 1/v.z);
        // transform.localScale = v;
        grabbed = true;
    }

    public void DetachFromPlayer()
    {
        rb.useGravity = true;
        rb.isKinematic = false;
        
        // Vector3 scale = transform.localScale;
        transform.parent = null;
        // transform.localScale = scale;
        
        // Vector3 v = Player.Instance.body.localScale;
        // v = new Vector3(1/v.x, 1/v.y, 1/v.z);
        // transform.localScale = v;
        
        grabbed = false;
        // rb.AddForce(Player.Instance.controller.velocity.normalized * dropPower);
        // Vector3 v = PlayerController.Instance._velocity;
    }

    public void Throw()
    {
        DetachFromPlayer();
        Vector2 pos = new Vector2(Screen.width / 2 , Screen.height / 2);
        // var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var ray = Camera.main.ScreenPointToRay(pos);
        rb.AddForce(ray.direction.normalized * throwSpeed);
    }

}