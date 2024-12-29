using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public Enemy GetClosestEnemy(float radius, Vector3 position)
    {
        float sqrDistance;
        float sqrClosestDistance = 50f * 50f;
        int enemyLayerIndex = 7;
        Enemy closestEnemy = null;

        Collider[] colliders = Physics.OverlapSphere(position, radius,
                                        LayerMaskConverter.GetLayerMask(enemyLayerIndex));

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Enemy enemy))
            {
                sqrDistance = Vector3Extensions.GetSqrDistance(position, enemy.transform.position);

                if (sqrDistance <= sqrClosestDistance)
                {
                    sqrClosestDistance = sqrDistance;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }
}
