using System;
using System.Collections;

namespace DTO.Common
{
    public class ModelResult <T> : IEnumerable
    {
        public T Results { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
        public bool HasResult { get; set; }
        public string Message { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
