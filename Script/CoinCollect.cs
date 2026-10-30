
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    private bool collected = false;

    void OnTriggerEnter(Collider other)
    {
        if (collected)
            return;

        if (other.CompareTag("Player"))
        {
            collected = true;

            if (GameManager.Instance != null)
                GameManager.Instance.AddCoin();

            Destroy(gameObject);
        }
    }
}
