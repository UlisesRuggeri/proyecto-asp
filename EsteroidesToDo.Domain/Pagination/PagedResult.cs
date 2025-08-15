
namespace EsteroidesToDo.Domain.Pagination
{
/*
    PagedResult<T> es como una caja de pizza con rodajas:

    Items = las rodajas que comés ahora.

    TotalCount = cuántas rodajas hay en total.

    PageNumber = qué rodaja estás agarrando.

    PageSize = cuántas rodajas podés agarrar por vez.

    TotalPages = cuántas cajas necesitás para todas las rodajas.

    HasPrevious/HasNext = si podés agarrar la caja anterior o la siguiente.
*/

    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;
    }
}
