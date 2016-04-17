using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class PaintIcon : MonoBehaviour {
	public PaintColor paintColor;
	public int count;
	public Text textCount;
	public MixerManager mixer;

	public GameObject paintPrefab;
	private GameObject temp;

	public void Awake() {
		textCount = GetComponentInChildren<Text>();
		textCount.text = count.ToString();
	}

	public void OnDrag(UnityEngine.EventSystems.BaseEventData eventData) {
		if (temp == null) {
			if (count > 0) {
				count--;
				if (count <= 1) {
					textCount.text = "";
				} else {
					textCount.text = count.ToString ();
				}

				temp = Instantiate (paintPrefab);

				temp.GetComponent<DraggingPaint> ().setColor (this);
				temp.GetComponent<DraggingPaint> ().icon = this;
				temp.GetComponent<DraggingPaint> ().icon.mixer = mixer;

				temp.transform.SetParent(transform.parent.parent.parent);
				temp.transform.localScale = Vector3.one;
			}
		}

		temp.transform.position = Input.mousePosition;
	}

	public void SetCount(int count) {
		this.count = count;
		if (this.count <= 1) {
			textCount.text = "";
		} else {
			textCount.text = count.ToString ();
		}
	}
}
