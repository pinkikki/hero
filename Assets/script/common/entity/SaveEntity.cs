using script.core.scene;

namespace script.common.entity
{
    public class SaveEntity
    {
        public int SaveId { get; set; }
        public string SceneId { get; set; }
        public int ClassroomProcedure { get; set; }
        public int CorridorProcedure { get; set; }
        public int ArtroomProcedure { get; set; }
        public int SchoolyardProcedure { get; set; }
        public int Starting { get; set; }
        public int Cancomeinclassroom { get; set; }
        public int Hasquiza { get; set; }
        public int Hascicada { get; set; }
        public int Hasbroom { get; set; }
        public int Iscompletedquiza { get; set; }
        public int Hasquizb { get; set; }
        public int Cansearchmarble { get; set; }
        public int Cansearchmatomari { get; set; }
        public int Hasgraveroada { get; set; }
        public int Cangetgraveroadb { get; set; }
        public int Hasgraveroadb { get; set; }
        public int Hasmatomari { get; set; }
        public int Cancreatenerikeshi { get; set; }
        public int Hasglue { get; set; }
        public int Isfinishedwashinghands { get; set; }
        public int Hasduster { get; set; }
        public int Hasnerikeshi { get; set; }
        public int Cangetmuddumplings { get; set; }
        public int Hasmuddumplings { get; set; }
        public int Hasmarble { get; set; }
        public int Hasquizc { get; set; }
        public int Hasquizd { get; set; }
        public int Isfinishedfirstunlocking { get; set; }
        public int Isfinishedsecondunlocking { get; set; }
        public int Hasquize { get; set; }
        public int Canflowendroll { get; set; }
        public int Iscompletedshinoburooma { get; set; }
        
        public void reflect()
        {
            SceneStatus.ProcedureWithSceneId("classroom", ClassroomProcedure);
            SceneStatus.ProcedureWithSceneId("corridor", CorridorProcedure);
            SceneStatus.ProcedureWithSceneId("artroom", ArtroomProcedure);
            SceneStatus.ProcedureWithSceneId("schoolyard", SchoolyardProcedure);
            SceneStatus.Starting = Starting == 1;
            SceneStatus.CanComeInClassroom = Cancomeinclassroom == 1;
            SceneStatus.HasQuizA = Hasquiza == 1;
            SceneStatus.HasCicada = Hascicada == 1;
            SceneStatus.HasBroom = Hasbroom == 1;
            SceneStatus.IsCompletedQuizA = Iscompletedquiza == 1;
            SceneStatus.HasQuizB = Hasquizb == 1;
            SceneStatus.CanSearchMarble = Cansearchmarble == 1;
            SceneStatus.CanSearchMatomari = Cansearchmatomari == 1;
            SceneStatus.HasGraveRoadA = Hasgraveroada == 1;
            SceneStatus.CanGetGraveRoadB = Cangetgraveroadb == 1;
            SceneStatus.HasGraveRoadB = Hasgraveroadb == 1;
            SceneStatus.HasMatomari = Hasmatomari == 1;
            SceneStatus.CanCreateNerikeshi = Cancreatenerikeshi == 1;
            SceneStatus.HasGlue = Hasglue == 1;
            SceneStatus.IsFinishedWashingHands = Isfinishedwashinghands == 1;
            SceneStatus.HasDuster = Hasduster == 1;
            SceneStatus.HasNerikeshi = Hasnerikeshi == 1;
            SceneStatus.CanGetMudDumplings = Cangetmuddumplings == 1;
            SceneStatus.HasMudDumplings = Hasmuddumplings == 1;
            SceneStatus.HasMarble = Hasmarble == 1;
            SceneStatus.HasQuizC = Hasquizc == 1;
            SceneStatus.HasQuizD = Hasquizd == 1;
            SceneStatus.IsFinishedFirstUnLocking = Isfinishedfirstunlocking == 1;
            SceneStatus.IsFinishedSecondUnLocking = Isfinishedsecondunlocking == 1;
            SceneStatus.HasQuizE = Hasquize == 1;
            SceneStatus.CanFlowEndRoll = Canflowendroll == 1;
            SceneStatus.IsCompletedShinobuRoomA = Iscompletedshinoburooma == 1;
        }
    }
}