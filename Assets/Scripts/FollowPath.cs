using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    // Используемый путь
    public MovementPath MyPath;

    // Скорость движения
    public float speed = 1;

    // На какое расстояние должен подъехать объект к точке,
    // чтобы распознать, что это точка
    public float maxDistance = .1f;

    // Проверка точек
    private IEnumerator<Transform> pointInPath;

    // Start is called before the first frame update
    void Start()
    {
        // Проверка - прикрепили ли мы путь
        if (MyPath == null)
        {
            Debug.Log("Необходимо применить путь");
            return;
        }

        // Обращение к корутине
        // есть точка или нет
        pointInPath = MyPath.GetNextPathPoint();

        // Получение следующей точки пути
        pointInPath.MoveNext();

        //  есть ли точка, к которой передвигаться
        if (pointInPath.Current == null)
        {
            Debug.Log("Необходимо добавить точки в путь");
            return;
        }

        // Объект помещается на первую точку пути
        transform.position = pointInPath.Current.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Провека отсутствия пути
        if (pointInPath == null || pointInPath.Current == null)
        {
            return;
        }

        // Движение объекта к следующей точке
        transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * speed);

        // Проверка - достаточно ли мы близко к точке, чтобы начать движение к следующей точке
        var distanceSquare = (transform.position - pointInPath.Current.position).sqrMagnitude;

        // Достаточно ли мы близко
        // Теорема Пифагора
        if (distanceSquare < maxDistance * maxDistance)
        {
            // Двигаемся к следующей точке
            pointInPath.MoveNext();
        }

    }
}
