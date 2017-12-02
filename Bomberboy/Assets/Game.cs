using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    [SerializeField]
    private KeyValuePair<string, string>[] placement;

    // Use this for initialization
    void Start() {
        placement = new KeyValuePair<string, string>[4];
    }

    // Update is called once per frame
    void Update() {

    }

    public void RegisterPlayerDeath(KeyValuePair<string, string> player) {
        if (placement.Length >= 4) {
            return;
        }
        placement[placement.Length - 1] = player;
        // TODO: Remove this logging statement
        Debug.Log(placement);
    }
}
