using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestEnemy : MonoBehaviour
{
    public float speed;
    Transform target;
    int waves;
    public int blood;
    public int power;
    public float timefireCD;
    float fireCD = 0;
    float Pspeed;

    //GameObject closestSolider;
    void Start()
    {
        Pspeed = speed;
        target = WayPoint.points[0];
        fireCD = timefireCD;
        //InvokeRepeating("LocknShoot", 0f, 0.4f);
    }
    //void LocknShoot()
    //{
    //    GameObject[] LowSoliders = GameObject.FindGameObjectsWithTag("LowSolider");
    //    foreach (GameObject Solider in LowSoliders)
    //    {
    //        float distanceToEnemy = Vector3.Distance(transform.position, Solider.transform.position);
    //        if (distanceToEnemy < 0.3f && Solider.GetComponent<Character>().StopCount < Solider.GetComponent<Character>().MaxStopCount)
    //        {
    //            Solider.GetComponent<Character>().StopCount++;
    //            speed = 0;
    //            closestSolider = Solider;

    //        }
    //        else
    //        {
    //            speed = Pspeed;
    //        }
    //    }
    //}
    void Update()
    {


        gameObject.GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.y;
        GetComponentInChildren<Slider>().value = blood;
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            if (waves >= WayPoint.points.Length - 1)
            {
                GameManage.instance.deathCount++;
                GameManage.instance.GoHomeCount--;
                DestroyImmediate(gameObject, true);

            }
            waves++;
            target = WayPoint.points[waves];
        }


        //if (fireCD <= 0f)
        //{
        //    closestSolider.GetComponent<Character>().blood -= power;
        //    fireCD = timefireCD;
        //}
        //fireCD -= Time.deltaTime;

        else if (blood <= 0 && speed != 0)
        {
            GameManage.instance.deathCount++;
            Destroy(gameObject);
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "LowSolider")
        {
            col.GetComponent<TestCharacter>().StopCount++;
        }
    }
    IEnumerator Ea()
    {
        yield return 7f;
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "LowSolider" && col.GetComponent<TestCharacter>().StopCount <= col.GetComponent<TestCharacter>().MaxStopCount)
        {
            speed = 0;
            if (fireCD <= 0f)
            {
                col.GetComponent<TestCharacter>().TakeDamage(power);
                fireCD = timefireCD;
            }
            fireCD -= Time.deltaTime;
            if (blood <= 0)
            {
                col.GetComponent<TestCharacter>().StopCount--;
                GameManage.instance.deathCount++;
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "LowSolider")
        {
            col.GetComponent<TestCharacter>().StopCount--;
        }
        speed = Pspeed;

    }
    public void TakeDamage(int damage)
    {

        blood -= damage;
    }
}
