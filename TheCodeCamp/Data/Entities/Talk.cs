using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheCodeCamp.Data
{
    public class Talk
    {
        public int TalkId { get; set; }

        public string Title { get; set; }
        public string Abstract { get; set; }
        public int Level { get; set; }

        public ICollection<TalkSpeakers> TalkSpeakers { get; set; }

        public virtual Camp Camp { get; set; }
    }
}