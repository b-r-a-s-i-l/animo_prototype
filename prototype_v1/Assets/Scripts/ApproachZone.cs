using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachZone : MonoBehaviour
{
    private GameObject zone;
    private BoxCollider2D box;

    public void CreateApproachZone(GameObject reference)
    {
        GameObject father;
        father = reference;

        zone = new GameObject("AprroachZone");
        zone.transform.SetParent(father.transform);
        zone.transform.position = father.transform.position;

        box = zone.AddComponent<BoxCollider2D>();
        box.size = father.GetComponent<BoxCollider2D>().size + new Vector2(3f, 3f);
        box.offset = father.GetComponent<BoxCollider2D>().offset;
        box.isTrigger = true;
        
        zone.AddComponent<ApproachZone>();
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        Transform father = transform.parent;

        if (other.gameObject.tag == "Player")
        {
            if(father.GetComponent<Item>()) father.GetComponent<Item>().IsTouching = true;
            else if(father.GetComponent<Usable>()) father.GetComponent<Usable>().IsTouching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Transform father = transform.parent;

        if (other.gameObject.tag == "Player")
        {
            if (father.GetComponent<Item>()) father.GetComponent<Item>().IsTouching = false;
            else if (father.GetComponent<Usable>()) father.GetComponent<Usable>().IsTouching = false;
        }
    }
}
