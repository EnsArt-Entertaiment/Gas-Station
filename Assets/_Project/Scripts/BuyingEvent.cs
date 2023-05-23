using UnityEngine;
using UnityEngine.UI;

public class BuyingEvent : MonoBehaviour
{
    [SerializeField] GameObject unlockUIBtn;
    [SerializeField] GameObject player;
    [SerializeField] Animator CustomerCarAnimator;


    private void Start()
    {
        unlockUIBtn.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            unlockUIBtn.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
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
