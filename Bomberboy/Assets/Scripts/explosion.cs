using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class explosion : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.tag.Equals("Character") && !coll.gameObject.GetComponent<PlayerStat>().invuln) {
            coll.gameObject.GetComponent<PlayerStat>().TakeDamage();
        } else if (coll.tag.Equals("Bomb")) {
            coll.gameObject.GetComponent<BombBoom>().CmdExplode();

        }
    }
}
