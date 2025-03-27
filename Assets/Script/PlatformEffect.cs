using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Platform : MonoBehaviour
{
    [SerializeField] private PlatformEffector2D effector;
    [SerializeField] private float waitTime = 0.5f;
    [SerializeField]private bool isFliping = false;
    private InputAction crouchAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        crouchAction = new InputAction(binding: "<Keyboard>/s", interactions: "press");
        crouchAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (crouchAction.triggered)
        {
            if(!isFliping)
            {
                StartCoroutine(Flip());
            }
        }
    }

    public IEnumerator Flip()
    {
        isFliping = true;
        effector.rotationalOffset = 180;
        yield return new WaitForSeconds(waitTime);
        effector.rotationalOffset = 0;
        isFliping = false;
    }
}
