using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandStats : MonoBehaviour {

    [Tooltip("ASSIGN THE SPRITE HERE")]
    public Sprite WandSprite;

   // [Tooltip("ASSIGN THE WAND MODEL HERE")]
   // public GameObject WandModel;

    [Tooltip("ASSIGN THE FIRERATE HERE")]
    [SerializeField]
    private float firerate;

    [Tooltip("ASSIGN THE PARTICLE SYSTEM HERE - WRAP IT IN A GAME OBJECT!")]
    [SerializeField]
    public GameObject particleGameObject, handParticleSystem;

    [Tooltip("ASSIGN THE DAMAGE HERE")]
    [SerializeField]
    private int damage;

    [Tooltip("ASSIGN THE AMMO HERE")]
    [SerializeField]
    private int maxAmmo;
    private int ammo;

    [Tooltip("ASSIGN THE CLIP SIZE HERE")]
    [SerializeField]
    private int clipSize;
    private int currentClip;


    public bool pickUp = true;


    public enum BulletTypes
    {
        LIGHTNING, //Chain across enemies
        FIRE,      //Explosion
        ICE,       //Slow enemies
        DEATH,     //Chance to take damage
        WIND,      //KnockBack
        POISEN,    //DOT
        RANDOM,    //Random
        NORMAL     //Normal bullet
    };

    public BulletTypes bulletTypes;
    

    [Tooltip("ASSIGN THE RELOAD TIME HERE")]
    [SerializeField]
    private float reloadtime;


    private void Awake()
    {
        ammo = maxAmmo;
        reload();
    }

    public float shoot(Vector3 Direction)
    {
        if(currentClip > 0)      
        {
            GameObject bull = Instantiate(particleGameObject, transform.position, Quaternion.identity);
            bull.GetComponent<BulletPhysics>().damage = damage;
            bull.GetComponent<Rigidbody>().AddForce(-Direction * 300);
            switch (bulletTypes)
            {
                case BulletTypes.NORMAL:
                    
                    break;
                case BulletTypes.LIGHTNING:
                    bull.GetComponent<BulletPhysics>().typeOfBullet = BulletTypes.LIGHTNING;
                    break;
                case BulletTypes.FIRE:
                    bull.GetComponent<BulletPhysics>().typeOfBullet = BulletTypes.FIRE;
                    break;
                case BulletTypes.ICE:
                    bull.GetComponent<BulletPhysics>().typeOfBullet = BulletTypes.ICE;
                    break;
                case BulletTypes.DEATH:
                    bull.GetComponent<BulletPhysics>().typeOfBullet = BulletTypes.DEATH;
                    break;
                case BulletTypes.WIND:
                    bull.GetComponent<BulletPhysics>().typeOfBullet = BulletTypes.WIND;
                    break;
                case BulletTypes.POISEN:
                    bull.GetComponent<BulletPhysics>().typeOfBullet = BulletTypes.POISEN;
                    break;
                case BulletTypes.RANDOM:
                    bull.GetComponent<BulletPhysics>().typeOfBullet = BulletTypes.RANDOM;
                    break;

            }
            
            
            currentClip--;
            print("Firing");
            return firerate;
        }
        else if(ammo !=0)
        {
            
            print("RELOAD");
            return reload();
        } else
        {
            print("need ammo");
            return firerate;
        }
    }

    public float reload()
    {
        if (ammo > clipSize)
        {
            ammo -= clipSize;
            currentClip += clipSize;
            return reloadtime;
        }
        else if (ammo > 0)
        {
            currentClip = ammo;
            ammo = 0;
            return reloadtime;
        }
        else
        {
            print("NO AMMO!");
            return 1;
        }
    }



    private void Update()
    {
        particleGameObject.transform.localPosition = new Vector3(0, 0, 0);
        handParticleSystem.transform.localPosition = new Vector3(0, 0, 0);
    }

}
