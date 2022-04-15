using FootballChairman.Models;

namespace FootballChairman.Messages
{
    public class ViewThisPlayerMessage
    {
        public int PlayerId { get; set; }

        public ViewThisPlayerMessage(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
