
namespace Cards.API.Models.Common
{/// <summary>
 /// 
 /// </summary>
 /// <typeparam name="T"></typeparam>
    public class PageCollectionInfo<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<T> PageCollection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ItemsCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal BalanceBroughtFoward { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal BalanceCarriedForward { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalDebits { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalCredits { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalApportioned { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalShortage { get; set; }
    }
}
