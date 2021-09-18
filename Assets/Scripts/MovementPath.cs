using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    // направление движения
    // 1 - вперед, по часовой
    // -1 - назад, против часовой
    public int movementDirection = 1;

    // к какой точке двигаться
    public int movingTo = 0;

    public Transform[] pathElements;

    public void OnDrawGizmos()
    {
        // проверка пути на пустоту
        if (pathElements == null || pathElements.Length < 2)
        {
            return;
        }

        for (int i = 1; i < pathElements.Length; i++)
        {
            Gizmos.DrawLine(pathElements[i - 1].position, pathElements[i].position);
        }
    }

    public IEnumerator<Transform> GetNextPathPoint()
    {
        if (pathElements == null || pathElements.Length < 1)
        {
            // Выход из корутины
            yield break;
        }

        while (true)
        {
            // Возвращает текущее положение точки
            yield return pathElements[movingTo];

            // Если точка всего одна, выходим
            if (pathElements.Length == 1)
            {
                continue;
            }

            // Если двгигаемся по нарастающей
            if (movingTo <= 0)
            {
                movementDirection = 1;
            }
            // Если двигаемся по убывающей
            else if (movingTo >= pathElements.Length - 1)
            {
                movementDirection = -   1;
            }

            // Диапазон движений от 1 до -1
            movingTo += movementDirection;
        }
    }
}
