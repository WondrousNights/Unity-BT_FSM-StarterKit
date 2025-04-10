using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
