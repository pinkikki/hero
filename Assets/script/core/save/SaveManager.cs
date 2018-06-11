using script.common.dao;
using script.core.audio;
using script.core.character;
using script.core.hint;
using script.core.operation;
using script.core.quiz;
using script.core.scene;
using script.logic.school;
using UnityEngine;

namespace script.core.save
{
    public class SaveManager : MonoBehaviour
    {
        
        [SerializeField] GameObject SaveSelect;
        [SerializeField] GameObject SaveCompletion;
        [SerializeField] GameObject NotSave;
        
        void Start ()
        {
            if (SaveSelect == null)
            {
                SaveSelect = GameObject.Find("Savepoint/SaveSelect");
            }

            HideSaveSelect();
            
            if (SaveCompletion == null)
            {
                SaveCompletion = GameObject.Find("Savepoint/SaveCompletion");
            }

            HideSaveCompletion();

            if (NotSave == null)
            {
                NotSave = GameObject.Find("Savepoint/NotSave");
            }

            HideNotSave();
        }

        private bool saving;
        public void Save()
        {
            if (!saving)
            {
                saving = true;
                AudioManager.Instance.PlaySe(MusicDao.SelectByPrimaryKey(7).MusicName);
                SaveDao.Update(
                    SceneStatus.SceneId,
                    SceneStatus.ProcedureBySceneId("classroom"),
                    SceneStatus.ProcedureBySceneId("corridor"),
                    SceneStatus.ProcedureBySceneId("artroom"),
                    SceneStatus.ProcedureBySceneId("schoolyard"),
                    SceneStatus.CompletedList,
                    SchoolYardOrsStatus.ClassmateOName,
                    SchoolYardOrsStatus.ClassmateRName,
                    SchoolYardOrsStatus.ClassmateSName
                );
            }
            HideSaveSelect();
            ShowSaveCompletion();
            saving = false;
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.name == "yusuke")
            {
                if (SceneStatus.SceneId == "classroom" && SceneStatus.Procedure == 1)
                {
                    SearchButton.Instance.OnRegister(ShowNotSave);
                }
                else
                {
                    SearchButton.Instance.OnRegister(ShowSaveSelect);
                }
            }
        }
        
        void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.name == "yusuke") {
                SearchButton.Instance.OnDialog();
            }
        }

        public void ShowSaveSelect()
        {
            AudioManager.Instance.PlaySe(MusicDao.SelectByPrimaryKey(7).MusicName);
            SaveSelect.SetActive(true);
            SearchButton.Instance.Hide();
            QuizManager.Instance.Hide();
            if (HintManager.Exist())
            {
                HintManager.Instance.Hide();
            }
            if (HelpManager.Exist())
            {
                HelpManager.Instance.Hide();
            }

            var yusuke = GameObject.Find("yusuke");
            if (yusuke != null)
            {
                var mainCharacterController = yusuke.GetComponent<MainCharacterController>();
                if (mainCharacterController != null)
                {
                    mainCharacterController.FreezeFlg = true;
                }
            }

        }
        
        public void HideSaveSelect()
        {
            SaveSelect.SetActive(false);
        }
        
        public void ShowSaveCompletion()
        {
            SaveCompletion.SetActive(true);
        }
        
        public void HideSaveCompletion()
        {
            SaveCompletion.SetActive(false);
        }

        public void ShowNotSave()
        {
            NotSave.SetActive(true);
            SearchButton.Instance.Hide();
            QuizManager.Instance.Hide();
            if (HintManager.Exist())
            {
                HintManager.Instance.Hide();
            }
            if (HelpManager.Exist())
            {
                HelpManager.Instance.Hide();
            }

            var yusuke = GameObject.Find("yusuke");
            if (yusuke != null)
            {
                var mainCharacterController = yusuke.GetComponent<MainCharacterController>();
                if (mainCharacterController != null)
                {
                    mainCharacterController.FreezeFlg = true;
                }
            }
        }
        
        public void HideNotSave()
        {
            NotSave.SetActive(false);
        }

        
        public void Hide()
        {
            AudioManager.Instance.PlaySe(MusicDao.SelectByPrimaryKey(7).MusicName);
            HideSaveSelect();
            HideSaveCompletion();
            HideNotSave();
            SearchButton.Instance.Show();
            QuizManager.Instance.Show();
            if (HintManager.Exist())
            {
                HintManager.Instance.Show();
            }
            if (HelpManager.Exist())
            {
                HelpManager.Instance.Show();
            }

            var yusuke = GameObject.Find("yusuke");
            if (yusuke != null)
            {
                var mainCharacterController = yusuke.GetComponent<MainCharacterController>();
                if (mainCharacterController != null)
                {
                    mainCharacterController.FreezeFlg = false;
                }
            }

        }
    }
}