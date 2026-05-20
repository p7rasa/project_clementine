using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Guarding,
        Investigating,
        Chasing,
        Returning
    }

    public EnemyState currentState;

    public NavMeshAgent agent;

    public Transform doorPoint;
    public Transform player;

    public float catchDistance = 2f;
    public float viewDistance = 8f;

    private Vector3 alarmPosition;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        ChangeState(EnemyState.Guarding);
    }

    void Update()
    {
        if (player == null) return;

        HandleTransitions();

        switch (currentState)
        {
            case EnemyState.Guarding:
                GuardingUpdate();
                break;

            case EnemyState.Investigating:
                InvestigatingUpdate();
                break;

            case EnemyState.Chasing:
                ChasingUpdate();
                break;

            case EnemyState.Returning:
                ReturningUpdate();
                break;
        }
    }

    void HandleTransitions()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < viewDistance)
        {
            if (currentState != EnemyState.Chasing)
                ChangeState(EnemyState.Chasing);

            return;
        }

        if (Alarm.alarmActive && currentState == EnemyState.Guarding)
        {
            alarmPosition = Alarm.alarmPosition;
            ChangeState(EnemyState.Investigating);
        }
    }

    void ChangeState(EnemyState newState)
    {
        if (currentState == newState) return;

        currentState = newState;

        agent.ResetPath();
        agent.isStopped = false;

        switch (newState)
        {
            case EnemyState.Guarding:
                agent.SetDestination(doorPoint.position);
                Debug.Log("Kapıyı koruyor");
                break;

            case EnemyState.Investigating:
                agent.SetDestination(alarmPosition);
                Debug.Log("Alarmı araştırıyor");
                break;

            case EnemyState.Chasing:
                Debug.Log("Oyuncuyu kovalıyor");
                break;

            case EnemyState.Returning:
                agent.SetDestination(doorPoint.position);
                Debug.Log("Göreve dönüyor");
                break;
        }
    }

    bool ReachedDestination()
    {
        if (agent.pathPending) return false;

        if (agent.remainingDistance > agent.stoppingDistance)
            return false;

        return true;
    }

    void GuardingUpdate()
    {
        // Sabit guard
    }

    void InvestigatingUpdate()
    {
        agent.SetDestination(alarmPosition);

        if (ReachedDestination())
        {
          
            Alarm.alarmActive = false;
            ChangeState(EnemyState.Returning);
        }
    }

    void ChasingUpdate()
    {
        agent.SetDestination(player.position);

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < catchDistance)
        {
            LoseGame();
        }

        if (distance > viewDistance * 1.5f)
        {
            ChangeState(EnemyState.Returning);
        }
    }

    void ReturningUpdate()
    {
        if (ReachedDestination())
        {
            ChangeState(EnemyState.Guarding);
        }
    }

    void LoseGame()
    {
        Debug.Log("YAKALANDIN!");
        GameManager.instance.LoseGame();
    }
}