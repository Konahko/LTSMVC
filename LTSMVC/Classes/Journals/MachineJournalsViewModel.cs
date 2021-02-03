using LTSMVC.Models;
using System.Collections.Generic;

namespace LTSMVC.Classes.Journals
{
    public class MachineJournalsViewModel
    {
        public List<JournalMachine> journalMachines;
        public JournalMachine Object = new JournalMachine();
        public int Count;
        public int Page;
        public string Search;
        public string SortButton;
    }
}
