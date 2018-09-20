using Assets.draco18s.generpg.init;
using Assets.draco18s.runic;
using Assets.draco18s.runic.init;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneUI : MonoBehaviour {
	Coroutine execution;
	public GameObject source;

	void Start () {
		RuneRegistry.Initialize();
		ObjectRegistry.Initialize();
		transform.Find("Button").GetComponent<Button>().onClick.AddListener(delegate {
			execution = StartCoroutine(Execute(transform.Find("InputField").GetComponent<InputField>().text));
		});
	}

	private IEnumerator Execute(string code) {
		yield return null;
		ExecutionContext context;
		ParseError err = Parser.Parse(code, source, out context);
		if(err.type != ParseErrorType.NONE || context == null) {
			ShowError(err);
			yield break;
		}
		context.Initialize();
		bool continueExecuting = false;
		int counter = 0;
		do {
			counter++;
			continueExecuting = context.Tick();
			yield return null;
		} while(continueExecuting && counter < 100);
	}

	private void ShowError(ParseError err) {
		Debug.Log(err.type + ": '" + err.character + "' @ " + err.pos);
	}
}
