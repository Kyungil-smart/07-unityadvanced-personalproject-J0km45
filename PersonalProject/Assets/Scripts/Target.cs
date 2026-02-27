using UnityEngine;

public class Target : MonoBehaviour, IHittable
{
    public void OnHit()
    {
        // TODO: 점수 추가
        Destroy(gameObject);
    }
}