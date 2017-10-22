using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SeedValue
{

	public class SimpleSpriteAnimator : MonoBehaviour
	{

		public Dictionary<string, SimpleSpriteOneAnimation> m_AllAnimations;





		public bool m_PlayOnStartAnimation = true;
		public string m_OnStartAnimationName = "Idle";



		[Header ("Debug all animations:")]
		public List<SimpleSpriteOneAnimation> m_AllAnimList;
		public SimpleSpriteOneAnimation m_CurrentPlaing;




		private IEnumerator InitAnimations ()
		{
			yield return StartCoroutine (ScanChildsAnimations ());

			if (m_PlayOnStartAnimation == true) {
				this.PlayAnimation (m_OnStartAnimationName);
			}
		}





		public void PlayAnimation (string _name)
		{
			if (m_AllAnimations.ContainsKey (_name)) {
			
//
//				if (m_CurrentPlaing == m_AllAnimations [_name]) {
//				//same animation
//					return;
//				}


				//HideAllAnimations ();
				m_AllAnimations [_name].gameObject.SetActive (true);
				m_AllAnimations [_name].Play ();


				if (m_CurrentPlaing != null && m_CurrentPlaing != m_AllAnimations [_name]) {
					//stop previuse, but if playing same, not stop
					m_CurrentPlaing.Stop ();
				}

				m_CurrentPlaing = m_AllAnimations [_name];

			} else {
				Debug.LogError ("SimpleSpriteAnimator : PlayAnimation : Anim name = \" " + _name + "\" is not exist in animator  \"" + transform.name + "\". Check childs of this object \"" + transform.name + "\" to exist GameObject as  \"" + _name + "\" ");
			}
		}





		private IEnumerator ScanChildsAnimations ()
		{
			m_AllAnimList = new List<SimpleSpriteOneAnimation> ();

			foreach (Transform child in transform) {
				//child is your child transform
				//m_SpritesList.Add (child.gameObject);
				SimpleSpriteOneAnimation _oneAnim = child.transform.GetComponent<SimpleSpriteOneAnimation> ();




				if (_oneAnim != null) {
					m_AllAnimList.Add (_oneAnim);
				}


			}


			m_AllAnimations = new Dictionary<string, SimpleSpriteOneAnimation> ();

			foreach (SimpleSpriteOneAnimation _oneTmp in m_AllAnimList) {
				_oneTmp.SetCurrentAnimator (this);
				m_AllAnimations.Add (_oneTmp.transform.name, _oneTmp);
				_oneTmp.gameObject.SetActive (false);
			}

			//_animListTmp.Clear ();


			yield return null;
		}


	




		private void HideAllAnimations ()
		{
			foreach (SimpleSpriteOneAnimation _one in m_AllAnimList) {
				_one.gameObject.SetActive (false);
			}
		}






		void Awake ()
		{
			
		}


		void Start ()
		{
			StartCoroutine (InitAnimations ());
		}







	}

}