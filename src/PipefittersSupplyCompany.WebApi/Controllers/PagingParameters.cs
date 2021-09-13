namespace PipefittersSupplyCompany.WebApi.Controllers
{
    public class PagingParameters
    {
        const int MAXPAGESIZE = 50;
        private int _pageSize = 10;

        public int Page { get; set; } = 1;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > MAXPAGESIZE) ? MAXPAGESIZE : value;
            }
        }
    }
}