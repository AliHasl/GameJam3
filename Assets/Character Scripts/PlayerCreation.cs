using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreation : MonoBehaviour {


    //Basic Character Stuff

    //public variables

    public GameObject[] CharacterModels;
    public GameObject wand;
    public GameObject canvas;




    public enum CharacterTypes
    {
        MELLISA,
        BRAD,
        WYATT,
        LACHLAN
    };


    private PlayerController playerController;


    public void blah(int enumset)
    {
        switch (enumset)
        {
            case 0:
                LoadCharacter(CharacterTypes.MELLISA);
                break;
            case 1:
                LoadCharacter(CharacterTypes.BRAD);
                break;
            case 2:
                LoadCharacter(CharacterTypes.WYATT);
                break;
        }
    }


    // Use this for initialization
    public void LoadCharacter(CharacterTypes CharacterName)
    {
        //playerController = new PlayerController();



        GameObject model = null;
        switch (CharacterName)
        {
            case CharacterTypes.MELLISA:
                model = Instantiate(CharacterModels[0], new Vector3(0, 1, 0), Quaternion.identity);
                
                model.AddComponent<PlayerController>();
                model.GetComponent<PlayerController>().BuildCharacter(6, .08f, "Mellisa", wand);
                print("ive built");
                
                break;
            case CharacterTypes.BRAD:
                model = Instantiate(CharacterModels[1], new Vector3(0, -1, 0), Quaternion.identity);
                //Instantiate(model, new Vector3(0, 0, 0), Quaternion.identity);
                model.AddComponent<PlayerController>();
                model.GetComponent<PlayerController>().BuildCharacter(4, .1f, "Brad", wand);
                
                break;
            case CharacterTypes.WYATT:
                model = Instantiate(CharacterModels[2], new Vector3(0, 0, 0), Quaternion.identity);
                Instantiate(model, new Vector3(0, 0, 0), Quaternion.identity);
                model.AddComponent<PlayerController>();
                model.GetComponent<PlayerController>().BuildCharacter(8, .06f, "Wyatt", wand);
               
                break;
        }
       // model.GetComponent<PlayerController>().AddNewWand(wand);
        // GameObject player = CharacterModels[0];
        //  PlayerController pc = player.AddComponent<PlayerController>();
        //  pc = playerController;
        // GameObject Player = InitializeCharacter();

        Destroy(canvas);

        GameManager.instance.SetPlayerObject();
        GameManager.instance.displayHUD();
        Camera.main.transform.Find("UICanvas").GetComponent<UIBehaviour>().SetCharacterPortrait(CharacterName);
        Camera.main.transform.Find("UICanvas").GetComponent<UIBehaviour>().setMaxHealth(2);
        Destroy(gameObject);
    }




   
}
