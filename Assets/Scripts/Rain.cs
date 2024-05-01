using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    // ������������� ����� �� ���� � ����, � ����������� ����� ����� ��������� �������

    public Light dirLight;
    private ParticleSystem _ps;
    private bool _isRain = false; //������ � ���� ���������� ���� �� ������ ����� ��� ���

    private void Start()
    {
        _ps = GetComponent<ParticleSystem>();
        StartCoroutine(Weather()); //������� StartCoroutine ��������� ����� �������������, ����� ������������ ���������� �������
    }

    private void Update()
    {
        if (_isRain && dirLight.intensity > 0.2f) // �������� �������� �������� ����� ������ ������������� ����� (����������)
            LightIntensity(-1);
        else if (!_isRain && dirLight.intensity < 0.5f) // �������� �������� �������� ����� ������ ������������� ����� (����������)
            LightIntensity(1);
    }

    private void LightIntensity(int mult) // ����� �������� ���������� ������������� �����
    {
        dirLight.intensity += 0.1f * Time.deltaTime * mult;
    }

    IEnumerator Weather() //�������� Coroutine ��� ������� ���������� �������
    {
        while (true)       //�������� ������������ �����
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(10f, 15f)); //���������� � �����, ��� ������ ���������� � ������ ������ � ���������� ���������� �������, ���� ����� ��� �� ����� ������ ���������� �������, � ������

            if (_isRain)        //���� ����� ����, �� ���������� � _ps � ������������� �����
                _ps.Stop();
            else                //���� ����� �� ����, �� ���������� � _ps � ��������� �����
                _ps.Play();

            _isRain = !_isRain; // ������ �������� ���� �� ��������������� ���� ��������� �������� �����
        }
    }

}
