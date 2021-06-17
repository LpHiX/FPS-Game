using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    public Rigidbody bulletObject;
    public float bulletSpeed = 30f;
    public GameObject camera;

    public float fireDelay = 0.1f;
    public float reloadDelay = 1;

    float fireCooldown;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        fireBullet();
        fireCooldown -= Time.deltaTime;
        Debug.Log(fireCooldown);
    }

    public void fireBullet()
    {
        if (Input.GetButton("Fire1") && fireCooldown < 0)
        {
            Rigidbody bullet = Instantiate(bulletObject, camera.transform.position + camera.transform.forward * 1f, camera.transform.rotation * Quaternion.Euler(90, 0, 0));
            bullet.AddForce((camera.transform.forward + new Vector3(NextGaussian(), NextGaussian(), NextGaussian())) * bulletSpeed, ForceMode.Impulse);
            fireCooldown = fireDelay;
        }
    }
    public static float NextGaussian()
    {
        float v1, v2, s;
        do
        {
            v1 = 2.0f * Random.Range(0f, 1f) - 1.0f;
            v2 = 2.0f * Random.Range(0f, 1f) - 1.0f;
            s = v1 * v1 + v2 * v2;
        } while (s >= 1.0f || s == 0f);
        s = Mathf.Sqrt((-2.0f * Mathf.Log(s)) / s);

        return v1 * s * 0.02f;
    }
}
