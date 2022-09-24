using F747;
using System;
using UnityEngine;

[RequireComponent(typeof(SteeringBehaviors))]
public class EnemyController : MonoBehaviour
{
    public event Action<GameObject> hitPlayerEvent;
    public event Action caughtPlayerEvent;

    private SteeringBehaviors _steeringEnemy;
    private GameObject _target;
    private Vector2 _xPlayableLimits;
    private Vector2 _yPlayableLimits;

    public SteeringBehaviors SteeringEnemy { get => _steeringEnemy; }
    public GameObject Target { get => _target; set => _target = value; }
    public Vector2 XPlayableLimits { get => _xPlayableLimits; set => _xPlayableLimits = value; }
    public Vector2 YPlayableLimits { get => _yPlayableLimits; set => _yPlayableLimits = value; }

    private void Start()
    {
        Prepare();
    }

    private void Update()
    {
        //if(_target != null) CheckDistance();
    }

    private void LateUpdate()
    {
        ClampPosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _target == null && _steeringEnemy.State != SteeringBehaviors.MovingState.PURSUIT)
        {
            hitPlayerEvent?.Invoke(this.gameObject);
        } else if (collision.CompareTag("Player") && _steeringEnemy.State == SteeringBehaviors.MovingState.PURSUIT)
        {
            caughtPlayerEvent?.Invoke();
        }
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

    private void ClampPosition()
    {
        float x = Math.Clamp(this.transform.position.x, _xPlayableLimits.x, _xPlayableLimits.y);
        float y = Math.Clamp(this.transform.position.y, _yPlayableLimits.x, _yPlayableLimits.y);
        this.transform.position = new Vector3(x,y,0);
    }

    private void Prepare()
    {
        _steeringEnemy = GetComponent<SteeringBehaviors>();
        _xPlayableLimits = this.transform.position;
        _yPlayableLimits = this.transform.position;
    }
}
