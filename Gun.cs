using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Rigidbody projectile;
    public float shootSpeed;
    private float attackStart = 0f;
    public float attackCooldown = 1f;

    public GameObject muzzleFlash;
    public GameObject muzzleflashLight;
    public Transform attackPoint;

    public int range = 100;
    public Camera fpsCam;
    public int damage = 1;

    public GameObject stonehitPrefab;
    public GameObject bloodsplatPrefab;

    public BatAI bAI;

    AudioSource shootSound;

    void Start()
    {
        shootSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && (Time.time > attackStart + attackCooldown))
        {
            Shoot();        
        }
    }

    void Shoot()
    {
        Rigidbody clone;
        Vector3 startPosition;
        startPosition = transform.position;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            clone = Instantiate(projectile, startPosition, transform.rotation);
            clone.velocity = (hit.point - transform.position).normalized * shootSpeed;

            BatAI target = hit.transform.GetComponent<BatAI>();
            if (target != null)
            {
                target.TakeDamage(damage);
                GameObject bloodsplat = Instantiate(bloodsplatPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                
            }

            GameObject stonehit = Instantiate(stonehitPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(stonehit, 2f);
        }       

        
        
        

        
        attackStart = Time.time;

        Instantiate(muzzleFlash, attackPoint.position, transform.rotation);
        Instantiate(muzzleflashLight, attackPoint.position, transform.rotation);

        shootSound.Play();
    }
}
