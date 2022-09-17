using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace F747
{
    public class SteeringBehaviors : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private GameObject _steeredObject;
        [SerializeField] private GameObject _target;

        [Header("Settings")]
        [SerializeField] private float _speed;
        [SerializeField] private float _mass;
        [SerializeField] private float _steerForce;
        [SerializeField] private float _arrivalDistance;
        [SerializeField] private float _fleeDistance;

        private Vector3 _velocity;

        public enum MovingState
        {
            SEEK,
            FLEE,
            ARRIVE,
            LEAVE,
            WANDER,
            PURSUIT,
            EVADE
        }

        #region Getters and Setters
        public GameObject SteeredObject { get => _steeredObject; set => _steeredObject = value; }
        public GameObject Target { get => _target; set => _target = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public float Mass { get => _mass; set => _mass = value; }
        public float ArrivalDistance { get => _arrivalDistance; set => _arrivalDistance = value; }
        public float FleeDistance { get => _fleeDistance; set => _fleeDistance = value; }
        public Vector3 Velocity{ get => _velocity; set => _velocity = value; }
        public MovingState State { get; set; }
        #endregion

        private void Start()
        {
            Prepare();
        }

        private void FixedUpdate()
        {
            _velocity += SeekObject(_steeredObject, _target, _velocity, _speed, _steerForce, _mass);
            _steeredObject.transform.position += _velocity * Time.deltaTime;
        }

        public Vector3 SeekObject (GameObject seeker, GameObject target, Vector3 currentVelocity, float speed, float steerForce, float mass)
        {
            Vector3 desired = target.transform.position - seeker.transform.position;
            Vector3 desiredV = desired.normalized * (speed / mass);
            Vector3 steering = desiredV - currentVelocity;
            if (steering.magnitude > steerForce)
            {
                steering.Normalize();
                steering *= steerForce;
            }
            return steering;
        }

        private void Prepare()
        {
            if (_steeredObject == null) _steeredObject = this.gameObject; Debug.LogWarning("No steered object selected, set self as SteeredObject", this);
            if (_target == null) Debug.LogError("No target selected", this);
            if (_mass == 0) Debug.LogWarning("The object has a mass of 0", this);
            if (_mass < 0) Debug.LogWarning("The object has negative mass", this);
            _velocity = Vector3.zero;
        }
    }

}
