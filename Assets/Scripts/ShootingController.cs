using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingController : MonoBehaviour
{
    public PlayerController playerController;
    public Animator weaponAnimator;
    public GameObject projectilePrefab;
    public Transform lineStartPoint;
    public LayerMask mask;

    private static Queue<GameObject> projQueue;
    private static int queueMaxSize = 100;

    private void Start()
    {
        projQueue = new Queue<GameObject>();
    }

    public void Shoot()
    {
        Ray ray = playerController.headCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        Vector3 hitPoint;
        GameObject hitObject = null;
        bool hit;

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, mask, QueryTriggerInteraction.Ignore))
        {
            hitPoint = hitInfo.point;
            hitObject = hitInfo.collider.gameObject;
            hit = true;
        }
        else
        {
            hitPoint = playerController.headCamera.transform.position + (ray.direction * 100f);
            hit = false;
        }

        // Create the projectile
        PhysicalProjectile proj = CreateProjectile(hitPoint, hitObject, !hit);

        proj.StartFadeLine(lineStartPoint.position);

        // Start reload
        weaponAnimator.SetTrigger("Reload");
    }

    private PhysicalProjectile CreateProjectile(Vector3 position, GameObject parent, bool autodelete)
    {
        GameObject newObject = Instantiate(projectilePrefab, position, playerController.headCamera.transform.rotation);

        if (parent != null)
            newObject.transform.SetParent(parent.transform);

        PhysicalProjectile proj = newObject.GetComponent<PhysicalProjectile>();

        if (autodelete)
            proj.DestroyOnFadeEnd();
        else
            projQueue.Enqueue(newObject);

        // Delete from projectile queue if too long
        if (projQueue.Count > queueMaxSize)
            Destroy(projQueue.Dequeue());

        return proj;
    }
}
