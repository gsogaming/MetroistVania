using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyShooter : MonoBehaviour
{
    public Light2D myLight;
    public Transform player; // Reference to the player's transform
    public GameObject projectilePrefab; // Prefab of the projectile    
    


    public float fireRate = 2f; // Time interval between each projectile instantiation
    private float lastTimeShotTaken;
    public GameObject fireEffectSFX;
    public GameObject fireEffectVFX;
    public float firingRange;

    private float timeSincePlayerLeftTheRange;
    public float lightDelay;

    private void Start()
    {
        // Find the player object using a tag (assuming the player has a tag "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;         

    }

    private void Update()
    {
        RotateTowardsThePlayer();
        TryToFire();

    }

    private void TryToFire()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= firingRange)
        {
            myLight.gameObject.SetActive(true);
            timeSincePlayerLeftTheRange = 0;

            if (lastTimeShotTaken + fireRate < Time.time)
            {
                Fire();
            }
        }
        else
        {
            timeSincePlayerLeftTheRange += Time.deltaTime;

            if (timeSincePlayerLeftTheRange >= lightDelay)
            {
                myLight.gameObject.SetActive(false);
            }
        }

        
    }

    

    private void RotateTowardsThePlayer()
    {

        if (player != null)
        {
            // Calculate the direction from the enemy to the player        
            Vector3 directionToPlayer = player.position - transform.position;


            // Calculate the angle in degrees between the direction and the right vector
            float angle = Mathf.Atan2(-directionToPlayer.x, directionToPlayer.y) * Mathf.Rad2Deg;

            // Create a target rotation using the calculated angle
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

            // Smoothly rotate the object towards the player
            transform.rotation = targetRotation;
        }
    }

    public void Fire()
    {
        lastTimeShotTaken = Time.time;
        Instantiate(projectilePrefab, transform.position, transform.rotation);
        SpawnFireEffect();
        
    }

    private void SpawnFireEffect()
    {
        if (fireEffectSFX != null)
        {
            Instantiate(fireEffectSFX, transform.position, transform.rotation, null);
            
        }

        if (fireEffectVFX != null)
        {
            Instantiate(fireEffectVFX, transform.position, transform.rotation, null);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, firingRange);
    }


}
