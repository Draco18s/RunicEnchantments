using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Assets.draco18s.ui {

	public static class ButtonExtensions {
		public delegate void OnHoverDelegate(Vector3 pos);
		public delegate void OnClickDelegate();
		public static void AddHover(this Button button, OnHoverDelegate callback) {
			AddHover(button, callback, true);
		}
		public static void AddHover(this Button button, OnHoverDelegate callback, bool redrawOnUpdate) {
			TooltipTrigger trig = button.gameObject.GetComponent<TooltipTrigger>();
			if(trig == null)
				trig = button.gameObject.AddComponent<TooltipTrigger>();
			trig.AddHover(callback, redrawOnUpdate);
		}
		public static void AddHover(this Toggle button, OnHoverDelegate callback) {
			AddHover(button, callback, true);
		}
		public static void AddHover(this Toggle button, OnHoverDelegate callback, bool redrawOnUpdate) {
			TooltipTrigger trig = button.gameObject.GetComponent<TooltipTrigger>();
			if(trig == null)
				trig = button.gameObject.AddComponent<TooltipTrigger>();
			trig.AddHover(callback, redrawOnUpdate);
			//EventTrigger trig = button.gameObject.GetComponent<EventTrigger>();
			//trig.OnPointerEnter.
		}
		public static void AddHover(this Image img, OnHoverDelegate callback) {
			AddHover(img, callback, true);
		}
		public static void AddHover(this Image img, OnHoverDelegate callback, bool redrawOnUpdate) {
			Button b = img.GetComponent<Button>();
			if(b == null) {
				b = img.gameObject.AddComponent<Button>();
				ColorBlock cb = b.colors;
				cb.pressedColor = Color.white;
			}
			b.AddHover(callback, redrawOnUpdate);
		}

		public static void RemoveAllEvents(this Button button) {
			TooltipTrigger trig = button.gameObject.GetComponent<TooltipTrigger>();
			if(trig == null) return;
			trig.removeAllEvents();
		}
		public static void OnRightClick(this Button button, OnClickDelegate callback) {
			TooltipTrigger trig = button.gameObject.GetComponent<TooltipTrigger>();
			if(trig == null)
				trig = button.gameObject.AddComponent<TooltipTrigger>();
			trig.AddRightClick(callback);
		}
	}

	public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler, IPointerClickHandler {
		List<ButtonExtensions.OnHoverDelegate> hoverCallbacks = new List<ButtonExtensions.OnHoverDelegate>();
		List<ButtonExtensions.OnHoverDelegate> updateCallbacks = new List<ButtonExtensions.OnHoverDelegate>();
		List<ButtonExtensions.OnClickDelegate> rClickCallbacks = new List<ButtonExtensions.OnClickDelegate>();
		private Vector3 lastPos = Vector3.zero;

		public void Update() {
			if(lastPos != Vector3.zero) {
				foreach(ButtonExtensions.OnHoverDelegate callback in updateCallbacks) {
					callback(lastPos);
				}
			}
		}

		public void AddHover(ButtonExtensions.OnHoverDelegate callback, bool redrawOnUpdate) {
			if(redrawOnUpdate) {
				updateCallbacks.Add(callback);
			}
			else {
				hoverCallbacks.Add(callback);
			}
		}

		public void removeAllEvents() {
			hoverCallbacks.Clear();
			updateCallbacks.Clear();
			StopHover();
		}

		public void OnPointerEnter(PointerEventData eventData) {
			StartHover(new Vector3(eventData.position.x, eventData.position.y - 18f, 0f));
		}
		public void OnSelect(BaseEventData eventData) {
			StartHover(transform.position);
		}
		public void OnPointerExit(PointerEventData eventData) {
			StopHover();
		}
		public void OnDeselect(BaseEventData eventData) {
			StopHover();
		}

		void StartHover(Vector3 position) {
			foreach(ButtonExtensions.OnHoverDelegate callback in hoverCallbacks) {
				callback(position);
			}
			lastPos = position;
			//TooltipView.Instance.ShowTooltip(text, position);
		}
		void StopHover() {
			lastPos = Vector3.zero;
			SceneUI.instance.tooltip.SetActive(false);
			//TooltipView.Instance.HideTooltip();
		}

		public void OnPointerClick(PointerEventData eventData) {
			if(eventData.button == PointerEventData.InputButton.Left) {
				//Debug.Log("Left click");
			}
			else if(eventData.button == PointerEventData.InputButton.Middle) {
				//Debug.Log("Middle click");
			}
			else if(eventData.button == PointerEventData.InputButton.Right) {
				//Debug.Log("Right click");
				foreach(ButtonExtensions.OnClickDelegate callback in rClickCallbacks) {
					callback();
				}
			}
			if(!eventData.pointerPress.activeSelf)
				StopHover();
		}

		internal void AddRightClick(ButtonExtensions.OnClickDelegate callback) {
			rClickCallbacks.Add(callback);
		}
	}
}
