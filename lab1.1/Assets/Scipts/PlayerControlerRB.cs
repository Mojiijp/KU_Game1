using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///using UnityEngine.UI;

public class PlayerControlerRB : MonoBehaviour
{
    public PlaygroundScenceManager manager;    
    Rigidbody rb;
    public float speed = 2f;
    float newRotY = 0;
    public float rotspeed = 2f;
    public float JumpPower = 2f;
    public GameObject prefabBullet;
    public GameObject gunposition;    
    public bool hasgun;
    public float gunPower = 15f;
    public float gunCooldown = 1f;
    public float gunCooldownCount = 0;
    public int bulletCount = 0;
    public float hor;
    public float ver;

    public int coinCount = 0;
    public AudioSource audioCoin;
    public AudioSource audioFire;
    //public Text uiTextcoin;
    //public Text uiTextbullet;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(manager == null)
        {
            manager = FindObjectOfType<PlaygroundScenceManager>();
        }
        if(PlayerPrefs.HasKey("CoinCount"))
        {
            coinCount = PlayerPrefs.GetInt("CoinCount");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /* if (Input.GetKey(KeyCode.UpArrow))
         {
             rb.AddForce(new Vector3(0, 0, speed), ForceMode.VelocityChange);
             newRotY = 0;
         }
         if (Input.GetKey(KeyCode.DownArrow))
         {
             rb.AddForce(new Vector3(0, 0, -speed), ForceMode.VelocityChange);
             newRotY = 180;
         }
         if (Input.GetKey(KeyCode.RightArrow))
         {
             rb.AddForce(new Vector3(speed, 0, 0), ForceMode.VelocityChange);
             newRotY = 90;
         }
         if (Input.GetKey(KeyCode.LeftArrow))
         {
             rb.AddForce(new Vector3(-speed, 0, 0), ForceMode.VelocityChange);
             newRotY = -90;
         }
         if (Input.GetButtonDown("Jump"))
         {
             rb.AddForce(0, JumpPower, 0, ForceMode.Impulse);
         }*/
         if(Input.GetButtonDown("Fire1") && bulletCount>0 && (gunCooldownCount >= gunCooldown))
         {
             gunCooldownCount = 0;
             bulletCount--;
             manager.SetTextBullet(bulletCount); //บอก manager ให้แสดงจำนวนกระสุน
             audioFire.Play();
             //uiTextbullet.text = bulletCount.ToString();
             GameObject bullet =  Instantiate(prefabBullet,gunposition.transform.position,gunposition.transform.rotation);
             bullet.GetComponent<Rigidbody>().AddForce(transform.forward * gunPower, ForceMode.Impulse);
             Rigidbody bRb = bullet.GetComponent<Rigidbody>();
             //bRb.AddForce(transform.forward * gunPower, ForceMode.Impulse);
             Destroy(bullet, 3f);
         }

        float horizontal = Input.GetAxis("Horizontal") * speed;
        float vertical = Input.GetAxis("Vertical") * speed;
        if(horizontal > 0)
        {
            newRotY = 90;
        }
        else if(horizontal < 0)
        {
            newRotY = -90;
        }
        if(vertical > 0)
        {
            newRotY = 0;
        }
        else if(vertical < 0)
        {
            newRotY = 180;
        }
        rb.AddForce(horizontal, 0, vertical, ForceMode.VelocityChange);

        gunCooldownCount = gunCooldownCount + Time.fixedDeltaTime;
        //gunCooldownCount += Time.fixedDeltaTime;
        
        transform.rotation = Quaternion.Lerp(
                                              Quaternion.Euler(0, newRotY, 0),
                                              transform.rotation,
                                              Time.deltaTime * rotspeed
                                           );
    }
    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.tag == "Collactable")
        {
            Destroy(collision.gameObject);
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if (other.gameObject.tag == "Collactable")
        {
            Destroy(other.gameObject);
            coinCount ++;
            //uiTextcoin.text = coinCount.ToString();
            manager.SetTextCoin(coinCount);
            audioCoin.Play();
            PlayerPrefs.SetInt("CoinCount", coinCount);
        }

        if (other.gameObject.name == "Gun Trigger")
        {
            hasgun = true;
            bulletCount += 10;
            //uiTextbullet.text = bulletCount.ToString();
            manager.SetTextBullet(bulletCount);
            Destroy(other.gameObject);
            audioFire.Play();
        }
    }

}