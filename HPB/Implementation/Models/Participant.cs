namespace Implementation.Models
{
    public class Participant : ModelWithIdentity
    {
        public Participant(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}