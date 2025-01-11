namespace DTO.Common
{
    public class DbProcess
    {
        public DbProcess()
        {
            Page = 1;
            RowPerPage = 10;
            SortCol = "1";
            SortType = "desc";
        }
        public int cbColDT { get; set; }
        public int Page { get; set; }
        public int RowPerPage { get; set; }
        public string SortCol { get; set; }
        public string SortType { get; set; }
        public bool IsList { get; set; }
    }
}
