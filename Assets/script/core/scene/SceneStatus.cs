using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace script.core.scene
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
            get
            {
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
        
        public static bool CanSearchMatomari { get; set; }
        
        public static bool HasGraveRoadA { get; set; }
        
        public static bool CanGetGraveRoadB { get; set; }
        
        public static bool HasGraveRoadB { get; set; }
        
        public static bool HasMatomari { get; set; }

        private static ArtObject lastSearchedArtObject = ArtObject.None;

        public static ArtObject LastSearchedArtObject
        {
            get { return lastSearchedArtObject; }
            set { lastSearchedArtObject = value; }
        }

        public enum ArtObject
        {
            Smartball,
            // TODO Lionは仮、他にも色々追加していき、triggerもその分追加する
            Lion,
            None
        }
    }
}
