using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandStats : MonoBehaviour {

    [Tooltip("ASSIGN THE SPRITE HERE")]
    public Sprite WandSprite;

    [Tooltip("ASSIGN THE WAND MODEL HERE")]
    public GameObject WandModel;

    [Tooltip("ASSIGN THE FIRERATE HERE")]
    [SerializeField]
    private float firerate;

    [Tooltip("ASSIGN THE PARTICLE SYSTEM HERE - WRAP IT IN A GAME OBJECT!")]
    [SerializeField]
    private GameObject particleGameObject;

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
    

    [Tooltip("ASSIGN THE RELOAD TIME HERE")]
    [SerializeField]
    private float reloadtime;


    private void Awake()
    {
        ammo = maxAmmo;
        reload();
        //currentClip = clipSize;

    }
    // Use this for initialization

    public float shoot(Vector3 Direction)
    {
        if(currentClip > 0)
        {
            GameObject bull = Instantiate(particleGameObject, WandModel.transform.position, Quaternion.identity);
            bull.GetComponent<Rigidbody>().AddForce(-Direction * 300);
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
    
}
