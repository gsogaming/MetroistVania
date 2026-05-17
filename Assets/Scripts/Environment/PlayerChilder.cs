using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will make the player a child of whatever it is attached to
/// It is meant to be used with the moving platform to keep the player moving along with the platform
/// </summary>
public class PlayerChilder : MonoBehaviour
{
    [Header("Platform Check Settings")]
    [Tooltip("The solid platform collider the player must be standing above. If unset, this script will find a non-trigger Collider2D on this object.")]
    public Collider2D platformCollider = null;
    [Tooltip("How far below the platform top the player's feet can be and still count as standing on it.")]
    public float topCheckTolerance = 0.08f;

    private void Awake()
    {
        FindPlatformCollider();
    }

    /// <summary>
    /// Description:
    /// Built-in Unity function that is called whenever a trigger collider is entered by another collider
    /// Input:
    /// Collider2D collision
    /// return:
    /// void (no return)
    /// </summary>
    /// <param name="collision">The collider that entered the trigger</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MakeAChildOfAttachedTransform(collision);
    }

    /// <summary>
    /// Description:
    /// Built-in Unity function that is called whenever a trigger collider stays inside another collider
    /// Input:
    /// Collider2D collision
    /// return:
    /// void (no return)
    /// </summary>
    /// <param name="collision">The collider that is still in the trigger</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        MakeAChildOfAttachedTransform(collision);
    }

    /// <summary>
    /// Description:
    /// Built-in Unity function that is called whenever a collider exits a trigger collider
    /// Input:
    /// Collider2D collision
    /// return:
    /// void (no return)
    /// </summary>
    /// <param name="collision">The collider that exited the trigger</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        DeChild(collision);
    }

    /// <summary>
    /// Description:
    /// Makes the passed collider a child of the transform that this script is attached to
    /// Works only for the player character but could be expanded to work for non-player characters
    /// Input:
    /// Collider2D collision
    /// Return:
    /// void (no return)
    /// </summary>
    /// <param name="collision">The collision to make no longer a child</param>
    private void DeChild(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject.transform.parent == transform)
        {
            collision.gameObject.transform.SetParent(null);
        }
    }


    /// <summary>
    /// Description:
    /// Makes the passed collider a child of the transform that this script is attached to
    /// Input:
    /// Collider2D collision
    /// Return:
    /// void (no return)
    /// </summary>
    /// <param name="collision">The collision whos transform will be childed</param>
    private void MakeAChildOfAttachedTransform(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerIsStandingOnPlatform(collision))
        {
            collision.gameObject.transform.SetParent(transform, true);
        } 
    }

    private bool PlayerIsStandingOnPlatform(Collider2D collision)
    {
        if (platformCollider == null)
        {
            FindPlatformCollider();
        }

        if (platformCollider == null)
        {
            return true;
        }

        return collision.bounds.min.y >= platformCollider.bounds.max.y - topCheckTolerance;
    }

    private void FindPlatformCollider()
    {
        if (platformCollider != null)
        {
            return;
        }

        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D attachedCollider in colliders)
        {
            if (!attachedCollider.isTrigger)
            {
                platformCollider = attachedCollider;
                return;
            }
        }
    }
}
