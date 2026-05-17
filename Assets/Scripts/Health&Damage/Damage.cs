using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the dealing of damage to health components.
/// </summary>
public class Damage : MonoBehaviour
{
    [Header("Team Settings")]
    [Tooltip("The team associated with this damage")]
    public int teamId = 0;

    [Header("Damage Settings")]
    [Tooltip("How much damage to deal")]
    public int damageAmount = 1;
    [Tooltip("Whether or not to destroy the attached game object after dealing damage")]
    public bool destroyAfterDamage = true;
    [Tooltip("Whether or not to apply damage when triggers collide")]
    public bool dealDamageOnTriggerEnter = false;
    [Tooltip("Whether or not to apply damage when triggers stay, for damage over time")]
    public bool dealDamageOnTriggerStay = false;
    [Tooltip("Whether or not to apply damage on non-trigger collider collisions")]
    public bool dealDamageOnCollision = false;

    [Header("Effect Settings")]
    [Tooltip("Visual Effect to show when attacked")]
    public GameObject damageVFX;
    public GameObject damageSFX;
    public Transform vfxLocation;
    [Tooltip("Optional particle effect to spawn when this damage successfully hits.")]
    public ParticleSystem hitParticles;

    [Header("Hit Response Settings")]
    [Tooltip("Whether successful enemy hits briefly freeze time.")]
    public bool hitPauseOnEnemyHit = true;
    [Tooltip("How long the hit pause lasts, in real seconds.")]
    public float hitPauseDuration = 0.05f;
    [Tooltip("Whether successful enemy hits push enemies away from this damage source.")]
    public bool knockbackEnemiesOnHit = true;
    [Tooltip("How far enemies are pushed when hit.")]
    public float enemyKnockbackDistance = 0.25f;
    [Tooltip("How long the enemy knockback movement lasts.")]
    public float enemyKnockbackDuration = 0.08f;

    private static float hitPauseRestoreTime = 0.0f;
    private static float timeScaleBeforeHitPause = 1.0f;

    /// <summary>
    /// Description:
    /// Standard unity function called whenever a Collider2D enters any attached 2D trigger collider
    /// Input:
    /// Collider2D collision
    /// Return:
    /// void (no return)
    /// </summary>
    /// <param name="collision">The collider that entered the trigger<</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dealDamageOnTriggerEnter)
        {
            DealDamage(collision.gameObject);
        }
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called every frame a Collider2D stays in any attached 2D trigger collider
    /// Input:
    /// Collider2D collision
    /// Return:
    /// void (no return)
    /// </summary>
    /// <param name="collision">The collider that is still in the trigger</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (dealDamageOnTriggerStay)
        {
            DealDamage(collision.gameObject);
        }
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called when a Collider2D hits another Collider2D (non-triggers)
    /// Input:
    /// Collision2D collision
    /// Return:
    /// void (no return)
    /// </summary>
    /// <param name="collision">The Collider2D that has hit this Collider2D</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (dealDamageOnCollision)
        {
            DealDamage(collision.gameObject);
        }
    }

    /// <summary>
    /// Description:
    /// This function deals damage to a health component 
    /// if the collided with gameobject has a health component attached AND it is on a different team.
    /// Input:
    /// GameObject collisionGameObject
    /// Return:
    /// void (no return)
    /// </summary>
    /// <param name="collisionGameObject">The game object that has been collided with</param>
    private void DealDamage(GameObject collisionGameObject)
    {
        Health collidedHealth = collisionGameObject.GetComponent<Health>();
        if (collidedHealth != null)
        {
            if (collidedHealth.teamId != this.teamId)
            {
                collidedHealth.TakeDamage(damageAmount);
                EnemyBase hitEnemy = collisionGameObject.GetComponent<EnemyBase>();
                ApplyHitResponse(hitEnemy, collisionGameObject.transform.position);
                SpawnHitEffects(collisionGameObject.transform.position);

                if (destroyAfterDamage)
                {
                    DestroyAfterDamage(hitEnemy != null);
                }
            }
        }
    }

    private void ApplyHitResponse(EnemyBase hitEnemy, Vector3 hitPosition)
    {
        if (hitEnemy == null)
        {
            return;
        }

        if (hitPauseOnEnemyHit && hitPauseDuration > 0.0f)
        {
            StartCoroutine(HitPause(hitPauseDuration));
        }

        if (knockbackEnemiesOnHit && enemyKnockbackDistance > 0.0f && enemyKnockbackDuration > 0.0f)
        {
            Vector2 knockbackDirection = hitPosition - transform.position;
            hitEnemy.ApplyKnockback(knockbackDirection, enemyKnockbackDistance, enemyKnockbackDuration);
        }
    }

    private IEnumerator HitPause(float duration)
    {
        if (Time.timeScale > 0.0f)
        {
            timeScaleBeforeHitPause = Time.timeScale;
        }

        hitPauseRestoreTime = Mathf.Max(hitPauseRestoreTime, Time.unscaledTime + duration);
        Time.timeScale = 0.0f;

        while (Time.unscaledTime < hitPauseRestoreTime)
        {
            yield return null;
        }

        Time.timeScale = timeScaleBeforeHitPause;
    }

    private void SpawnHitEffects(Vector3 hitPosition)
    {
        Vector3 effectPosition = vfxLocation != null ? vfxLocation.position : hitPosition;

        if (damageVFX != null)
        {
            Instantiate(damageVFX, effectPosition, transform.rotation, null);
        }

        if (hitParticles != null)
        {
            Instantiate(hitParticles, effectPosition, transform.rotation, null);
        }

        if (damageSFX != null)
        {
            Instantiate(damageSFX, transform.position, transform.rotation, null);
        }
    }

    private void DestroyAfterDamage(bool hitEnemy)
    {
        if (hitEnemy && hitPauseOnEnemyHit && hitPauseDuration > 0.0f)
        {
            Destroy(this.gameObject, hitPauseDuration);
            return;
        }

        Destroy(this.gameObject);
    }
}
