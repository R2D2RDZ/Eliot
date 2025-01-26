using UnityEngine;

public class destroyBubble : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    ParticleSystem _PS;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pop()
    {
        _PS = Instantiate(_particleSystem,transform.position,Quaternion.identity);
        Invoke("Destroy all", 1f);
        Destroy(this.gameObject);

    }

    public void DestroyAll()
    {
        Destroy(_PS);
    }
}
