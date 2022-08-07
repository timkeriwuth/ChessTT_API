namespace Labo.API.DTO
{
    public class IndexDTO<IEnumerable>
    {
        public IEnumerable Results { get; set; }

        public int Count { get; set; }

        public IndexDTO(IEnumerable results, int count)
        {
            Results = results;
            Count = count;
        }
    }
}
