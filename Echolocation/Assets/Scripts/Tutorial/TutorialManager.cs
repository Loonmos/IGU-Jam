using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    // state machine my beloved

    public GameObject tutScreen;
    public TextMeshProUGUI tutText;

    public enum State {Sound, Move, Jump, Wall, End}
    public State state = State.Sound;

    private float cooldown = 0;
    public float changeTime;
    
    void Start()
    {
        tutScreen.SetActive(false);
    }

    
    void Update()
    {
        CheckState();

        if (state != State.End)
        {
            cooldown += Time.deltaTime;
        }
    }

    private void CheckState()
    {
        switch (state)
        {
            case State.Sound: Sound(); break;
            case State.Move: Move(); break;
            case State.Jump: Jump(); break;
            case State.Wall: Wall(); break;
            case State.End: End(); break;
        }
    }

    private void Sound()
    {
        tutScreen.SetActive(true);
        tutText.SetText("Press the 'Left Mouse Button' to make a sound in the direction of your mouse");

        if (cooldown >= changeTime)
        {
            cooldown = 0;
            state = State.Move;
        }
    }

    private void Move()
    {
        tutScreen.SetActive(true);
        tutText.SetText("Use 'A' and 'D' to move");

        if (cooldown >= changeTime)
        {
            state = State.End;
        }
    }

    private void Jump()
    {
        tutScreen.SetActive(true);
        tutText.SetText("Press 'Spacebar' to jump");

        if (cooldown >= changeTime)
        {
            state = State.End;
        }
    }

    private void Wall()
    {
        tutScreen.SetActive(true);
        tutText.SetText("Hold 'A' or 'D' to cling to walls");

        if (cooldown >= changeTime)
        {
            state = State.End;
        }
    }

    private void End()
    {
        tutScreen.SetActive(false);
        cooldown = 0;
    }
}
