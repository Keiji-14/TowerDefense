using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public GameObject uiElementPrefab; // �\��������UI�v�f��Prefab
        private Canvas canvas; // UI��z�u����Canvas

        void Start()
        {
            // Canvas���������Ď擾����
            canvas = FindObjectOfType<Canvas>();
        }

        void Update()
        {
            // �}�E�X���N���b�N���ꂽ�������o
            if (Input.GetMouseButtonDown(0))
            {
                // �N���b�N���ꂽ�X�N���[����̍��W���擾
                Vector3 clickPosition = Input.mousePosition;

                // �X�N���[�����W���烏�[���h���W�ɕϊ�
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(clickPosition);
                worldPosition.z = 0f; // 2D�̏ꍇ�Az���W�͒ʏ�0�ɐݒ肷��

                // UI�v�f���C���X�^���X������Canvas�̎q�v�f�Ƃ��Ĕz�u
                GameObject uiElement = Instantiate(uiElementPrefab, worldPosition, Quaternion.identity, canvas.transform);
            }
        }
    }
}