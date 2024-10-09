using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Arrow Prefabs")]
    public GameObject bulletPrefabs;
    public Transform arrowSpawn;
    public float arrowVelocity = 1000f;
    public float distanceArrow = 500f;

    public float spreadIntensity = 2f;
    public float arrowPrefabsTime = 3f;

    [Header("Fx Muzespas")]
    public ParticleSystem muzlerFlash;
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
        // muzlerFlash.Play();
        Vector3 shootingDirection = CaculateDirection().normalized;
        GameObject arrow = Instantiate(bulletPrefabs, arrowSpawn.position, Quaternion.identity);
        arrow.transform.forward = shootingDirection;
        // arrow.GetComponent<Rigidbody>().AddForce(shootingDirection * arrowVelocity, ForceMode.Impulse);
        arrow.GetComponent<Rigidbody>().velocity = shootingDirection * arrowVelocity * Time.deltaTime;
        StartCoroutine(DestroyArrowAfterTime(arrow, arrowPrefabsTime));

    }
    IEnumerator DestroyArrowAfterTime(GameObject arrow, float arrowTime)
    {
        yield return new WaitForSeconds(arrowTime);
        Destroy(arrow);
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
        float z = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        
        return direction + new Vector3(0, y, z);
    }
}
