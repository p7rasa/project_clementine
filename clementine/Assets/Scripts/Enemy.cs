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
        CheckPlayerDistance();

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

    void ChangeState(EnemyState newState)
    {
        currentState = newState;

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

                Debug.Log("Oyuncuyu kovalıyor!");
                break;

            case EnemyState.Returning:

                agent.SetDestination(doorPoint.position);

                Debug.Log("Göreve dönüyor");
                break;
        }
    }

    void GuardingUpdate()
    {
        if (Alarm.alarmActive)
        {
            alarmPosition = Alarm.alarmPosition;

            ChangeState(EnemyState.Investigating);
        }
    }

    void InvestigatingUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance < 1.5f)
        {
            if (!Alarm.alarmActive)
            {
                ChangeState(EnemyState.Returning);
            }
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
    }

    void ReturningUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance < 1.5f)
        {
            ChangeState(EnemyState.Guarding);
        }
    }

    void CheckPlayerDistance()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < viewDistance)
        {
            ChangeState(EnemyState.Chasing);
        }
    }

    void LoseGame()
    {
        Debug.Log("YAKALANDIN!");
        Time.timeScale = 0f;
    }
}