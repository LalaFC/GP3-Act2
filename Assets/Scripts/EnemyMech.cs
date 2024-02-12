using Unity.VisualScripting;
using UnityEngine;

public class EnemyMech : MonoBehaviour
{
    float speed;
    [SerializeField] GameObject target;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        speed = 1;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        LookRotation();

    }
    void LookRotation()
    {
        float rotspeed = 20;
        Vector3 relativePos = target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotspeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pullable")
        {
            Destroy(this.gameObject);
        }
    }

}