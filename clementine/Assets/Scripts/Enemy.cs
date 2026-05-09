using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform doorPoint;
    public Transform player;

void Awake()
{
    agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
}
    public float catchDistance = 2f;

    void Start()
    {
        agent.SetDestination(doorPoint.position);
    }

    void Update()
    {
        // 🔊 Alarm varsa oraya git
        if (Alarm.alarmActive)
        {
            agent.SetDestination(Alarm.alarmPosition);
        }
        else
        {
            agent.SetDestination(doorPoint.position);
        }

        // 💀 Yakalama sistemi
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < catchDistance)
        {
            LoseGame();
        }
    }

    void LoseGame()
    {
        Debug.Log("YAKALANDIN!");
        Time.timeScale = 0f;
    }
}