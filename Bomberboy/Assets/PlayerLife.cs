using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerLife : NetworkBehaviour {

    [SerializeField]
    private GameObject playerLifeImage;

    private List<GameObject> lifeImages;

    void Start() {
        if (isLocalPlayer) {
            GameObject playerLivesGrid = GameObject.Find("PlayerLivesGrid");

            this.lifeImages = new List<GameObject>();
            for (int lifeIndex = 0; lifeIndex < this.gameObject.GetComponent<PlayerStat>().health; ++lifeIndex) {
                GameObject lifeImage = Instantiate(playerLifeImage, playerLivesGrid.transform) as GameObject;
                this.lifeImages.Add(lifeImage);
            }
        }
    }

    public void UpdateLife() {
        GameObject lifeImage = this.lifeImages[this.lifeImages.Count - 1];
        Destroy(lifeImage);
        this.lifeImages.RemoveAt(this.lifeImages.Count - 1);
    }
}