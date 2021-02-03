using System;

namespace LTSMVC.Classes.Journals
{
    public class MachineJournalsDetailsViewModel
    {
        public int Id;
        public int? TriggerUser;
        public Models.Machine Machine;
        public int MachinesId;
        public string[] State;
        public DateTime? Time;
    }
}
