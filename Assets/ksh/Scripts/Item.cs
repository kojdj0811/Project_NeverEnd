using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private GameObject soundPrefab;

    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D collider;

    AudioSource audioSource;
    //public AudioClip getItemSound;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<CapsuleCollider2D>();

        audioSource = GetComponent<AudioSource>();
        //audioSource.clip = getItemSound;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bird")
        {
            Debug.Log(audioSource.clip.name);
            //audioSource.Play();
            Destroy(Instantiate(soundPrefab), 1.0f);
            Use();
        }
            
    }

    public virtual void Use() {}
}