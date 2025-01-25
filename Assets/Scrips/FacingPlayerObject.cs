using UnityEngine;

public class FacingPlayerObject : MonoBehaviour
{
    GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
            transform.LookAt(Player.transform.position);
        }
    }
}
