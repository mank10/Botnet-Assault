using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIScript : MonoBehaviour
{
    [SerializeField] private Text goodPktText;
    [SerializeField] private Text virusTerminatedText;
    [SerializeField] private Slider antivirusSlider;
    [SerializeField] private Slider serverHealthSlider;
    [SerializeField] private Text gameOverText;
    [SerializeField] private GameObject restartButton;

    private int goodPacketCount = 0;
    private int virusTerminatedCount = 0;

    public bool isGameOver = false;

    #region Singleton
    public static GameUIScript _Instance;
    public static GameUIScript Instance
    {
        get
        { 
            if(_Instance == null)
            {
                _Instance = (GameUIScript)FindObjectOfType(typeof(GameUIScript));
            }

            return _Instance;
        }
    }
    #endregion

    public void UpdateGoodPackets(int antivirusShield)
    {
        //Updates the Good Packets UI and the Antivirus shield slider.
        goodPacketCount++;
        goodPktText.text = goodPacketCount.ToString();
        antivirusSlider.value = antivirusShield;
    }

    public void UpdateVirusTerminated()
    {
        //Updates the virus terminated text value.
        virusTerminatedCount++;
        virusTerminatedText.text = virusTerminatedCount.ToString();
    }

    public void VirusAttack()
    {
        //Updates the server health after virus packet is hit.
        serverHealthSlider.value -= 5;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        //Game Over Logic
        isGameOver = true;
        restartButton.SetActive(true);
    }

    public void RestartGame()
    {
        restartButton.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        goodPacketCount = 0;
        virusTerminatedCount = 0;
        serverHealthSlider.value = 100;
        antivirusSlider.value = 0;
        goodPktText.text = goodPacketCount.ToString();
        virusTerminatedText.text = virusTerminatedCount.ToString();
        isGameOver = false;
    } 
}
