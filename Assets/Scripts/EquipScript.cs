using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipScript : MonoBehaviour
{
    public Transform PlayerTransform;
    public GameObject Gun;
    public Camera Camera;
    public TextMeshProUGUI Item;
    public TextMeshProUGUI Instruction;
    public float range = 2f;
    public float open = 100f;

    bool canGrab = false;
    bool inRange = false;
    bool grabbed = false;

    // Start is called before the first frame update
    void Start()
    {
        Gun.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        CheckEquip();
        if (Input.GetKeyDown("f") )
        {
            if (canGrab && inRange)
            {
                EquipObject();
            }
            else if (grabbed )
            {
                UnequipObject();
            }
        }
    }

    void Shoot ()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                inRange = true;
                Item.text = hit.transform.name;
                Item.gameObject.SetActive(true);
                Instruction.gameObject.SetActive(true);
                Instruction.text = "Press 'F' to Grab.";
            }
        }
        else
        {
            inRange = false;
            Item.gameObject.SetActive(false);
        }

    }

    void UnequipObject()
    {
        PlayerTransform.DetachChildren();
        Gun.transform.eulerAngles = new Vector3(Gun.transform.eulerAngles.x, Gun.transform.eulerAngles.y, Gun.transform.eulerAngles.z - 45);
        Gun.GetComponent<Rigidbody>().isKinematic = false;
        canGrab = true;
    }

    void EquipObject()
    {
        Gun.GetComponent<Rigidbody>().isKinematic = true;
        Gun.transform.position = PlayerTransform.transform.position;
        Gun.transform.rotation = PlayerTransform.transform.rotation;
        Gun.transform.SetParent(PlayerTransform);
        canGrab= false;
    }

    void CheckEquip()
    {
        grabbed = Gun.transform.position == PlayerTransform.transform.position ? true : false; 
        if (grabbed) {
            Instruction.text = "Press 'F' to Drop.";
        }
        else if (!grabbed && !inRange)
        {
            Instruction.gameObject.SetActive(false);
        }
    }
}
