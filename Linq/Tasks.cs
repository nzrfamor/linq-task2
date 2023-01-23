using System;
using System.Collections.Generic;
using System.Linq;
using Linq.Objects;

namespace Linq
{
    public static class Tasks
    {


        public static IEnumerable<MaxDiscountOwner> Task2(IEnumerable<Supplier> supplierList,
                IEnumerable<SupplierDiscount> supplierDiscountList)
        {
            return supplierDiscountList.Join(supplierList, sD => sD.SupplierId, sL => sL.Id, (sD, sL) => new
            {
                shopName = sD.ShopName,
                owner = sL,
                discount = sD.Discount

            }).GroupBy(x => x.shopName, x => (x.discount, x.owner), (sN, sD) => new MaxDiscountOwner
            {
                ShopName = sN,
                Owner = sD.Where(x => sD.Select(x => x.discount).Max() == x.discount).Select(x => x.owner).First(),
                Discount = sD.Select(x => x.discount).Max()
            }).OrderBy(x => x.ShopName);

        }
    }
}
