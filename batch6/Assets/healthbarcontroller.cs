using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface healthbarcontroller
{
    void updateHealthBar(float damage, float statusdamage);
    void status(int damagetype, float statusdamage);
    bool checkstatus();
    bool checkdead();
    void grenadekill1(float stack, float slowedby);
    void marked(float markdamage);
}
