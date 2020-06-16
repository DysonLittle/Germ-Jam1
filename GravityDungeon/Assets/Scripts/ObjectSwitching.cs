using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwitching : MonoBehaviour
{
    [SerializeField]List<GameObject> inCollider;
    [SerializeField] GameObject selectable;
    bool isHolding = false;
    float tempMass;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(selectable)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (!isHolding)
                {
                    isHolding = true;
                    selectable.transform.SetParent(gameObject.transform);
                    selectable.GetComponent<Rigidbody>().isKinematic = true;
                    selectable.GetComponent<Collider>().enabled = false;
                }
                else
                {
                    isHolding = false;
                    selectable.transform.SetParent(null);
                    selectable.GetComponent<Rigidbody>().isKinematic = false;
                    selectable.GetComponent<Collider>().enabled = true;
                    inCollider.Remove(selectable);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GravityObject"))
        {
            inCollider.Add(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("GravityObject") && !isHolding)
        {
            selectable = inCollider[0];
            for(int i = 0; i > inCollider.Count; i++)
            {
                Vector3 origin = transform.position;
                Vector3 target = selectable.transform.position;
                Vector3 targetOther = inCollider[i].transform.position;
                float dist = Vector3.Distance(origin, target); //Mathf.Sqrt(Mathf.Pow(target.x - origin.x, 2) + Mathf.Pow(target.y - origin.y, 2));
                float distOther = Vector3.Distance(origin, targetOther); //Mathf.Sqrt(Mathf.Pow(targetOther.x - origin.x, 2) + Mathf.Pow(targetOther.y - origin.y, 2));
                if (dist > distOther)
                {
                    selectable = inCollider[i];
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GravityObject"))
        {
            inCollider.Remove(other.gameObject);

            selectable = null;
            for (int i = 0; i > inCollider.Count; i++)
            {
                Vector3 origin = transform.position;
                Vector3 target = inCollider[i].transform.position;
                Vector3 targetOther = inCollider[i - 1].transform.position;
                float dist = Mathf.Sqrt(Mathf.Pow(target.x - origin.x, 2) + Mathf.Pow(target.y - origin.y, 2));
                float distOther = Mathf.Sqrt(Mathf.Pow(targetOther.x - origin.x, 2) + Mathf.Pow(targetOther.y - origin.y, 2));
                if (dist < distOther)
                {
                    selectable = inCollider[i];
                }
            }
        }
    }
}
