using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenDoor : MonoBehaviour
{
    private Animator anim;
   
    private bool IsAtDoor = false;

    [SerializeField] private TextMeshProUGUI CodeText;
    string codeTextValue ="";
    public string safeCode;
    public GameObject CodePanel;

    void Start()
    {
       anim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
       if(codeTextValue == safeCode )
        {
            anim.SetTrigger("OpenDoor");
            CodePanel.SetActive(false);
            
            GameManager.instance.LevelCompleted();
        }
        if(codeTextValue.Length >= 4)
        {
           codeTextValue =""; 
        }
        if(Input.GetKey(KeyCode.E)&& IsAtDoor == true)
        {
            CodePanel.SetActive(true);

             Cursor.lockState = CursorLockMode.None;
             Cursor.visible = true;
        }
        
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            IsAtDoor = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        IsAtDoor = false;
        CodePanel.SetActive(false);
    }

    public void AddDigit(string digit)
    {
       codeTextValue += digit; 
       CodeText.text = codeTextValue;
    }

}
