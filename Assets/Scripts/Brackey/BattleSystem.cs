using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	public Transform playerBattleStation;
	public Transform enemyBattleStation;

	Unit playerUnit;
	Unit enemyUnit;

	public Text dialogueText;

	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

	public BattleState state;

	System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
		state = BattleState.START;
		StartCoroutine(SetupBattle());
    }

	IEnumerator SetupBattle()
	{
		GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<Unit>();

		GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyGO.GetComponent<Unit>();

		dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(2f);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	IEnumerator PlayerAttack()
	{
		bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

		state = BattleState.ENEMYTURN;
		enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "The attack is successful!";

		yield return new WaitForSeconds(2f);

		if(isDead)
		{
			state = BattleState.WON;
			EndBattle();
		} else
		{
			StartCoroutine(EnemyTurn());
		}
	}

	IEnumerator ChargedAttack()
	{
		bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

		state = BattleState.ENEMYTURN;
		
		dialogueText.text = "You carefully aim your next shot...";

		yield return new WaitForSeconds(2f);

		if(isDead)
		{
			state = BattleState.WON;
			EndBattle();
		} else
		{
			StartCoroutine(EnemyChargedTurn());
		}
	}

	IEnumerator EnemyTurn()
	{
		bool isDead;

		int roll = random.Next(100);

		if (roll >= 75)
		{
			dialogueText.text = enemyUnit.unitName + " attacks!";

			yield return new WaitForSeconds(1f);

			dialogueText.text = "Critical hit!";

			yield return new WaitForSeconds(1f);

			isDead = playerUnit.TakeDamage(enemyUnit.damage * 3);

			playerHUD.SetHP(playerUnit.currentHP);
		} else
		{
			dialogueText.text = enemyUnit.unitName + " attacks!";

			yield return new WaitForSeconds(1f);

			isDead = playerUnit.TakeDamage(enemyUnit.damage);

			playerHUD.SetHP(playerUnit.currentHP);

			yield return new WaitForSeconds(1f);
		}

		if(isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		} else
		{
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}

	}

	IEnumerator EnemyChargedTurn()
	{
		dialogueText.text = enemyUnit.unitName + " attacks!";

		yield return new WaitForSeconds(1f);

		bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

		playerHUD.SetHP(playerUnit.currentHP);

		yield return new WaitForSeconds(1f);

		if(isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		} else
		{
			StartCoroutine(PlayerReleaseAttack());
		}

	}

	IEnumerator PlayerReleaseAttack()
	{
		bool isDead = enemyUnit.TakeDamage(playerUnit.damage * 3);

		state = BattleState.ENEMYTURN;
		enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "You loose your arrow with pinpoint accuracy!";

		yield return new WaitForSeconds(2f);

		if(isDead)
		{
			state = BattleState.WON;
			EndBattle();
		} else
		{
			StartCoroutine(EnemyTurn());
		}
	}

	void EndBattle()
{
	StartCoroutine(HandleEndBattle());
}

IEnumerator HandleEndBattle()
{
	if (state == BattleState.WON)
	{
		dialogueText.text = "You won the battle!";
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene("WorldBuilding");
	}
	else if (state == BattleState.LOST)
	{
		dialogueText.text = "You were defeated.";
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene("WorldBuilding");
	}
}


	void PlayerTurn()
	{
		dialogueText.text = "Choose an action:";
	}

	IEnumerator PlayerHeal()
	{
		playerUnit.Heal(5);

		playerHUD.SetHP(playerUnit.currentHP);
		dialogueText.text = "You feel renewed strength!";

		yield return new WaitForSeconds(2f);

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}

	public void OnAttackButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerAttack());
	}

	public void OnChargeButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(ChargedAttack());
	}

	public void OnHealButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerHeal());
	}

}
