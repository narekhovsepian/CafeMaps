namespace CafeMaps
{
    class WorkingDaysAndTimes
    {
        public string Day { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        public WorkingDaysAndTimes() { }

        public WorkingDaysAndTimes(string day, string from, string to)
        {
            Day = day;
            From = from;
            To = to;
        }

        public override string ToString()
        {
            return "    " + Day + " From: " + From + " - To: " + To;
        }
    }
}
