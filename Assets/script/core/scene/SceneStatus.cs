﻿using System.Collections.Generic;
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

        public static bool Starting { get; set; }
        
        public static bool IsCompletedQuizA { get; set; }

        public static bool HasQuizA { get; set; }

        public static bool HasCicada { get; set; }

        public static bool HasBroom { get; set; }
        
        public static bool HasQuizB { get; set; }
            
        public static bool IsCompletedGhostStories1 { get; set; }
        
        public static bool IsCompletedGhostStories2 { get; set; }
        
        public static bool IsCompletedGhostStories3 { get; set; }
        
        public static bool IsCompletedGhostStories4 { get; set; }
        
        public static bool IsCompletedGhostStories5 { get; set; }
        
        public static bool IsCompletedGhostStories6 { get; set; }
        
        public static bool CanSearchMatomari { get; set; }
        
        public static bool HasGraveRoadA { get; set; }
        
        public static bool CanGetGraveRoadB { get; set; }
        
        public static bool HasGraveRoadB { get; set; }
        
        public static bool HasMatomari { get; set; }
        
        public static bool CanCreateNerikeshi { get; set; }
        
        public static bool HasGlue { get; set; }
        
        public static bool IsFinishedWashingHands { get; set; }
        
        public static bool HasDuster { get; set; }
        
        public static bool HasNerikeshi { get; set; }
        
        public static bool CanGetMudDumplings { get; set; }
        
        public static bool HasMudDumplings { get; set; }
        
        public static bool HasMarble { get; set; }
        
        public static bool HasQuizC { get; set; }
        
        public static bool HasQuizD { get; set; }
        
        public static bool IsFinishedFirstUnLocking { get; set; }
        
        public static bool IsFinishedSecondUnLocking { get; set; }
        
        public static bool HasQuizE { get; set; }
        
        public static bool CanFlowEndRoll { get; set; }
        
        public static bool IsCompletedShinobuRoomA { get; set; }

        private static ArtObject lastSearchedArtObject = ArtObject.None;

        public static ArtObject LastSearchedArtObject
        {
            get { return lastSearchedArtObject; }
            set { lastSearchedArtObject = value; }
        }

        public enum ArtObject
        {
            Smartball,
            ArtworkA,
            ArtworkB,
            ArtworkC,
            ArtworkD,
            ArtworkE,
            ArtworkF,
            ArtworkG,
            None
        }
        
        public static void ProcedureWithSceneId(string sceneId, int pro)
        {
            procedure[sceneId] = pro;
        }
    }
}
