using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Assets.script.core.scene
{
    public class SceneStatus
    {
        public static string SceneId
        {
            get { return SceneManager.GetActiveScene().name; }
        }

        private static readonly Dictionary<string, int> procedure = new Dictionary<string, int>();

        public static int Procedure
        {
            get {
                if (procedure.ContainsKey(SceneId))
                {
                    return procedure[SceneId];
                }
                procedure[SceneId] = 1;
                return 1;
            }
            set { procedure[SceneId] = value; }
        }

        public static int EntranceNo { get; set; }

        public static bool IsCompletedQuizA { get; set; }

        public static bool HasQuizA { get; set; }

        public static bool HasCicada { get; set; }

        public static bool HasBroom { get; set; }

    }
}
