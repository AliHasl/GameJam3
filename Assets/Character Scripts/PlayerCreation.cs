using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreation : MonoBehaviour {


    //Basic Character Stuff

    //public variables

    public Transform HandTransform;
    public GameObject[] CharacterModels;
    public WandStats wand;




    public enum CharacterTypes
    {
        MELLISA,
        BRAD,
        WYATT
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
    void LoadCharacter(CharacterTypes CharacterName)
    {
        playerController = new PlayerController();



        GameObject model = null;
        switch (CharacterName)
        {
            case CharacterTypes.MELLISA:
                model = CharacterModels[0];
                model.AddComponent<PlayerController>();
                model.GetComponent<PlayerController>().BuildCharacter(6, .08f, "Mellisa");
                break;
            case CharacterTypes.BRAD:
                model = CharacterModels[1];
                model.AddComponent<PlayerController>();
                model.GetComponent<PlayerController>().BuildCharacter(4, .1f, "Brad");
                
                break;
            case CharacterTypes.WYATT:
                model = CharacterModels[2];
                model.AddComponent<PlayerController>();
                model.GetComponent<PlayerController>().BuildCharacter(8, .06f, "Wyatt");
               
                break;
        }
        model.GetComponent<PlayerController>().AddNewWand(wand);
       // GameObject player = CharacterModels[0];
      //  PlayerController pc = player.AddComponent<PlayerController>();
      //  pc = playerController;
        // GameObject Player = InitializeCharacter();


        Destroy(this.gameObject);
    }




   
}
