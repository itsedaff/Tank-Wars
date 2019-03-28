﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Weapon
{
    // Constructors 
    public Bishop(Tank tank) {
        this.damage = 30;
        this.distance = 5;
        this.tank = tank;
        this.knockback = 0;

        // Orientation is up by default for red and down by default for blue
        this.orientation = (this.tank.getPlayer().getPlayerColor() == PlayerColors.Red) ? Orientations.Up : Orientations.Down;
    }

    // Member Functions
    public override bool isValidAttack(Grid grid, CoordinateSet targetCoordinates, bool updateState) {
        bool validAttack = false;
        GridNode targetNode = grid.getGridNode(targetCoordinates);
        // Check for valid attack
        for (int i = 0; i < 4; i++) {
            validAttack = attackCheck(grid, i, tank.getCoordinates(), targetNode, updateState);

            if (validAttack) {
                break;
            }
        }

        // If all is well, decrement targeted player health and update the game state.
        if (validAttack) {
            if (updateState) {
                Tank targetTank = targetNode.getTank();
                targetTank.decrementHealth(this.damage);

                // cannonScript.fire(orientation);

                Debug.Log("Player " + this.tank.getPlayer().getPlayerColor() + " attacks Player " +
                          targetTank.getPlayer().getPlayerColor() + " for " + this.damage + " damage!");
                Debug.Log("Player " + targetTank.getPlayer().getPlayerColor() + "'s health is now at: " + targetTank.getHealth());
            }

            return true;

        }
        else {
            Debug.Log("Invalid Attack!");
        }

        return false;
    }

    private bool attackCheck(Grid grid, int currentIteration,
                            CoordinateSet currentTankCoordinates,
                            GridNode targetNode,
                            bool updateState) {
        int gridSize = grid.getGridSize();
        int currentTankX = currentTankCoordinates.getX();
        int currentTankY = currentTankCoordinates.getY();
        int targetNodeX = targetNode.getCoordinateSet().getX();
        int targetNodeY = targetNode.getCoordinateSet().getY();

        if (updateState) {
            /*
            redTanks = GameObject.FindGameObjectsWithTag("Red Tank");
            blueTanks = GameObject.FindGameObjectsWithTag("Blue Tank");

            for (int i = 0; i < redTanks.Length; i++) {
                if (redTanks[i].transform.position == new Vector3(currentTankCoordinates.getX(), 1f, currentTankCoordinates.getY())) {
                    cannonFire = redTanks[i];
                }
            }

            for (int i = 0; i < blueTanks.Length; i++) {
                if (blueTanks[i].transform.position == new Vector3(currentTankCoordinates.getX(), 1f, currentTankCoordinates.getY())) {
                    cannonFire = blueTanks[i];
                }
            }

            cannonScript = cannonFire.GetComponent<CannonProjectile>();

            if (cannonScript == null) {
                Debug.Log("CannonProjectile Script is null");
            }
            */
        }

        for (int i = 1; i <= this.distance; i++) {
            switch (currentIteration) {
                case 0:
                    // Check for index out of bounds
                    if (currentTankX + i >= gridSize || currentTankY + i >= gridSize) { continue; }

                    // Check for mountains
                    if (grid.getGridNode(currentTankX + i, currentTankY + i).getTerrain() is Mountain) {
                        return false;
                    }

                    // Do final check to see if this is the target node
                    if ((currentTankX + i) == targetNodeX && (currentTankY + i) == targetNodeY) {
                        return true;
                    }

                    break;
                case 1:
                    // Check for index out of bounds
                    if (currentTankX - i < 0 || currentTankY - i < 0) { continue; }

                    // Check for mountains
                    if (grid.getGridNode(currentTankX - i, currentTankY - i).getTerrain() is Mountain) {
                        return false;
                    }

                    // Do final check to see if this is the target node
                    if ((currentTankX - i) == targetNodeX && (currentTankY - i) == targetNodeY) {
                        return true;
                    }

                    break;
                case 2:
                    // Check for index out of bounds
                    if (currentTankX - i < 0 ||currentTankY + i >= gridSize) { continue; }

                    // Check for mountains
                    if (grid.getGridNode(currentTankX - i, currentTankY + i).getTerrain() is Mountain) {
                        return false;
                    }

                    // Do final check to see if this is the target node
                    if ((currentTankX - i) == targetNodeX && (currentTankY + i) == targetNodeY) {
                        return true;
                    }

                    break;
                case 3:
                    // Check for index out of bounds
                    if (currentTankX + i >= gridSize || currentTankY - i < 0) { continue; }

                    // Check for mountains
                    if (grid.getGridNode(currentTankX + i, currentTankY - i).getTerrain() is Mountain) {
                        return false;
                    }

                    // Do final check to see if this is the target node
                    if ((currentTankX + i) == targetNodeX && (currentTankY - i) == targetNodeY) {
                        return true;
                    }

                    break;
            }
        }

        return false;
    }

    public override ArrayList getValidAttacks(Grid grid) {
        int gridSize = grid.getGridSize();
        int currentTankX = this.tank.getCoordinates().getX();
        int currentTankY = this.tank.getCoordinates().getY();
        ArrayList validAttacks = new ArrayList();
        bool skip = false;

        for (int i = 0; i < 4; i++) {
            skip = false;
            for (int j = 1; j <= this.distance; j++) {
                switch (i) {
                    case 0:
                        // Check for index out of bounds
                        if (currentTankX + j >= gridSize || currentTankY + j >= gridSize) { continue; }

                        // Check for mountains
                        if (grid.getGridNode(currentTankX + j, currentTankY + j).getTerrain() is Mountain) {
                            skip = true;
                            break;
                        }

                        validAttacks.Add(new CoordinateSet(currentTankX + j, currentTankY + j));

                        break;
                    case 1:
                        // Check for index out of bounds
                        if (currentTankX - j < 0 || currentTankY - j < 0) { continue; }

                        // Check for mountains
                        if (grid.getGridNode(currentTankX - j, currentTankY - j).getTerrain() is Mountain) {
                            skip = true;
                            break;
                        }

                        validAttacks.Add(new CoordinateSet(currentTankX - j, currentTankY - j));
                        break;
                    case 2:
                        // Check for index out of bounds
                        if (currentTankX - j < 0 || currentTankY + j >= gridSize) { continue; }

                        // Check for mountains
                        if (grid.getGridNode(currentTankX - j, currentTankY + j).getTerrain() is Mountain) {
                            skip = true;
                            break;
                        }

                        validAttacks.Add(new CoordinateSet(currentTankX - j, currentTankY + j));
                        break;
                    case 3:
                        // Check for index out of bounds
                        if (currentTankX + j >= gridSize || currentTankY - j < 0) { continue; }

                        // Check for mountains
                        if (grid.getGridNode(currentTankX + j, currentTankY - j).getTerrain() is Mountain) {
                            skip = true;
                            break;
                        }

                        validAttacks.Add(new CoordinateSet(currentTankX + j, currentTankY - j));
                        break;
                }

                if (skip) {
                    break;
                }
            }
        }

        return validAttacks;
    }
}
