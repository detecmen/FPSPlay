using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("TrannChanh/ArrowDamageEnemy")]
public class ArrowDamageEnemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision objectHit)
    {
        if (objectHit.collider == null)
        {
            if (objectHit.collider.CompareTag("Wall"))
            {
                CreateArrowEffect(objectHit);
                Destroy(gameObject);
            }
        }
    }

    private void CreateArrowEffect(Collision objectHit)
    {
        ContactPoint contact = objectHit.contacts[0];
        GameObject hole = Instantiate(GameRenerence.Instance.arrowEffectImpactPrefabs, contact.point, Quaternion.LookRotation(contact.normal));
        hole.transform.SetParent(objectHit.gameObject.transform);
    }
}
