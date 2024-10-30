using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialBullet : MonoBehaviour
{

    private static PlayerSpecialBullet instance;

    [Header("Ư�� źȯ")]
    [SerializeField] private int[] specialBullet;
    public int[] SpecialBullet { get { return specialBullet; } }



    public static PlayerSpecialBullet Instance
    {
        get
        {
            return instance;

        }
    }

    void Awake()
    {
        if (instance == null)
        {

            instance = this;
        }
        else
        {
            Destroy(this);
        }

        

    }

}
