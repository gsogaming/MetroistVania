using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Activates a console after all tracked enemies in a room are dead.
/// </summary>
public class RoomEnemyConsoleDoorController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Enemies that must be dead before the console becomes active.")]
    public Health[] enemiesToDefeat;
    [Tooltip("Console that becomes active when every enemy is defeated.")]
    public ConsoleDoorTerminal console = null;

    [Header("Events")]
    [Tooltip("Optional events to call when every tracked enemy is defeated.")]
    public UnityEvent roomClearedEvent = new UnityEvent();

    private bool roomCleared = false;

    private void Start()
    {
        if (console != null)
        {
            console.SetActiveState(false);
        }
    }

    private void Update()
    {
        if (roomCleared || !AllEnemiesDefeated())
        {
            return;
        }

        roomCleared = true;

        if (console != null)
        {
            console.SetActiveState(true);
        }

        roomClearedEvent.Invoke();
    }

    private bool AllEnemiesDefeated()
    {
        if (enemiesToDefeat == null || enemiesToDefeat.Length == 0)
        {
            return false;
        }

        foreach (Health enemy in enemiesToDefeat)
        {
            if (enemy != null && enemy.currentHealth > 0)
            {
                return false;
            }
        }

        return true;
    }
}
