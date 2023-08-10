using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbishCollectable : MonoBehaviour
{
    public EquipSlotControl.objectType type;
    public BinControl.BinColour belonging;

    public int num = 1;

    private bool isCollected;
    private bool isThrown;

    public ParticleSystem collectEffect;
    //public GameObject collectedEffect;
    public AudioClip collectClip;

    // Start is called before the first frame update
    void Start()
    {
        isCollected = false;
        isThrown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollected) return;
        if (isThrown) return;
    }

    public void Collected()
    {
        isCollected = true;
        Instantiate(collectEffect, transform.position, Quaternion.identity);
        AudioManager.instance.AudioPlay(collectClip);

        Destroy(this.gameObject);
    }

    public void Thrown()
    {
        isThrown = true;
        Destroy(this.gameObject);
    }
}
