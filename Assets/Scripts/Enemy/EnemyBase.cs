using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains settings for and handles the control of an enemy
/// </summary>
public abstract class EnemyBase : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("How fast this enemy moves")]
    public float moveSpeed = 2f;

    [Header("Hit Response Settings")]
    [Tooltip("Whether this enemy can be pushed back when hit.")]
    public bool canReceiveKnockback = true;

    private Coroutine knockbackRoutine = null;


    /// <summary>
    /// Enum to track which state the enemy is in
    /// </summary>
    public enum EnemyState { Walking, Dead, Idle }

    [Tooltip("The state the enemy is in for animation playback")]
    public EnemyState enemyState;

    /// <summary>
    /// Description:
    /// Standard Unity function called once before update
    /// Input: 
    /// none
    /// Return: 
    /// void (no return)
    /// </summary>
    protected virtual void Start()
    {
        Setup();
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called once every frame
    /// Input: 
    /// none
    /// Return: 
    /// void (no return)
    /// </summary>
    protected virtual void Update()
    {
        // Every frame, get the desired movement of this enemy, then move it.
        Vector3 movement = GetMovement();
        MoveEnemy(movement);
    }

    /// <summary>
    /// Description:
    /// Sets up this enemy
    /// Input: 
    /// none
    /// Return: 
    /// void (no return)
    /// </summary>
    protected virtual void Setup()
    {

    }

    /// <summary>
    /// Description:
    /// Returns the desired movement for this frame
    /// Input: none
    /// Return: 
    /// Vector3
    /// </summary>
    /// <returns>Vector3: The vector representing the movement this enemy will take this frame</returns>
    protected virtual Vector3 GetMovement()
    {
        return Vector3.zero;
    }

    /// <summary>
    /// Description:
    /// Moves the enemy according to a movement vector
    /// Input: 
    /// Vector3 movement
    /// Return: 
    /// void (no return)
    /// </summary>
    /// <param name="movement">The vector representing the movement this enemy will make.</param>
    protected virtual void MoveEnemy(Vector3 movement)
    {
        transform.position = transform.position + movement;
    }

    public virtual void ApplyKnockback(Vector2 direction, float distance, float duration)
    {
        if (!canReceiveKnockback || distance <= 0.0f || duration <= 0.0f)
        {
            return;
        }

        if (knockbackRoutine != null)
        {
            StopCoroutine(knockbackRoutine);
        }

        knockbackRoutine = StartCoroutine(KnockbackRoutine(direction, distance, duration));
    }

    private IEnumerator KnockbackRoutine(Vector2 direction, float distance, float duration)
    {
        Vector3 startPosition = transform.position;
        Vector2 normalizedDirection = direction.sqrMagnitude > 0.0f ? direction.normalized : Vector2.right;
        Vector3 endPosition = startPosition + (Vector3)(normalizedDirection * distance);
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / duration);
            transform.position = Vector3.Lerp(startPosition, endPosition, progress);
            yield return null;
        }

        transform.position = endPosition;
        knockbackRoutine = null;
    }

    
}
