using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            animator.SetBool("isHitting", true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("isHitting", false);
        }
    }
}
