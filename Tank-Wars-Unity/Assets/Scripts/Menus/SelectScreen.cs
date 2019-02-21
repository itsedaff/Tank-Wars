﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectScreen : MonoBehaviour
{
    [SerializeField] bool player1FishedPicking = false;             //if this goes true, player 1 pick phase has ended
    [SerializeField] bool player2FishedPicking = false;             //if this goes true, player 1 pick phase has ended
    [SerializeField] bool bothPlayersReady = false;                 //if this goes true, both players are ready and it will load next level

    [SerializeField] int whatLevelAmI = 1;                          //checks which diff

    int p1ArrayIdex = 0;                                            //keeps track of players 1 tank pick in the array
    int p2ArrayIdex = 0;                                            //keeps track of players 2 tank pick in the array

    [SerializeField] int[] player1TankPicks;                        //holds players 1 picks
    [SerializeField] int[] player2TankPicks;                        //holds players 1 picks

    [SerializeField] GameObject p1Ready;
    [SerializeField] GameObject p1Redo;


    [SerializeField] GameObject p2Ready;
    [SerializeField] GameObject p2Redo;

    GameStatus currentState;


    void Start()
    {
        currentState = FindObjectOfType<GameStatus>();
        whatLevelAmI = currentState.GetDifficuity();

        player1TankPicks = new int[] {0, 0, 0};                     //players 1 picks, 0 = empty, 1 = normal tank, 2 = sniper tank, 3 = mortar tank
        player2TankPicks = new int[] {0, 0, 0};                     //checks which difficulty was picked, 1 = easy, 2 = medium , 3 = hard.
    }


    void Update()
    {
        //this will pop up redo? and ready GUI for player 1
        if(p1ArrayIdex == whatLevelAmI && !player1FishedPicking)
        {
            p1Redo.SetActive(true);
            p1Ready.SetActive(true);
        }

        //this will pop up redo? and ready GUI for player 2
        if (p2ArrayIdex == whatLevelAmI && !player2FishedPicking)
        {
            p2Redo.SetActive(true);
            p2Ready.SetActive(true);
        }

        //player 1 keys
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q was hit");
            Player1Qpick();

        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("w was hit");
            Player1Wpick();

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("e was hit");
            Player1Epick();
            
        }

        //player 2 keys
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Debug.Log("keypad1 was hit");
            Player2KeyPad1pick();

        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Debug.Log("keypad2 was hit");
            Player2KeyPad2pick();

        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Debug.Log("keypad3 was hit");
            Player2KeyPad3pick();

        }


    }
    //player1 pick functions
    private void Player1Qpick()
    {
        if(whatLevelAmI > p1ArrayIdex) 
        {
            player1TankPicks[p1ArrayIdex] = 1;
            p1ArrayIdex++;
            return;
        }
        else
        {

            return;
        }
    }

    private void Player1Wpick()
    {
        if (whatLevelAmI > p1ArrayIdex)
        {
            player1TankPicks[p1ArrayIdex] = 2;
            p1ArrayIdex++;
            return;
        }
        else
        {
            return;
        }
    }

    private void Player1Epick()
    {
        if (whatLevelAmI > p1ArrayIdex)
        {
            player1TankPicks[p1ArrayIdex] = 3;
            p1ArrayIdex++;
            return;
        }
        else
        {
            return;
        }
    }
    //end of player 1 functions

    //player 2 pick functions
    private void Player2KeyPad1pick()
    {
        if (whatLevelAmI > p2ArrayIdex)
        {
            player2TankPicks[p2ArrayIdex] = 1;
            p2ArrayIdex++;
            return;
        }
        else
        {

            return;
        }
    }

    private void Player2KeyPad2pick()
    {
        if (whatLevelAmI > p2ArrayIdex)
        {
            player2TankPicks[p2ArrayIdex] = 2;
            p2ArrayIdex++;
            return;
        }
        else
        {
            return;
        }
    }

    private void Player2KeyPad3pick()
    {
        if (whatLevelAmI > p2ArrayIdex)
        {
            player2TankPicks[p2ArrayIdex] = 3;
            p2ArrayIdex++;
            return;
        }
        else
        {
            return;
        }
    }
    //end of player 2 functions

    //player 1 GUI control functions
    public void Player1RedoPick()
    {
        p1Redo.SetActive(false);
        p1Ready.SetActive(false);
        p1ArrayIdex = 0;
    }

    public void p1DonePicking()
    {
        p1Ready.SetActive(false);
        p1Redo.SetActive(false);
        player1FishedPicking = true;
        currentState.SetPlayer1TanksPick(player1TankPicks);
    }
    //end of player 1 GUI control functions

    //player 2 GUI control functions
    public void Player2RedoPick()
    {
        p2Redo.SetActive(false);
        p2Ready.SetActive(false);
        p2ArrayIdex = 0;
    }

    public void p2DonePicking()
    {
        p2Ready.SetActive(false);
        p2Redo.SetActive(false);
        player2FishedPicking = true;
        currentState.SetPlayer2TanksPick(player2TankPicks);
    }
    //end of player 2 GUI control functions



}
