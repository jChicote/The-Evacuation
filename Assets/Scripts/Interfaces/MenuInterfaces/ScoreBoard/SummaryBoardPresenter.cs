using System;
using System.Collections;
using TheEvacuation.Interfaces.GameInterfaces.Text;
using TMPro;
using UnityEngine;

namespace TheEvacuation.Interfaces.MenuInterfaces.ScoreBoard
{

    public class SummaryBoardPresenter : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [Space]
        [SerializeField]
        protected SummaryBoardRowItem[] summaryBoardRowItem;

        [Space]
        [Range(0f, 1f)]
        public float displayIntervalTime = 0.05f;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void PresentSummaryBoard()
        {
            StartCoroutine(DisplayEachSummaryRowItem(displayIntervalTime));
        }

        public void HideSummaryDetails()
        {
            foreach (SummaryBoardRowItem rowItem in summaryBoardRowItem)
                rowItem.parentObject.SetActive(false);
        }

        private IEnumerator DisplayEachSummaryRowItem(float displayIteration)
        {
            foreach (SummaryBoardRowItem rowItem in summaryBoardRowItem)
            {
                rowItem.parentObject.SetActive(true);
                yield return new WaitForSeconds(displayIteration);
            }
        }

        #endregion Methods

    }

    [Serializable]
    public class SummaryBoardRowItem
    {

        #region - - - - - - Fields - - - - - -

        public SummaryBoardRowItemType type;
        public GameObject parentObject;
        public TMP_Text summaryLabel;
        public DynamicText dynamicText;

        #endregion Fields

    }

    public enum SummaryBoardRowItemType
    {
        KillCount,
        Score,
        SkillPoints,
        TotalDeaths
    }

}
