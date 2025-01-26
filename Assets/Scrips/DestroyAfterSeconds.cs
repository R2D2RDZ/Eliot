using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] float seconds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, seconds);
    }

}
