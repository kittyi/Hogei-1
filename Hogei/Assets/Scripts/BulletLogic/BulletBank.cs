//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BulletBank : MonoBehaviour {

//    private Queue<GameObject> regularBulletQueue = new Queue<GameObject>();
//    private Queue<GameObject> setupStraightBulletQueue = new Queue<GameObject>();
//    private Queue<GameObject> playerStraightBulletQueue = new Queue<GameObject>();
//    private Queue<GameObject> playerHomingBulletQueue = new Queue<GameObject>();

//    [Header("Bullet Objects")]
//    [Tooltip("Prefab of regular straight bullet")]
//    public GameObject regularStraightBulletObject;
//    [Tooltip("Prefab of setup straight bullet")]
//    public GameObject setupStraightBulletObject;
//    [Tooltip("Prefab of player straight bullet")]
//    public GameObject playerStraightBullet;
//    [Tooltip("Prefab of player homing bullet")]
//    public GameObject playerHomingBullet;

//    [Header("Number of bullets")]
//    [Tooltip("Num of Regular Straight Bullets")]
//    public int numRegularStraightBullets = 2000;
//    [Tooltip("Num of Setup Straight Bullets")]
//    public int numSetupStraightBullets = 2000;

//    [Tooltip("Number of player straight bullets")]
//    public int numPlayerStraightBullets = 2000;
//    [Tooltip("Number of player straight bullets")]
//    public int numPlayerHomingBullets = 2000;

//    // Use this for initialization
//    void Start () {
//		//fill queues
//        for (int i = 0; i < numRegularStraightBullets; i++)
//        {
//            GameObject newBullet = Instantiate(regularStraightBulletObject, transform.position, transform.rotation);
//            newBullet.GetComponent<RegularStraightBullet>().SetBulletBank(this);
//            newBullet.transform.SetParent(gameObject.transform);
//            newBullet.SetActive(false);
//            regularBulletQueue.Enqueue(newBullet);
//        }

//        for (int i = 0; i < numSetupStraightBullets; i++)
//        {
//            GameObject newBullet = Instantiate(setupStraightBulletObject, transform.position, transform.rotation);
//            newBullet.GetComponent<SetupStraightBullet>().SetBulletBank(this);
//            newBullet.transform.SetParent(gameObject.transform);
//            newBullet.SetActive(false);
//            setupStraightBulletQueue.Enqueue(newBullet);
//        }

//        for (int i = 0; i < numPlayerStraightBullets; i++)
//        {
//            GameObject newBullet = Instantiate(playerStraightBullet, transform.position, transform.rotation);
//            newBullet.GetComponent<PlayerStraightBullet>().SetBulletBank(this);
//            newBullet.transform.SetParent(gameObject.transform);
//            newBullet.SetActive(false);
//            playerStraightBulletQueue.Enqueue(newBullet);
//        }

//        for (int i = 0; i < numPlayerHomingBullets; i++)
//        {
//            GameObject newBullet = Instantiate(playerHomingBullet, transform.position, transform.rotation);
//            newBullet.GetComponent<PlayerHomingBullet>().SetBulletBank(this);
//            newBullet.transform.SetParent(gameObject.transform);
//            newBullet.SetActive(false);
//            playerHomingBulletQueue.Enqueue(newBullet);
//        }
//    }
	
//	// Update is called once per frame
//	void Update () {
		
//	}

//    //get bullet bullet from queue
//    public GameObject GetRegularStraightBullet()
//    {
//        GameObject bullet;
//        if (bullet = regularBulletQueue.Dequeue())
//        {
//            bullet.SetActive(true);
//        }
//        else
//        {
//            bullet = Instantiate(regularStraightBulletObject, transform.position, transform.rotation);
//            bullet.GetComponent<RegularStraightBullet>().SetBulletBank(this);
//            bullet.transform.SetParent(gameObject.transform);
//        }
//        return bullet;
//    }

//    public GameObject GetSetupStraightBullet()
//    {
//        GameObject bullet;
//        if (bullet = setupStraightBulletQueue.Dequeue())
//        {
//            bullet.SetActive(true);
//        }
//        else
//        {
//            print("Making new bullet");
//            bullet = Instantiate(setupStraightBulletObject, transform.position, transform.rotation);
//            bullet.GetComponent<SetupStraightBullet>().SetBulletBank(this);
//            bullet.transform.SetParent(gameObject.transform);
//        }
//        return bullet;
//    }

//    public GameObject GetPlayerStraightBullet()
//    {
//        GameObject bullet;
//        if (bullet = playerStraightBulletQueue.Dequeue())
//        {
//            bullet.SetActive(true);
//        }
//        else
//        {
//            bullet = Instantiate(playerStraightBullet, transform.position, transform.rotation);
//            bullet.GetComponent<PlayerStraightBullet>().SetBulletBank(this);
//            bullet.transform.SetParent(gameObject.transform);
//        }
//        return bullet;
//    }

//    public GameObject GetPlayerHomingBullet()
//    {
//        GameObject bullet;
//        if (bullet = playerHomingBulletQueue.Dequeue())
//        {

//            bullet.SetActive(true);
//        }
//        else
//        {
//            bullet = Instantiate(playerHomingBullet, transform.position, transform.rotation);
//            bullet.GetComponent<PlayerHomingBullet>().SetBulletBank(this);
//            bullet.transform.SetParent(gameObject.transform);
//        }
//        return bullet;
//    }

//    //return bullet to queue
//    public void ReturnRegularStraightBullet(GameObject regularBullet)
//    {
//        regularBullet.SetActive(false);
//        regularBulletQueue.Enqueue(regularBullet);
//    }

//    public void ReturnSetupStraightBullet(GameObject setupBullet)
//    {
//        setupBullet.SetActive(false);
//        setupStraightBulletQueue.Enqueue(setupBullet);
//    }

//    public void ReturnPlayerStraightBullet(GameObject playerBullet)
//    {
//        playerBullet.SetActive(false);
//        playerStraightBulletQueue.Enqueue(playerBullet);
//    }

//    public void ReturnPlayerHomingBullet(GameObject playerBullet)
//    {
//        playerBullet.SetActive(false);
//        playerHomingBulletQueue.Enqueue(playerBullet);
//    }
//}
