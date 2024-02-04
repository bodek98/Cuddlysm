using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] float timeToLive;
    
    void Start()
    {
        Destroy(gameObject, timeToLive);
    }
}
