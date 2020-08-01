using System.Collections.Generic;

namespace ExploringMars.Application.Dtos
{
    public class ProbesStartingSetupDto
    {
        public List<int> Position { get; set; }
        
        public string Direction { get; set; }
    }
}