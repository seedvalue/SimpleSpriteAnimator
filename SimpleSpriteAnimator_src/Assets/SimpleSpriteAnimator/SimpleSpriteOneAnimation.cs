using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SeedValue
{

	public class SimpleSpriteOneAnimation : MonoBehaviour
	{
	

		Transform m_RootForSprites;

		public bool m_PlayInStart = true;


		[Header ("When loop settings:")]
		// in first run from 0 to last frame, but when loop end, its will start loop from other frame;
		public int m_LoopedFrom = 0;


		public AnimationType m_AnimationType = AnimationType.LOOP;

		public float m_WaitBetweenFramesTime = 0.5F;


		[Header ("Debug: not edit, filled auto")]
		public List<GameObject> m_SpritesList;
		public int m_CurrentFrame = 0;
		public bool m_isNowPlaying = false;







		public void Play ()
		{
			m_CurrentFrame = 0;

			if (m_isNowPlaying == true) {
				//not start Co
			} else {
				StartCoroutine (StartAll ());
			}

			m_isNowPlaying = true;
		}



		public void Stop ()
		{
			StopAllCoroutines ();
			m_CurrentFrame = 0;
			_isIncreasePingPong = true;

			ShowFrame (m_CurrentFrame);
			m_isNowPlaying = false;
			transform.gameObject.SetActive (false);
		}





		private IEnumerator StartAll ()
		{
			//	yield return StartCoroutine (ScanChildsRootCo ());
			yield return StartCoroutine (AnimationLoop ());

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





		//for handle change type on fly when tests in inspector, or may be will changed with script
		private AnimationType m_AnimationTypeTMP;


		private IEnumerator AnimationLoop ()
		{
			m_AnimationTypeTMP = m_AnimationType;




			while (true) {

				if (m_AnimationTypeTMP != m_AnimationType) {
					// have changed, we reset to zero
					m_CurrentFrame = 0;
					_isIncreasePingPong = true;
				}


				switch (m_AnimationType) {

				case AnimationType.LOOP:

					TypeLoop ();
					yield return  new WaitForSeconds (m_WaitBetweenFramesTime);
					break;
				


				case AnimationType.PING_PONG:

					TypePingPong ();
					yield return  new WaitForSeconds (m_WaitBetweenFramesTime);
					break;
				}


			}
		}






		private void TypeLoop ()
		{
			this.ShowFrame (m_CurrentFrame);
			m_CurrentFrame++;
			if (m_CurrentFrame > m_SpritesList.Count - 1) {
				m_CurrentFrame = m_LoopedFrom;
			}
		}




		private bool _isIncreasePingPong = true;

		private void TypePingPong ()
		{
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





		//		private void HideAllFrames ()
		//		{
		//			for (int i = 0; i < m_SpritesList.Count - 1; i++) {
		//				m_SpritesList [i].SetActive (false);
		//			}
		//		}


		void OnEnable ()
		{
			ScanChildsRoot ();
		}



		void Awake ()
		{
			m_RootForSprites = this.transform;
			ScanChildsRoot ();

		}



		void Start ()
		{


			if (m_PlayInStart == true) {
				StartCoroutine (StartAll ());
			}

		}




		public enum AnimationType
		{
			ONCE,
			LOOP,
			PING_PONG,
		}
	}
}
