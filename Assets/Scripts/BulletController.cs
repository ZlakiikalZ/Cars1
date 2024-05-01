using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [NonSerialized] public Vector3 position;
    public float speed = 30f; // скорость снаряла
    public int damage = 20;

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, step); // назначение пути снаряда

        if (transform.position == position) //если снаряд достиг точки назначениие, то он уничтожается
            Destroy(gameObject);
    }

    //отслеживание соприкосновения снаряда с другим автомобилем
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") || other.CompareTag("Player")) //нанесение урона
        {
            CarAttack attack = other.GetComponent<CarAttack>();
            attack._health -= damage; //нанесение урона в 20 единиц

            Transform healthBar = other.transform.GetChild(0).transform; // уменьшение шкалы здоровья машинки
            healthBar.localScale = new Vector3(
                healthBar.localScale.x - 0.3f,
                healthBar.localScale.z,
                healthBar.localScale.y);

            if (attack._health <= 0)
                Destroy(other.gameObject); //уничтожение объекта если его здоровье меньше или равно 0 
        }
    }
}
