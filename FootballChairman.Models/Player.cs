namespace FootballChairman.Models
{
    public class Player : Person
    {
        private int _goalkeeping;
        private int _defense;
        private int _midfield;
        private int _attack;
        public int Attack
        {
            get => _attack;
            set
            {
                if (value > 99)
                {
                    _attack = 99;
                }
                else
                {
                    _attack = value;
                }
            }
        }
        public int Defense
        {
            get => _defense;
            set
            {
                if (value > 99)
                {
                    _defense = 99;
                }
                else
                {
                    _defense = value;
                }
            }
        }
        public int Midfield
        {
            get => _midfield;
            set
            {
                if (value > 99)
                {
                    _midfield = 99;
                }
                else
                {
                    _midfield = value;
                }
            }
        }
        public int Goalkeeping
        {
            get => _goalkeeping;
            set
            {
                if (value > 99)
                {
                    _goalkeeping = 99;
                }
                else
                {
                    _goalkeeping = value;
                }
            }
        }

        public int Potential { get; set; }
        public float TransferValue
        {
            get
            {
                return Wage * ContractYears;
            }
        }
    }
}
