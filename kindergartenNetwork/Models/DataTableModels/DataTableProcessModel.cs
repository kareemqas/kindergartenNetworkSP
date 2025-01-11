namespace kindergartenNetwork.Models.DataTableModels
{
    public class DataTableProcessModel
    {
        public string Search { get; set; }
        public string SortCol { get; set; }
        public int Page { get; set; }
        public int RowPerPage { get; set; }
        public string SortType { get; set; }
    }
}