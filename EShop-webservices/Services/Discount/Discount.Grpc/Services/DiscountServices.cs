using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Discount.Grpc.Services;
public class DiscountServices(DiscountContext dbContext,ILogger<DiscountServices> logger) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        try
        {
            var coupon = await dbContext.coupons.FirstOrDefaultAsync(a => a.ProductName == request.ProductName);
            if (coupon is null)
            {
                coupon = new Coupon { ProductName = "No Dicount", Amount = 0, Description = "No Discount" };

            }
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;

        }
        catch (Exception ex) {
            throw new Exception(ex.Message);
        }

    }
    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon=request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Argument"));
        dbContext.coupons.Add(coupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Discount id successfully created. ProductName :{ProductName}",coupon.ProductName);
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }
    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Argument"));
        dbContext.coupons.Update(coupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Discount id successfully updated. ProductName :{ProductName}", coupon.ProductName);
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }
    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.coupons.FirstOrDefaultAsync(a => a.ProductName == request.ProductName);
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with Product Name={request.ProductName} is required"));
        dbContext.coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully deleted. ProductName :{ProductName}", request.ProductName);
        return new DeleteDiscountResponse { Success = true };
    }

}
