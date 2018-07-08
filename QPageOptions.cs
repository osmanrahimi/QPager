namespace QPager
{
    public  class QPageOptions
    {
        public string NextPageTitle { get; set; }
        public string PrevPageTitle { get; set; }

        
        public QPageOptions()
        {
            NextPageTitle = "Next";
            PrevPageTitle = "Prev";
        }
    }
}
