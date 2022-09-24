using F747;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteeringBehaviors))]
public class PlayerController : MonoBehaviour
{

    private SteeringBehaviors _steeringPlayer;
    private Camera _mainCamera;
    private Vector3 _mousePos;
    private GameObject _target;

    private void Start()
    {
        Prepare();
    }

    private void Update()
    {
        ReadMouseInput();
        CheckDistance();
    }

    private void CheckDistance()
    {
        if (Vector3.Distance(this.transform.position, _target.transform.position) <= 0.06f)
        {
            _steeringPlayer.State = SteeringBehaviors.MovingState.IDLE;
        } else
        {
            _steeringPlayer.State = SteeringBehaviors.MovingState.ARRIVE;
        }
    }

    private void ReadMouseInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GetMousePosition();
            _target.transform.position = _mousePos;
        }
    }

    private void GetMousePosition()
    {
        _mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _mousePos.z = 0f;
    }

    private void Prepare()
    {
        _mainCamera = Camera.main;
        _steeringPlayer = GetComponent<SteeringBehaviors>();
        _target = new GameObject("MouseTargetObject");
        _steeringPlayer.Target = _target;
    }
}
