namespace TechLibrary.Communication.Responses
{
    public class ResponseBooksJson
    {
        // "default!" here is used to assure that the property is not null
        public ResponsePaginationJson Pagination { get; set; } = default!;

        public List<ResponseBookJson> Books { get; set; } = [];
    }
}
