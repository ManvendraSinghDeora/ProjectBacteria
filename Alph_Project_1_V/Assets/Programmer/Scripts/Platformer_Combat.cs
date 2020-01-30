using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer_Combat : MonoBehaviour
{
    [SerializeField] KeyCode Attack_Key;

    [SerializeField] GameObject AttackHitBox;

    [SerializeField] int maxAttackRandomValue;

    int AttackRandomValue;

    bool isAttacking = false;
    bool isGrounded;

    private PlayerPlatofrmerController playerPlatformerController;

    // Start is called before the first frame update
    void Start()
    {
        AttackHitBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    { 
        //Checking if player is not grounded and is pressing attack key
        if (isGrounded == false && Input.GetKeyDown(Attack_Key))
        {
            //Flying kich animation will playe here or flying attack.
        }

        if (Input.GetKeyDown(Attack_Key))
        {
            isAttacking = true;
            DoAttack(); 
            //if (Input.GetKey(Attack_Key))
            //    return;
        }
    }
    private void DoAttack()
    {
        //Random value in int for switch case
        AttackRandomValue = UnityEngine.Random.Range(0, maxAttackRandomValue);

        // Switch case for grounded attack method which will be called randomly.
        switch (AttackRandomValue)
        {
            case 0:
                {
                    StartCoroutine(LightAttack());
                    break;
                }
            case 1:
                {
                    StartCoroutine(HeavyAttack());
                    break;
                }

            default:
                Debug.Log("Wrong case number");
                break;
        }

        // Switch case for flying attack method which will be called randomly
    }
    IEnumerator LightAttack()
    {
        Debug.Log("LightAttack");

        //Set attack hit box active
        AttackHitBox.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        AttackHitBox.SetActive(false);
    }
    IEnumerator HeavyAttack()
    {
        Debug.Log("HeavyAttack");

        //Set attack hit box active
        AttackHitBox.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        AttackHitBox.SetActive(false);
    }
}
