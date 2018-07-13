using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SpriteAnimation : MonoBehaviour
{/*
    public AnimationClip animClip;
    public Image spriteRenderer;
    public Sprite[] sprites;
    public bool update;
    AnimationEvent[] animationEvents;

    public virtual void Update ()
    {/*
        if (!update)
            return;
        update = false;
        animationEvents = new AnimationEvent[sprites.Length];
        for (int i = 0; i < sprites.Length; i ++)
        {
            animationEvents[i] = new AnimationEvent();
            animationEvents[i].time = i * (1f / animClip.frameRate);
            animationEvents[i].functionName = "SetSprite";
            animationEvents[i].objectReferenceParameter = sprites[i];
        }
        AnimationUtility.SetAnimationEvents(animClip, animationEvents);
    }

    public virtual void SetSprite (Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
*/}