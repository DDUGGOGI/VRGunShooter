using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class inputTest : MonoBehaviour
{
    public PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        InputActionMap playerActionMap = playerInput.actions.FindActionMap("XRI LeftHand");
        InputAction fireAction = playerActionMap.FindAction("Activate");

        fireAction.performed += Test001;
    }

    // Update is called once per frame
    void Update()
    {
        // print(playerInput.actions.actionMaps[1].enabled);
        
    }


    public void Test001(InputAction.CallbackContext ctx)
    {
        print(ctx);
        
        
    }
}
