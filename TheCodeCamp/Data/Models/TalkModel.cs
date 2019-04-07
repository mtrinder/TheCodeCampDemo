using System;
using System.Collections.Generic;

namespace TheCodeCamp.Data.Models
{
    public class TalkModel
    {
        public string Title { get; set; }
        public string Abstract { get; set; }
        public int Level { get; set; }

        public ICollection<TalkSpeakerModel> TalkSpeakers { get; set; }
    }
}
