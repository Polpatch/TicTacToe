using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModalInfoController : MonoBehaviour
{

    public delegate void CallbackFunc();
    private CallbackFunc callback;

    private TMP_Text dialogText;

    public GameObject dialogTextGO;

    private static ModalInfoController _instance;

    private Animator animator;

    public static ModalInfoController Instance{
        get{
            if(_instance != null) return _instance;

            return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogText = dialogTextGO.GetComponent<TMP_Text>();
        _instance = this;

        animator = GetComponent<Animator>();

        Button btn = GetComponent<Button>();

        btn.onClick.AddListener(CloseModal);
        
    }

    public void OpenModal(string text, CallbackFunc callback){
        dialogText.text = text;

        animator.SetBool("openModal", true);

        this.callback = callback;
    }

    public void CloseModal(){
        animator.SetBool("openModal", false);
        if(this.callback != null) callback();
    }
}
