using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_triggered : MonoBehaviour
{
    public bool EndDialog;
    public GameObject Dialog;
    public GameObject Text;
    bool isText;
    // Start is called before the first frame update
    void Start()
    {
        Text.SetActive(false);
        Dialog.GetComponent<UIManager>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Dialog.GetComponent<UIManager>().isActive)
        {
            Text.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Text.SetActive(true);
            Dialog.GetComponent<UIManager>().enabled = true;
            Dialog.GetComponent<UIManager>().isExit = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Text.SetActive(false);
            Dialog.GetComponent<UIManager>().enabled = false;
            Dialog.GetComponent<UIManager>().isExit = true;
        }
    }
}
