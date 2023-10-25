using DG.Tweening;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public float MoveDuration = 1.5f;
    public Ease MoveEase = Ease.Linear;

    private Tween StartDirectShot(Transform targetPosition)
    {
        // Create a bullet GameObject
        var projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
        
        // Use DoTween to move the bullet to the target position
        return projectile.transform.DOMove(targetPosition.position, MoveDuration)
            .SetEase(MoveEase)
            .OnComplete(() => Destroy(projectile)); // Destroy the bullet when the animation is complete
    }
}