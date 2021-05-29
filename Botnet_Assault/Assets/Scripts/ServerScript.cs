using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerScript : MonoBehaviour
{
    public int serverHealth = 100;
    public int antivirusShield = 0;

    public GameObject shieldEffect;
    private Renderer shieldRenderer;

    [SerializeField]private bool serverInvincible = false;

    private void Start()
    {
        shieldRenderer = shieldEffect.transform.GetComponentInChildren<Renderer>();
    }

    IEnumerator InvincibilityTimer()
    {
        shieldEffect.SetActive(true);
        yield return new WaitForSeconds(4);
        for (int i = 5; i > 0; i--)
        {
            shieldRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            shieldRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
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
