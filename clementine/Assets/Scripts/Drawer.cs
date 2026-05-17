using UnityEngine;
using System.Collections;

public class Drawer : MonoBehaviour
{
    public Vector3 openOffset = new Vector3(0,0,0.4f);
    public float speed = 3f;

    private Vector3 closedPos;
    private Vector3 openPos;

    private bool isOpen = false;
    private bool moving = false;

    void Start()
    {
        closedPos = transform.localPosition;
        openPos = closedPos + openOffset;
    }

    public void ToggleDrawer()
    {
        if (!moving)
        {
            StartCoroutine(MoveDrawer());
        }
    }

    IEnumerator MoveDrawer()
    {
        moving = true;

        Vector3 target = isOpen ? closedPos : openPos;

        while(Vector3.Distance(transform.localPosition, target) > 0.01f)
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                target,
                speed * Time.deltaTime
            );

            yield return null;
        }

        transform.localPosition = target;

        isOpen = !isOpen;
        moving = false;
    }
}