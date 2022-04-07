namespace FootballChairman.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public int CountryId { get; set; }
        public string LastNameFirstLetterFirstName { get => LastName + " " + FirstName[0] + "."; }
    }
}
