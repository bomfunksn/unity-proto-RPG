using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    public Collider2D[] targetColliders;


    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targerCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;

    public void PerformAttack()
    {

        foreach (var target in GetDetectedColliders())
        {
            Debug.Log("Atacking:" + target.name);
        }
    }

    private Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targerCheckRadius, whatIsTarget);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targerCheckRadius);
    }
}
