using UnityEngine;

public class PickUpAndDrop : MonoBehaviour
{
    public Transform holdPosition; // Position to hold the picked object
    public float pickupRange = 5f; // Range within which an object can be picked up

    private GameObject currentObject; // The currently picked-up object

    void Update()
    {
        // Check for input to pick up an object
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentObject == null) // Only pick up if not already holding an object
            {
                TryPickUpObject();
            }
        }

        // Check for input to drop the object
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentObject != null) // Only drop if holding an object
            {
                DropObject();
            }
        }
    }

    private void TryPickUpObject()
    {
        // Detect nearby objects with a collider
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupRange);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Pickup")) // Ensure the object has the "Pickup" tag
            {
                PickUpObject(collider.gameObject);
                break;
            }
        }
    }

    private void PickUpObject(GameObject obj)
    {
        currentObject = obj;
        Rigidbody rb = currentObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Disable physics on the picked-up object
        }

        // Attach the object to the hold position
        currentObject.transform.position = holdPosition.position;
        currentObject.transform.parent = holdPosition;
    }

    private void DropObject()
    {
        Rigidbody rb = currentObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // Re-enable physics for the dropped object
        }

        // Detach the object from the player
        currentObject.transform.parent = null;
        currentObject = null;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the pickup range in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}
