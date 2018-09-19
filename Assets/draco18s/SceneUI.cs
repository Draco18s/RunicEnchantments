using Assets.draco18s.runic;
using Assets.draco18s.runic.init;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneUI : MonoBehaviour {
	Parser parser;// = new Parser();
	Coroutine execution;
	public GameObject source;

	void Start () {
		parser = new Parser(source);
		RuneRegistry.Initialize();
		transform.Find("Button").GetComponent<Button>().onClick.AddListener(delegate {
			execution = StartCoroutine(Execute(transform.Find("InputField").GetComponent<InputField>().text));
		});
	}

	private IEnumerator Execute(string code) {
		yield return null;
		ParseError err = parser.Parse(code);
		if(err.type != ParseErrorType.NONE) {
			ShowError(err);
			yield break;
		}
		parser.SpawnPointer();
		bool continueExecuting = false;
		int counter = 0;
		do {
			counter++;
			continueExecuting = parser.Tick();
			yield return null;
		} while(continueExecuting && counter < 100);
	}

	private void ShowError(ParseError err) {
		Debug.Log(err.type + ": '" + err.character + "' @ " + err.pos);
	}
}
