using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damageAmount;

    public Transform stonehitPrefab;
    public Transform bloodsplatPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            collision.gameObject.GetComponent<BatAI>().TakeDamage(damageAmount);

            ContactPoint contact = collision.contacts[0];
            
            // Rotate the object so that the y-axis faces along the normal of the surface
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(bloodsplatPrefab, pos, rot);
            Destroy(gameObject);
        }
        else
        {
            ContactPoint contact = collision.contacts[0];

            // Rotate the object so that the y-axis faces along the normal of the surface
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(stonehitPrefab, pos, rot);
            Destroy(gameObject);
        }
    }
}
