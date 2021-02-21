using LTSMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTSMVC.Classes.Journals
{
    public class ExpendablesItemsJournalsView
    {
        public List<JournalExpendable> journalExpendables;
        public JournalExpendable Object = new JournalExpendable();
        public int Count;
        public int Page;
        public string Search;
        public string SortButton;
    }
}
