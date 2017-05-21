using UnityEngine.SceneManagement;

namespace script.core.scene
{
    public class SceneStatus
    {
        public static string SceneId
        {
            get { return SceneManager.GetActiveScene().name; }
        }

        public static int Procedure { get; set; }

        public static int EntranceNo { get; set; }

        public static bool IsCompletedQuizA { get; set; }

        public static bool HasQuizA { get; set; }

        public static bool HasCicada { get; set; }

        public static bool HasBroom { get; set; }

    }
}
