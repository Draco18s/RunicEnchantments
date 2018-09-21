using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.draco18s.generpg {
	public class TempNotification : MonoBehaviour,ITemporary {
		public void DoDestroy(int amt) {
			Destroy(this.gameObject);
		}

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}