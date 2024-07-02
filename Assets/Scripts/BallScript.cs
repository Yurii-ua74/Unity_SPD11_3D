using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField]
    private Material daySkyBox;
    [SerializeField]
    private Material nightSkyBox;
    [SerializeField]
    private Light mainLight;


    private Rigidbody rb;
    private float forceFactor = 2f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float fx = Input.GetAxis("Horizontal") * Time.timeScale * forceFactor;
        float fz = Input.GetAxis("Vertical") * Time.timeScale * forceFactor;
        // rb.AddForce(fx, 0, fz); - за світовими координатами, незалежно від камери
        // якщо потрібно обертати камеру, то слід використовувати її вектори
        // forward та right
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0f;
        camForward = camForward.normalized;

        Vector3 moveDirection = 
            fz * camForward +
            fx * Camera.main.transform.right;
        rb.AddForce(moveDirection);

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (RenderSettings.skybox = daySkyBox)
            {
                RenderSettings.skybox = nightSkyBox;
                RenderSettings.skybox.SetFloat("_Exposure", 0f);
                RenderSettings.ambientIntensity = 0f;
                mainLight.intensity = 0f;
            }
            else
            {
                RenderSettings.skybox = daySkyBox;
                RenderSettings.skybox.SetFloat("_Exposure", 1f);
                RenderSettings.ambientIntensity = 1f;
                mainLight.intensity = 1f;
            }
        }
    }
}
