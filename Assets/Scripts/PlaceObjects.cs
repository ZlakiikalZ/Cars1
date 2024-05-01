using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    public LayerMask layer;
    public float rotateSpeed = 60f;

    public void Start()
    {
        PositionObject();
    }

    private void PositionObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //функция отслеживания курсора мыши в игры по координатам

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, layer))  //выпускает луч от курсора и если он попадает на поверхность ground то дом можно будет построить
            transform.position = hit.point;
    }

    private void Update()
    {
        PositionObject();

        if (Input.GetMouseButtonDown(0)) // если пользователь нажмет на правую кнопку мыши, то удаляется скрипт и строиться здание
        {
            gameObject.GetComponent<AutoCarCreate>().enabled = true;

            Destroy(gameObject.GetComponent<PlaceObjects>());
        }

        // вращение для добавления объектов
        if (Input.GetKey(KeyCode.LeftShift)) // если зажат левый шифт то происходит вращение объекта
            transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
    }
}
