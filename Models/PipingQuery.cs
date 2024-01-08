namespace mvc_query_hub.Models
{
    public class PipingQuery
    {

        public int Id { get; set; }
        public string Title { get; set; }

        public string From { get; set; }
        public string To { get; set; }

        public string Tags { get; set; }

        public string Image { get; set; }


        public DateTime PostDate { get; set; }

        public DateTime EditDate { get; set; }


    }
}
