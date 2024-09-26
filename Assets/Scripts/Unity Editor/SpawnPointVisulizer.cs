using UnityEngine;

[ExecuteInEditMode]
public class SpawnPointVisulizer : MonoBehaviour
{
    [SerializeField] private Color color = Color.red;
    [SerializeField] private float radius = 1f;

    private CircleCollider2D circleCollider;

    private void OnDrawGizmos()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = radius;

        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
