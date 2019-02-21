using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPhysics : MonoBehaviour {


    public WandStats.BulletTypes typeOfBullet;

    public int damage;
    private GameObject currentCollider;

    public bool isEnemyWand;



    public Collider[] createSphere(float radius)
    {

        Collider[] hit = Physics.OverlapSphere(transform.position, radius);
        //print("HIT LENGTH: " + hit.Length);
        return hit;
    }




    private  void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, damage);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isEnemyWand)
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerController>().takeDamage();
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.gameObject.tag == "Enemy")
            {
                //remove health here
                switch (typeOfBullet)
                {

                    case WandStats.BulletTypes.LIGHTNING:
                        Collider[] hit = createSphere(damage);
                        GameObject enemyToTravelTo = null;
                        foreach (Collider Enemy in hit)
                        {
                            // print("Running This");
                            if (Enemy.transform.gameObject.tag == "Enemy" && Enemy.gameObject != other.gameObject)
                            {
                                //print(other.gameObject.name);
                                if (enemyToTravelTo == null)
                                {
                                    //print("Found one");
                                    enemyToTravelTo = Enemy.transform.gameObject;
                                }
                                else if (Vector3.Distance(transform.position, Enemy.transform.position) < Vector3.Distance(transform.position, enemyToTravelTo.transform.position))
                                {
                                    enemyToTravelTo = Enemy.transform.gameObject;
                                }
                            }

                        }
                        if (currentCollider != other.gameObject)
                        {
                            damage /= 2;
                            currentCollider = other.gameObject;
                        }
                        if (enemyToTravelTo != null && damage != 1)
                        {
                            //print(enemyToTravelTo.name);
                            GetComponent<Rigidbody>().velocity = Vector3.zero;
                            GetComponent<Rigidbody>().AddForce((enemyToTravelTo.transform.position - transform.position).normalized * 300);
                            other.gameObject.GetComponent<BaseAIScript>().Health -= damage;
                            enemyToTravelTo = null;
                        }
                        else if (damage == 1)
                        {
                            other.gameObject.GetComponent<BaseAIScript>().Health -= damage;
                            Destroy(gameObject);
                        }
                        else if (enemyToTravelTo == null)
                        {
                            other.gameObject.GetComponent<BaseAIScript>().Health -= damage;
                            Destroy(gameObject);
                        }
                        break;


                    case WandStats.BulletTypes.FIRE:
                        other.gameObject.GetComponent<BaseAIScript>().Health -= damage;

                        Collider[] col = createSphere(damage / 2);
                        foreach (Collider Enemy in col)
                        {
                            //Instaniate explosion particle
                            if (Enemy.gameObject.GetComponent<BaseAIScript>() != null)
                            {
                                //TakeDamage
                                Destroy(gameObject);
                            }
                        }
                        break;
                    case WandStats.BulletTypes.ICE:
                        other.gameObject.GetComponent<BaseAIScript>().slowDown(damage / 2);
                        Destroy(gameObject);
                        break;
                    case WandStats.BulletTypes.DEATH:
                        damage *= 2;
                        int damageInt = Random.Range(0, damage * 4);
                        if (damageInt == 1)
                        {
                            //player takes damage
                        }
                        Destroy(gameObject);
                        break;
                    case WandStats.BulletTypes.WIND:
                        other.gameObject.GetComponent<Rigidbody>().AddForce((other.transform.position - transform.position).normalized * damage);
                        break;
                    case WandStats.BulletTypes.POISEN:
                        break;
                    case WandStats.BulletTypes.RANDOM:
                        break;
                }
            }
        }


        if(other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

    }

}

