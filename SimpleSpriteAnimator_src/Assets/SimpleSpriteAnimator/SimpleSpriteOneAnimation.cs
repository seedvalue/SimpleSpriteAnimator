using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SeedValue
{

	public class SimpleSpriteOneAnimation : MonoBehaviour
	{
	

		Transform m_RootForSprites;

		[Header ("Animation type:")]
		public AnimationType m_AnimationType = AnimationType.LOOP;
		public float m_WaitBetweenFramesTime = 0.5F;


	


		//	public bool m_PlayInStart = true;




		[Header ("When LOOP settings:")]

		// in first run from 0 to last frame, but when loop end, its will start loop from other frame;
		public int m_LoopedFrom = 0;






		[Header ("When ONCE settings:")]
		public bool m_IsNeedPlayOtherAnimationAfter = false;
		public SimpleSpriteOneAnimation m_PlayedAfterCurrentFinished;

		[Space]




		[Header ("Debug: not edit, filled auto")]
		public SimpleSpriteAnimator m_CurrentRootAnimator;
		public List<GameObject> m_SpritesList;
		public int m_CurrentFrame = 0;
		public bool m_isNowPlaying = false;







		public void Play ()
		{

			if (m_AnimationType == AnimationType.LOOP && m_isNowPlaying == true) {
				//when loop already started we dont need run again
				return;
			}

			m_IsPaused = false;

			m_CurrentFrame = 0;

			if (m_isNowPlaying == true) {
				//not start Co
			} else {
				//StartCoroutine (StartAll ());
				AnimationLoop ();
			}

			m_isNowPlaying = true;
		}




		public void Pause ()
		{
			//StopAllCoroutines ();
			//m_CurrentFrame = 0;
			//_isIncreasePingPong = true;

			//ShowFrame (m_CurrentFrame);
			m_isNowPlaying = true;
			m_IsPaused = true;

			//transform.gameObject.SetActive (!_isNeedHide);

		}







		public void Stop (bool _isNeedHide)
		{
			StopAllCoroutines ();
			//m_CurrentFrame = 0;
			_isIncreasePingPong = true;

			//ShowFrame (m_CurrentFrame);
			m_isNowPlaying = false;

			transform.gameObject.SetActive (!_isNeedHide);

		}





		private IEnumerator StartAll ()
		{
			//	yield return StartCoroutine (ScanChildsRootCo ());
			Debug.Log ("StartAll");
			//	yield return StartCoroutine (AnimationLoopCo ());

			yield return null;

		}





		private bool _isChildsScanned = false;

		private IEnumerator ScanChildsRootCo ()
		{
			if (_isChildsScanned == true) {
				yield return null;
			}

			if (m_RootForSprites == null) {
				Debug.LogError ("SimpleSpriteAnimator : ScanChildsRoot : m_RootForSprites == null. Need setup in inspector root for sprites object. All sprites is need placed child of root.  game object name = " + transform.name);
				yield return null;
			}


			m_SpritesList = new List<GameObject> ();

			foreach (Transform child in m_RootForSprites) {
				//child is your child transform
				m_SpritesList.Add (child.gameObject);
				child.gameObject.SetActive (false);
			}
			_isChildsScanned = true;
			yield return null;
		}






		private void ScanChildsRoot ()
		{


			if (_isChildsScanned == true) {
				return;
			}

			if (m_RootForSprites == null) {
				Debug.LogError ("SimpleSpriteAnimator : ScanChildsRoot : m_RootForSprites == null. Need setup in inspector root for sprites object. All sprites is need placed child of root.  game object name = " + transform.name);
				return;
			}


			m_SpritesList = new List<GameObject> ();

			foreach (Transform child in m_RootForSprites) {
				//child is your child transform
				m_SpritesList.Add (child.gameObject);
				child.gameObject.SetActive (false);
			}
			_isChildsScanned = true;

		}



		public bool m_IsPaused = false;

		//for handle change type on fly when tests in inspector, or may be will changed with script
		private AnimationType m_AnimationTypeTMP;


		private IEnumerator AnimationLoopCo ()
		{
			m_AnimationTypeTMP = m_AnimationType;


			Debug.Log ("AnimationLoop");


			while (m_IsPaused == false) {

				if (m_AnimationTypeTMP != m_AnimationType) {
					// have changed, we reset to zero
					m_CurrentFrame = 0;
					_isIncreasePingPong = true;
				}


				switch (m_AnimationType) {

				case AnimationType.LOOP:

					TypeLoop ();
					Debug.Log ("AnimationLoop");

					yield return  new WaitForSeconds (m_WaitBetweenFramesTime);
					break;
				


				case AnimationType.PING_PONG:

					TypePingPong ();
					yield return  new WaitForSeconds (m_WaitBetweenFramesTime);
					break;
				
				
				case AnimationType.ONCE:

					TypeOnce ();
					yield return  new WaitForSeconds (m_WaitBetweenFramesTime);
					break;

				
				
				
				}






			}
		}



		//for can change on fly in editor //to Fixed update
		private void CheckForTypeChanged ()
		{

			if (m_AnimationTypeTMP != m_AnimationType) {
				// have changed, we reset to zero
				m_CurrentFrame = 0;
				_isIncreasePingPong = true;
				CancelInvoke ();
				AnimationLoop ();
			}
		}



		private void AnimationLoop ()
		{
			m_AnimationTypeTMP = m_AnimationType;
			m_IsPaused = false;
			m_CurrentFrame = 0;
			//Debug.Log ("AnimationLoop");


			//	while (m_IsPaused == false) {



			switch (m_AnimationType) {

			case AnimationType.LOOP:

				InvokeRepeating ("TypeLoop", 0, m_WaitBetweenFramesTime);
				//TypeLoop ();

				Debug.Log ("AnimationLoop");

			//	yield return  new WaitForSeconds (m_WaitBetweenFramesTime);
				break;



			case AnimationType.PING_PONG:

				InvokeRepeating ("TypePingPong", 0, m_WaitBetweenFramesTime);
				//TypePingPong ();
			//	yield return  new WaitForSeconds (m_WaitBetweenFramesTime);
				break;


			case AnimationType.ONCE:
				InvokeRepeating ("TypeOnce", 0, m_WaitBetweenFramesTime);

				//TypeOnce ();
				//yield return  new WaitForSeconds (m_WaitBetweenFramesTime);
				break;




			//	}






			}
		}






		private void TypeLoop ()
		{
			if (m_IsPaused == true) {
				CancelInvoke ();
			}
			
			this.ShowFrame (m_CurrentFrame);
			m_CurrentFrame++;
			if (m_CurrentFrame > m_SpritesList.Count - 1) {
				m_CurrentFrame = m_LoopedFrom;
			}


		}




		private bool _isIncreasePingPong = true;

		private void TypePingPong ()
		{
			if (m_IsPaused == true) {
				CancelInvoke ();
			}

			this.ShowFrame (m_CurrentFrame);

			if (_isIncreasePingPong == true) {
				m_CurrentFrame++;
			} else {
				m_CurrentFrame--;
			}


			if (m_CurrentFrame <= 0) {
				_isIncreasePingPong = true;
				//m_CurrentFrame = 0;
			}

			if (m_CurrentFrame >= m_SpritesList.Count - 1) {
				_isIncreasePingPong = false;
				//m_CurrentFrame = m_SpritesList.Count - 1;
			}
		}






		private void TypeOnce ()
		{
			if (m_IsPaused == true) {
				CancelInvoke ();
			}

			this.ShowFrame (m_CurrentFrame);
			m_CurrentFrame++;
			if (m_CurrentFrame > m_SpritesList.Count - 1) {
				CancelInvoke ();
				m_CurrentFrame = m_SpritesList.Count - 1;
				this.OnOnceFinished ();

			}
		}






		private void OnOnceFinished ()
		{
			if (m_IsNeedPlayOtherAnimationAfter == false) {
				this.Pause ();
				//this.Stop (false);
				return;
			} else {
				//need play next animation

				if (m_PlayedAfterCurrentFinished == null) {
					Debug.LogError ("SimpleSpriteOneAnimation : OnOnceFinished : m_PlayedAfterCurrentFinished == null. Need setup in inspector this value. Animation name = " + transform.name);
				} else {
					if (m_CurrentRootAnimator) {
						m_CurrentRootAnimator.PlayAnimation (m_PlayedAfterCurrentFinished.transform.name);
					} else {
						Debug.LogError ("SimpleSpriteOneAnimation : OnOnceFinished : m_CurrentRootAnimator == null");
					}
				}


			}



		}





		private void ShowFrame (int _frame)
		{
			for (int i = 0; i <= m_SpritesList.Count - 1; i++) {
				if (m_CurrentFrame == i) {
					m_SpritesList [i].SetActive (true); 
				} else {
					m_SpritesList [i].SetActive (false); 
				}
			}
		}



	



		 
		//called from root animator Start() to every one animation. (for run after finished some other animation)
		public void SetCurrentAnimator (SimpleSpriteAnimator _animator)
		{
			m_CurrentRootAnimator = _animator;
		}





		void OnEnable ()
		{
			Debug.Log ("SimpleSpriteOneAnimation : OnEnable");

			ScanChildsRoot ();

//			if (m_isNowPlaying == true) {
//				this.Stop (false);
//				StartCoroutine (StartAll ());
//			}



			//this.StartAll ();
			if (m_isNowPlaying == true) {
				//StopAllCoroutines ();
				//this.StartAll ();
				AnimationLoop ();

			}
		}



		void OnDisable ()
		{
			CancelInvoke ();
		}



		void Awake ()
		{
			m_RootForSprites = this.transform;
			ScanChildsRoot ();

		}



		void Start ()
		{


			//if (m_PlayInStart == true) {
			//	StartCoroutine (StartAll ());
			//}

		}



		void FixedUpdate ()
		{
			CheckForTypeChanged ();
		}


		public enum AnimationType
		{
			ONCE,
			LOOP,
			PING_PONG,
		}
	}
}
