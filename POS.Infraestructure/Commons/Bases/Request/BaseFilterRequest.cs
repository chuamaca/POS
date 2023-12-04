namespace POS.Infraestructure.Commons.Bases.Request
{
    public class BaseFilterRequest : BasePaginationRequest
    {
        //Numero de Filtro- Que filtro voy a procesar.
        public int? NumFilter { get; set; } = null;

        public string? TextFilter { get; set; } = null;
        public int ? stateFilter { get; set; } = null;

        public string? StartDate { get; set; } = null;
        public string? EndDate { get; set; } = null;

        public bool? Download { get; set; } = null;


    }
}
