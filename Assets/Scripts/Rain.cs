using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    // Первоначально доджь не идет в игре, а запускается через время благодаря скрипту

    public Light dirLight;
    private ParticleSystem _ps;
    private bool _isRain = false; //хранит в себе информацию идет ли сейчас дождь или нет

    private void Start()
    {
        _ps = GetComponent<ParticleSystem>();
        StartCoroutine(Weather()); //функция StartCoroutine запускает дождь автоматически, через определенные промежутки времени
    }

    private void Update()
    {
        if (_isRain && dirLight.intensity > 0.2f) // создание плавного перехода между сменой интенсивности цвета (уменьшение)
            LightIntensity(-1);
        else if (!_isRain && dirLight.intensity < 0.5f) // создание плавного перехода между сменой интенсивности цвета (увеличение)
            LightIntensity(1);
    }

    private void LightIntensity(int mult) // метод плавного уменьшения интенсивности цвета
    {
        dirLight.intensity += 0.1f * Time.deltaTime * mult;
    }

    IEnumerator Weather() //создание Coroutine для задания промежутка времени
    {
        while (true)       //создание бесконечного цикла
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(10f, 15f)); //обращаемся к Юнити, где оттуда обращаемся к классу Рандом и орпеделяем промежуток рандома, чтоб дождь шел не через ровные промежутки времени, а разные

            if (_isRain)        //если дождь идет, то обращаемся к _ps и останавливаем дождь
                _ps.Stop();
            else                //если дождь не идет, то обращаемся к _ps и запускаем дождь
                _ps.Play();

            _isRain = !_isRain; // меняем значение поля на противоположное чтоб зациклить анимация дождя
        }
    }

}
