using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public Canvas canvas;
    public GameObject playerCamera;
    public float health = 100;
    [Header("Test")]
    public Image healthBar;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        canvas.transform.LookAt(playerCamera.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            health--;
            healthBar.fillAmount = health * 0.01f; 

            if (health < 0)
            {
                kill();
            }
        }
    }

    public void kill()
    {
        Destroy(gameObject);
    }
}
