using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	public GameObject hitParticleEffect;


    public float bulletSpeed = 100f;
    private Transform target; 

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        Vector3 targetPosition;
        if (target == null)
        {
            targetPosition = TypingManager.tm.oldActiveWordPosition;
            Destroy(gameObject, 0.1f);
            return;
        }
        else
        {
            targetPosition = target.position;
        }
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        GameObject pe = Instantiate(hitParticleEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
