using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wall : MonoBehaviour
{
    public float explosionRadius = 10.0f;
    public float explosionPower = 300.0f;
    public bool isFinishWall;
    bool processing;
    public GameObject FinalwallCountText;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !processing)
        {
            processing = true;

            if (isFinishWall)
            {
                GetComponent<BoxCollider>().enabled = false;
                EventManager.Instance.ScoreUpdate();
                EventManager.Instance.HitWall(true);
                if (FinalwallCountText!=null)
                {
                    FinalwallCountText.SetActive(false);
                }          
            }
            else
            {
                GetComponent<BoxCollider>().enabled = false;
                EventManager.Instance.HitWall(false);
                gameObject.transform.parent.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
             
            }
            processing = false;

            if (FindObjectOfType<AnswerManager>().isAnswerCorrect)
            {
                explosionPower = 150;

            }
            else
            {
                explosionPower =1;

            }

            foreach (Transform child in transform)
            {
                child.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                child.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionPower, transform.position, explosionRadius);
                child.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-5, 5), Random.Range(0, 5), Random.Range(0, 5)), ForceMode.Impulse);
                child.gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(10, 50), Random.Range(10, 50), Random.Range(10, 50)));
            }


        }

    }
}