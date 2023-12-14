using System.Collections;
using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{

    public bool firstLoop = true;

    public void Awake()
    {
        firstLoop = true;
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !CollisionDeath.endGame) // Assuming your player has the tag "Player"
        {
            CollisionDeath.endGame = true;
            Debug.Log("Game Won!");
            //disable box collider
            GetComponent<BoxCollider>().enabled = false;
            //game won
            NotificationManager.Instance.AddNotification("Game Won!", 5f);

           
            // Load character selection scene when any button is pressed
            StartCoroutine(WaitForKeyPressAndLoadLevel());
        }
    }

    private IEnumerator WaitForKeyPressAndLoadLevel()
    {

        while (firstLoop)
        {
            firstLoop = false;
            yield return new WaitForSecondsRealtime(5f);

        }

        while (!Input.anyKey)
        {
            NotificationManager.Instance.AddNotification("Press any button to return to Home Screen", 0.01f);
            yield return null;

        }

        NotificationManager.Instance.ClearAllNotifications();
        LoadingScreen.Instance.LoadLevel(0);
            
    }
}

