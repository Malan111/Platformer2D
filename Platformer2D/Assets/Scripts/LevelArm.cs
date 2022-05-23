using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelArm : MonoBehaviour
{
    private Finish finish;
    private void Start()
    {
        finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
    }

    public void ActivateLeverArm()
    {
        finish.Activate();
    }

}
