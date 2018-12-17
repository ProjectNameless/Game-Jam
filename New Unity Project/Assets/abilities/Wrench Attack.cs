using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchAttack : MonoBehaviour
{

    public int damage;
    public float range;
    public float attackDelay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && attackDelay <= 0f)
        {
            Shoot();
        }

        if(attackDelay > 0f)
        {
            attackDelay = attackDelay - Time.deltaTime;
        }
        
    }

    void Shoot ()
    {
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePos = new Vector3(mousePos.x, mousePos.y);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, range))
        {
            if(hit.collider != null)
            {
                hit.collider.GetComponentInParent<enemyHealth>().takeDamage(damage);
            }
        }
    }
}
