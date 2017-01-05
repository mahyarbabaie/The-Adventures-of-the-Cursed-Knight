using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsManager : MonoBehaviour
{

    // Text from the button that will be modified
    public Text Up, Down, Left, Right, Attack, Block, Action, Menu;

	// Use this for initialization
	void Start ()
    {
        Up.text = "Up Arrow";
        Down.text = "Down Arrow";
        Left.text = "Left Arrow";
        Right.text = "Right Arrow";
        Attack.text = "Z";
        Block.text = "X";
        Action.text = "C";
        Menu.text = "Escape";
	}

    
	
}
