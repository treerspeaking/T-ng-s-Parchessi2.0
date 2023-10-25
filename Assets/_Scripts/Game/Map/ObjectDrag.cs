using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectDrag : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Transform targetCell; // The cell where the object will be dropped
    private Vector3 initialPosition; // Store the initial position
    private static List<Vector3> piecePositions = new List<Vector3>();

    void Start()
    {
        initialPosition = transform.position;
        piecePositions.Add(initialPosition);
    }

    void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (targetCell != null && targetCell.CompareTag("Cell"))
        {
            // Calculate the center position of the target cell
            Vector3 targetCenter = targetCell.position;

            // Adjust the position of the object to fit the center of the target cell
            transform.position = new Vector3(targetCenter.x, targetCenter.y, 0);

            // Update the stored position for this piece
            piecePositions[piecePositions.IndexOf(initialPosition)] = transform.position;
        }
        else
        {
            // If not dropped on an "EmptyCell," find and move to the nearest "EmptyCell"
            MoveToNearestEmptyCell();
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, 0);

            // Check for collisions with empty cells
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, Vector2.one * 0.5f, 0);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Cell"))
                {
                    targetCell = collider.transform;
                    return;
                }
            }

            // If no empty cell is found, reset the target cell
            targetCell = null;
        }
    }

    void MoveToNearestEmptyCell()
    {
        // Find all GameObjects with the "EmptyCell" tag
        GameObject[] emptyCells = GameObject.FindGameObjectsWithTag("Cell");

        if (emptyCells.Length > 0)
        {
            // Find the nearest empty cell
            Transform nearestCell = emptyCells[0].transform;
            float nearestDistance = Vector3.Distance(transform.position, nearestCell.position);

            foreach (var cell in emptyCells)
            {
                float distance = Vector3.Distance(transform.position, cell.transform.position);
                if (distance < nearestDistance)
                {
                    nearestCell = cell.transform;
                    nearestDistance = distance;
                }
            }

            // Move the object to the center of the nearest empty cell
            Vector3 targetCenter = nearestCell.position;
            transform.position = new Vector3(targetCenter.x, targetCenter.y, 0);

            // Update the stored position for this piece
            piecePositions[piecePositions.IndexOf(initialPosition)] = transform.position;
        }
    }

    //void ViewPiecePositions()
    //{
    //    for (int i = 0; i < piecePositions.Count; i++)
    //    {
    //        Debug.Log("Piece " + i + " Position: " + piecePositions[i]);
    //    }
    //}
}