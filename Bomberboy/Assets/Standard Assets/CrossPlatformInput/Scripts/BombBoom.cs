using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BombBoom :  NetworkBehaviour {

    [SerializeField]
    private BoxCollider2D collider2D;

    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField]
    private int explosionRange;

    [SerializeField]
    private float explosionDuration;

    [SerializeField]
    private float timeUntillBoom;

    private float spawnTime;

    void Start() {
        spawnTime = Time.time;
    }

    void Update() {
        if (spawnTime + timeUntillBoom < Time.time) {
            CmdExplode();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        this.collider2D.isTrigger = false;
    }

    [Command]
    public void CmdExplode() {
        if (NetworkServer.active) {
            GameObject explosion = Instantiate(explosionPrefab, this.gameObject.transform.position, Quaternion.identity) as GameObject;
            NetworkServer.Spawn(explosion);
            Destroy(explosion, this.explosionDuration);
            CmdCreateExplosions(Vector2.left);
            CmdCreateExplosions(Vector2.right);
            CmdCreateExplosions(Vector2.up);
            CmdCreateExplosions(Vector2.down);
            NetworkServer.Destroy(this.gameObject);
        }
    }

    [Command]
    private void CmdCreateExplosions(Vector2 direction) {
        ContactFilter2D contactFilter = new ContactFilter2D();

        Vector2 explosionDimensions = explosionPrefab.GetComponent<SpriteRenderer>().bounds.size;
        Vector2 explosionPosition = (Vector2)this.gameObject.transform.position + (explosionDimensions.x * direction);
        for (int explosionIndex = 1; explosionIndex < explosionRange; explosionIndex++) {
            Collider2D[] colliders = new Collider2D[4];
            Physics2D.OverlapBox(explosionPosition, explosionDimensions, 0.0f, contactFilter, colliders);
            bool foundBlockOrWall = false;
            foreach (Collider2D collider in colliders) {
                if (collider) {
                    foundBlockOrWall = collider.tag == "Wall" || collider.tag == "Block";
                    if (collider.tag == "Block") {
                        NetworkServer.Destroy(collider.gameObject);
                    }
                    if (foundBlockOrWall) {
                        break;
                    }
                }
            }
            if (foundBlockOrWall) {
                break;
            }
            GameObject explosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity) as GameObject;
            NetworkServer.Spawn(explosion);
            Destroy(explosion, this.explosionDuration);
            explosionPosition += (explosionDimensions.x * direction);
        }
    }
}
