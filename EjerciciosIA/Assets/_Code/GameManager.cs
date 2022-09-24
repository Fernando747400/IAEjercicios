using F747;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] GameObject _playerObject;
    [SerializeField] Vector2 _xPlayableLimits;
    [SerializeField] Vector2 _yPlayableLimits;

    private SteeringBehaviors _playerSteer;

    public static GameManager Instance;

    private List<GameObject> _enemiesList = new List<GameObject>();
    private List<GameObject> _enemiesLeftList = new List<GameObject>();
    private Queue<GameObject> _enemiesQueue = new Queue<GameObject>();
    private List<GameObject> _enemiesFollowing = new List<GameObject>();

    private float _oneThird;
    private float _twoThirds;
    private int _currentInteracted = 0;
    private bool _oneThirdInvoked;
    private bool _twoThirdsInvoked;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Prepare();
    }

    private void Prepare()
    {
        GetEnemeniesList();
        SubscribeToEnemeyHit();
        _oneThird = _enemiesList.Count / 3;
        _twoThirds = _oneThird * 2;
        _oneThirdInvoked = false;
        _twoThirdsInvoked = false;
        _playerSteer = _playerObject.GetComponent<SteeringBehaviors>();
    }

    private void GetEnemeniesList()
    {
        GameObject[] aux = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var item in aux)
        {
            _enemiesList.Add(item);
            _enemiesLeftList.Add(item);
            item.GetComponent<EnemyController>().XPlayableLimits = _xPlayableLimits;
            item.GetComponent<EnemyController>().YPlayableLimits = _yPlayableLimits;
        }      
    }

    private void SubscribeToEnemeyHit()
    {
        foreach (var item in _enemiesList)
        {
            item.GetComponent<EnemyController>().hitPlayerEvent += FollowLast;
            item.GetComponent<EnemyController>().caughtPlayerEvent += DropFirstItem;
        }
    }

    private void FollowLast(GameObject enemy)
    {
        GameObject lead = _enemiesFollowing.Count != 0 ? _enemiesFollowing.Last() : _playerObject;
        enemy.GetComponent<EnemyController>().FollowState(lead);
        UpdateLists(enemy);
    } 

    private void FollowLeader(GameObject enemy)
    {
        enemy.GetComponent<SteeringBehaviors>().FollowTime = _playerSteer.FollowTime + _enemiesFollowing.Count; 
        enemy.GetComponent<EnemyController>().FollowState(_playerObject);
        UpdateLists(enemy);
    }

    private void UpdateLists(GameObject enemy)
    {
        enemy.GetComponent<SteeringBehaviors>().Speed = _playerSteer.Speed - 0.5f;
        enemy.GetComponent<SpriteRenderer>().color = Color.green;
        _enemiesFollowing.Add(enemy);
        _enemiesQueue.Enqueue(enemy);
        _enemiesLeftList.Remove(enemy);
        _currentInteracted++;
        Debug.Log("Current interacted " + _currentInteracted);
        CheckEnemiesLeft();
    }

    private void CheckEnemiesLeft()
    {
        if (!_oneThirdInvoked && _currentInteracted >= _oneThird)
        {
            _oneThirdInvoked = true;
            foreach (var item in _enemiesLeftList)
            {
                SteeringBehaviors enemySteer = item.GetComponent<SteeringBehaviors>();
                item.GetComponent<SpriteRenderer>().color = Color.cyan;
                enemySteer.Target = _playerObject;
                enemySteer.State = SteeringBehaviors.MovingState.FLEE;
                enemySteer.Speed = _playerSteer.Speed/2;
            }
        }

        if (!_twoThirdsInvoked && _currentInteracted >= _twoThirds)
        {
            _twoThirdsInvoked = true;
            foreach (var item in _enemiesLeftList)
            {
                SteeringBehaviors enemySteer = item.GetComponent<SteeringBehaviors>();
                item.GetComponent<SpriteRenderer>().color = Color.red;
                enemySteer.Target = _playerObject;
                enemySteer.State = SteeringBehaviors.MovingState.EVADE;
                enemySteer.Speed = _playerSteer.Speed * 0.8f;               
            }
            ChangeFirstThree();
        }

        if (_enemiesLeftList.Count == 0)
        {
            Debug.Log("Game over");
        }
    }

    private void ChangeFirstThree()
    {
        for (int i = 0; i < 3; i++)
        {
            SteeringBehaviors enemySteer = _enemiesQueue.Peek().GetComponent<SteeringBehaviors>();
            enemySteer.Target = _playerObject;
            _enemiesQueue.Peek().GetComponent<EnemyController>().Target = _playerObject;
            enemySteer.State = SteeringBehaviors.MovingState.PURSUIT;
            Debug.Log(enemySteer.State + "This is the state of " + enemySteer.gameObject.name);
            enemySteer.Speed = _playerSteer.Speed * Random.Range(0.55f, 0.85f);
            _enemiesQueue.Peek().GetComponent<SpriteRenderer>().color = Color.yellow;
            _enemiesList.Remove(_enemiesQueue.Peek());
            _enemiesQueue.Dequeue();
        }
        _enemiesQueue.Peek().GetComponent<EnemyController>().FollowState(_playerObject);
    }

    private void DropFirstItem()
    {
        if (_enemiesQueue.Count == 0) return;
        SteeringBehaviors enemySteer = _enemiesQueue.Peek().GetComponent<SteeringBehaviors>();
        enemySteer.State = SteeringBehaviors.MovingState.IDLE;
        enemySteer.Target = null;
        enemySteer.gameObject.GetComponent<EnemyController>().Target = null;
        _enemiesFollowing.Remove(_enemiesQueue.Peek());
        _enemiesQueue.Dequeue();
        enemySteer.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        _enemiesLeftList.Add(enemySteer.gameObject);
        _enemiesQueue.Peek().GetComponent<EnemyController>().FollowState(_playerObject);
    }
}
