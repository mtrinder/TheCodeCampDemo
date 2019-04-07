namespace TheCodeCamp.Data
{
    public class TalkSpeakers
    {
        public int TalkId { get; set; }
        public Talk Talk { get; set; }

        public int SpeakerId { get; set; }
        public Speaker Speaker { get; set; }
    }
}