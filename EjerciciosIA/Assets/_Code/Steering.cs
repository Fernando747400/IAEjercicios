using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Steering : MonoBehaviour
{
    [SerializeField]
    private enum MovingState
    {
        SEEK,
        FLEE,
        ARRIVE,
        FLEEEASED
    }

    [Header("Dependencies")]
    [SerializeField] private GameObject _object;
    [SerializeField] private GameObject _target;

    [Header("Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _mass;
    [SerializeField] private float _arrivalDistance;
    [SerializeField] private float _fleeDistance;
   
    [SerializeField] private MovingState _movingState;

    private void Start()
    {
        if (_object == null) _object = this.gameObject;
        if (_mass == 0) Debug.LogWarning("The GameObject " + this.gameObject.name + " has a mass of 0");
        if(_mass <0) Debug.LogWarning("The GameObject " + this.gameObject.name + " has negative mass. It might fly away");
    }

    private void FixedUpdate()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        switch (_movingState)
        {
            case MovingState.SEEK:
                _object.transform.position += SeekObject(_object, _target, _speed, _mass);
                break;

            case MovingState.FLEE:
                _object.transform.position += FleeObject(_object, _target, _speed, _mass);
                break;

            case MovingState.ARRIVE:
                _object.transform.position += Arrival(_object, _target, _speed, _mass, _arrivalDistance);
                break;
            case MovingState.FLEEEASED:
                _object.transform.position += FleeEased(_object, _target, _speed, _mass, _fleeDistance);
                break;
        }
    }

    #region steering behaivors
    public Vector3 SeekObject(GameObject seeker, GameObject target, float speed, float mass)
    {
        Vector3 distance = target.transform.position - seeker.transform.position;
        Vector3 desiredV = distance.normalized * (speed/mass);
        Vector3 currentV = Vector3.zero;
        Vector3 steering = desiredV - currentV;
        currentV += steering;
        return currentV * Time.deltaTime;
    }

    public Vector3 FleeObject(GameObject fleer, GameObject target, float speed, float mass)
    {
        Vector3 distance = fleer.transform.position - target.transform.position;
        Vector3 desiredV = distance.normalized * (speed / mass);
        Vector3 currentV = Vector3.zero;
        Vector3 steering = desiredV - currentV;
        currentV += steering;
        return currentV * Time.deltaTime;
    }

    public Vector3 Arrival(GameObject arriver, GameObject target, float speed, float mass, float arrivalDistance)
    {
        Vector3 distance = target.transform.position - arriver.transform.position;
        float arrivalS = speed * (distance.magnitude / arrivalDistance);
        Vector3 desiredV = distance.normalized * (Mathf.Min(arrivalS, speed)/mass);
        Vector3 currentV = Vector3.zero;
        Vector3 steering = desiredV - currentV;
        currentV += steering;
        return currentV * Time.deltaTime;
    }

    public Vector3 FleeEased(GameObject fleer, GameObject target, float speed, float mass, float fleeDistance)
    {
        Vector3 distance = fleer.transform.position - target.transform.position;
        float fleeS = (speed * (fleeDistance / distance.magnitude)) - speed;
        Vector3 desiredV = distance.normalized * (Mathf.Max(Mathf.Min(fleeS, speed), 0) / mass);
        Vector3 currentV = Vector3.zero;
        Vector3 steering = desiredV - currentV;
        currentV += steering;
        return currentV * Time.deltaTime;
    }

    #endregion
}
