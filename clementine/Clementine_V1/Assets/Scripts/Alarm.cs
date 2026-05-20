using UnityEngine;
using System.Collections;

public class Alarm : MonoBehaviour
{
    public static bool alarmActive = false;
    public static Vector3 alarmPosition;

    public float alarmDuration = 5f;

    public void ActivateAlarm()
    {
        alarmPosition = transform.position;
        alarmActive = true;

        Debug.Log("Alarm calisiyo");

        StartCoroutine(StopAlarm());
    }

    IEnumerator StopAlarm()
    {
        yield return new WaitForSeconds(alarmDuration);

        alarmActive = false;
    }
}