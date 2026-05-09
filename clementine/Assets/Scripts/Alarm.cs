using UnityEngine;

public class Alarm : MonoBehaviour
{
    public static bool alarmActive = false;
    public static Vector3 alarmPosition;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateAlarm();
        }
    }

    void ActivateAlarm()
    {
        alarmActive = true;
        alarmPosition = transform.position;

        Debug.Log("Alarm çalisti!");
    }
}