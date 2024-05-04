using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public GameObject uiElementPrefab; // 表示させるUI要素のPrefab
        private Canvas canvas; // UIを配置するCanvas

        void Start()
        {
            // Canvasを検索して取得する
            canvas = FindObjectOfType<Canvas>();
        }

        void Update()
        {
            // マウスがクリックされたかを検出
            if (Input.GetMouseButtonDown(0))
            {
                // クリックされたスクリーン上の座標を取得
                Vector3 clickPosition = Input.mousePosition;

                // スクリーン座標からワールド座標に変換
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(clickPosition);
                worldPosition.z = 0f; // 2Dの場合、z座標は通常0に設定する

                // UI要素をインスタンス化してCanvasの子要素として配置
                GameObject uiElement = Instantiate(uiElementPrefab, worldPosition, Quaternion.identity, canvas.transform);
            }
        }
    }
}