using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{

    public Animator moleAnimator;
    public GameObject mySpawnerLocation;
    public GameObject accessory;
    public bool sameAsLastMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator up_and_down()
    {
        this.moleAnimator.SetTrigger("Up");
        yield return new WaitForSeconds(1.1f);
        this.moleAnimator.SetTrigger("Down");
    }

    public void down()
    {
        this.moleAnimator.SetTrigger("Down");
    }

    public void idle()
    {
        this.moleAnimator.SetTrigger("Idle");
    }

    public void up()
    {
        this.moleAnimator.SetTrigger("Up");
    }

    public void hit()
    {
        this.moleAnimator.SetTrigger("Hit");
    }

    public void wrong()
    {
        this.moleAnimator.SetTrigger("Wrong");
    }

    public void resetAllAnims()
    {
        //this.moleAnimator.ResetTrigger("Wrong");
        this.moleAnimator.ResetTrigger("Down");
        this.moleAnimator.ResetTrigger("Up");
        this.moleAnimator.ResetTrigger("Hit");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //print("hit spawn");
        if (col.gameObject.tag == "position_1" || col.gameObject.tag == "position_2" || col.gameObject.tag == "position_3" || col.gameObject.tag == "position_4")
        {
            mySpawnerLocation = col.gameObject;
        }
    }
}
