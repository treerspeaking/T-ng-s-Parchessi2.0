using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCell : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object being dragged entered this cell's collider
        if (other.CompareTag("DraggableObject"))
        {
            // Perform teleport logic or other actions
            TeleportObject(other.transform);
        }
    }

    void TeleportObject(Transform objToTeleport)
    {
        // Implement logic to teleport the object to this cell
        objToTeleport.position = transform.position;
    }
}
