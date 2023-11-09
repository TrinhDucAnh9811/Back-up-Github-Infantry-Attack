using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DestroyAll();
        }
    }
    public void DestroyAll()
    {
        EnemyHealth[] ees =GameObject.FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);
        foreach(EnemyHealth e in ees)
        {
            e.TakeDamage(300);
        }
    }
}
