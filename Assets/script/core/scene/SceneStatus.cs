using System.Collections.Generic;
using script.common.dao;
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

        private static bool starting;

        public static bool Starting
        {
            get { return starting; }
            set
            {
                starting = value;
                if (starting)
                {
                    SaveDao.Update("Starting");
                }
            }
        }

        private static bool canComeInClassroom;

        public static bool CanComeInClassroom
        {
            get { return canComeInClassroom; }
            set
            {
                canComeInClassroom = value;
                if (canComeInClassroom)
                {
                    SaveDao.Update("CanComeInClassroom");
                }
            }
        }

        private static bool hasQuizA;

        public static bool HasQuizA
        {
            get { return hasQuizA; }
            set
            {
                hasQuizA = value;
                if (hasQuizA)
                {
                    SaveDao.Update("HasQuizA");
                }
            }
        }

        private static bool hasCicada;

        public static bool HasCicada
        {
            get { return hasCicada; }
            set
            {
                hasCicada = value;
                if (hasCicada)
                {
                    SaveDao.Update("HasCicada");
                }
            }
        }

        private static bool hasBroom;

        public static bool HasBroom
        {
            get { return hasBroom; }
            set
            {
                hasBroom = value;
                if (hasBroom)
                {
                    SaveDao.Update("HasBroom");
                }
            }
        }

        private static bool isCompletedQuizA;

        public static bool IsCompletedQuizA
        {
            get { return isCompletedQuizA; }
            set
            {
                isCompletedQuizA = value;
                if (isCompletedQuizA)
                {
                    SaveDao.Update("IsCompletedQuizA");
                }
            }
        }

        private static bool hasQuizB;

        public static bool HasQuizB
        {
            get { return hasQuizB; }
            set
            {
                hasQuizB = value;
                if (hasQuizB)
                {
                    SaveDao.Update("HasQuizB");
                }
            }
        }

        private static bool canSearchMarble;

        public static bool CanSearchMarble
        {
            get { return canSearchMarble; }
            set
            {
                canSearchMarble = value;
                if (canSearchMarble)
                {
                    SaveDao.Update("CanSearchMarble");
                }
            }
        }

        public static bool IsCompletedGhostStories1 { get; set; }

        public static bool IsCompletedGhostStories2 { get; set; }

        public static bool IsCompletedGhostStories3 { get; set; }

        public static bool IsCompletedGhostStories4 { get; set; }

        public static bool IsCompletedGhostStories5 { get; set; }

        public static bool IsCompletedGhostStories6 { get; set; }

        private static bool canSearchMatomari;

        public static bool CanSearchMatomari
        {
            get { return canSearchMatomari; }
            set
            {
                canSearchMatomari = value;
                if (canSearchMatomari)
                {
                    SaveDao.Update("CanSearchMatomari");
                }
            }
        }

        private static bool hasGraveRoadA;

        public static bool HasGraveRoadA
        {
            get { return hasGraveRoadA; }
            set
            {
                hasGraveRoadA = value;
                if (hasGraveRoadA)
                {
                    SaveDao.Update("HasGraveRoadA");
                }
            }
        }

        private static bool canGetGraveRoadB;

        public static bool CanGetGraveRoadB
        {
            get { return canGetGraveRoadB; }
            set
            {
                canGetGraveRoadB = value;
                if (canGetGraveRoadB)
                {
                    SaveDao.Update("CanGetGraveRoadB");
                }
            }
        }

        private static bool hasGraveRoadB;

        public static bool HasGraveRoadB
        {
            get { return hasGraveRoadB; }
            set
            {
                hasGraveRoadB = value;
                if (hasGraveRoadB)
                {
                    SaveDao.Update("HasGraveRoadB");
                }
            }
        }

        private static bool hasMatomari;

        public static bool HasMatomari
        {
            get { return hasMatomari; }
            set
            {
                hasMatomari = value;
                if (hasMatomari)
                {
                    SaveDao.Update("HasMatomari");
                }
            }
        }

        private static bool canCreateNerikeshi;

        public static bool CanCreateNerikeshi
        {
            get { return canCreateNerikeshi; }
            set
            {
                canCreateNerikeshi = value;
                if (canCreateNerikeshi)
                {
                    SaveDao.Update("CanCreateNerikeshi");
                }
            }
        }

        private static bool hasGlue;

        public static bool HasGlue
        {
            get { return hasGlue; }
            set
            {
                hasGlue = value;
                if (hasGlue)
                {
                    SaveDao.Update("HasGlue");
                }
            }
        }

        private static bool isFinishedWashingHands;

        public static bool IsFinishedWashingHands
        {
            get { return isFinishedWashingHands; }
            set
            {
                isFinishedWashingHands = value;
                if (isFinishedWashingHands)
                {
                    SaveDao.Update("IsFinishedWashingHands");
                }
            }
        }

        private static bool hasDuster;

        public static bool HasDuster
        {
            get { return hasDuster; }
            set
            {
                hasDuster = value;
                if (hasDuster)
                {
                    SaveDao.Update("HasDuster");
                }
            }
        }

        private static bool hasNerikeshi;

        public static bool HasNerikeshi
        {
            get { return hasNerikeshi; }
            set
            {
                hasNerikeshi = value;
                if (hasNerikeshi)
                {
                    SaveDao.Update("HasNerikeshi");
                }
            }
        }

        private static bool canGetMudDumplings;

        public static bool CanGetMudDumplings
        {
            get { return canGetMudDumplings; }
            set
            {
                canGetMudDumplings = value;
                if (canGetMudDumplings)
                {
                    SaveDao.Update("CanGetMudDumplings");
                }
            }
        }

        private static bool hasMudDumplings;

        public static bool HasMudDumplings
        {
            get { return hasMudDumplings; }
            set
            {
                hasMudDumplings = value;
                if (hasMudDumplings)
                {
                    SaveDao.Update("HasMudDumplings");
                }
            }
        }

        private static bool hasMarble;

        public static bool HasMarble
        {
            get { return hasMarble; }
            set
            {
                hasMarble = value;
                if (hasMarble)
                {
                    SaveDao.Update("HasMarble");
                }
            }
        }

        private static bool hasQuizC;

        public static bool HasQuizC
        {
            get { return hasQuizC; }
            set
            {
                hasQuizC = value;
                if (hasQuizC)
                {
                    SaveDao.Update("HasQuizC");
                }
            }
        }

        private static bool hasQuizD;

        public static bool HasQuizD
        {
            get { return hasQuizD; }
            set
            {
                hasQuizD = value;
                if (hasQuizD)
                {
                    SaveDao.Update("HasQuizD");
                }
            }
        }

        private static bool isFinishedFirstUnLocking;

        public static bool IsFinishedFirstUnLocking
        {
            get { return isFinishedFirstUnLocking; }
            set
            {
                isFinishedFirstUnLocking = value;
                if (isFinishedFirstUnLocking)
                {
                    SaveDao.Update("IsFinishedFirstUnLocking");
                }
            }
        }

        private static bool isFinishedSecondUnLocking;

        public static bool IsFinishedSecondUnLocking
        {
            get { return isFinishedSecondUnLocking; }
            set
            {
                isFinishedSecondUnLocking = value;
                if (isFinishedSecondUnLocking)
                {
                    SaveDao.Update("IsFinishedSecondUnLocking");
                }
            }
        }

        private static bool hasQuizE;

        public static bool HasQuizE
        {
            get { return hasQuizE; }
            set
            {
                hasQuizE = value;
                if (hasQuizE)
                {
                    SaveDao.Update("HasQuizE");
                }
            }
        }

        private static bool canFlowEndRoll;

        public static bool CanFlowEndRoll
        {
            get { return canFlowEndRoll; }
            set
            {
                canFlowEndRoll = value;
                if (canFlowEndRoll)
                {
                    SaveDao.Update("CanFlowEndRoll");
                }
            }
        }

        private static bool isCompletedShinobuRoomA;

        public static bool IsCompletedShinobuRoomA
        {
            get { return isCompletedShinobuRoomA; }
            set
            {
                isCompletedShinobuRoomA = value;
                if (isCompletedShinobuRoomA)
                {
                    SaveDao.Update("IsCompletedShinobuRoomA");
                }
            }
        }

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

        public static int ProcedureBySceneId(string sceneId)
        {
            if (procedure.ContainsKey(sceneId))
            {
                return procedure[sceneId];
            }

            return 1;
        }
    }
}