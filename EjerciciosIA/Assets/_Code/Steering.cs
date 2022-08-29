using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject _object;
    [SerializeField] private GameObject _target;

    [Header("Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private bool _isSeeking;
    [SerializeField] private float _arrivalDistance;
    [SerializeField] private enum MovingState
    {
        SEEK,
        FLEE,
        ARRIVE
    }
    [SerializeField] private MovingState _movingState;

    private void Update()
    {
        switch (_movingState)
        {
            case MovingState.SEEK:
                _object.transform.position += (SeekObject(_object, _target, _speed));
                break;

           case MovingState.FLEE:
                _object.transform.position += (FleeObject(_object, _target, _speed));
                break;

            case MovingState.ARRIVE:
                _object.transform.position += Arrival(_object, _target, _speed, _arrivalDistance);
                break;
        }  
    }

    public Vector3 SeekObject(GameObject seeker, GameObject target, float speed)
    {
        Vector3 distance = target.transform.position - seeker.transform.position;
        Vector3 desiredV = distance.normalized * speed;
        Vector3 currentV = Vector3.zero;
        Vector3 steering = desiredV - currentV;
        currentV += steering;
        return currentV * Time.deltaTime;
    }

    public Vector3 FleeObject(GameObject fleer, GameObject target, float speed)
    {
        Vector3 distance = fleer.transform.position - target.transform.position;
        Vector3 desiredV = distance.normalized * speed;
        Vector3 currentV = Vector3.zero;
        Vector3 steering = desiredV - currentV;
        currentV += steering;
        return currentV * Time.deltaTime;
    }

    public Vector3 Arrival(GameObject arriver, GameObject target, float speed, float arrivalDistance)
    {
        Vector3 distance = target.transform.position - arriver.transform.position;
        float arrivalS = speed * (distance.magnitude / arrivalDistance);
        Vector3 desiredV = distance.normalized * Mathf.Min(arrivalS, speed);
        Vector3 currentV = Vector3.zero;
        Vector3 steering = desiredV - currentV;
        currentV += steering;
        return currentV * Time.deltaTime;
    }
}
