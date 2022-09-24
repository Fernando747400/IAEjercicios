using F747;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(SteeringBehaviors))]
public class EnemyController : MonoBehaviour
{
    private SteeringBehaviors _steeringEnemy;
    private GameObject _target;

    public SteeringBehaviors SteeringEnemy { get => _steeringEnemy; }
    public GameObject Target { get => _target; set => _target = value; }

    private void Start()
    {
        Prepare();
    }

    private void Update()
    {
        if(_target != null) CheckDistance();
    }

    public void FollowState(GameObject target)
    {
        _target = target;
        _steeringEnemy.Target = _target;
        _steeringEnemy.State = SteeringBehaviors.MovingState.FOLLOW;
    }

    private void CheckDistance()
    {
        if (Vector3.Distance(this.transform.position, _target.transform.position) <= 0.06f)
        {
            _steeringEnemy.State = SteeringBehaviors.MovingState.IDLE;
        }
        else
        {
            _steeringEnemy.State = SteeringBehaviors.MovingState.FOLLOW;
        }
    }

    private void Prepare()
    {
        _steeringEnemy = GetComponent<SteeringBehaviors>();
    }
}
