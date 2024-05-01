using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControle : MonoBehaviour
{

    public float rotateSpeed = 10.0f; // �������� �������� ������
    public float speed = 10.0f; // �������� ������������ ������
    public float zoomSpeed = 50.0f; // �������� �����������\���������

    private float _mult = 1f;

    private void Update()
    {
        float hor = Input.GetAxis("Horizontal"); //������� ��� ���������� ������� a � d � �������
        float ver = Input.GetAxis("Vertical"); //������� ��� ���������� ������� w � s � �������


        float rotate = 0;

        if (Input.GetKey(KeyCode.Q))//���� ������������ ������ �� Q �� ������ ����� ��������� �����
            rotate = -1f;
        if (Input.GetKey(KeyCode.E))//���� ������������ ������ �� Q �� ������ ����� ��������� ������
            rotate = 1f;

        _mult = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f; //��������, ���� ����� ����� ����, �� ������ ��������� �� ��������� �2, ����� ������������ ������ ��������


        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * rotate * _mult, Space.World); // ���������� � ������� transform � � ������� Rotate ������� ������ �� ����

        transform.Translate(new Vector3(hor, 0, ver) * Time.deltaTime * _mult * speed, Space.Self);// ������������ � ������� ������� Translate �� ����������� xyz �� ���������� ������

        transform.position += transform.up * zoomSpeed * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel");// ���������\����������� ������ ������� ����

        transform.position = new Vector3( 
            transform.position.x,
            Mathf.Clamp(transform.position.y, -20f, 30f),
            transform.position.z); //����������� ��� ������
    }
}
