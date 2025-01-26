using UnityEngine;
using TMPro;

public class CreditBubble : MonoBehaviour {
    public Transform playerCamera; // Assign the player's camera in the Inspector
    public TextMeshProUGUI bubbleText; // Assign the Unity UI Text component in the Inspector
    public string[] possibleTexts; // Add your list of strings in the Inspector
    public GameObject gameObject;

    void Start()
    {
        // Pick a random text from the list and assign it to the text component
        if (possibleTexts.Length > 0 && bubbleText != null)
        {
            int randomIndex = Random.Range(0, possibleTexts.Length);
            bubbleText.text = possibleTexts[randomIndex];
        }
        else
        {
            Debug.LogWarning("No text options provided or Text component not assigned!");
        }
    }

    void LateUpdate()
    {
        // Make the text object face the player's camera
        if (playerCamera != null)
        {
            gameObject.transform.LookAt(playerCamera);
            gameObject.transform.rotation = Quaternion.LookRotation(playerCamera.forward);
        }
    }
}
