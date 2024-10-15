using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{   
    public enum ShotingMode
    {
        Single,
        Burst,
        Auto
    }
    [Header("Change Weapon")]
    public ShotingMode currentShotingMode = ShotingMode.Auto;
    public int magazineSize = 20;
    public bool allowReset;
    [Range(0f, 2f)]
    public float shotingDelay = 0.5f;
    [Range(0f, 2f)]
    public float spreadIntensity = 2f;

    [Header("Fx Muzespas")]
    public ParticleSystem muzlerFlash;
    [Header("Audio Weapon")]
    public AudioClip shotClip;
    public AudioClip reloadClip;

    [Header("Arrow Prefabs")]
    public GameObject bulletPrefabs;
    public Transform arrowSpawn;
    public float arrowVelocity = 1000f;
    public float distanceArrow = 500f;


    public float arrowPrefabsTime = 3f;
    
    private int arrowLeft;
    private bool isShoting, readyShot;
    private int isShotingId;
    private int isReloadingId;
    private Animator anim;
    private bool isReloading = false;

    private void Awake()
    {
        arrowLeft = magazineSize;
        readyShot = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isShotingId = Animator.StringToHash("IsBowShot");
        isReloadingId = Animator.StringToHash("IsReloading");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentShotingMode == ShotingMode.Auto)
        {
            isShoting = Input.GetKey(KeyCode.Mouse0);
        }
        else if ((currentShotingMode == ShotingMode.Burst) || (currentShotingMode == ShotingMode.Single))
        {
            isShoting = Input.GetKeyDown(KeyCode.Mouse0);
        }
        if (isShoting && readyShot && arrowLeft > 0)
        {
            FireWeapon();
        }
        if (Input.GetKeyDown(KeyCode.R) && arrowLeft < magazineSize && !isReloading)
        {
            Reload();
        }
    }

    private void Reload()
    {
        anim.SetTrigger(isReloadingId);
        AudioManager.Instance.PlaysfxPlayer(reloadClip);
    }

    private void FireWeapon()
    {   
        readyShot = false;
        arrowLeft--;
        AudioManager.Instance.PlaysfxPlayer(shotClip);
        anim.SetTrigger(isShotingId);
        muzlerFlash.Play();
        Vector3 shootingDirection = CaculateDirection().normalized;
        GameObject arrow = Instantiate(bulletPrefabs, arrowSpawn.position, Quaternion.identity);
        arrow.transform.forward = shootingDirection;
        // arrow.GetComponent<Rigidbody>().AddForce(shootingDirection * arrowVelocity, ForceMode.Impulse);
        arrow.GetComponent<Rigidbody>().velocity = shootingDirection * arrowVelocity * Time.deltaTime;
        StartCoroutine(DestroyArrowAfterTime(arrow, arrowPrefabsTime));
        if (allowReset)
        {
            Invoke("ResetShot", shotingDelay);
            allowReset = false;
        }
        else
        {   
            if (arrowLeft > 0)
            {
                Invoke("FireWeapon", shotingDelay);
            }
        }
    }
    void ResetShot()
    {
        readyShot = true;
        allowReset = true;
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
