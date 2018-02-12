using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour {

    public Vector2 playerPosition;
    private GameObject playerIcon;
    private GameObject map;
    private Vector2 topRightMap;
    private Vector2 playerMapPosition;

	// Use this for initialization
	void Start () {
        map = GameObject.Find("Content");

        RectTransform rt = map.GetComponent<RectTransform>();
        
        topRightMap = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y);

        float x = topRightMap.x * playerPosition.x;
        float y = topRightMap.y * playerPosition.y;
        playerMapPosition = new Vector2(x, y);

        playerIcon = GameObject.Find("PlayerIcon");
        playerIcon.transform.localPosition = playerMapPosition;
    }

    private void Update()
    {
        float x = topRightMap.x * playerPosition.x;
        float y = topRightMap.y * playerPosition.y;
        playerMapPosition = new Vector2(x, y);
        playerIcon.transform.localPosition = playerMapPosition;
    }

}
