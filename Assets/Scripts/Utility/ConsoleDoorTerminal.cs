using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Lets a console unlock and open a door when the player interacts with it.
/// </summary>
public class ConsoleDoorTerminal : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Door opened by this console.")]
    public Door targetDoor = null;
    [Tooltip("Optional interactable message display on this console.")]
    public Interactable interactable = null;

    [Header("Interaction")]
    [Tooltip("Keyboard key used to interact with the console.")]
    public Key interactKey = Key.E;
    [Tooltip("Whether this console can currently open the door.")]
    public bool isActive = false;
    [Tooltip("Whether this console should become inactive after opening the door once.")]
    public bool disableAfterUse = true;

    [Header("Messages")]
    [TextArea]
    public string lockedMessage = "Console locked. Eliminate nearby threats.";
    [TextArea]
    public string activeMessage = "Console online. Press E to open the door.";
    [TextArea]
    public string openedMessage = "Door override accepted.";

    private bool playerInRange = false;
    private bool hasOpenedDoor = false;

    private void Awake()
    {
        if (interactable == null)
        {
            interactable = GetComponent<Interactable>();
        }
    }

    private void Start()
    {
        RefreshMessage();
    }

    private void Update()
    {
        if (!playerInRange || hasOpenedDoor)
        {
            return;
        }

        if (Keyboard.current != null && Keyboard.current[interactKey].wasPressedThisFrame)
        {
            TryUseConsole();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            RefreshMessage();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void SetActiveState(bool active)
    {
        isActive = active;
        RefreshMessage();
    }

    public void TryUseConsole()
    {
        if (!isActive || targetDoor == null)
        {
            RefreshMessage();
            return;
        }

        KeyRing.AddKey(targetDoor.doorID);
        targetDoor.AttemptToOpen();
        hasOpenedDoor = targetDoor.isOpen;

        if (hasOpenedDoor)
        {
            SetMessage(openedMessage);

            if (disableAfterUse)
            {
                isActive = false;
            }
        }
    }

    private void RefreshMessage()
    {
        if (hasOpenedDoor)
        {
            SetMessage(openedMessage);
            return;
        }

        SetMessage(isActive ? activeMessage : lockedMessage);
    }

    private void SetMessage(string message)
    {
        if (interactable != null)
        {
            interactable.SetMessage(message);
        }
    }
}
