using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordUpgrade : MonoBehaviour
{
    [SerializeField] private Loot fireSwordLoot;
    [SerializeField] private AnimatorOverrideController fireSwordAnimations;
    private Inventory inventory = Inventory.Instance;
    private bool alreadyTriggered = false;

    private void Awake()
    {
        if (fireSwordLoot == null || fireSwordAnimations == null || inventory == null)
        {
            Debug.LogError("fire sword loot or animation overrider or inventory are not assigned in SwordUpgradeTrigger.");
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (alreadyTriggered)
        {
            return;
        }
        if (collision.CompareTag("Player"))
        {
            if (inventory.HasItem(fireSwordLoot))
            {
                inventory.Remove(fireSwordLoot);
                StartCoroutine(UpgradeSword(collision.gameObject));
                alreadyTriggered = true;
            }
            else
            {
                Debug.Log("Player does not have the Fire Sword.");
            }
        }
    }
    private IEnumerator UpgradeSword(GameObject player)
    {
        Animator animator = player.GetComponent<Animator>();
        animator.SetTrigger("UpgradeSword");
        yield return new WaitForSeconds(1.6f);

        animator.runtimeAnimatorController = fireSwordAnimations;

        IncreaseAllAttackDamages(player);
    }

    private void IncreaseAllAttackDamages(GameObject player)
    {
        PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
        if (playerAttack != null)
        {
            foreach (var attack in playerAttack.Attacks)
            {
                attack.SetDamage(attack.Damage * 2f);
            }
        }
    }
}
