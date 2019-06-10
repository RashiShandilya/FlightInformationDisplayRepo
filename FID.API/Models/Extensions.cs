using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FID.API.Models
{
#pragma warning disable CS1591
    public static class FIDExtensions
    {
        public static IQueryable<FlightDetail> GetFlightDetail(this FIDContext dbContext, int pageSize = 10, int pageNumber = 1, int? lastEditedBy = null, int? colorID = null, int? outerPackageID = null, int? supplierID = null, int? unitPackageID = null)
        {
            // Get query from DbSet
            var query = dbContext.FlightDetail.AsQueryable();

            // Filter by: 'LastEditedBy'
            //if (lastEditedBy.HasValue)
            //    query = query.Where(item => item.LastEditedBy == lastEditedBy);

            //// Filter by: 'ColorID'
            //if (colorID.HasValue)
            //    query = query.Where(item => item.ColorID == colorID);

            //// Filter by: 'OuterPackageID'
            //if (outerPackageID.HasValue)
            //    query = query.Where(item => item.OuterPackageID == outerPackageID);

            //// Filter by: 'SupplierID'
            //if (supplierID.HasValue)
            //    query = query.Where(item => item.SupplierID == supplierID);

            //// Filter by: 'UnitPackageID'
            //if (unitPackageID.HasValue)
            //    query = query.Where(item => item.UnitPackageID == unitPackageID);

            return query;
        }

        public static async Task<FlightDetail> GetFlightDetailAsync(this FIDContext dbContext, FlightDetail entity)
            => await dbContext.FlightDetail.FirstOrDefaultAsync(item => item.FlightNumber == entity.FlightNumber);

        public static async Task<FlightDetail> GetFlightDetailByAirlineNameAsync(this FIDContext dbContext, FlightDetail entity)
            => await dbContext.FlightDetail.FirstOrDefaultAsync(item => item.AirlineName == entity.AirlineName);
    }

    public static class IQueryableExtensions
    {
        public static IQueryable<TModel> Paging<TModel>(this IQueryable<TModel> query, int pageSize = 0, int pageNumber = 0) where TModel : class
            => pageSize > 0 && pageNumber > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize) : query;
    }
#pragma warning restore CS1591
}