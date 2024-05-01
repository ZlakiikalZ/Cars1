using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControle : MonoBehaviour
{

    public float rotateSpeed = 10.0f; // скорость вращения камеры
    public float speed = 10.0f; // скорость передвижения камеры
    public float zoomSpeed = 50.0f; // скорость приближения\отдаления

    private float _mult = 1f;

    private void Update()
    {
        float hor = Input.GetAxis("Horizontal"); //функция для считывания нажатий a и d и стрелок
        float ver = Input.GetAxis("Vertical"); //функция для считывания нажатий w и s и стрелок


        float rotate = 0;

        if (Input.GetKey(KeyCode.Q))//если пользователь нажмет на Q то камера будет вращаться влево
            rotate = -1f;
        if (Input.GetKey(KeyCode.E))//если пользователь нажмет на Q то камера будет вращаться вправо
            rotate = 1f;

        _mult = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f; //проверка, если зажат левый шифт, то камера вращается со скоростью х2, иначе возвращается пержня скорость


        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * rotate * _mult, Space.World); // обращаемся к функции transform и с помощью Rotate вращаем камеру по осям

        transform.Translate(new Vector3(hor, 0, ver) * Time.deltaTime * _mult * speed, Space.Self);// передвижение с помощью функции Translate по координатам xyz из полученных данных

        transform.position += transform.up * zoomSpeed * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel");// Отдаление\приблежение камеры скролом мыши

        transform.position = new Vector3( 
            transform.position.x,
            Mathf.Clamp(transform.position.y, -20f, 30f),
            transform.position.z); //ограничения для камеры
    }
}
