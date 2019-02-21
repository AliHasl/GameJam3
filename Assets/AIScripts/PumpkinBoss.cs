using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinBoss : MonoBehaviour {


    private BaseAIScript baseAIScript;
    public GameObject bullet, mineEnemy;
    public bool test = false;

    private bool SpecialPlaying = false;
    private bool CurrentlyAttack = false;


    private PumpkinStates lastState;

    // private BaseAIScript baseAIScript;

    


    private enum PumpkinStates
    {
        MINES,
        WAVE_ATTACK,
        BASIC_ATTACK
    };


    private void Awake()
    {
        baseAIScript = GetComponent<BaseAIScript>();
        StartCoroutine("basicBrain");
        lastState = PumpkinStates.BASIC_ATTACK;
    }

    IEnumerator CircleAttack()
    {
        for (int x = 0; x < 20; x++)
        {
            for (float i = 0; i < 1; i += .05f)
            {
                Quaternion qua = new Quaternion(0, i * 360, 0, 0);
                GameObject bull = Instantiate(bullet, transform.position, Quaternion.Euler(0, i * 360, 0));
                bull.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, i * 360, 0);
                print(bull.GetComponent<Rigidbody>().rotation);
                bull.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * 300);

            }
            yield return new WaitForSeconds(.25f);
            for (float i = 0; i < 1; i += .05f)
            {
                // Quaternion qua = new Quaternion(0, i * 360 + 9, 0, 0);
                GameObject bull = Instantiate(bullet, transform.position, Quaternion.Euler(0, i * 360 + 9, 0));
                bull.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, i * 360 + 9, 0);
                print(bull.GetComponent<Rigidbody>().rotation);
                bull.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * 300);

            }
            yield return new WaitForSeconds(.25f);
            for (float i = 0; i < 1; i += .05f)
            {
                // Quaternion qua = new Quaternion(0, i * 360 + 9, 0, 0);
                GameObject bull = Instantiate(bullet, transform.position, Quaternion.Euler(0, i * 360 + 4.5f, 0));
                bull.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, i * 360 + 4.5f, 0);
                print(bull.GetComponent<Rigidbody>().rotation);
                bull.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * 300);

            }
            yield return new WaitForSeconds(.25f);
        }
        SpecialPlaying = false;
        StartCoroutine("basicBrain");
    }



    IEnumerator RandomBullets()
    {
        for(int i = 0; i < 1200; i++)
        {
            //Quaternion qua = new Quaternion(0, Random.Range(0,360), 0, 0);
            GameObject bull = Instantiate(bullet, transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0));
            bull.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            print(bull.GetComponent<Rigidbody>().rotation);
            bull.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * 300);
            yield return new WaitForSeconds(.001f);
        }
        SpecialPlaying = false;
        StartCoroutine("basicBrain");
    }


    IEnumerator SinBullets()
    {

        for (int q = 0; q < 6; q++)
        {
            for (float x = 0; x < 1f; x += .05f)
            {
               // print("1");
                float y = Mathf.Sin(x);
                y *= 36;
                Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
                pos.x += y;
               
                //pos;
                GameObject bull = Instantiate(bullet, transform.position, Quaternion.identity);
                //print(pos);
                bull.GetComponent<Rigidbody>().AddForce(pos.normalized * 300);
                //bull.GetComponent<Rigidbody>().AddForce(-pos.normalized * -300);
                yield return new WaitForSeconds(.01f);
            }
            for (float x = 1; x > 0; x -= .05f)
            {
              //  print("2");
                float y = Mathf.Sin(x);
                y *= 36;
                Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
                pos.x += y;
                
                //pos;
                GameObject bull = Instantiate(bullet, transform.position, Quaternion.identity);
               // print(pos);
                bull.GetComponent<Rigidbody>().AddForce(pos.normalized * 300);
               // bull.GetComponent<Rigidbody>().AddForce(-pos.normalized * -300);
                yield return new WaitForSeconds(.01f);
            }
            for (float x = 0; x > -1; x -= .05f)
            {
             //   print("3");
                float y = Mathf.Sin(x);
                y *= 36;
                Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
                pos.x += y;
                
                //pos;
                GameObject bull = Instantiate(bullet, transform.position, Quaternion.identity);
               // print(pos);
                bull.GetComponent<Rigidbody>().AddForce(pos.normalized * 300);
                //bull.GetComponent<Rigidbody>().AddForce(-pos.normalized * -300);
                yield return new WaitForSeconds(.01f);
            }
            for (float x = -1; x < 0; x += .05f)
            {
             //   print("4");
                float y = Mathf.Sin(x);
                y *= 36;
                Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
                pos.x += y;
               
                //pos;
                GameObject bull = Instantiate(bullet, transform.position, Quaternion.identity);
               // print(pos);
                bull.GetComponent<Rigidbody>().AddForce(pos.normalized * 300);
               // bull.GetComponent<Rigidbody>().AddForce(-pos.normalized * 300);
                yield return new WaitForSeconds(.01f);
            }

            //yield return new WaitForSeconds(.1f);
        }
        CurrentlyAttack = false;

    }



    IEnumerator SpawnMineEnemies()
    {
        for(int i = 0; i < 5; i++)
        {
            Instantiate(mineEnemy, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(.75f);
        }
        CurrentlyAttack = false;
    }

    IEnumerator BasicAttack()
    {
        for (int i = 0; i < 100; i++)
        {
            Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
            GameObject bull = Instantiate(bullet, transform.position, Quaternion.identity);
            bull.GetComponent<Rigidbody>().AddForce(pos.normalized * 300);
            yield return new WaitForSeconds(.3f);
        }
        CurrentlyAttack = false;

    }



    IEnumerator basicBrain()
    {
        baseAIScript.states = BaseAIScript.States.MOVE_AI_NAVMESH;

        if (!CurrentlyAttack)
        {
            //print("BoolWorks");
           // print(lastState);
            switch (lastState)
            {
                case PumpkinStates.BASIC_ATTACK:
                  //  print("BasicAttack");
                    StartCoroutine("SpawnMineEnemies");
                    lastState = PumpkinStates.MINES;
                    CurrentlyAttack = true;
                    break;
                case PumpkinStates.WAVE_ATTACK:
                  //  print("WaveAttack");
                    StartCoroutine("BasicAttack");
                    lastState = PumpkinStates.BASIC_ATTACK;
                    CurrentlyAttack = true;
                    break;
                case PumpkinStates.MINES:
                  //  print("Mines");
                    StartCoroutine("SinBullets");
                    lastState = PumpkinStates.WAVE_ATTACK;
                    CurrentlyAttack = true;
                    break;
            }
        }


        yield return new WaitForSeconds(1f);
    }



    private void Update()
    {

        //20% off is a circle attack
        //Cycles between Basic, Sin and mines
        //Random at 30% and 10%
        //3% let it all out

        if ((baseAIScript.Health/baseAIScript.maxHealth*100)%20 == 0 && !SpecialPlaying && baseAIScript.maxHealth !=baseAIScript.Health )
        {
            StopAllCoroutines();
            baseAIScript.states = BaseAIScript.States.FIND_PLAYER;
            StartCoroutine("CircleAttack");
            SpecialPlaying = true;
        }

        if ((baseAIScript.Health / baseAIScript.maxHealth * 100) == 30 ||(baseAIScript.Health / baseAIScript.maxHealth * 100) == 10 && !SpecialPlaying)
        {
            StopAllCoroutines();
            baseAIScript.states = BaseAIScript.States.FIND_PLAYER;
            StartCoroutine("RandomBullets");
            SpecialPlaying = true;
        }


        if ((baseAIScript.Health / baseAIScript.maxHealth * 100) == 5 &&!SpecialPlaying)
        {
            StopAllCoroutines();
            baseAIScript.states = BaseAIScript.States.FIND_PLAYER;
            StartCoroutine("CircleAttack");
            StartCoroutine("RandomBullets");
            SpecialPlaying = true;
        }

        if (!CurrentlyAttack)
        {
            StartCoroutine("basicBrain");
        }


    }




}
