using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerScript : Actor
{
    private CharacterController charController;

    [SerializeField] private GameObject weapon;
    [SerializeField] private float rotationSpeed = 180;
    [SerializeField] private float acceleration = 4;
    [SerializeField] Camera cam;
    [SerializeField] WeaponScript[] weapons;

    private Quaternion targetRotation;
    private WeaponScript currentWeaponScript;
    private ScoreKeeper scoreKeeper;
    private Vector3 speedMod;
    private float unequipTimer;
    private float invulnTime;
    private float speedTime;
    private float score = 0;
    private bool bonusGun = false;
    private bool invulnerable = false;
    private bool speedUp = false;
    private bool slowDown = false;

    public static Action<float> GameOverEvent;
    private void Awake() 
    {
        charController = GetComponent<CharacterController>();
        currentWeaponScript = weapon.GetComponent<WeaponScript>();
        scoreKeeper = GetComponent<ScoreKeeper>();
    }
    private void Start()
    {
        currentWeaponScript.SetCurrentPlayer(this);
    }
    void Update()
    {
        if (unequipTimer > 0 && bonusGun)
        {
            unequipTimer-=Time.deltaTime;
        } 
        else if (bonusGun) 
        {
            EquipWeapon(0);
        }
 

        if (Input.GetKey(KeyCode.Mouse0))
        {
            currentWeaponScript.Shoot();
            MouseAim();
        }
        else
        {
            HandleInput();
        }

        if(invulnerable)
        {
            if (invulnTime>0) 
            {
                invulnTime-=Time.deltaTime;
            }
            else
            {
                Debug.Log("No invuln");
                invulnerable=false;
            }
        }
        if(speedUp)
        {
            if (speedTime>0) 
            {
                speedTime-=Time.deltaTime;
            }
            else
            {
                Debug.Log("No longer fast");
                speedUp=false;
            }
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        Debug.Log("Something is touching the collider!");
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if(!invulnerable)
            {
                Die();
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        //Debug.Log("Bonus entered!");
        if(other.gameObject.TryGetComponent<GunBonus>(out GunBonus bonus))
        {
            if (bonusGun)
            {
                return;
            }
            EquipWeapon(bonus.GetGunType());
            Destroy(bonus.gameObject);
        }

        if(other.gameObject.TryGetComponent<StatBonus>(out StatBonus stat))
        {
            Debug.Log("Got a bonus!");
            int bonusType = stat.GetBonusType();
            switch (bonusType)
            {
                case 0:
                { 
                    Debug.Log("Invulnerable!");
                    invulnerable=true;
                    invulnTime = stat.GetBonusDuration();
                    Destroy(stat.gameObject);
                    break;
                }
                case 1: 
                {
                    Debug.Log("Speed up!");
                    speedUp=true;
                    speedTime = stat.GetBonusDuration();
                    Destroy(stat.gameObject);
                    break;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.TryGetComponent<DangerZone>(out DangerZone zone))
        {
            int type = zone.GetDangerType();
            switch(type)
            {
                case 0:
                {
                    Die();
                    break;
                }
                
                case 1:
                {
                    slowDown=true;
                    break;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        slowDown=false;
    }

    private void HandleInput()
    {
        //Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (Input.GetKey(KeyCode.Mouse0))
        {
            return;
        }
        Movement();
    }

    private void MouseAim()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
        targetRotation = Quaternion.LookRotation(mousePos - new Vector3(transform.position.x, 0, transform.position.z));
        transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
        Movement();
    }
    private void Movement()
    {
        float speed = movementSpeed;
        if (slowDown)
        {
            speed = movementSpeed * 0.6f;
        }
        else if (speedUp) 
        {
            speed = movementSpeed * 1.5f;
        }
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        speedMod = Vector3.MoveTowards(speedMod, input, Time.deltaTime * acceleration);
        Vector3 motion = speedMod;
        motion *= (Mathf.Abs(input.x)==1 && Mathf.Abs(input.z)==1)?.7f:1;
        charController.Move(motion * speed * Time.deltaTime);
    }

    public void EquipWeapon(int index) //Также может быть вызвано через event-ы
    {
        GameObject newGun = weapons[index].gameObject;
        currentWeaponScript.gameObject.SetActive(false);
        currentWeaponScript = weapons[index];
        currentWeaponScript.gameObject.SetActive(true);
        currentWeaponScript.SetCurrentPlayer(this);
        if (currentWeaponScript.gunId!=0)
        {
            bonusGun=true;
            unequipTimer = currentWeaponScript.gunTimer;
        }
        else
        {
            bonusGun=false;
            unequipTimer=-1;
        }
    }

    public override void Die()
    {
        //Debug.Log("Game over!");
        GameOverEvent?.Invoke(score);
    }
    
    public int GetCurrentWeapon()
    {
        return currentWeaponScript.gunId;
    }
    
    public Vector3 GetMousePosition() 
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
        return mousePos;
    }

    public void AddScore(float value)
    {
        score += value;
        scoreKeeper.UpdateScore(score);
    }
}
