using UnityEngine;

public class Stock : MonoBehaviour
{
    [SerializeField] private Base _base;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bot bot))
        {
            Crystal crystal = bot.GetComponentInChildren<Crystal>();

            if (crystal != null)
            {
                crystal.transform.parent = transform;
                crystal.GetComponent<Rigidbody>().useGravity = true;
                crystal.enabled = false;

                _base.Park(bot);
            }
        }
    }
}
