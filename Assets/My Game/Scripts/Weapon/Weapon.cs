using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefabs;
    public Transform arrowSpawn;
    public float arrowVelocity = 100f;
    public float distanceArrow = 200f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireWeapon();
        }
    }
    private void FireWeapon()
    {
        Vector3 shootingDirection = CaculateDirection().normalized;
        GameObject arrow = Instantiate(bulletPrefabs, arrowSpawn.position, Quaternion.identity);
        arrow.transform.forward = shootingDirection;
        arrow.GetComponent<Rigidbody>().AddForce(shootingDirection * arrowVelocity, ForceMode.Impulse);
        

    }

    private Vector3 CaculateDirection()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else 
        {
            targetPoint = ray.GetPoint(distanceArrow);
        }
        Vector3 direction = targetPoint - arrowSpawn.position;

        return direction;
    }
}
