using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class TestCharacter : MonoBehaviour
{
    //public float range;
    public float timefireCD;
    float fireCD = 0;
    GameObject closestEnemy;
    public int cost;
    public bool High = false;
    public int power;
    public int blood;
    public int MaxStopCount;
    public int StopCount = 0;
    List<GameObject> enemies = new List<GameObject>();
    GameObject EndPoint;
    Animator Attack;
    AudioSource AttackAudio;
    public GameObject ButtonChara;
    void Awake()
    {
        StopCount = 0;
        if (GameManage.instance.Allcost - cost <= 0 || GameManage.instance.charaRange <= 0)
        {
            Destroy(gameObject);
        }
        if (transform.tag == "LowSolider")
        {
            GameObject[] Highblocks = GameObject.FindGameObjectsWithTag("Highblock");
            foreach (GameObject highblock in Highblocks)
            {
                if (transform.position == highblock.transform.position)
                    Destroy(gameObject);
            }
        }
        if (transform.tag == "HighSolider")
        {
            GameObject[] Lowblocks = GameObject.FindGameObjectsWithTag("Lowblock");
            foreach (GameObject lowblock in Lowblocks)
            {
                if (transform.position == lowblock.transform.position)
                    Destroy(gameObject);
            }
        }
        EndPoint = GameObject.Find("EndPoint");

    }
    void Start()
    {
   
        GameManage.instance.SwitchtoSpawn(null);
        AttackAudio = GetComponent<AudioSource>();
        Attack = GetComponent<Animator>();
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.y;
        GameManage.instance.Allcost -= cost;
        GameManage.instance.charaRange -= 1;

        InvokeRepeating("LocknShoot", 0f, 0.4f);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            enemies.Add(col.gameObject);
        }
    }
    //    void OnTriggerStay2D(Collider2D col)
    //{

    //    if (col.tag=="Enemy"&&fireCD <= 0f)
    //    {
    //        col.GetComponent<testEnemy>().TakeDamage(power);
    //        fireCD = timefireCD;
    //    }
    //    fireCD -= Time.deltaTime;

    //}
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            enemies.Remove(col.gameObject);
        }
    }
    void LocknShoot()
    {

        float closestDis = Mathf.Infinity;
        if (enemies.ToArray().Length != 0)
        {
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(EndPoint.transform.position, enemy.transform.position);
                if (distanceToEnemy < closestDis)
                {
                    closestDis = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
        }
    }
    void Update()
    {


        GetComponentInChildren<Slider>().value = blood;

        if (enemies.ToArray().Length != 0&&closestEnemy == null)
        {
            enemies.Remove(closestEnemy.gameObject);
        }
        if(closestEnemy!=null)
        {
            if (fireCD <= 0.25f)
                Attack.SetBool("Attacking", true);
            else
                Attack.SetBool("Attacking", false);

            if (fireCD <= 0f)
            {
                AttackAudio.Play();
                closestEnemy.GetComponent<TestEnemy>().TakeDamage(power);
                fireCD = timefireCD;
            }
            fireCD -= Time.deltaTime;
        }
        if (enemies.ToArray().Length == 0)
        {
            Attack.SetBool("Attacking", false);
        }
       
        if (blood <= 0)
        {
            GameManage.instance.charaRange++;
            Destroy(gameObject);
        }

    }
    public void TakeDamage(int damage)
    {
        blood -= damage;
    }

}