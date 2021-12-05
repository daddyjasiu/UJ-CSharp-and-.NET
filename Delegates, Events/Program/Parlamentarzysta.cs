namespace Program
{
    public delegate bool GiveVote();
    public class Parlamentarzysta
    {
        public event GiveVote OnGiveVote;
        
        public bool Vote(){
            return OnGiveVote.Invoke();
        }
    }

}