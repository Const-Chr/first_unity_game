//Use the SampleInputMapCreation.cs to create an input asset (or create one in editor)
//Add run functionality (code + animation)

using UnityEngine;
using UnityEngine.InputSystem;


namespace nm19716
{
    public class Homework3_nm19716 : MonoBehaviour
    {
        private InputActionMap keyboardMap;
        private InputAction fistpump;
        private Animator animator;

        void Start()
        {
            keyboardMap = new InputActionMap("Keyboard");
            fistpump = keyboardMap.AddAction("FistPump", InputActionType.Button, "<Keyboard>/c");
            keyboardMap.Enable();
        }

        void Update()
        {
            animator.SetFloat("fistPumpValue", fistpump.ReadValue<float>());
        }
    }
}
