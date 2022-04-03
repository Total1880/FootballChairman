using FootballChairman.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Repositories
{
    public class PersonNameRepository : IPersonNameRepository
    {
        public IList<string> GetFirstNames(int countryId)
        {
            var firstNames = new List<string>();

            switch (countryId)
            {
                case 0:
                    firstNames.Add("Arthur");
                    firstNames.Add("Noah");
                    firstNames.Add("Jules");
                    firstNames.Add("Louis");
                    firstNames.Add("Lucas");
                    firstNames.Add("Liam");
                    firstNames.Add("Adam");
                    firstNames.Add("Victor");
                    firstNames.Add("Gabriel");
                    firstNames.Add("Mohamed");
                    break;
                case 1:
                    firstNames.Add("David");
                    firstNames.Add("John");
                    firstNames.Add("Michael");
                    firstNames.Add("Paul");
                    firstNames.Add("Andrew");
                    firstNames.Add("Peter");
                    firstNames.Add("James");
                    firstNames.Add("Robert");
                    firstNames.Add("Mark");
                    firstNames.Add("Richard");
                    break;
                case 3:
                    firstNames.Add("Noah");
                    firstNames.Add("Sem");
                    firstNames.Add("Liam");
                    firstNames.Add("Lucas");
                    firstNames.Add("Daan");
                    firstNames.Add("Finn");
                    firstNames.Add("Levi");
                    firstNames.Add("Luuk");
                    firstNames.Add("Mees");
                    firstNames.Add("James");
                    break;
                case 4:
                    firstNames.Add("Liam");
                    firstNames.Add("Lucas");
                    firstNames.Add("Raphael");
                    firstNames.Add("Leo");
                    firstNames.Add("Noah");
                    firstNames.Add("Ethan");
                    firstNames.Add("Louis");
                    firstNames.Add("Gabriel");
                    firstNames.Add("Jules");
                    firstNames.Add("Nathan");
                    break;
                case 5:
                    firstNames.Add("Jose");
                    firstNames.Add("Juan");
                    firstNames.Add("Francisco");
                    firstNames.Add("Antonio");
                    firstNames.Add("Manuel");
                    firstNames.Add("Miguel");
                    firstNames.Add("David");
                    firstNames.Add("Luis");
                    firstNames.Add("Carlos");
                    firstNames.Add("Jesus");
                    break;
                case 6:
                    firstNames.Add("Thorsten");
                    firstNames.Add("Tore");
                    firstNames.Add("Friedo");
                    firstNames.Add("Kaleo");
                    firstNames.Add("Mauritz");
                    firstNames.Add("Hektor");
                    firstNames.Add("Norman");
                    firstNames.Add("Ruven");
                    firstNames.Add("Elias");
                    firstNames.Add("Matteo");
                    break;
                default:
                    firstNames.Add("John");
                    break;
            }

            return firstNames;
        }

        public IList<string> GetLastNames(int countryId)
        {
            var lastNames = new List<string>();

            switch (countryId)
            {
                case 0:
                    lastNames.Add("Peeters");
                    lastNames.Add("Janssens");
                    lastNames.Add("Maes");
                    lastNames.Add("Jacobs");
                    lastNames.Add("Mertens");
                    lastNames.Add("Willems");
                    lastNames.Add("Claes");
                    lastNames.Add("Goossens");
                    lastNames.Add("Wouters");
                    lastNames.Add("De Smet");
                    break;
                case 1:
                    lastNames.Add("Smith");
                    lastNames.Add("Jones");
                    lastNames.Add("Taylor");
                    lastNames.Add("Brown");
                    lastNames.Add("Williams");
                    lastNames.Add("Wilson");
                    lastNames.Add("Johnson");
                    lastNames.Add("Davies");
                    lastNames.Add("Patel");
                    lastNames.Add("Robinson");
                    break;
                case 3:
                    lastNames.Add("de Jong");
                    lastNames.Add("Jansen");
                    lastNames.Add("de Vries");
                    lastNames.Add("van den Berg");
                    lastNames.Add("Van Dijk");
                    lastNames.Add("Bakker");
                    lastNames.Add("Janssen");
                    lastNames.Add("Visser");;
                    lastNames.Add("Smit");
                    lastNames.Add("Meijer");
                    break;
                case 4:
                    lastNames.Add("Martin");
                    lastNames.Add("Bernard");
                    lastNames.Add("Robert");
                    lastNames.Add("Richard");
                    lastNames.Add("Durand");
                    lastNames.Add("Dubois");
                    lastNames.Add("Moreau");
                    lastNames.Add("Simon");
                    lastNames.Add("Laurent");
                    lastNames.Add("Michel");
                    break;
                case 5:
                    lastNames.Add("Garcia");
                    lastNames.Add("Rodriguez");
                    lastNames.Add("Gonzalez");
                    lastNames.Add("Fernandez");
                    lastNames.Add("Lopez");
                    lastNames.Add("Martinez");
                    lastNames.Add("Sanchez");
                    lastNames.Add("Perez");
                    lastNames.Add("Gomez");
                    lastNames.Add("Martin");
                    break;
                case 6:
                    lastNames.Add("Muller");
                    lastNames.Add("Schmidt");
                    lastNames.Add("Schneider");
                    lastNames.Add("Fischer");
                    lastNames.Add("Weber");
                    lastNames.Add("Meyer");
                    lastNames.Add("Wagner");
                    lastNames.Add("Becker");
                    lastNames.Add("Schulz");
                    lastNames.Add("Hoffmann");
                    break;
                default:
                    lastNames.Add("Doe");
                    break;
            }

            return lastNames;
        }
    }
}
