using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace F747
{
    public class SteeringBehaviors : MonoBehaviour
    {
        #region Properties
        [Header("Dependencies")]
        [SerializeField] private GameObject _steeredObject;
        [SerializeField] private GameObject _target;

        [Header("Behavior")]
        [SerializeField] private MovingState _state;

        [Header("General Settings")]
        [SerializeField] private float _speed;
        [SerializeField] private float _mass;
        [SerializeField] private float _steerForce;

        [Header("Arrive Settings")]
        [SerializeField] private float _arrivalDistance;

        [Header("Leave Settings")]
        [SerializeField] private float _leaveDistance;

        [Header("Wander Settings")]
        [SerializeField] private float _wanderDistance;
        [SerializeField] private float _wanderRadius;
        [SerializeField] private float _wanderTime;

        [Header("Pursuit / Evade Settings")]
        [SerializeField] private float _pursuitTime;



        private Vector3 _velocity;
        private float _wanderTimer = 0f;
        private float _wanderAngle;

        public enum MovingState
        {
            SEEK,
            FLEE,
            ARRIVE,
            LEAVE,
            WANDER,
            PURSUIT,
            EVADE,
            IDLE
        }
        #endregion

        #region Getters and Setters
        public GameObject SteeredObject { get => _steeredObject; set => _steeredObject = value; }
        public GameObject Target { get => _target; set => _target = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public float Mass { get => _mass; set => _mass = value; }
        public float ArrivalDistance { get => _arrivalDistance; set => _arrivalDistance = value; }
        public float LeaveDistance { get => _leaveDistance; set => _leaveDistance = value; }
        public float WanderDistance { get => _wanderDistance; set => _wanderDistance = value; }
        public float WanderRadius { get => _wanderRadius; set => _wanderRadius = value; }
        public float WanderTimer { get => _wanderTime; set => _wanderTime = value; }
        public float AheadTime { get => _pursuitTime; set => _pursuitTime = value; }
        public Vector3 Velocity{ get => _velocity; set => _velocity = value; }
        public MovingState State { get => _state; set => _state = value; }
        #endregion

        private void Start()
        {
            Prepare();
        }

        private void FixedUpdate()
        {
            MoveObject();           
        }

        #region SteerMethods
        public Vector3 SeekObject (GameObject seeker, GameObject target, Vector3 currentVelocity, float speed, float steerForce, float mass)
        {
            Vector3 distance = target.transform.position - seeker.transform.position;
            Vector3 desiredV = distance.normalized * (speed / mass);
            Vector3 steering = desiredV - currentVelocity;
            if (steering.magnitude > steerForce)
            {
                steering.Normalize();
                steering *= steerForce;
            }
            return steering;
        }

        public Vector3 FleeObject(GameObject fleer, GameObject target, Vector3 currentVelocity, float speed, float steerForce, float mass)
        {
            Vector3 distance = fleer.transform.position - target.transform.position;
            Vector3 desiredV = distance.normalized * (speed / mass);
            Vector3 steering = desiredV - currentVelocity;
            if (steering.magnitude > steerForce)
            {
                steering.Normalize();
                steering *= steerForce;
            }
            return steering;
        }

        public Vector3 Arrival(GameObject arriver, GameObject target, Vector3 currentVelocity, float speed, float steerForce, float mass, float arrivalDistance)
        {
            Vector3 distance = target.transform.position - arriver.transform.position;
            float arrivalS = speed * (distance.magnitude / arrivalDistance);
            Vector3 desiredV = distance.normalized * (Mathf.Min(arrivalS,speed) / mass);
            Vector3 steering = desiredV - currentVelocity;
            if (steering.magnitude > steerForce)
            {
                steering.Normalize();
                steering *= steerForce;
            }
            return steering;
        }

        public Vector3 Leaving(GameObject leaver, GameObject target, Vector3 currentVelocity, float speed, float steerForce, float mass, float fleeDistance)
        {
            Vector3 distance = leaver.transform.position - target.transform.position;
            float fleeS = (speed * (fleeDistance / distance.magnitude)) - speed;
            Vector3 desiredV = distance.normalized * (Mathf.Max(Mathf.Min(fleeS, speed),0) / mass);
            Vector3 steering = desiredV - currentVelocity;
            if (steering.magnitude > steerForce)
            {
                steering.Normalize();
                steering *= steerForce;
            }
            return steering;
        }

        public Vector3 Wander(GameObject wanderer, Vector3 currentVelocity, float speed, float steerForce, float mass, float wanderDistance, float wanderRadius, float wanderAngle)
        {
            Vector3 wanderCircle = wanderer.transform.position + (currentVelocity.normalized * wanderDistance);
            Vector3 rotated = Quaternion.AngleAxis(wanderAngle, Vector3.forward) * _velocity.normalized;
            Vector3 target = wanderCircle + (rotated * wanderRadius / 8.1f);

            Vector3 distance = target - wanderer.transform.position;
            Vector3 desiredV = distance.normalized * (speed / mass);
            Vector3 steering = desiredV - currentVelocity;
            if (steering.magnitude > steerForce)
            {
                steering.Normalize();
                steering *= steerForce;
            }
            return steering;
        }

        public Vector3 Pursuit(GameObject pursuier, GameObject target, Vector3 currentVelocity, Vector3 targetVelocity, float speed, float steerForce, float mass, float timeAhead)
        {
            Vector3 futureTargetPosition = target.transform.position + (targetVelocity * timeAhead);

            Vector3 distance = futureTargetPosition - pursuier.transform.position;
            Vector3 desiredV = distance.normalized * (speed / mass);
            Vector3 steering = desiredV - currentVelocity;
            if (steering.magnitude > steerForce)
            {
                steering.Normalize();
                steering *= steerForce;
            }
            return steering;
        }

        public Vector3 Evade(GameObject pursuier, GameObject target, Vector3 currentVelocity, Vector3 targetVelocity, float speed, float steerForce, float mass, float timeAhead)
        {
            Vector3 futureTargetPosition = target.transform.position + (targetVelocity * timeAhead);

            Vector3 distance = pursuier.transform.position - futureTargetPosition;
            Vector3 desiredV = distance.normalized * (speed / mass);
            Vector3 steering = desiredV - currentVelocity;
            if (steering.magnitude > steerForce)
            {
                steering.Normalize();
                steering *= steerForce;
            }
            return steering;
        }
        #endregion

        private void MoveObject()
        {
            switch (_state)
            {
                case MovingState.SEEK:
                    _velocity += SeekObject(_steeredObject, _target, _velocity, _speed, _steerForce, _mass);
                    break;

                case MovingState.FLEE:
                    _velocity += FleeObject(_steeredObject, _target, _velocity, _speed, _steerForce, _mass);
                    break;

                case MovingState.ARRIVE:
                    _velocity += Arrival(_steeredObject, _target, _velocity, _speed, _steerForce, _mass, _arrivalDistance);
                    break;

                case MovingState.LEAVE:
                    _velocity += Leaving(_steeredObject, _target, _velocity, _speed, _steerForce, _mass, _leaveDistance);
                    break;

                case MovingState.WANDER:
                    if (_wanderTimer > _wanderTime)
                    {
                        _wanderTimer = 0f;
                        _wanderAngle = Random.Range(-180f, 180f);
                    }
                    else _wanderTimer += Time.deltaTime;

                    _velocity += Wander(_steeredObject, _velocity, _speed, _steerForce, _mass, _wanderDistance, _wanderRadius, _wanderAngle);
                    break;

                case MovingState.PURSUIT:
                    _velocity += Pursuit(_steeredObject, _target, _velocity, _target.GetComponent<SteeringBehaviors>().Velocity, _speed, _steerForce, _mass, _pursuitTime);
                    break;

                case MovingState.EVADE:
                    _velocity += Evade(_steeredObject, _target, _velocity, _target.GetComponent<SteeringBehaviors>().Velocity, _speed, _steerForce, _mass, _pursuitTime);
                    break;

                case MovingState.IDLE:
                    _velocity = Vector3.zero;
                    Debug.Log("This GameObject is set to Idle " + this.name);
                    break;
            }

            _steeredObject.transform.position += _velocity * Time.deltaTime;
        }

        private void Prepare()
        {
            if (_steeredObject == null) _steeredObject = this.gameObject; Debug.LogWarning("No steered object selected, set self as SteeredObject", this);
            if (_target == null) Debug.LogError("No target selected", this);
            if (_mass == 0) Debug.LogWarning("The object has a mass of 0", this);
            if (_mass < 0) Debug.LogWarning("The object has negative mass", this);
            _velocity = new Vector3(0.1f,0,0);
        }
    }

}
