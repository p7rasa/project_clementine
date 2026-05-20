using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenDoor : MonoBehaviour
{
    private Animator anim;
    private bool IsAtDoor = false;
    private bool doorOpened = false;

    [SerializeField] private TextMeshProUGUI CodeText;
    private string codeTextValue = "";

    public string safeCode;
    public GameObject CodePanel;

    public bool isLevelDoor = false;

    // ⭐ AKTİF KAPI SİSTEMİ (tek UI için)
    public static OpenDoor activeDoor;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && IsAtDoor)
        {
            CodePanel.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (codeTextValue == safeCode && !doorOpened)
        {
            doorOpened = true;

            anim.SetTrigger("OpenDoor");
            CodePanel.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (isLevelDoor)
            {
                GameManager.instance.LevelCompleted();
            }

            codeTextValue = "";
            CodeText.text = "";
        }

        if (codeTextValue.Length >= 4)
        {
            codeTextValue = "";
            CodeText.text = "";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsAtDoor = true;
            activeDoor = this; // ⭐ bu kapı aktif olur
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IsAtDoor = false;

        if (activeDoor == this)
            activeDoor = null;

        CodePanel.SetActive(false);
    }

    public void AddDigit(string digit)
    {
        if (doorOpened) return;

        codeTextValue += digit;
        CodeText.text = codeTextValue;
    }

    // ⭐ UI BUTTONLARIN BUNU ÇAĞIRMASI GEREK
    public void PressKey(string digit)
    {
        if (activeDoor == null) return;

        activeDoor.AddDigit(digit);
    }
}