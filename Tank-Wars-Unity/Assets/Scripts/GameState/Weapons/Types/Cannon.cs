﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Weapon
{
    // Data Members

    // Constructors 
    public Cannon() {
        this.damage = 2;
        this.distance = 3;
    }

    // Member Functions
    public override bool isValidAttack(Grid grid) {
        return true;
    }
}