using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerScript : MonoBehaviour
{
    public int serverHealth = 100;
    public int antivirusShield = 0;

    public GameObject shieldEffect;

    [SerializeField]private bool serverInvincible = false;

    IEnumerator InvincibilityTimer()
    {
        shieldEffect.SetActive(true);
        for(int i = 5; i > 0; i--)
        {
            yield return new WaitForSeconds(1);
        }
        antivirusShield = 0;
        serverInvincible = false;
        shieldEffect.SetActive(false);
    }

    public void GameRestarted()
    {
        serverHealth = 100;
        antivirusShield = 0;
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.CompareTag("GoodPacket"))
        {
            //Debug.Log("GoodPacket collided");
            antivirusShield += 5;
            if (antivirusShield >= 100)
            {
                serverInvincible = true;
                StartCoroutine("InvincibilityTimer");
            }
            GameUIScript.Instance.UpdateGoodPackets(antivirusShield);
        }
        else if (obj.gameObject.CompareTag("Virus"))
        {
            //Debug.Log("Virus collided");
            if(!serverInvincible)
            {
                serverHealth -= 5;
                GameUIScript.Instance.VirusAttack();
                if(serverHealth <= 0)
                {
                    GameUIScript.Instance.GameOver();
                }
            }
        }
        obj.transform.parent.GetComponentInChildren<ClientComputer>().PacketReached();
        obj.gameObject.SetActive(false);
    }
}
