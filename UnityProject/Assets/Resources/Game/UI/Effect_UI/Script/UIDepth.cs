using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class UIDepth : MonoBehaviour {
	[SerializeField]
	public int _order;
	[SerializeField]
	public bool _isUI = true;
	[SerializeField]
	public bool _isSort = false;

	[SerializeField]
	public bool _isAddRayCast = false;
	void Start () 
	{
		if(_isUI){
			Canvas canvas = GetComponent<Canvas>();
			if( canvas == null){
				canvas = gameObject.AddComponent<Canvas>();
			}
			canvas.overrideSorting = true;
			canvas.sortingOrder = _order;

			if(_isAddRayCast)
			{
				GraphicRaycaster raycast = GetComponent<GraphicRaycaster>();
				if(raycast == null)
					raycast = gameObject.AddComponent<GraphicRaycaster>();
			}
		}
		else
		{
			Renderer []renders  =  GetComponentsInChildren<Renderer>();
 
			foreach(Renderer render in renders){
				if(_isSort)
				render.sortingOrder += _order;
				else
					render.sortingOrder = _order;
			}
		}
	}
}