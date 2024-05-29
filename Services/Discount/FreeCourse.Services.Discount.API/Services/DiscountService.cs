using Dapper;
using FreeCourse.Shared.Dtos;
using Npgsql;
using System.Data;

namespace FreeCourse.Services.Discount.API.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int discountId)
        {
            var status = await _dbConnection.ExecuteAsync("Delete from discount where discountid=@DiscountId", new { DiscountId = discountId });

            return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount not found!", 404);
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("Select * From discount");

            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = await _dbConnection.QueryAsync<Models.Discount>("Select * From discount Where userid = @UserId and code = @Code", new { UserId = userId, Code = code });

            var hasDiscount = discount.FirstOrDefault();

            if (hasDiscount == null)
            {
                return Response<Models.Discount>.Fail("Discount not found!", 404);
            }

            return Response<Models.Discount>.Success(hasDiscount, 200);
        }

        public async Task<Response<Models.Discount>> GetById(int discountId)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("Select * From discount Where discountid = @Id", new { Id = discountId })).SingleOrDefault();

            if (discount == null)
            {
                return Response<Models.Discount>.Fail("Discount not found!", 404);
            }

            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(Models.Discount discount)
        {
            var saveStatus = await _dbConnection.ExecuteAsync("Insert Into discount (userid, rate, code) Values(@UserId, @Rate, @Code)", discount);

            if (saveStatus > 0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("An error occurred while adding!", 500);
        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("Update discount set userid=@UserId, code=@Code, rate=@Rate Where discountid=@DiscountId",
                new
                {
                    DiscountId = discount.DiscountId,
                    UserId = discount.UserId,
                    Code = discount.Code,
                    Rate = discount.Rate
                });

            if (status > 0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("Discount not found!", 404);
        }
    }
}
