using UnityEngine;

public class KerosenObject : MonoBehaviour
{
    [SerializeField] LampController lamp;
    private void OnTriggerEnter(Collider other)
    {
        float cure = 10f;
        if (other.TryGetComponent<LampController>(out lamp))
        {
            lamp.LifeLamp(cure);
            Destroy(this.gameObject);
        }
    }
}
