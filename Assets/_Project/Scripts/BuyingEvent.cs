using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyingEvent : MonoBehaviour
{
    [SerializeField] GameObject unlockUIBtn;
    [SerializeField] GameObject player;
    [SerializeField] Animator CustomerCarAnimator;

    void Start()
    {
        unlockUIBtn.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            unlockUIBtn.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            unlockUIBtn.SetActive(false);
        }
    }

    public void OnButtonClick()
    {
        CustomerCarAnimator.SetTrigger("DriveIn");
    }
}
