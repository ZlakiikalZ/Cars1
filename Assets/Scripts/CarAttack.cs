using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarAttack : MonoBehaviour
{
    [NonSerialized] public int _health = 100; // уровень жизнни для каждого автомобиля

    public float radius = 70f; // задаем для машинок радиус 70, если вражеская машина попадает в этот радиус то они начинают друг друга атаковать
    public GameObject bullet;
    private Coroutine _coroutine = null;

    private void Update()
    {
        DetectCollision();
    }

    private void DetectCollision()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius); //рисует виртуальную сферу и отслеживает все возможные коллайдеры которые попадают в эту сферу

        if (hitColliders.Length == 0 && _coroutine != null) //если враг перестал быть в радиусе, то машинки перестают стрелять
        {
            StopCoroutine(_coroutine);
            _coroutine = null;

            if (gameObject.CompareTag("Enemy"))
                GetComponent<NavMeshAgent>().SetDestination(gameObject.transform.position);
        }

        foreach (var el in hitColliders)
        {
            if ((gameObject.CompareTag("Player") && el.gameObject.CompareTag("Enemy")) ||
                (gameObject.CompareTag("Enemy") && el.gameObject.CompareTag("Player")))  // условие на отслеживание только на вражеский коллайдер
            {
                if (gameObject.CompareTag("Enemy"))
                    GetComponent<NavMeshAgent>().SetDestination(el.transform.position);  // когда враг попадает в область нашей машинки, он начинает ехать навстречу

                if (_coroutine == null)
                    _coroutine = StartCoroutine(StartAttack(el));
            }
        }
    }

    IEnumerator StartAttack(Collider enemyPos)
    {

        GameObject obj = Instantiate(bullet, transform.GetChild(1).position, Quaternion.identity);
        obj.GetComponent<BulletController>().position = enemyPos.transform.position; //снаряды будут выпускаться в то место, где на данный момент есть враг
        yield return new WaitForSeconds(1); //в 1 секунду будет выпускаться 1 снаряд
        StopCoroutine(_coroutine);
        _coroutine = null;
    }
}
