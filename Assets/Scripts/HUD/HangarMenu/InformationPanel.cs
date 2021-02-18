using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UserInterface
{
    public class InformationPanel : MonoBehaviour
    {
        [Header("Content Attributes")]
        public TextMeshProUGUI titleLabel;
        public TextMeshProUGUI description;
        public Image image;

        public void SetPanelInfo(string name, string description, Sprite sprite)
        {
            this.titleLabel.text = name;
            this.description.text = description;
            this.image.sprite = sprite;
        }

        public void ClosePanel()
        {
            gameObject.SetActive(false);
        }
    }

}