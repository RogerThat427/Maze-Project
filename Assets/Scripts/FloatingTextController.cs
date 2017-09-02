using UnityEngine;
using System.Collections;

public class FloatingTextController : MonoBehaviour
{
	private static FloatingText popupText;
	private static GameObject canvas;

	public static void Initialize()
	{
		canvas = GameObject.Find("EnemyDamageOverlay");
        if (!popupText){
            popupText = Resources.Load<FloatingText>("UIResources/PopupTextParent");
        }
	}

	public static void CreateFloatingText(string text, Transform location)
	{
		FloatingText instance = Instantiate(popupText);
        //Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.position.x + Random.Range(-.2f, .2f), location.position.y + Random.Range(-.2f, .2f)));
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(new Vector3(location.position.x, location.position.y + 1f, location.position.z));
		instance.transform.SetParent(canvas.transform, false);
		instance.transform.position = screenPosition;

		instance.SetText(text);
	}
}