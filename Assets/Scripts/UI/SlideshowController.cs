using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Image))]
public class SlideshowController : MonoBehaviour {
	public Sprite[] slides;
	int activeSlide = 0;

	public GameObject forward;
	public GameObject backward;

	Image target;

	// Use this for initialization
	void Start () {
		target = GetComponent<Image> ();
		updateState ();
	}

	private void updateState(){
		if (slides.Length > 0) {
			target.sprite = slides [activeSlide];
		}

		forward.SetActive (activeSlide + 1 < slides.Length);
		backward.SetActive (0 <= activeSlide - 1);
	}

	public void Prev(){
		if (0 <= activeSlide - 1) {
			activeSlide--;
			updateState ();
		}
	}

	public void Next(){
		if (activeSlide + 1 < slides.Length) {
			activeSlide++;
			updateState ();
		}
	}
}
