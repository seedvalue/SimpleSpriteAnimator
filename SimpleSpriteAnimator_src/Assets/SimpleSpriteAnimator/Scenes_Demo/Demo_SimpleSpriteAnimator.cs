using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// !!!
using SeedValue;





public class Demo_SimpleSpriteAnimator : MonoBehaviour
{


	public SimpleSpriteAnimator m_ZombeyAnimator;










	#region BUTTONS



	public void OnClicked_ZombeyIdle ()
	{
		Debug.Log ("Demo_SimpleSpriteAnimator : OnClicked_ZombeyIdle");

		if (m_ZombeyAnimator != null) {
			m_ZombeyAnimator.PlayAnimation ("Idle");
		} else {
			Debug.LogError ("Demo_SimpleSpriteAnimator : OnClicked_ZombeyIdle : m_ZombeyAnimator == null. Need setup with inspector to pulic value");
		}
	}





	public void OnClicked_ZombeyRun ()
	{
		Debug.Log ("Demo_SimpleSpriteAnimator : OnClicked_ZombeyRun");

		if (m_ZombeyAnimator != null) {
			m_ZombeyAnimator.PlayAnimation ("Run");
		} else {
			Debug.LogError ("Demo_SimpleSpriteAnimator : OnClicked_ZombeyRun : m_ZombeyAnimator == null. Need setup with inspector to pulic value");
		}
	}





	public void OnClicked_ZombeyJump ()
	{
		Debug.Log ("Demo_SimpleSpriteAnimator : OnClicked_ZombeyJump");

		if (m_ZombeyAnimator != null) {
			m_ZombeyAnimator.PlayAnimation ("Jump");
		} else {
			Debug.LogError ("Demo_SimpleSpriteAnimator : OnClicked_ZombeyJump : m_ZombeyAnimator == null. Need setup with inspector to pulic value");
		}
	}




	public void OnClicked_ZombeyAttack ()
	{
		Debug.Log ("Demo_SimpleSpriteAnimator : OnClicked_ZombeyAttack");

		if (m_ZombeyAnimator != null) {
			m_ZombeyAnimator.PlayAnimation ("Attack");
		} else {
			Debug.LogError ("Demo_SimpleSpriteAnimator : OnClicked_ZombeyJump : m_ZombeyAnimator == null. Need setup with inspector to pulic value");
		}
	}




	public void OnClicked_ZombeyDead ()
	{
		Debug.Log ("Demo_SimpleSpriteAnimator : OnClicked_ZombeyDead");

		if (m_ZombeyAnimator != null) {
			m_ZombeyAnimator.PlayAnimation ("Dead");
		} else {
			Debug.LogError ("Demo_SimpleSpriteAnimator : OnClicked_ZombeyDead : m_ZombeyAnimator == null. Need setup with inspector to pulic value");
		}
	}

	#endregion





}
