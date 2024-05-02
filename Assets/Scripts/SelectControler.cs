using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class SelectControler : MonoBehaviour
{
    public GameObject cube;
    private Camera _cam;
    public LayerMask layer, layerMask;
    private GameObject _cubeSelection;
    private RaycastHit _hit;
    public List<GameObject> players;

    private void Awake()
    {
        _cam = GetComponent<Camera>();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && players.Count > 0) //когда нажмется левая клавиша мыши то передвигаются игроки выбранные
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition); //функция отслеживания курсора мыши в игры 

            if (Physics.Raycast(ray, out RaycastHit agentTarget, 1000f, layer))
                foreach (var el in players)
                    el.GetComponent<NavMeshAgent>().SetDestination(agentTarget.point);
        }

        if (Input.GetMouseButtonDown(0))
        {
            foreach (var el in players)
                if (el != null)
                    el.transform.GetChild(0).gameObject.SetActive(false);

            players.Clear(); // Очищение списка выбранных машиных

            Ray ray = _cam.ScreenPointToRay(Input.mousePosition); //функция отслеживания курсора мыши в игры 

            if (Physics.Raycast(ray, out _hit, 1000f, layer))  //выпускает луч от курсора и если он попадает на поверхность ground то создается "куб" для выделения объектов
                _cubeSelection = Instantiate(cube, new Vector3(_hit.point.x, 1, _hit.point.z), Quaternion.identity);   //
        }

        // в момент отпускания кнопки мыши куб разрушается:
        if (_cubeSelection) //если куб уже существует, то можем увеличить его размеры по тому, куда двигается наша мышка
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition); //функция отслеживания курсора мыши в игры 

            if (Physics.Raycast(ray, out RaycastHit hitDrag, 1000f, layer))  //выпускает луч от курсора и если он попадает на поверхность ground то создается "куб" для выделения объектов
            {
                float xScale = (_hit.point.x - hitDrag.point.x) * -1;
                float zScale = _hit.point.z - hitDrag.point.z;

                if (xScale < 0.0f && zScale < 0.0f)
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                else if (xScale < 0.0f)
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                else if (zScale < 0.0f)
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(180, 0, 0));
                else
                    _cubeSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

                _cubeSelection.transform.localScale = new Vector3(Mathf.Abs(xScale), 1, Mathf.Abs(zScale));
            }

        }


        if (Input.GetMouseButtonUp(0) && _cubeSelection)
        {
            RaycastHit[] hits = Physics.BoxCastAll(  //выделение всех машинок которые попали в куб
                _cubeSelection.transform.position,
                _cubeSelection.transform.localScale,
                Vector3.up,
                Quaternion.identity,
                0,
                layerMask);

            foreach (var el in hits)
            {
                if (el.collider.CompareTag("Enemy")) continue;

                players.Add(el.transform.gameObject);
                el.transform.GetChild(0).gameObject.SetActive(true); //при выделениее объекта над ним появляется полоса здоровья
            }

            Destroy(_cubeSelection);
        }
    }
}
