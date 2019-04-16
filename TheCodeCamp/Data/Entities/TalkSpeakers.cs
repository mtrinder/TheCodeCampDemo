namespace TheCodeCamp.Data
{
    public class TalkSpeakers
    {
        public int TalkId { get; set; }
        public virtual Talk Talk { get; set; }

        public int SpeakerId { get; set; }
        public virtual Speaker Speaker { get; set; }
    }
}