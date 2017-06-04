using System.Collections.Generic;

namespace Assets.script.core.@event
{
    public class EventHolder {
        public Dictionary<string, int> ObjectMappingDic { get; set; }
        public Dictionary<int, EventTask> EventDic { get; set; }
    }
}
